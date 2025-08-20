Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Newtonsoft.Json.Linq

Public Class Form1
    Private funcSettings As New ClassSettings

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            btnRefresh.Enabled = True
            btnCancel.Enabled = False
            pbLoadDevices.Visible = False
            pbLoadDevices.Style = ProgressBarStyle.Marquee
            lblLoadingDevies.Text = ""
            lblLoadingDevies.ForeColor = Color.Black

            loadDevices()
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub loadDevices()
        Try
            btnRefresh.Enabled = False
            btnCancel.Enabled = True
            pbLoadDevices.Visible = True
            lblLoadingDevies.Text = "Loading..."
            lblLoadingDevies.ForeColor = Color.Black

            If bgwLoadDevices.IsBusy = False Then
                bgwLoadDevices.WorkerSupportsCancellation = True
                bgwLoadDevices.WorkerReportsProgress = True
                bgwLoadDevices.RunWorkerAsync()
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
            If bgwLoadDevices.IsBusy Then
                If bgwLoadDevices.WorkerSupportsCancellation Then
                    bgwLoadDevices.CancelAsync()
                End If
            End If
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub bgwLoadDevices_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwLoadDevices.DoWork
        Try
            If bgwLoadDevices.CancellationPending Then
                e.Cancel = True
                Exit Sub
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
                If bgwLoadDevices.CancellationPending Then
                    e.Cancel = True
                    Exit Sub
                End If

                dt.Columns.Add(col.Name)
            Next
            For Each obj As JObject In arr
                If bgwLoadDevices.CancellationPending Then
                    e.Cancel = True
                    Exit Sub
                End If

                Dim row As DataRow = dt.NewRow()
                For Each col As JProperty In obj.Properties()
                    row(col.Name) = col.Value.ToString()
                Next
                dt.Rows.Add(row)
            Next

            e.Result = dt
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub bgwLoadDevices_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwLoadDevices.RunWorkerCompleted
        Try
            btnRefresh.Enabled = True
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
                        dgvResults.DataSource = dt

                        lblLoadingDevies.Text = "Done"
                        lblLoadingDevies.ForeColor = Color.Green
                    End If
                End If
            End If
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            If bgwLoadDevices.IsBusy Then
                If bgwLoadDevices.WorkerSupportsCancellation Then
                    bgwLoadDevices.CancelAsync()
                End If
            End If
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub
End Class
