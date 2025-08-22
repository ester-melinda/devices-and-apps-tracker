Imports System.Windows.Forms.LinkLabel
Imports MySql.Data.MySqlClient

Public Class ClassCRUD
    Private funcSettings As New ClassSettings
    Public dbConn As New DllConnection.connection
    Dim cmdmysql As New MySqlCommand

    Public Function loadDevices() As DataTable
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
            cmdmysql.CommandText = "SELECT * FROM devices_apps_tracker.devices ORDER BY device_name"
            da.SelectCommand = cmdmysql
            da.Fill(dt)

            Return dt
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
            Return Nothing
        Finally
            dbConn.disconnectedMysql()
        End Try
    End Function

    Public Function deleteAllDevices() As Boolean
        Try
            dbConn.connectedMySQL()

            If Not (dbConn.cnnMysql.State = ConnectionState.Open) Then
                dbConn.cnnMysql.Open()
            End If

            cmdmysql.Parameters.Clear()
            cmdmysql.CommandType = CommandType.Text
            cmdmysql.Connection = dbConn.cnnMysql
            cmdmysql.CommandText = "TRUNCATE TABLE devices_apps_tracker.devices"
            cmdmysql.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
            Return False
        Finally
            dbConn.disconnectedMysql()
        End Try
    End Function

    Public Function insertDevices(ByVal deviceName As String, ByVal deviceEnabled As String, ByVal deviceStatus As String, ByVal deviceIP As String, ByVal deviceUserLogin As String) As Boolean
        Try
            dbConn.connectedMySQL()

            'insert device
            If Not (dbConn.cnnMysql.State = ConnectionState.Open) Then
                dbConn.cnnMysql.Open()
            End If

            cmdmysql.Parameters.Clear()
            cmdmysql.CommandType = CommandType.Text
            cmdmysql.Connection = dbConn.cnnMysql
            cmdmysql.CommandText = "INSERT INTO devices_apps_tracker.devices(device_name, device_enabled, device_status, device_ip, device_user_login) VALUES(@device_name, @device_enabled, @device_status, @device_ip, @device_user_login)"
            cmdmysql.Parameters.AddWithValue("@device_name", deviceName)
            cmdmysql.Parameters.AddWithValue("@device_enabled", deviceEnabled)
            cmdmysql.Parameters.AddWithValue("@device_status", deviceStatus)
            cmdmysql.Parameters.AddWithValue("@device_ip", deviceIP)
            cmdmysql.Parameters.AddWithValue("@device_user_login", deviceUserLogin)
            cmdmysql.ExecuteNonQuery()

            'insert log
            If Not (dbConn.cnnMysql.State = ConnectionState.Open) Then
                dbConn.cnnMysql.Open()
            End If

            cmdmysql.Parameters.Clear()
            cmdmysql.CommandType = CommandType.Text
            cmdmysql.Connection = dbConn.cnnMysql
            cmdmysql.CommandText = "INSERT INTO devices_apps_tracker.logs(device_name, device_enabled, device_status, device_ip, device_user_login) VALUES(@device_name, @device_enabled, @device_status, @device_ip, @device_user_login)"
            cmdmysql.Parameters.AddWithValue("@device_name", deviceName)
            cmdmysql.Parameters.AddWithValue("@device_enabled", deviceEnabled)
            cmdmysql.Parameters.AddWithValue("@device_status", deviceStatus)
            cmdmysql.Parameters.AddWithValue("@device_ip", deviceIP)
            cmdmysql.Parameters.AddWithValue("@device_user_login", deviceUserLogin)
            cmdmysql.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
            Return False
        Finally
            dbConn.disconnectedMysql()
        End Try
    End Function

    Public Function loadLogs(ByVal params As String) As DataTable
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
            cmdmysql.CommandText = "SELECT * FROM devices_apps_tracker.logs" & params & " ORDER BY created_at DESC"
            da.SelectCommand = cmdmysql
            da.Fill(dt)

            Return dt
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
            Return Nothing
        Finally
            dbConn.disconnectedMysql()
        End Try
    End Function

    Public Function getDeviceName() As DataTable
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
            cmdmysql.CommandText = "SELECT DISTINCT device_name FROM devices_apps_tracker.logs ORDER BY device_name ASC"
            cmdmysql.CommandTimeout = 4200
            da.SelectCommand = cmdmysql
            da.Fill(dt)

            Return dt
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
            Return Nothing
        Finally
            dbConn.disconnectedMysql()
        End Try
    End Function

    Public Function getDeviceIpAddress() As DataTable
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
            cmdmysql.CommandText = "SELECT DISTINCT device_ip FROM devices_apps_tracker.logs ORDER BY device_ip ASC"
            cmdmysql.CommandTimeout = 4200
            da.SelectCommand = cmdmysql
            da.Fill(dt)

            Return dt
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
            Return Nothing
        Finally
            dbConn.disconnectedMysql()
        End Try
    End Function

End Class
