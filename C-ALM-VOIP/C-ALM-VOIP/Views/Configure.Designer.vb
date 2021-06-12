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
        Me.txtbxextaddIPv4 = New System.Windows.Forms.TextBox()
        Me.nududpextpIPv4 = New System.Windows.Forms.NumericUpDown()
        Me.nududpextpIPv6 = New System.Windows.Forms.NumericUpDown()
        Me.txtbxextaddIPv6 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.butOK = New System.Windows.Forms.Button()
        Me.butCANCEL = New System.Windows.Forms.Button()
        Me.cmbxsid = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.chkbxrdtcpc = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.nudtcpextpIPv6 = New System.Windows.Forms.NumericUpDown()
        Me.nudtcpextpIPv4 = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.nudtcpto = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtbxcnom = New System.Windows.Forms.TextBox()
        Me.chkbxsan = New System.Windows.Forms.CheckBox()
        Me.cmbxis = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.nudsr = New System.Windows.Forms.NumericUpDown()
        Me.nudrb = New System.Windows.Forms.NumericUpDown()
        CType(Me.nudspudpipv4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudsptcpipv4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudspudpipv6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudsptcpipv6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudtcpbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nududpextpIPv4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nududpextpIPv6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.nudtcpextpIPv6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudtcpextpIPv4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.nudtcpto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudsr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudrb, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "IPv6:"
        '
        'cmbxsniipv4
        '
        Me.cmbxsniipv4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxsniipv4.FormattingEnabled = True
        Me.cmbxsniipv4.Location = New System.Drawing.Point(180, 59)
        Me.cmbxsniipv4.Name = "cmbxsniipv4"
        Me.cmbxsniipv4.Size = New System.Drawing.Size(210, 21)
        Me.cmbxsniipv4.TabIndex = 0
        '
        'cmbxsniipv6
        '
        Me.cmbxsniipv6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxsniipv6.FormattingEnabled = True
        Me.cmbxsniipv6.Location = New System.Drawing.Point(180, 91)
        Me.cmbxsniipv6.Name = "cmbxsniipv6"
        Me.cmbxsniipv6.Size = New System.Drawing.Size(210, 21)
        Me.cmbxsniipv6.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(402, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "UDP Port"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(495, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "TCP Port"
        '
        'nudspudpipv4
        '
        Me.nudspudpipv4.Location = New System.Drawing.Point(405, 59)
        Me.nudspudpipv4.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudspudpipv4.Name = "nudspudpipv4"
        Me.nudspudpipv4.Size = New System.Drawing.Size(71, 20)
        Me.nudspudpipv4.TabIndex = 1
        '
        'nudsptcpipv4
        '
        Me.nudsptcpipv4.Location = New System.Drawing.Point(498, 59)
        Me.nudsptcpipv4.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudsptcpipv4.Name = "nudsptcpipv4"
        Me.nudsptcpipv4.Size = New System.Drawing.Size(71, 20)
        Me.nudsptcpipv4.TabIndex = 2
        '
        'nudspudpipv6
        '
        Me.nudspudpipv6.Location = New System.Drawing.Point(405, 91)
        Me.nudspudpipv6.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudspudpipv6.Name = "nudspudpipv6"
        Me.nudspudpipv6.Size = New System.Drawing.Size(71, 20)
        Me.nudspudpipv6.TabIndex = 4
        '
        'nudsptcpipv6
        '
        Me.nudsptcpipv6.Location = New System.Drawing.Point(498, 91)
        Me.nudsptcpipv6.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudsptcpipv6.Name = "nudsptcpipv6"
        Me.nudsptcpipv6.Size = New System.Drawing.Size(71, 20)
        Me.nudsptcpipv6.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(130, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "TCP Connection Backlog:"
        '
        'nudtcpbl
        '
        Me.nudtcpbl.Location = New System.Drawing.Point(162, 19)
        Me.nudtcpbl.Maximum = New Decimal(New Integer() {65536, 0, 0, 0})
        Me.nudtcpbl.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudtcpbl.Name = "nudtcpbl"
        Me.nudtcpbl.Size = New System.Drawing.Size(210, 20)
        Me.nudtcpbl.TabIndex = 12
        Me.nudtcpbl.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'chkbxena
        '
        Me.chkbxena.AutoSize = True
        Me.chkbxena.Font = New System.Drawing.Font("Consolas", 6.0!, System.Drawing.FontStyle.Bold)
        Me.chkbxena.Location = New System.Drawing.Point(387, 23)
        Me.chkbxena.Name = "chkbxena"
        Me.chkbxena.Size = New System.Drawing.Size(152, 14)
        Me.chkbxena.TabIndex = 13
        Me.chkbxena.Text = "Enable Nagle's Algorithm for TCP"
        Me.chkbxena.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "IPv4:"
        '
        'txtbxextaddIPv4
        '
        Me.txtbxextaddIPv4.Location = New System.Drawing.Point(162, 14)
        Me.txtbxextaddIPv4.Name = "txtbxextaddIPv4"
        Me.txtbxextaddIPv4.Size = New System.Drawing.Size(210, 20)
        Me.txtbxextaddIPv4.TabIndex = 6
        '
        'nududpextpIPv4
        '
        Me.nududpextpIPv4.Location = New System.Drawing.Point(387, 14)
        Me.nududpextpIPv4.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nududpextpIPv4.Name = "nududpextpIPv4"
        Me.nududpextpIPv4.Size = New System.Drawing.Size(71, 20)
        Me.nududpextpIPv4.TabIndex = 7
        '
        'nududpextpIPv6
        '
        Me.nududpextpIPv6.Location = New System.Drawing.Point(387, 43)
        Me.nududpextpIPv6.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nududpextpIPv6.Name = "nududpextpIPv6"
        Me.nududpextpIPv6.Size = New System.Drawing.Size(71, 20)
        Me.nududpextpIPv6.TabIndex = 10
        '
        'txtbxextaddIPv6
        '
        Me.txtbxextaddIPv6.Location = New System.Drawing.Point(162, 43)
        Me.txtbxextaddIPv6.Name = "txtbxextaddIPv6"
        Me.txtbxextaddIPv6.Size = New System.Drawing.Size(210, 20)
        Me.txtbxextaddIPv6.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 46)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(32, 13)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "IPv6:"
        '
        'butOK
        '
        Me.butOK.Location = New System.Drawing.Point(405, 374)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(71, 23)
        Me.butOK.TabIndex = 22
        Me.butOK.Text = "&Ok"
        Me.butOK.UseVisualStyleBackColor = True
        '
        'butCANCEL
        '
        Me.butCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCANCEL.Location = New System.Drawing.Point(498, 374)
        Me.butCANCEL.Name = "butCANCEL"
        Me.butCANCEL.Size = New System.Drawing.Size(71, 23)
        Me.butCANCEL.TabIndex = 23
        Me.butCANCEL.Text = "&Cancel"
        Me.butCANCEL.UseVisualStyleBackColor = True
        '
        'cmbxsid
        '
        Me.cmbxsid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxsid.FormattingEnabled = True
        Me.cmbxsid.Location = New System.Drawing.Point(180, 330)
        Me.cmbxsid.Name = "cmbxsid"
        Me.cmbxsid.Size = New System.Drawing.Size(210, 21)
        Me.cmbxsid.TabIndex = 20
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(24, 333)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(104, 13)
        Me.Label11.TabIndex = 26
        Me.Label11.Text = "Select Input Device:"
        '
        'chkbxrdtcpc
        '
        Me.chkbxrdtcpc.AutoSize = True
        Me.chkbxrdtcpc.Font = New System.Drawing.Font("Consolas", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbxrdtcpc.Location = New System.Drawing.Point(387, 54)
        Me.chkbxrdtcpc.Name = "chkbxrdtcpc"
        Me.chkbxrdtcpc.Size = New System.Drawing.Size(148, 14)
        Me.chkbxrdtcpc.TabIndex = 15
        Me.chkbxrdtcpc.Text = "Remove Disconnected TCP Clients"
        Me.chkbxrdtcpc.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 43)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(554, 74)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Network Interface Selection:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "IPv4:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtbxextaddIPv6)
        Me.GroupBox2.Controls.Add(Me.txtbxextaddIPv4)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.nududpextpIPv4)
        Me.GroupBox2.Controls.Add(Me.nududpextpIPv6)
        Me.GroupBox2.Controls.Add(Me.nudtcpextpIPv4)
        Me.GroupBox2.Controls.Add(Me.nudtcpextpIPv6)
        Me.GroupBox2.Location = New System.Drawing.Point(18, 120)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(554, 70)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "External Address:"
        '
        'nudtcpextpIPv6
        '
        Me.nudtcpextpIPv6.Location = New System.Drawing.Point(480, 43)
        Me.nudtcpextpIPv6.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudtcpextpIPv6.Name = "nudtcpextpIPv6"
        Me.nudtcpextpIPv6.Size = New System.Drawing.Size(71, 20)
        Me.nudtcpextpIPv6.TabIndex = 11
        '
        'nudtcpextpIPv4
        '
        Me.nudtcpextpIPv4.Location = New System.Drawing.Point(480, 14)
        Me.nudtcpextpIPv4.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudtcpextpIPv4.Name = "nudtcpextpIPv4"
        Me.nudtcpextpIPv4.Size = New System.Drawing.Size(71, 20)
        Me.nudtcpextpIPv4.TabIndex = 8
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.nudtcpto)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.chkbxrdtcpc)
        Me.GroupBox3.Controls.Add(Me.nudtcpbl)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.chkbxena)
        Me.GroupBox3.Location = New System.Drawing.Point(18, 198)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(554, 75)
        Me.GroupBox3.TabIndex = 12
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "TCP Options:"
        '
        'nudtcpto
        '
        Me.nudtcpto.Increment = New Decimal(New Integer() {125, 0, 0, 0})
        Me.nudtcpto.Location = New System.Drawing.Point(162, 50)
        Me.nudtcpto.Maximum = New Decimal(New Integer() {600000, 0, 0, 0})
        Me.nudtcpto.Name = "nudtcpto"
        Me.nudtcpto.Size = New System.Drawing.Size(210, 20)
        Me.nudtcpto.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 52)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(138, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "TCP Timeout (Milliseconds):"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(24, 280)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(67, 13)
        Me.Label9.TabIndex = 32
        Me.Label9.Text = "Client Name:"
        '
        'txtbxcnom
        '
        Me.txtbxcnom.Location = New System.Drawing.Point(180, 277)
        Me.txtbxcnom.Name = "txtbxcnom"
        Me.txtbxcnom.Size = New System.Drawing.Size(210, 20)
        Me.txtbxcnom.TabIndex = 16
        '
        'chkbxsan
        '
        Me.chkbxsan.AutoSize = True
        Me.chkbxsan.Font = New System.Drawing.Font("Consolas", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbxsan.Location = New System.Drawing.Point(405, 279)
        Me.chkbxsan.Name = "chkbxsan"
        Me.chkbxsan.Size = New System.Drawing.Size(100, 14)
        Me.chkbxsan.TabIndex = 17
        Me.chkbxsan.Text = "Set Advertised Name"
        Me.chkbxsan.UseVisualStyleBackColor = True
        '
        'cmbxis
        '
        Me.cmbxis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxis.FormattingEnabled = True
        Me.cmbxis.Items.AddRange(New Object() {"XML Serializer", "Binary Serializer"})
        Me.cmbxis.Location = New System.Drawing.Point(180, 303)
        Me.cmbxis.Name = "cmbxis"
        Me.cmbxis.Size = New System.Drawing.Size(210, 21)
        Me.cmbxis.TabIndex = 18
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(24, 306)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(85, 13)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "Select Serializer:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(402, 306)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(93, 13)
        Me.Label13.TabIndex = 37
        Me.Label13.Text = "Sample Rate (Hz):"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(402, 333)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(98, 13)
        Me.Label14.TabIndex = 38
        Me.Label14.Text = "Record Buffer (ms):"
        '
        'nudsr
        '
        Me.nudsr.Location = New System.Drawing.Point(498, 304)
        Me.nudsr.Maximum = New Decimal(New Integer() {24000, 0, 0, 0})
        Me.nudsr.Minimum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.nudsr.Name = "nudsr"
        Me.nudsr.Size = New System.Drawing.Size(71, 20)
        Me.nudsr.TabIndex = 19
        Me.nudsr.Value = New Decimal(New Integer() {12000, 0, 0, 0})
        '
        'nudrb
        '
        Me.nudrb.Location = New System.Drawing.Point(498, 331)
        Me.nudrb.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.nudrb.Minimum = New Decimal(New Integer() {125, 0, 0, 0})
        Me.nudrb.Name = "nudrb"
        Me.nudrb.Size = New System.Drawing.Size(71, 20)
        Me.nudrb.TabIndex = 21
        Me.nudrb.Value = New Decimal(New Integer() {125, 0, 0, 0})
        '
        'Configure
        '
        Me.AcceptButton = Me.butOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.butCANCEL
        Me.ClientSize = New System.Drawing.Size(584, 411)
        Me.Controls.Add(Me.nudrb)
        Me.Controls.Add(Me.nudsr)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.cmbxis)
        Me.Controls.Add(Me.chkbxsan)
        Me.Controls.Add(Me.txtbxcnom)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cmbxsid)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.butCANCEL)
        Me.Controls.Add(Me.butOK)
        Me.Controls.Add(Me.nudsptcpipv6)
        Me.Controls.Add(Me.nudspudpipv6)
        Me.Controls.Add(Me.nudsptcpipv4)
        Me.Controls.Add(Me.nudspudpipv4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbxsniipv6)
        Me.Controls.Add(Me.cmbxsniipv4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(600, 450)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(600, 450)
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
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.nudtcpextpIPv6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudtcpextpIPv4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.nudtcpto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudsr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudrb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
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
    Friend WithEvents txtbxextaddIPv4 As System.Windows.Forms.TextBox
    Friend WithEvents nududpextpIPv4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nududpextpIPv6 As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtbxextaddIPv6 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents butOK As System.Windows.Forms.Button
    Friend WithEvents butCANCEL As System.Windows.Forms.Button
    Friend WithEvents cmbxsid As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkbxrdtcpc As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents nudtcpextpIPv6 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudtcpextpIPv4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents nudtcpto As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtbxcnom As System.Windows.Forms.TextBox
    Friend WithEvents chkbxsan As System.Windows.Forms.CheckBox
    Friend WithEvents cmbxis As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents nudsr As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudrb As System.Windows.Forms.NumericUpDown
End Class
