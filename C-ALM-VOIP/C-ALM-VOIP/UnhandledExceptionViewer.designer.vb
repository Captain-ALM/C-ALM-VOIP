<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UnhandledExceptionViewer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UnhandledExceptionViewer))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.butt = New System.Windows.Forms.Button()
        Me.butr = New System.Windows.Forms.Button()
        Me.txtbxex = New System.Windows.Forms.TextBox()
        Me.butcont = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Controls.Add(Me.butt, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.butr, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtbxex, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.butcont, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(584, 361)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'butt
        '
        Me.butt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.butt.Location = New System.Drawing.Point(391, 327)
        Me.butt.Name = "butt"
        Me.butt.Size = New System.Drawing.Size(190, 31)
        Me.butt.TabIndex = 3
        Me.butt.Text = "Terminate"
        Me.butt.UseVisualStyleBackColor = True
        '
        'butr
        '
        Me.butr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.butr.Location = New System.Drawing.Point(197, 327)
        Me.butr.Name = "butr"
        Me.butr.Size = New System.Drawing.Size(188, 31)
        Me.butr.TabIndex = 2
        Me.butr.Text = "Report"
        Me.butr.UseVisualStyleBackColor = True
        '
        'txtbxex
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtbxex, 3)
        Me.txtbxex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtbxex.Location = New System.Drawing.Point(3, 3)
        Me.txtbxex.Multiline = True
        Me.txtbxex.Name = "txtbxex"
        Me.txtbxex.ReadOnly = True
        Me.txtbxex.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtbxex.Size = New System.Drawing.Size(578, 318)
        Me.txtbxex.TabIndex = 0
        Me.txtbxex.WordWrap = False
        '
        'butcont
        '
        Me.butcont.Dock = System.Windows.Forms.DockStyle.Fill
        Me.butcont.Location = New System.Drawing.Point(3, 327)
        Me.butcont.Name = "butcont"
        Me.butcont.Size = New System.Drawing.Size(188, 31)
        Me.butcont.TabIndex = 1
        Me.butcont.Text = "Continue"
        Me.butcont.UseVisualStyleBackColor = True
        '
        'UnhandledExceptionViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 361)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(300, 200)
        Me.Name = "UnhandledExceptionViewer"
        Me.Text = "Unhandled Exception"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtbxex As System.Windows.Forms.TextBox
    Friend WithEvents butt As System.Windows.Forms.Button
    Friend WithEvents butr As System.Windows.Forms.Button
    Friend WithEvents butcont As System.Windows.Forms.Button
End Class
