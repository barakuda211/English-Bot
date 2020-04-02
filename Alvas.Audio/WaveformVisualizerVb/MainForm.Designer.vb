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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.pMain = New System.Windows.Forms.Panel
        Me.tbMain = New System.Windows.Forms.ToolBar
        Me.tbbOpen = New System.Windows.Forms.ToolBarButton
        Me.tbbPlay = New System.Windows.Forms.ToolBarButton
        Me.tbbStop = New System.Windows.Forms.ToolBarButton
        Me.ilMain = New System.Windows.Forms.ImageList(Me.components)
        Me.ofdAudio = New System.Windows.Forms.OpenFileDialog
        Me.pgMain = New System.Windows.Forms.PropertyGrid
        Me.SuspendLayout()
        '
        'pMain
        '
        Me.pMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pMain.Location = New System.Drawing.Point(0, 28)
        Me.pMain.Name = "pMain"
        Me.pMain.Size = New System.Drawing.Size(526, 375)
        Me.pMain.TabIndex = 10
        '
        'tbMain
        '
        Me.tbMain.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.tbMain.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbOpen, Me.tbbPlay, Me.tbbStop})
        Me.tbMain.DropDownArrows = True
        Me.tbMain.ImageList = Me.ilMain
        Me.tbMain.Location = New System.Drawing.Point(0, 0)
        Me.tbMain.Name = "tbMain"
        Me.tbMain.ShowToolTips = True
        Me.tbMain.Size = New System.Drawing.Size(526, 28)
        Me.tbMain.TabIndex = 9
        '
        'tbbOpen
        '
        Me.tbbOpen.Enabled = False
        Me.tbbOpen.ImageIndex = 0
        Me.tbbOpen.Name = "tbbOpen"
        '
        'tbbPlay
        '
        Me.tbbPlay.Enabled = False
        Me.tbbPlay.ImageIndex = 1
        Me.tbbPlay.Name = "tbbPlay"
        '
        'tbbStop
        '
        Me.tbbStop.Enabled = False
        Me.tbbStop.ImageIndex = 2
        Me.tbbStop.Name = "tbbStop"
        '
        'ilMain
        '
        Me.ilMain.ImageStream = CType(resources.GetObject("ilMain.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilMain.TransparentColor = System.Drawing.Color.Red
        Me.ilMain.Images.SetKeyName(0, "openHS.png")
        Me.ilMain.Images.SetKeyName(1, "")
        Me.ilMain.Images.SetKeyName(2, "")
        '
        'ofdAudio
        '
        Me.ofdAudio.DefaultExt = "wav"
        '
        'pgMain
        '
        Me.pgMain.Dock = System.Windows.Forms.DockStyle.Right
        Me.pgMain.Location = New System.Drawing.Point(526, 0)
        Me.pgMain.Name = "pgMain"
        Me.pgMain.Size = New System.Drawing.Size(225, 403)
        Me.pgMain.TabIndex = 11
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(751, 403)
        Me.Controls.Add(Me.pMain)
        Me.Controls.Add(Me.tbMain)
        Me.Controls.Add(Me.pgMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MainForm"
        Me.Text = "WaveformVisualizer example"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents pMain As System.Windows.Forms.Panel
    Private WithEvents tbMain As System.Windows.Forms.ToolBar
    Private WithEvents tbbOpen As System.Windows.Forms.ToolBarButton
    Private WithEvents tbbPlay As System.Windows.Forms.ToolBarButton
    Private WithEvents tbbStop As System.Windows.Forms.ToolBarButton
    Private WithEvents ilMain As System.Windows.Forms.ImageList
    Private WithEvents ofdAudio As System.Windows.Forms.OpenFileDialog
    Private WithEvents pgMain As System.Windows.Forms.PropertyGrid

End Class
