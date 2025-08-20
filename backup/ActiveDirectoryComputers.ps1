# Run in PowerShell as Administrator
# Add-WindowsCapability -Online -Name Rsat.ActiveDirectory.DS-LDS.Tools~~~~0.0.1.0

# Restart computer

# Requires: RSAT Active Directory module
Import-Module ActiveDirectory

# Get all AD computers (enabled + disabled)
$computers = Get-ADComputer -Filter * -Property Name, Enabled |
             Select-Object Name, Enabled

# Create a runspace pool for parallel execution
Add-Type -AssemblyName System.Management.Automation
$runspacePool = [runspacefactory]::CreateRunspacePool(1, 20)
$runspacePool.Open()

$jobs = @()

foreach ($comp in $computers) {
    $ps = [powershell]::Create().AddScript({
        param($comp)

        function Get-UsersFromQuser {
            param([string]$ComputerName)
            $output = quser /server:$ComputerName 2>$null
            if (-not $output) { return @() }

            $lines = ($output -split "`r?`n") | Where-Object { $_.Trim() }
            if ($lines.Count -lt 2) { return @() }

            $header = $lines[0]
            $data   = $lines | Select-Object -Skip 1

            # Column start indexes
            $nameStart   = $header.IndexOf("USERNAME")
            $sessStart   = $header.IndexOf("SESSIONNAME")
            $stateStart  = $header.IndexOf("STATE")
            if ($nameStart -lt 0 -or $sessStart -lt 0 -or $stateStart -lt 0) {
                return @()
            }

            $users = New-Object System.Collections.Generic.List[string]
            foreach ($line in $data) {
                $l = $line.Replace('>', ' ')
                try {
                    $username = $l.Substring($nameStart, $sessStart - $nameStart).Trim()
                    $state    = $l.Substring($stateStart).Trim().Split()[0]
                } catch {
                    continue
                }
                if ([string]::IsNullOrWhiteSpace($username)) { continue }

                if (@('Active','Disc') -contains $state) {
                    if (-not $users.Contains($username)) { $users.Add($username) }
                }
            }
            return $users.ToArray()
        }

        function Get-UsersFromInvokeCommand {
            param([string]$ComputerName)
            try {
                $session = Invoke-Command -ComputerName $ComputerName -ScriptBlock {
                    try {
                        (query user) -split "`r?`n" | ForEach-Object {
                            ($_ -split '\s+')[0]
                        } | Where-Object { $_ -and $_ -ne "USERNAME" }
                    } catch { @() }
                } -ErrorAction Stop
                return $session
            } catch {
                return @()
            }
        }

        $status     = "Offline"
        $ipAddress  = ""
        $usersText  = ""

        if ($comp.Enabled -eq $true -and (Test-Connection -ComputerName $comp.Name -Count 1 -Quiet)) {
            $status = "Online"

            # Get IP address
            try {
                $ping = Test-Connection -ComputerName $comp.Name -Count 1 -ErrorAction Stop
                $ipAddress = $ping.IPV4Address.IPAddressToString
            } catch {
                $ipAddress = "Unknown"
            }

            # 1. Prefer quser
            $users = Get-UsersFromQuser -ComputerName $comp.Name

            # 2. If quser failed, try Invoke-Command
            if (-not $users -or $users.Count -eq 0) {
                $users = Get-UsersFromInvokeCommand -ComputerName $comp.Name
            }

            # 3. If still empty, try CIM
            if (-not $users -or $users.Count -eq 0) {
                try {
                    $u = (Get-CimInstance Win32_ComputerSystem -ComputerName $comp.Name -ErrorAction Stop).UserName
                    if ($u) { $users = @($u) }
                } catch { }
            }

            # 4. Final fallback
            if (-not $users -or $users.Count -eq 0) {
                $usersText = "No user logged in"
            } else {
                $usersText = ($users | Sort-Object -Unique) -join ", "
            }
        }

        [PSCustomObject]@{
            ComputerName = $comp.Name
            AD_Enabled   = $comp.Enabled
            Status       = $status
            IPAddress    = $ipAddress
            LoggedOnUser = $(if ($status -eq 'Online') { $usersText } else { "" })
        }
    }).AddArgument($comp)

    $ps.RunspacePool = $runspacePool
    $jobs += [PSCustomObject]@{ Pipe = $ps; Status = $ps.BeginInvoke() }
}

# Wait for all threads to finish and collect results
$results = foreach ($job in $jobs) {
    $output = $job.Pipe.EndInvoke($job.Status)
    $job.Pipe.Dispose()
    $output
}

$runspacePool.Close()
$runspacePool.Dispose()

# Output as JSON (for VB.NET)
$results | ConvertTo-Json -Depth 3
# $results | Format-Table -AutoSize

# Optional: export to CSV
# $results | Export-Csv "AD_Computer_Status.csv" -NoTypeInformation
