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
    '<System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.progressBar1 = New System.Windows.Forms.ProgressBar
        Me.label1 = New System.Windows.Forms.Label
        Me.columnHeaderSize = New System.Windows.Forms.ColumnHeader
        Me.buttonSaveAs = New System.Windows.Forms.Button
        Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
        Me.columnHeaderType = New System.Windows.Forms.ColumnHeader
        Me.columnHeaderTrack = New System.Windows.Forms.ColumnHeader
        Me.columnHeaderTitle = New System.Windows.Forms.ColumnHeader
        Me.listViewTracks = New System.Windows.Forms.ListView
        Me.groupBoxCDCtrls = New System.Windows.Forms.GroupBox
        Me.labelTracks = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.statusBar = New System.Windows.Forms.StatusBar
        Me.buttonOpen = New System.Windows.Forms.Button
        Me.comboBoxDrives = New System.Windows.Forms.ComboBox
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.groupBoxCDCtrls.SuspendLayout()
        Me.SuspendLayout()
        '
        'progressBar1
        '
        Me.progressBar1.Location = New System.Drawing.Point(87, 240)
        Me.progressBar1.Name = "progressBar1"
        Me.progressBar1.Size = New System.Drawing.Size(429, 23)
        Me.progressBar1.TabIndex = 7
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(16, 4)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(64, 16)
        Me.label1.TabIndex = 6
        Me.label1.Text = "CD Drives:"
        '
        'columnHeaderSize
        '
        Me.columnHeaderSize.Text = "Size (bytes)"
        Me.columnHeaderSize.Width = 105
        '
        'buttonSaveAs
        '
        Me.buttonSaveAs.Enabled = False
        Me.buttonSaveAs.Location = New System.Drawing.Point(6, 238)
        Me.buttonSaveAs.Name = "buttonSaveAs"
        Me.buttonSaveAs.Size = New System.Drawing.Size(75, 23)
        Me.buttonSaveAs.TabIndex = 6
        Me.buttonSaveAs.Text = "Save as..."
        '
        'saveFileDialog
        '
        Me.saveFileDialog.DefaultExt = "wav"
        Me.saveFileDialog.Filter = "Wave files (*.wav)|*.wav|All files (*.*)|*.*"
        Me.saveFileDialog.Title = "Save tract to:"
        '
        'columnHeaderType
        '
        Me.columnHeaderType.Text = "Type"
        Me.columnHeaderType.Width = 115
        '
        'columnHeaderTrack
        '
        Me.columnHeaderTrack.Text = "Track"
        Me.columnHeaderTrack.Width = 62
        '
        'columnHeaderTitle
        '
        Me.columnHeaderTitle.Text = "Title"
        Me.columnHeaderTitle.Width = 200
        '
        'listViewTracks
        '
        Me.listViewTracks.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeaderTrack, Me.columnHeaderSize, Me.columnHeaderType, Me.columnHeaderTitle})
        Me.listViewTracks.FullRowSelect = True
        Me.listViewTracks.Location = New System.Drawing.Point(0, 40)
        Me.listViewTracks.Name = "listViewTracks"
        Me.listViewTracks.Size = New System.Drawing.Size(516, 192)
        Me.listViewTracks.TabIndex = 5
        Me.toolTip1.SetToolTip(Me.listViewTracks, "Select the track that you want to save")
        Me.listViewTracks.UseCompatibleStateImageBehavior = False
        Me.listViewTracks.View = System.Windows.Forms.View.Details
        '
        'groupBoxCDCtrls
        '
        Me.groupBoxCDCtrls.Controls.Add(Me.progressBar1)
        Me.groupBoxCDCtrls.Controls.Add(Me.buttonSaveAs)
        Me.groupBoxCDCtrls.Controls.Add(Me.listViewTracks)
        Me.groupBoxCDCtrls.Controls.Add(Me.labelTracks)
        Me.groupBoxCDCtrls.Controls.Add(Me.label2)
        Me.groupBoxCDCtrls.Enabled = False
        Me.groupBoxCDCtrls.Location = New System.Drawing.Point(16, 36)
        Me.groupBoxCDCtrls.Name = "groupBoxCDCtrls"
        Me.groupBoxCDCtrls.Size = New System.Drawing.Size(532, 272)
        Me.groupBoxCDCtrls.TabIndex = 10
        Me.groupBoxCDCtrls.TabStop = False
        '
        'labelTracks
        '
        Me.labelTracks.Location = New System.Drawing.Point(168, 16)
        Me.labelTracks.Name = "labelTracks"
        Me.labelTracks.Size = New System.Drawing.Size(88, 16)
        Me.labelTracks.TabIndex = 4
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(6, 16)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(40, 16)
        Me.label2.TabIndex = 3
        Me.label2.Text = "Tracks:"
        '
        'statusBar
        '
        Me.statusBar.Location = New System.Drawing.Point(0, 327)
        Me.statusBar.Name = "statusBar"
        Me.statusBar.Size = New System.Drawing.Size(556, 22)
        Me.statusBar.TabIndex = 9
        '
        'buttonOpen
        '
        Me.buttonOpen.Enabled = False
        Me.buttonOpen.Location = New System.Drawing.Point(176, 4)
        Me.buttonOpen.Name = "buttonOpen"
        Me.buttonOpen.Size = New System.Drawing.Size(75, 23)
        Me.buttonOpen.TabIndex = 8
        Me.buttonOpen.Text = "Open"
        Me.toolTip1.SetToolTip(Me.buttonOpen, "Open/Close the CD drive")
        '
        'comboBoxDrives
        '
        Me.comboBoxDrives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBoxDrives.Location = New System.Drawing.Point(80, 4)
        Me.comboBoxDrives.Name = "comboBoxDrives"
        Me.comboBoxDrives.Size = New System.Drawing.Size(80, 21)
        Me.comboBoxDrives.TabIndex = 7
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(556, 349)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.groupBoxCDCtrls)
        Me.Controls.Add(Me.statusBar)
        Me.Controls.Add(Me.buttonOpen)
        Me.Controls.Add(Me.comboBoxDrives)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "MainForm"
        Me.Text = "CDA Demo"
        Me.groupBoxCDCtrls.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents progressBar1 As System.Windows.Forms.ProgressBar
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents columnHeaderSize As System.Windows.Forms.ColumnHeader
    Private WithEvents buttonSaveAs As System.Windows.Forms.Button
    Private WithEvents saveFileDialog As System.Windows.Forms.SaveFileDialog
    Private WithEvents columnHeaderType As System.Windows.Forms.ColumnHeader
    Private WithEvents columnHeaderTrack As System.Windows.Forms.ColumnHeader
    Private WithEvents columnHeaderTitle As System.Windows.Forms.ColumnHeader
    Private WithEvents listViewTracks As System.Windows.Forms.ListView
    Private WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents groupBoxCDCtrls As System.Windows.Forms.GroupBox
    Private WithEvents labelTracks As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents statusBar As System.Windows.Forms.StatusBar
    Private WithEvents buttonOpen As System.Windows.Forms.Button
    Private WithEvents comboBoxDrives As System.Windows.Forms.ComboBox

End Class
