<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Editor
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtbxname = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbxtype = New System.Windows.Forms.ComboBox()
        Me.cmbxipv = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtbxaddr = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.nudport = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtbxmyaddr = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.nudmyport = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbxstrmode = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.nudport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudmyport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(226, 320)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 8
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Consolas", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(359, 30)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Editor:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Name:"
        '
        'txtbxname
        '
        Me.txtbxname.Location = New System.Drawing.Point(105, 61)
        Me.txtbxname.Name = "txtbxname"
        Me.txtbxname.Size = New System.Drawing.Size(264, 20)
        Me.txtbxname.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 97)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Protocol Type:"
        '
        'cmbxtype
        '
        Me.cmbxtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxtype.FormattingEnabled = True
        Me.cmbxtype.Items.AddRange(New Object() {"TCP", "UDP", "Block"})
        Me.cmbxtype.Location = New System.Drawing.Point(105, 94)
        Me.cmbxtype.Name = "cmbxtype"
        Me.cmbxtype.Size = New System.Drawing.Size(96, 21)
        Me.cmbxtype.TabIndex = 1
        '
        'cmbxipv
        '
        Me.cmbxipv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxipv.FormattingEnabled = True
        Me.cmbxipv.Items.AddRange(New Object() {"IPv4", "IPv6"})
        Me.cmbxipv.Location = New System.Drawing.Point(271, 94)
        Me.cmbxipv.Name = "cmbxipv"
        Me.cmbxipv.Size = New System.Drawing.Size(98, 21)
        Me.cmbxipv.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(207, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "IP Version:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 130)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Address:"
        '
        'txtbxaddr
        '
        Me.txtbxaddr.Location = New System.Drawing.Point(105, 130)
        Me.txtbxaddr.Name = "txtbxaddr"
        Me.txtbxaddr.Size = New System.Drawing.Size(264, 20)
        Me.txtbxaddr.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 163)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Port:"
        '
        'nudport
        '
        Me.nudport.Location = New System.Drawing.Point(105, 163)
        Me.nudport.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudport.Name = "nudport"
        Me.nudport.Size = New System.Drawing.Size(264, 20)
        Me.nudport.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 197)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "My Address:"
        '
        'txtbxmyaddr
        '
        Me.txtbxmyaddr.Location = New System.Drawing.Point(105, 197)
        Me.txtbxmyaddr.Name = "txtbxmyaddr"
        Me.txtbxmyaddr.Size = New System.Drawing.Size(264, 20)
        Me.txtbxmyaddr.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 234)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 13)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "My Port:"
        '
        'nudmyport
        '
        Me.nudmyport.Location = New System.Drawing.Point(105, 234)
        Me.nudmyport.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudmyport.Name = "nudmyport"
        Me.nudmyport.Size = New System.Drawing.Size(264, 20)
        Me.nudmyport.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 274)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(87, 13)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Streaming Mode:"
        '
        'cmbxstrmode
        '
        Me.cmbxstrmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbxstrmode.FormattingEnabled = True
        Me.cmbxstrmode.Items.AddRange(New Object() {"Disabled", "Receive Only", "Send Only", "Biderectional"})
        Me.cmbxstrmode.Location = New System.Drawing.Point(105, 271)
        Me.cmbxstrmode.Name = "cmbxstrmode"
        Me.cmbxstrmode.Size = New System.Drawing.Size(264, 21)
        Me.cmbxstrmode.TabIndex = 7
        '
        'Editor
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(384, 361)
        Me.Controls.Add(Me.nudmyport)
        Me.Controls.Add(Me.nudport)
        Me.Controls.Add(Me.txtbxmyaddr)
        Me.Controls.Add(Me.txtbxaddr)
        Me.Controls.Add(Me.cmbxipv)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbxstrmode)
        Me.Controls.Add(Me.cmbxtype)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtbxname)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(400, 400)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(400, 400)
        Me.Name = "Editor"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Editor"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.nudport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudmyport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtbxname As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbxtype As System.Windows.Forms.ComboBox
    Friend WithEvents cmbxipv As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtbxaddr As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents nudport As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtbxmyaddr As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents nudmyport As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbxstrmode As System.Windows.Forms.ComboBox

End Class
