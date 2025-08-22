Public Class FrmLogs
    Private funcSettings As New ClassSettings

    Public Sub loadLogs()
        Me.Cursor = Cursors.WaitCursor

        Try
            Dim funcCRUD As New ClassCRUD
            Dim dt As New DataTable
            Dim params As String = ""

            If cmbFilterEnabled.Text.ToUpper <> "ALL" And cmbFilterEnabled.Text <> "" Then
                If params = "" Then
                    params = "device_enabled = '" & cmbFilterEnabled.Text & "'"
                Else
                    params = params & " AND device_enabled = '" & cmbFilterEnabled.Text & "'"
                End If
            End If

            If cmbFilterStatus.Text.ToUpper <> "ALL" And cmbFilterStatus.Text <> "" Then
                If params = "" Then
                    params = "device_status = '" & cmbFilterStatus.Text & "'"
                Else
                    params = params & " AND device_status = '" & cmbFilterStatus.Text & "'"
                End If
            End If

            If cmbFilterDeviceName.Text.ToUpper <> "ALL" And cmbFilterDeviceName.Text <> "" Then
                If params = "" Then
                    params = "device_name LIKE '" & cmbFilterDeviceName.Text & "'"
                Else
                    params = params & " AND device_name LIKE '" & cmbFilterDeviceName.Text & "'"
                End If
            End If

            If cmbFilterIPAddress.Text.ToUpper <> "ALL" And cmbFilterIPAddress.Text <> "" Then
                If params = "" Then
                    params = "device_ip LIKE '" & cmbFilterIPAddress.Text & "'"
                Else
                    params = params & " AND device_ip LIKE '" & cmbFilterIPAddress.Text & "'"
                End If
            End If

            If cmbFilterLoggedInUser.Text.ToUpper <> "ALL" And cmbFilterLoggedInUser.Text <> "" Then
                If cmbFilterLoggedInUser.Text.ToUpper = "NO USER LOGGED IN" Then
                    If params = "" Then
                        params = "device_user_login = 'No user logged in'"
                    Else
                        params = params & " AND device_user_login = 'No user logged in'"
                    End If
                ElseIf cmbFilterLoggedInUser.Text.ToUpper = "AT LEAST ONE USER IS LOGGED IN" Then
                    If params = "" Then
                        params = "(device_user_login <> 'No user logged in' AND device_user_login <> '')"
                    Else
                        params = params & " AND (device_user_login <> 'No user logged in' AND device_user_login <> '')"
                    End If
                Else
                    If params = "" Then
                        params = "device_user_login LIKE '" & cmbFilterLoggedInUser.Text & "'"
                    Else
                        params = params & " AND device_user_login LIKE '" & cmbFilterLoggedInUser.Text & "'"
                    End If
                End If
            End If

            If params <> "" Then
                params = " WHERE " & params
            End If

            dt = funcCRUD.loadLogs(params)

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

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            loadLogs()
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub FrmLogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim funcCRUD As New ClassCRUD

            'load device name
            funcCRUD = New ClassCRUD
            Dim dtDeviceName As New DataTable
            dtDeviceName = funcCRUD.getDeviceName()

            With cmbFilterDeviceName
                .DataSource = dtDeviceName
                .DisplayMember = "device_name"
                .ValueMember = "device_name"
                .SelectedValue = ""
                '.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                .AutoCompleteSource = AutoCompleteSource.ListItems
            End With
            'end of load device name

            'load device ip address
            funcCRUD = New ClassCRUD
            Dim dtDeviceIpAddress As New DataTable
            dtDeviceIpAddress = funcCRUD.getDeviceIpAddress()

            With cmbFilterIPAddress
                .DataSource = dtDeviceIpAddress
                .DisplayMember = "device_ip"
                .ValueMember = "device_ip"
                .SelectedValue = ""
                '.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                .AutoCompleteSource = AutoCompleteSource.ListItems
            End With
            'end of load device ip address

            cmbFilterEnabled.SelectedValue = "ALL"
            cmbFilterStatus.SelectedValue = "ALL"
            cmbFilterLoggedInUser.Text = ""
            cmbFilterDeviceName.Text = ""
            cmbFilterIPAddress.Text = ""

            loadLogs()

            cmbFilterDeviceName.Focus()
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
End Class