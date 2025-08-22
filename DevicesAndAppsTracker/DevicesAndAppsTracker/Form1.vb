Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.VisualBasic.Logging
Imports Newtonsoft.Json.Linq

Public Class Form1
    Private logger As New DllLogger.ClassLogger
    Private funcSettings As New ClassSettings
    Dim thSleepGetDevices As Thread
    Dim getDevicesEvery As Integer = 0

    Public Sub reloadServerSetting()
        Try
            Dim serverPath As String = logger.appPath
            Dim localPath As String = Application.StartupPath
            Dim fileSetting As String() = {"Paths.ini", "SQLSetting.ini"}
            For i = 0 To UBound(fileSetting)
                If File.Exists(localPath & "\" & fileSetting(i)) Then
                    Dim valueServer As String = File.ReadAllText(serverPath & "\" & fileSetting(i))
                    Dim valueLocal As String = File.ReadAllText(localPath & "\" & fileSetting(i))
                    If valueServer <> valueLocal Then
                        File.Copy(serverPath & "\" & fileSetting(i), localPath & "\" & fileSetting(i), True)
                    End If
                Else
                    File.Copy(serverPath & "\" & fileSetting(i), localPath & "\" & fileSetting(i), True)
                End If
            Next
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            reloadServerSetting()

            If (System.IO.File.Exists("SQLSetting.ini")) Then
                Dim read2 As String
                read2 = My.Computer.FileSystem.ReadAllText(Application.StartupPath & "\SQLSetting.ini")
                If read2 <> "" Then
                    Dim splitRead2 As String() = read2.Split(",")
                    Dim host As String = splitRead2(0)
                    Dim database As String = "qantas_wine"
                    Me.Text = Me.Text & " | " & "MySQL Host: " & host & "  |  Database: " & database
                Else
                    Me.Text = Me.Text & " | " & "Database not found"
                End If
            End If

            thSleepGetDevices = New Thread(New ThreadStart(AddressOf goToSleepGetDevices))

            btnGetDevices.Enabled = True
            btnCancel.Enabled = False
            pbLoadDevices.Visible = False
            pbLoadDevices.Style = ProgressBarStyle.Marquee
            lblLoadingDevies.Text = ""
            lblLoadingDevies.ForeColor = Color.Black

            Dim lastDevicesRetrieved As String = ""
            Dim dtSettings As New DataTable
            funcSettings = New ClassSettings
            dtSettings = funcSettings.getSettings()

            If dtSettings.Rows.Count > 0 Then
                If Not String.IsNullOrEmpty(dtSettings.Rows(0).Item("last_devices_retrieved").ToString()) Then
                    lastDevicesRetrieved = dtSettings.Rows(0).Item("last_devices_retrieved").ToString()
                End If
            End If

            lblLastDevicesRetrieved.Text = "Last devices retrieved on " & lastDevicesRetrieved

            loadDevices()
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub getDevices()
        Try
            btnGetDevices.Enabled = False
            btnCancel.Enabled = True
            pbLoadDevices.Visible = True
            lblLoadingDevies.Text = "Loading..."
            lblLoadingDevies.ForeColor = Color.Black

            If bgwGetDevices.IsBusy = False Then
                bgwGetDevices.WorkerSupportsCancellation = True
                bgwGetDevices.WorkerReportsProgress = True
                bgwGetDevices.RunWorkerAsync()
            End If
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        loadDevices()
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try
            tmSleepGetDevices.Stop()

            If thSleepGetDevices.IsAlive Then
                thSleepGetDevices.Abort()
            End If

            If bgwGetDevices.IsBusy Then
                If bgwGetDevices.WorkerSupportsCancellation Then
                    bgwGetDevices.CancelAsync()
                End If
            End If
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub bgwGetDevices_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwGetDevices.DoWork
        Dim funcCRUD As New ClassCRUD

        Try
            If bgwGetDevices.CancellationPending Then
                e.Cancel = True
                Exit Sub
            End If

            tmSleepGetDevices.Stop()

            If thSleepGetDevices.IsAlive Then
                thSleepGetDevices.Abort()
            End If

            funcCRUD = New ClassCRUD
            If funcCRUD.deleteAllDevices() Then

            End If

            ' Path to your PowerShell script
            Dim scriptPath As String = Application.StartupPath & "\PowerShell\ActiveDirectoryComputers.ps1"

            '' Create PowerShell instance
            'Using ps As PowerShell = PowerShell.Create()
            '    ' Allow execution only in this process
            '    ps.AddCommand("Set-ExecutionPolicy").AddParameter("Scope", "Process").AddParameter("ExecutionPolicy", "Bypass").Invoke()
            '    ps.Commands.Clear()

            '    ' Load the script file
            '    ps.AddCommand(scriptPath)

            '    ' Run script and get results
            '    Dim results As Collection(Of PSObject) = ps.Invoke()

            '    ' Convert results into DataTable
            '    Dim dt As New DataTable()

            '    If results.Count > 0 Then
            '        ' Create columns based on properties
            '        For Each prop In results(0).Properties
            '            dt.Columns.Add(prop.Name)
            '        Next

            '        ' Fill rows
            '        For Each result As PSObject In results
            '            Dim row As DataRow = dt.NewRow()
            '            For Each prop In result.Properties
            '                row(prop.Name) = If(prop.Value, DBNull.Value)
            '            Next
            '            dt.Rows.Add(row)
            '        Next
            '    End If

            '    ' Bind DataTable to DataGridView
            '    dgvResults.DataSource = dt
            'End Using

            ' Call external PowerShell
            Dim psi As New ProcessStartInfo()
            psi.FileName = "C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe"
            psi.Arguments = "-NoProfile -ExecutionPolicy Bypass -File """ & scriptPath & """"
            psi.UseShellExecute = False
            psi.RedirectStandardOutput = True
            psi.RedirectStandardError = True
            psi.CreateNoWindow = True

            Dim proc As New Process()
            proc.StartInfo = psi
            proc.Start()

            Dim output As String = proc.StandardOutput.ReadToEnd()
            Dim errors As String = proc.StandardError.ReadToEnd()
            proc.WaitForExit()

            If Not String.IsNullOrEmpty(errors) Then
                MessageBox.Show("PowerShell error: " & errors)
                Return
            End If

            ' Parse JSON to DataTable (or directly to object)
            Dim arr As JArray = JArray.Parse(output)

            Dim dt As New DataTable()
            For Each col In arr(0).Children(Of JProperty)()
                If bgwGetDevices.CancellationPending Then
                    e.Cancel = True
                    Exit Sub
                End If

                dt.Columns.Add(col.Name)
            Next
            For Each obj As JObject In arr
                If bgwGetDevices.CancellationPending Then
                    e.Cancel = True
                    Exit Sub
                End If

                Dim row As DataRow = dt.NewRow()
                Dim deviceName As String = ""
                Dim deviceEnabled As String = ""
                Dim deviceStatus As String = ""
                Dim deviceIP As String = ""
                Dim deviceUserLogin As String = ""

                For Each col As JProperty In obj.Properties()
                    If bgwGetDevices.CancellationPending Then
                        e.Cancel = True
                        Exit Sub
                    End If

                    row(col.Name) = col.Value.ToString()

                    If col.Name = "ComputerName" Then
                        deviceName = col.Value.ToString()
                    ElseIf col.Name = "AD_Enabled" Then
                        deviceEnabled = col.Value.ToString()
                    ElseIf col.Name = "Status" Then
                        deviceStatus = col.Value.ToString()
                    ElseIf col.Name = "IPAddress" Then
                        deviceIP = col.Value.ToString()
                    ElseIf col.Name = "LoggedOnUser" Then
                        deviceUserLogin = col.Value.ToString()
                    End If
                Next

                dt.Rows.Add(row)
                funcCRUD = New ClassCRUD
                If funcCRUD.insertDevices(deviceName, deviceEnabled, deviceStatus, deviceIP, deviceUserLogin) Then

                End If
            Next

            e.Result = dt
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub bgwGetDevices_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwGetDevices.RunWorkerCompleted
        Try
            btnGetDevices.Enabled = True
            btnCancel.Enabled = False
            pbLoadDevices.Visible = False

            If e.Cancelled Then
                lblLoadingDevies.Text = "Cancelled"
                lblLoadingDevies.ForeColor = Color.DarkOrange
            Else
                If e.Error IsNot Nothing Then
                    lblLoadingDevies.Text = "Error"
                    lblLoadingDevies.ForeColor = Color.Red
                    MessageBox.Show("Error: " & e.Error.Message)
                Else
                    Dim dt As DataTable = DirectCast(e.Result, DataTable)

                    If dt.Rows.Count <= 0 Then
                        lblLoadingDevies.Text = "Error"
                        lblLoadingDevies.ForeColor = Color.Red
                    Else
                        'dgvResults.DataSource = dt

                        funcSettings = New ClassSettings
                        If funcSettings.setLastDevicesRetrieved() Then

                        End If

                        lblLoadingDevies.Text = "Done"
                        lblLoadingDevies.ForeColor = Color.Green
                    End If
                End If
            End If

            loadDevices()

            'repeat get devices
            Dim lastDevicesRetrieved As String = ""
            Dim dtSettings As New DataTable
            getDevicesEvery = 0
            funcSettings = New ClassSettings
            dtSettings = funcSettings.getSettings()

            If dtSettings.Rows.Count > 0 Then
                If Not String.IsNullOrEmpty(dtSettings.Rows(0).Item("last_devices_retrieved").ToString()) Then
                    lastDevicesRetrieved = dtSettings.Rows(0).Item("last_devices_retrieved").ToString()
                End If

                If Not String.IsNullOrEmpty(dtSettings.Rows(0).Item("get_devices_every").ToString()) Then
                    getDevicesEvery = CInt(dtSettings.Rows(0).Item("get_devices_every").ToString())
                Else
                    getDevicesEvery = 0
                End If
            End If

            lblLastDevicesRetrieved.Text = "Last devices retrieved on " & lastDevicesRetrieved

            If getDevicesEvery > 0 Then
                If Not thSleepGetDevices.IsAlive Then
                    thSleepGetDevices = New Thread(New ThreadStart(AddressOf goToSleepGetDevices))
                    thSleepGetDevices.Start()
                End If
            End If
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub goToSleepGetDevices()
        Me.Invoke(Sub()
                      tmSleepGetDevices.Stop()
                      tmSleepGetDevices.Interval = (getDevicesEvery * 3600) * 1000
                      tmSleepGetDevices.Start()
                  End Sub)
    End Sub

    Private Sub tmSleepGetDevices_Tick(sender As Object, e As EventArgs) Handles tmSleepGetDevices.Tick
        Me.Invoke(Sub()
                      If Not bgwGetDevices.IsBusy Then
                          getDevices()
                      End If
                  End Sub)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            tmSleepGetDevices.Stop()

            If thSleepGetDevices.IsAlive Then
                thSleepGetDevices.Abort()
            End If

            If bgwGetDevices.IsBusy Then
                If bgwGetDevices.WorkerSupportsCancellation Then
                    bgwGetDevices.CancelAsync()
                End If
            End If
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Public Sub loadDevices()
        Me.Cursor = Cursors.WaitCursor

        Try
            Dim funcCRUD As New ClassCRUD
            Dim dt As New DataTable
            dt = funcCRUD.loadDevices()

            dgvResults.DataSource = dt

            dgvResults.Columns("id").Frozen = True
            dgvResults.Columns("id").Visible = False
            dgvResults.Columns("id").ReadOnly = True
            dgvResults.Columns("device_name").HeaderText = "Device Name"
            dgvResults.Columns("device_name").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvResults.Columns("device_name").ReadOnly = True
            dgvResults.Columns("device_name").Frozen = True
            dgvResults.Columns("device_enabled").HeaderText = "Enabled"
            dgvResults.Columns("device_enabled").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvResults.Columns("device_enabled").ReadOnly = True
            dgvResults.Columns("device_status").HeaderText = "Status"
            dgvResults.Columns("device_status").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvResults.Columns("device_status").ReadOnly = True
            dgvResults.Columns("device_ip").HeaderText = "IP Address"
            dgvResults.Columns("device_ip").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvResults.Columns("device_ip").ReadOnly = True
            dgvResults.Columns("device_user_login").HeaderText = "Logged In User"
            dgvResults.Columns("device_user_login").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvResults.Columns("device_user_login").ReadOnly = True
            dgvResults.Columns("created_at").HeaderText = "Created At"
            dgvResults.Columns("created_at").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvResults.Columns("created_at").DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss"
            dgvResults.Columns("created_at").ReadOnly = True

            lblCount.Text = "Count : " & dgvResults.Rows.Count
            dgvResults.ClearSelection()
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Dim MemClass As New ClassMemory()
            MemClass = Nothing
        End Try
    End Sub

    Private Sub btnGetDevices_Click(sender As Object, e As EventArgs) Handles btnGetDevices.Click
        Try
            tmSleepGetDevices.Stop()

            If thSleepGetDevices.IsAlive Then
                thSleepGetDevices.Abort()
            End If

            getDevices()
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub dgvResults_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvResults.CellFormatting
        If dgvResults.Columns(e.ColumnIndex).Name = "device_status" AndAlso e.Value IsNot Nothing Then
            e.CellStyle.Font = New Font(dgvResults.Font, FontStyle.Bold)

            If e.Value.ToString().ToLower() = "online" Then
                e.CellStyle.ForeColor = Color.Green
            ElseIf e.Value.ToString().ToLower() = "offline" Then
                e.CellStyle.ForeColor = Color.Red
            End If
        End If
    End Sub

    Private Sub btnLogs_Click(sender As Object, e As EventArgs) Handles btnLogs.Click
        Try
            FrmLogs.Close()
            FrmLogs.Show()
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        Try
            FrmSettings.Close()
            FrmSettings.Show()
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub
End Class
