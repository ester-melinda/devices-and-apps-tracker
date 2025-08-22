<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container()
        Me.dgvResults = New System.Windows.Forms.DataGridView()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.pbLoadDevices = New System.Windows.Forms.ProgressBar()
        Me.bgwGetDevices = New System.ComponentModel.BackgroundWorker()
        Me.lblLoadingDevies = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.btnLogs = New System.Windows.Forms.Button()
        Me.btnGetDevices = New System.Windows.Forms.Button()
        Me.btnSettings = New System.Windows.Forms.Button()
        Me.lblLastDevicesRetrieved = New System.Windows.Forms.Label()
        Me.tmSleepGetDevices = New System.Windows.Forms.Timer(Me.components)
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvResults
        '
        Me.dgvResults.AllowUserToAddRows = False
        Me.dgvResults.AllowUserToDeleteRows = False
        Me.dgvResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvResults.Location = New System.Drawing.Point(12, 88)
        Me.dgvResults.Name = "dgvResults"
        Me.dgvResults.RowHeadersWidth = 51
        Me.dgvResults.RowTemplate.Height = 24
        Me.dgvResults.Size = New System.Drawing.Size(1132, 547)
        Me.dgvResults.TabIndex = 0
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(12, 12)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(125, 44)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "REFRESH"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'pbLoadDevices
        '
        Me.pbLoadDevices.Location = New System.Drawing.Point(510, 12)
        Me.pbLoadDevices.MarqueeAnimationSpeed = 30
        Me.pbLoadDevices.Name = "pbLoadDevices"
        Me.pbLoadDevices.Size = New System.Drawing.Size(497, 44)
        Me.pbLoadDevices.TabIndex = 2
        '
        'bgwGetDevices
        '
        '
        'lblLoadingDevies
        '
        Me.lblLoadingDevies.AutoSize = True
        Me.lblLoadingDevies.BackColor = System.Drawing.Color.Transparent
        Me.lblLoadingDevies.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoadingDevies.Location = New System.Drawing.Point(523, 23)
        Me.lblLoadingDevies.Name = "lblLoadingDevies"
        Me.lblLoadingDevies.Size = New System.Drawing.Size(90, 20)
        Me.lblLoadingDevies.TabIndex = 3
        Me.lblLoadingDevies.Text = "Loading..."
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(301, 12)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 44)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCount.Location = New System.Drawing.Point(12, 67)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(58, 16)
        Me.lblCount.TabIndex = 5
        Me.lblCount.Text = "Count : "
        '
        'btnLogs
        '
        Me.btnLogs.Location = New System.Drawing.Point(407, 12)
        Me.btnLogs.Name = "btnLogs"
        Me.btnLogs.Size = New System.Drawing.Size(95, 44)
        Me.btnLogs.TabIndex = 6
        Me.btnLogs.Text = "LOGS"
        Me.btnLogs.UseVisualStyleBackColor = True
        '
        'btnGetDevices
        '
        Me.btnGetDevices.Location = New System.Drawing.Point(143, 12)
        Me.btnGetDevices.Name = "btnGetDevices"
        Me.btnGetDevices.Size = New System.Drawing.Size(152, 44)
        Me.btnGetDevices.TabIndex = 7
        Me.btnGetDevices.Text = "GET DEVICES"
        Me.btnGetDevices.UseVisualStyleBackColor = True
        '
        'btnSettings
        '
        Me.btnSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSettings.Location = New System.Drawing.Point(1025, 12)
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Size = New System.Drawing.Size(120, 44)
        Me.btnSettings.TabIndex = 8
        Me.btnSettings.Text = "SETTINGS"
        Me.btnSettings.UseVisualStyleBackColor = True
        '
        'lblLastDevicesRetrieved
        '
        Me.lblLastDevicesRetrieved.AutoSize = True
        Me.lblLastDevicesRetrieved.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastDevicesRetrieved.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblLastDevicesRetrieved.Location = New System.Drawing.Point(140, 67)
        Me.lblLastDevicesRetrieved.Name = "lblLastDevicesRetrieved"
        Me.lblLastDevicesRetrieved.Size = New System.Drawing.Size(186, 16)
        Me.lblLastDevicesRetrieved.TabIndex = 9
        Me.lblLastDevicesRetrieved.Text = "Last devices retrieved on "
        '
        'tmSleepGetDevices
        '
        Me.tmSleepGetDevices.Interval = 1000
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1156, 647)
        Me.Controls.Add(Me.lblLastDevicesRetrieved)
        Me.Controls.Add(Me.btnSettings)
        Me.Controls.Add(Me.btnGetDevices)
        Me.Controls.Add(Me.btnLogs)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblLoadingDevies)
        Me.Controls.Add(Me.pbLoadDevices)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.dgvResults)
        Me.Name = "Form1"
        Me.Text = "DEVICES AND APPS TRACKER"
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvResults As DataGridView
    Friend WithEvents btnRefresh As Button
    Friend WithEvents pbLoadDevices As ProgressBar
    Friend WithEvents bgwGetDevices As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblLoadingDevies As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblCount As Label
    Friend WithEvents btnLogs As Button
    Friend WithEvents btnGetDevices As Button
    Friend WithEvents btnSettings As Button
    Friend WithEvents lblLastDevicesRetrieved As Label
    Friend WithEvents tmSleepGetDevices As Timer
End Class
