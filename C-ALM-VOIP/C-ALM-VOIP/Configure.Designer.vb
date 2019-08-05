<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Configure
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Configure))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbxsniipv4 = New System.Windows.Forms.ComboBox()
        Me.cmbxsniipv6 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.nudspudpipv4 = New System.Windows.Forms.NumericUpDown()
        Me.nudsptcpipv4 = New System.Windows.Forms.NumericUpDown()
        Me.nudspudpipv6 = New System.Windows.Forms.NumericUpDown()
        Me.nudsptcpipv6 = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.nudtcpbl = New System.Windows.Forms.NumericUpDown()
        Me.chkbxena = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtbxudpextaddIPv4 = New System.Windows.Forms.TextBox()
        Me.nududpextpIPv4 = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.nududpextpIPv6 = New System.Windows.Forms.NumericUpDown()
        Me.txtbxudpextaddIPv6 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.butOK = New System.Windows.Forms.Button()
        Me.butCANCEL = New System.Windows.Forms.Button()
        Me.cmbxsid = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.chkbxrdtcpc = New System.Windows.Forms.CheckBox()
        CType(Me.nudspudpipv4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudsptcpipv4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudspudpipv6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudsptcpipv6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudtcpbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nududpextpIPv4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nududpextpIPv6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Consolas", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(580, 30)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Configure : C-ALM VOIP"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(159, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Select Network Interface (IPv4):"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 103)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(159, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Select Network Interface (IPv6):"
        '
        'cmbxsniipv4
        '
        Me.cmbxsniipv4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxsniipv4.FormattingEnabled = True
        Me.cmbxsniipv4.Location = New System.Drawing.Point(180, 71)
        Me.cmbxsniipv4.Name = "cmbxsniipv4"
        Me.cmbxsniipv4.Size = New System.Drawing.Size(210, 21)
        Me.cmbxsniipv4.TabIndex = 0
        '
        'cmbxsniipv6
        '
        Me.cmbxsniipv6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxsniipv6.FormattingEnabled = True
        Me.cmbxsniipv6.Location = New System.Drawing.Point(180, 103)
        Me.cmbxsniipv6.Name = "cmbxsniipv6"
        Me.cmbxsniipv6.Size = New System.Drawing.Size(210, 21)
        Me.cmbxsniipv6.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(402, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "UDP Port"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(495, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "TCP Port"
        '
        'nudspudpipv4
        '
        Me.nudspudpipv4.Location = New System.Drawing.Point(405, 72)
        Me.nudspudpipv4.Maximum = New Decimal(New Integer() {65536, 0, 0, 0})
        Me.nudspudpipv4.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudspudpipv4.Name = "nudspudpipv4"
        Me.nudspudpipv4.Size = New System.Drawing.Size(71, 20)
        Me.nudspudpipv4.TabIndex = 1
        Me.nudspudpipv4.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nudsptcpipv4
        '
        Me.nudsptcpipv4.Location = New System.Drawing.Point(498, 72)
        Me.nudsptcpipv4.Maximum = New Decimal(New Integer() {65536, 0, 0, 0})
        Me.nudsptcpipv4.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudsptcpipv4.Name = "nudsptcpipv4"
        Me.nudsptcpipv4.Size = New System.Drawing.Size(71, 20)
        Me.nudsptcpipv4.TabIndex = 2
        Me.nudsptcpipv4.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nudspudpipv6
        '
        Me.nudspudpipv6.Location = New System.Drawing.Point(405, 104)
        Me.nudspudpipv6.Maximum = New Decimal(New Integer() {65536, 0, 0, 0})
        Me.nudspudpipv6.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudspudpipv6.Name = "nudspudpipv6"
        Me.nudspudpipv6.Size = New System.Drawing.Size(71, 20)
        Me.nudspudpipv6.TabIndex = 4
        Me.nudspudpipv6.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nudsptcpipv6
        '
        Me.nudsptcpipv6.Location = New System.Drawing.Point(498, 104)
        Me.nudsptcpipv6.Maximum = New Decimal(New Integer() {65536, 0, 0, 0})
        Me.nudsptcpipv6.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudsptcpipv6.Name = "nudsptcpipv6"
        Me.nudsptcpipv6.Size = New System.Drawing.Size(71, 20)
        Me.nudsptcpipv6.TabIndex = 5
        Me.nudsptcpipv6.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 132)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(130, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "TCP Connection Backlog:"
        '
        'nudtcpbl
        '
        Me.nudtcpbl.Location = New System.Drawing.Point(180, 132)
        Me.nudtcpbl.Maximum = New Decimal(New Integer() {65536, 0, 0, 0})
        Me.nudtcpbl.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudtcpbl.Name = "nudtcpbl"
        Me.nudtcpbl.Size = New System.Drawing.Size(210, 20)
        Me.nudtcpbl.TabIndex = 6
        Me.nudtcpbl.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'chkbxena
        '
        Me.chkbxena.AutoSize = True
        Me.chkbxena.Font = New System.Drawing.Font("Consolas", 6.0!, System.Drawing.FontStyle.Bold)
        Me.chkbxena.Location = New System.Drawing.Point(405, 135)
        Me.chkbxena.Name = "chkbxena"
        Me.chkbxena.Size = New System.Drawing.Size(152, 14)
        Me.chkbxena.TabIndex = 7
        Me.chkbxena.Text = "Enable Nagle's Algorithm for TCP"
        Me.chkbxena.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(15, 162)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(149, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "UDP External (IPv4): Address:"
        '
        'txtbxudpextaddIPv4
        '
        Me.txtbxudpextaddIPv4.Location = New System.Drawing.Point(180, 162)
        Me.txtbxudpextaddIPv4.Name = "txtbxudpextaddIPv4"
        Me.txtbxudpextaddIPv4.Size = New System.Drawing.Size(210, 20)
        Me.txtbxudpextaddIPv4.TabIndex = 8
        '
        'nududpextpIPv4
        '
        Me.nududpextpIPv4.Location = New System.Drawing.Point(498, 162)
        Me.nududpextpIPv4.Maximum = New Decimal(New Integer() {65536, 0, 0, 0})
        Me.nududpextpIPv4.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nududpextpIPv4.Name = "nududpextpIPv4"
        Me.nududpextpIPv4.Size = New System.Drawing.Size(71, 20)
        Me.nududpextpIPv4.TabIndex = 9
        Me.nududpextpIPv4.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(402, 162)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(29, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Port:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(402, 193)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 13)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "Port:"
        '
        'nududpextpIPv6
        '
        Me.nududpextpIPv6.Location = New System.Drawing.Point(498, 193)
        Me.nududpextpIPv6.Maximum = New Decimal(New Integer() {65536, 0, 0, 0})
        Me.nududpextpIPv6.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nududpextpIPv6.Name = "nududpextpIPv6"
        Me.nududpextpIPv6.Size = New System.Drawing.Size(71, 20)
        Me.nududpextpIPv6.TabIndex = 11
        Me.nududpextpIPv6.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtbxudpextaddIPv6
        '
        Me.txtbxudpextaddIPv6.Location = New System.Drawing.Point(180, 193)
        Me.txtbxudpextaddIPv6.Name = "txtbxudpextaddIPv6"
        Me.txtbxudpextaddIPv6.Size = New System.Drawing.Size(210, 20)
        Me.txtbxudpextaddIPv6.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(15, 193)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(149, 13)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "UDP External (IPv6): Address:"
        '
        'butOK
        '
        Me.butOK.Location = New System.Drawing.Point(405, 276)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(71, 23)
        Me.butOK.TabIndex = 14
        Me.butOK.Text = "&Ok"
        Me.butOK.UseVisualStyleBackColor = True
        '
        'butCANCEL
        '
        Me.butCANCEL.Location = New System.Drawing.Point(498, 276)
        Me.butCANCEL.Name = "butCANCEL"
        Me.butCANCEL.Size = New System.Drawing.Size(71, 23)
        Me.butCANCEL.TabIndex = 15
        Me.butCANCEL.Text = "&Cancel"
        Me.butCANCEL.UseVisualStyleBackColor = True
        '
        'cmbxsid
        '
        Me.cmbxsid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxsid.FormattingEnabled = True
        Me.cmbxsid.Location = New System.Drawing.Point(180, 232)
        Me.cmbxsid.Name = "cmbxsid"
        Me.cmbxsid.Size = New System.Drawing.Size(210, 21)
        Me.cmbxsid.TabIndex = 12
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(15, 232)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(104, 13)
        Me.Label11.TabIndex = 26
        Me.Label11.Text = "Select Input Device:"
        '
        'chkbxrdtcpc
        '
        Me.chkbxrdtcpc.AutoSize = True
        Me.chkbxrdtcpc.Font = New System.Drawing.Font("Consolas", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbxrdtcpc.Location = New System.Drawing.Point(405, 234)
        Me.chkbxrdtcpc.Name = "chkbxrdtcpc"
        Me.chkbxrdtcpc.Size = New System.Drawing.Size(148, 14)
        Me.chkbxrdtcpc.TabIndex = 13
        Me.chkbxrdtcpc.Text = "Remove Disconnected TCP Clients"
        Me.chkbxrdtcpc.UseVisualStyleBackColor = True
        '
        'Configure
        '
        Me.AcceptButton = Me.butOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.butCANCEL
        Me.ClientSize = New System.Drawing.Size(584, 311)
        Me.Controls.Add(Me.chkbxrdtcpc)
        Me.Controls.Add(Me.cmbxsid)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.butCANCEL)
        Me.Controls.Add(Me.butOK)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.nududpextpIPv6)
        Me.Controls.Add(Me.txtbxudpextaddIPv6)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.nududpextpIPv4)
        Me.Controls.Add(Me.txtbxudpextaddIPv4)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.chkbxena)
        Me.Controls.Add(Me.nudtcpbl)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.nudsptcpipv6)
        Me.Controls.Add(Me.nudspudpipv6)
        Me.Controls.Add(Me.nudsptcpipv4)
        Me.Controls.Add(Me.nudspudpipv4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbxsniipv6)
        Me.Controls.Add(Me.cmbxsniipv4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(600, 350)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(600, 350)
        Me.Name = "Configure"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Configure : C-ALM VOIP"
        CType(Me.nudspudpipv4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudsptcpipv4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudspudpipv6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudsptcpipv6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudtcpbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nududpextpIPv4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nududpextpIPv6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbxsniipv4 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbxsniipv6 As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents nudspudpipv4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudsptcpipv4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudspudpipv6 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudsptcpipv6 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents nudtcpbl As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkbxena As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtbxudpextaddIPv4 As System.Windows.Forms.TextBox
    Friend WithEvents nududpextpIPv4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents nududpextpIPv6 As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtbxudpextaddIPv6 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents butOK As System.Windows.Forms.Button
    Friend WithEvents butCANCEL As System.Windows.Forms.Button
    Friend WithEvents cmbxsid As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkbxrdtcpc As System.Windows.Forms.CheckBox
End Class
