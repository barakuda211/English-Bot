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
        Me.tcEffects = New System.Windows.Forms.TabControl
        Me.splitContainer1 = New System.Windows.Forms.SplitContainer
        Me.propertyGrid1 = New System.Windows.Forms.PropertyGrid
        Me.groupBox1 = New System.Windows.Forms.GroupBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnOpen = New System.Windows.Forms.Button
        Me.btnStop = New System.Windows.Forms.Button
        Me.btnPlay = New System.Windows.Forms.Button
        Me.ofdAudio = New System.Windows.Forms.OpenFileDialog
        Me.sfdAudio = New System.Windows.Forms.SaveFileDialog
        Me.splitContainer1.Panel1.SuspendLayout()
        Me.splitContainer1.Panel2.SuspendLayout()
        Me.splitContainer1.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tcEffects
        '
        Me.tcEffects.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcEffects.Location = New System.Drawing.Point(0, 0)
        Me.tcEffects.Multiline = True
        Me.tcEffects.Name = "tcEffects"
        Me.tcEffects.SelectedIndex = 0
        Me.tcEffects.Size = New System.Drawing.Size(572, 427)
        Me.tcEffects.TabIndex = 1
        '
        'splitContainer1
        '
        Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.splitContainer1.Name = "splitContainer1"
        '
        'splitContainer1.Panel1
        '
        Me.splitContainer1.Panel1.Controls.Add(Me.propertyGrid1)
        Me.splitContainer1.Panel1.Controls.Add(Me.groupBox1)
        '
        'splitContainer1.Panel2
        '
        Me.splitContainer1.Panel2.Controls.Add(Me.tcEffects)
        Me.splitContainer1.Size = New System.Drawing.Size(864, 427)
        Me.splitContainer1.SplitterDistance = 288
        Me.splitContainer1.TabIndex = 1
        '
        'propertyGrid1
        '
        Me.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.propertyGrid1.Location = New System.Drawing.Point(0, 78)
        Me.propertyGrid1.Name = "propertyGrid1"
        Me.propertyGrid1.Size = New System.Drawing.Size(288, 349)
        Me.propertyGrid1.TabIndex = 0
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.btnSave)
        Me.groupBox1.Controls.Add(Me.btnOpen)
        Me.groupBox1.Controls.Add(Me.btnStop)
        Me.groupBox1.Controls.Add(Me.btnPlay)
        Me.groupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.groupBox1.Location = New System.Drawing.Point(0, 0)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(288, 78)
        Me.groupBox1.TabIndex = 1
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Audio file"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(8, 32)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(65, 23)
        Me.btnSave.TabIndex = 18
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(77, 32)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(65, 23)
        Me.btnOpen.TabIndex = 17
        Me.btnOpen.Text = "Open..."
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Location = New System.Drawing.Point(215, 32)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(65, 23)
        Me.btnStop.TabIndex = 16
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnPlay
        '
        Me.btnPlay.Location = New System.Drawing.Point(146, 32)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(65, 23)
        Me.btnPlay.TabIndex = 15
        Me.btnPlay.Text = "Play"
        Me.btnPlay.UseVisualStyleBackColor = True
        '
        'ofdAudio
        '
        Me.ofdAudio.DefaultExt = "wav"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(864, 427)
        Me.Controls.Add(Me.splitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MainForm"
        Me.Text = "Effects Sample"
        Me.splitContainer1.Panel1.ResumeLayout(False)
        Me.splitContainer1.Panel2.ResumeLayout(False)
        Me.splitContainer1.ResumeLayout(False)
        Me.groupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents tcEffects As System.Windows.Forms.TabControl
    Private WithEvents splitContainer1 As System.Windows.Forms.SplitContainer
    Private WithEvents propertyGrid1 As System.Windows.Forms.PropertyGrid
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents btnSave As System.Windows.Forms.Button
    Private WithEvents btnOpen As System.Windows.Forms.Button
    Private WithEvents btnStop As System.Windows.Forms.Button
    Private WithEvents btnPlay As System.Windows.Forms.Button
    Private WithEvents ofdAudio As System.Windows.Forms.OpenFileDialog
    Private WithEvents sfdAudio As System.Windows.Forms.SaveFileDialog

End Class
