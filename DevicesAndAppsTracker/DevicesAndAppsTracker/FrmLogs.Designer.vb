<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLogs
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.dgvResults = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbFilterDeviceName = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbFilterEnabled = New System.Windows.Forms.ComboBox()
        Me.cmbFilterStatus = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbFilterIPAddress = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbFilterLoggedOnUser = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCount.Location = New System.Drawing.Point(9, 190)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(58, 16)
        Me.lblCount.TabIndex = 13
        Me.lblCount.Text = "Count : "
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(117, 119)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(101, 44)
        Me.btnRefresh.TabIndex = 9
        Me.btnRefresh.Text = "REFRESH"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'dgvResults
        '
        Me.dgvResults.AllowUserToAddRows = False
        Me.dgvResults.AllowUserToDeleteRows = False
        Me.dgvResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvResults.Location = New System.Drawing.Point(12, 212)
        Me.dgvResults.Name = "dgvResults"
        Me.dgvResults.RowHeadersWidth = 51
        Me.dgvResults.RowTemplate.Height = 24
        Me.dgvResults.Size = New System.Drawing.Size(1133, 465)
        Me.dgvResults.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 16)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Device Name"
        '
        'cmbFilterDeviceName
        '
        Me.cmbFilterDeviceName.FormattingEnabled = True
        Me.cmbFilterDeviceName.Location = New System.Drawing.Point(117, 16)
        Me.cmbFilterDeviceName.Name = "cmbFilterDeviceName"
        Me.cmbFilterDeviceName.Size = New System.Drawing.Size(198, 24)
        Me.cmbFilterDeviceName.TabIndex = 15
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(44, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 16)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Enabled"
        '
        'cmbFilterEnabled
        '
        Me.cmbFilterEnabled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFilterEnabled.FormattingEnabled = True
        Me.cmbFilterEnabled.Items.AddRange(New Object() {"ALL", "TRUE", "FALSE"})
        Me.cmbFilterEnabled.Location = New System.Drawing.Point(117, 46)
        Me.cmbFilterEnabled.Name = "cmbFilterEnabled"
        Me.cmbFilterEnabled.Size = New System.Drawing.Size(105, 24)
        Me.cmbFilterEnabled.TabIndex = 17
        '
        'cmbFilterStatus
        '
        Me.cmbFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFilterStatus.FormattingEnabled = True
        Me.cmbFilterStatus.Items.AddRange(New Object() {"ALL", "Online", "Offline"})
        Me.cmbFilterStatus.Location = New System.Drawing.Point(117, 76)
        Me.cmbFilterStatus.Name = "cmbFilterStatus"
        Me.cmbFilterStatus.Size = New System.Drawing.Size(105, 24)
        Me.cmbFilterStatus.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(58, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 16)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Status"
        '
        'cmbFilterIPAddress
        '
        Me.cmbFilterIPAddress.FormattingEnabled = True
        Me.cmbFilterIPAddress.Location = New System.Drawing.Point(480, 16)
        Me.cmbFilterIPAddress.Name = "cmbFilterIPAddress"
        Me.cmbFilterIPAddress.Size = New System.Drawing.Size(198, 24)
        Me.cmbFilterIPAddress.TabIndex = 21
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(390, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 16)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "IP Address"
        '
        'cmbFilterLoggedOnUser
        '
        Me.cmbFilterLoggedOnUser.FormattingEnabled = True
        Me.cmbFilterLoggedOnUser.Location = New System.Drawing.Point(480, 46)
        Me.cmbFilterLoggedOnUser.Name = "cmbFilterLoggedOnUser"
        Me.cmbFilterLoggedOnUser.Size = New System.Drawing.Size(198, 24)
        Me.cmbFilterLoggedOnUser.TabIndex = 23
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(357, 49)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(106, 16)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Logged On User"
        '
        'FrmLogs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1157, 689)
        Me.Controls.Add(Me.cmbFilterLoggedOnUser)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbFilterIPAddress)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbFilterStatus)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbFilterEnabled)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbFilterDeviceName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.dgvResults)
        Me.Name = "FrmLogs"
        Me.Text = "Logs"
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblCount As Label
    Friend WithEvents btnRefresh As Button
    Friend WithEvents dgvResults As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbFilterDeviceName As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbFilterEnabled As ComboBox
    Friend WithEvents cmbFilterStatus As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbFilterIPAddress As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cmbFilterLoggedOnUser As ComboBox
    Friend WithEvents Label5 As Label
End Class
