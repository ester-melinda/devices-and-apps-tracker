Public Class FrmSettings
    Private funcSettings As New ClassSettings

    Private Sub txtGetDevicesEvery_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGetDevicesEvery.KeyPress
        If Asc(e.KeyChar) <> 8 And Asc(e.KeyChar) <> 9 And Asc(e.KeyChar) <> 13 And Asc(e.KeyChar) <> 37 And Asc(e.KeyChar) <> 39 And Asc(e.KeyChar) <> 190 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim result As DialogResult = MessageBox.Show("Are you sure want to save all settings?", "Information", MessageBoxButtons.YesNo)
            If result = DialogResult.No Then
                Exit Sub
            ElseIf result = DialogResult.Yes Then
                Dim getDevicesEvery As String = "0"

                If IsNumeric(txtGetDevicesEvery.Text) Then
                    getDevicesEvery = txtGetDevicesEvery.Text
                Else
                    getDevicesEvery = "0"
                End If

                funcSettings = New ClassSettings
                If Not funcSettings.saveSettings(getDevicesEvery) Then
                    MessageBox.Show(New Form With {.TopMost = True}, "Save Settings Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    MessageBox.Show(New Form With {.TopMost = True}, "Save Settings Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub FrmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim dtSettings As New DataTable
            funcSettings = New ClassSettings
            dtSettings = funcSettings.getSettings()

            If dtSettings.Rows.Count > 0 Then
                txtGetDevicesEvery.Text = dtSettings.Rows(0).Item("get_devices_every").ToString()
            End If
        Catch ex As Exception
            funcSettings.writeLog(Me.GetType().Name, ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub
End Class