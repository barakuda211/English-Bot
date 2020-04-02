<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.tbResult = New System.Windows.Forms.TextBox
        Me.toolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.miOpen = New System.Windows.Forms.ToolStripMenuItem
        Me.miFile = New System.Windows.Forms.ToolStripMenuItem
        Me.miExit = New System.Windows.Forms.ToolStripMenuItem
        Me.msMain = New System.Windows.Forms.MenuStrip
        Me.msMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbResult
        '
        Me.tbResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbResult.Location = New System.Drawing.Point(0, 24)
        Me.tbResult.Multiline = True
        Me.tbResult.Name = "tbResult"
        Me.tbResult.ReadOnly = True
        Me.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbResult.Size = New System.Drawing.Size(284, 238)
        Me.tbResult.TabIndex = 3
        '
        'toolStripMenuItem1
        '
        Me.toolStripMenuItem1.Name = "toolStripMenuItem1"
        Me.toolStripMenuItem1.Size = New System.Drawing.Size(149, 6)
        '
        'miOpen
        '
        Me.miOpen.Name = "miOpen"
        Me.miOpen.Size = New System.Drawing.Size(152, 22)
        Me.miOpen.Text = "Open"
        '
        'miFile
        '
        Me.miFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miOpen, Me.toolStripMenuItem1, Me.miExit})
        Me.miFile.Name = "miFile"
        Me.miFile.Size = New System.Drawing.Size(37, 20)
        Me.miFile.Text = "File"
        '
        'miExit
        '
        Me.miExit.Name = "miExit"
        Me.miExit.Size = New System.Drawing.Size(152, 22)
        Me.miExit.Text = "Exit"
        '
        'msMain
        '
        Me.msMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miFile})
        Me.msMain.Location = New System.Drawing.Point(0, 0)
        Me.msMain.Name = "msMain"
        Me.msMain.Size = New System.Drawing.Size(284, 24)
        Me.msMain.TabIndex = 2
        Me.msMain.Text = "menuStrip1"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.tbResult)
        Me.Controls.Add(Me.msMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MainForm"
        Me.Text = "Media File Inspector"
        Me.msMain.ResumeLayout(False)
        Me.msMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents tbResult As System.Windows.Forms.TextBox
    Private WithEvents toolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents miOpen As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents miFile As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents miExit As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents msMain As System.Windows.Forms.MenuStrip

End Class
