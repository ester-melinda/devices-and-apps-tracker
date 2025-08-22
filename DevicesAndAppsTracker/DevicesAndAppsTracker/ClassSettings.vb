Imports System.IO
Imports MySql.Data.MySqlClient

Public Class ClassSettings
    Public dbConn As New DllConnection.connection
    Dim cmdmysql As New MySqlCommand

    Function GetUserName() As String
        Try
            If TypeOf My.User.CurrentPrincipal Is
  Security.Principal.WindowsPrincipal Then
                ' The application is using Windows authentication.
                ' The name format is DOMAIN\USERNAME.
                Dim parts() As String = Split(My.User.Name, "\")
                Dim username As String = parts(1)
                Return username
            Else
                ' The application is using custom authentication.
                Return My.User.Name
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub writeLog(errState As String, errMesg As String)
        Dim logDir As String = Application.StartupPath & "\errorLog\" & GetUserName()
        Dim logFile As String = logDir & "\" & Now().ToString("yyyy-MM-dd") & ".log"

        Dim sw As StreamWriter
        sw = Nothing

        Try
            If Not Directory.Exists(logDir) Then
                Directory.CreateDirectory(logDir)
            End If

            sw = File.AppendText(logFile)
            sw.WriteLine(Now() & vbTab & errState & vbTab & errMesg)
        Catch ex As Exception

        Finally
            GC.Collect()
            GC.WaitForPendingFinalizers()

            If Not sw Is Nothing Then
                sw.Close()
            End If

            sw = Nothing

            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Public Function saveSettings(ByVal getDevicesEvery As String) As Boolean
        Try
            dbConn.connectedMySQL()

            If Not (dbConn.cnnMysql.State = ConnectionState.Open) Then
                dbConn.cnnMysql.Open()
            End If

            cmdmysql.Parameters.Clear()
            cmdmysql.CommandType = CommandType.Text
            cmdmysql.CommandText = "UPDATE devices_apps_tracker.settings SET get_devices_every = @get_devices_every, updated_at = NOW() WHERE `id` = 1"
            cmdmysql.Parameters.AddWithValue("@get_devices_every", getDevicesEvery)
            cmdmysql.Connection = dbConn.cnnMysql
            cmdmysql.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
            Return False
        Finally
            dbConn.disconnectedMysql()
        End Try
    End Function

    Public Function getSettings() As DataTable
        Try
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmdmysql)

            dbConn.connectedMySQL()

            If Not (dbConn.cnnMysql.State = ConnectionState.Open) Then
                dbConn.cnnMysql.Open()
            End If

            cmdmysql.Parameters.Clear()
            cmdmysql.Connection = dbConn.cnnMysql
            cmdmysql.CommandType = CommandType.Text
            cmdmysql.CommandText = "SELECT * FROM devices_apps_tracker.settings WHERE `id` = 1"
            da.SelectCommand = cmdmysql
            da.Fill(dt)

            Return dt
        Catch ex As Exception
            writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
            Return Nothing
        Finally
            dbConn.disconnectedMysql()
        End Try
    End Function

    Public Function setLastDevicesRetrieved() As Boolean
        Try
            dbConn.connectedMySQL()

            If Not (dbConn.cnnMysql.State = ConnectionState.Open) Then
                dbConn.cnnMysql.Open()
            End If

            cmdmysql.Parameters.Clear()
            cmdmysql.CommandType = CommandType.Text
            cmdmysql.CommandText = "UPDATE devices_apps_tracker.settings SET last_devices_retrieved = NOW() WHERE `id` = 1"
            cmdmysql.Connection = dbConn.cnnMysql
            cmdmysql.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
            Return False
        Finally
            dbConn.disconnectedMysql()
        End Try
    End Function
End Class
