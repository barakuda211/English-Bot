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
        Me.groupBox1 = New System.Windows.Forms.GroupBox
        Me.label3 = New System.Windows.Forms.Label
        Me.btnOutput = New System.Windows.Forms.Button
        Me.tbOutput = New System.Windows.Forms.TextBox
        Me.cbType = New System.Windows.Forms.ComboBox
        Me.btnConvert = New System.Windows.Forms.Button
        Me.label2 = New System.Windows.Forms.Label
        Me.btnInput = New System.Windows.Forms.Button
        Me.tbInput = New System.Windows.Forms.TextBox
        Me.label1 = New System.Windows.Forms.Label
        Me.btnSoxPath = New System.Windows.Forms.Button
        Me.tbSoxPath = New System.Windows.Forms.TextBox
        Me.ofdSoxPath = New System.Windows.Forms.OpenFileDialog
        Me.ofdInput = New System.Windows.Forms.OpenFileDialog
        Me.sdOutput = New System.Windows.Forms.SaveFileDialog
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.label3)
        Me.groupBox1.Controls.Add(Me.btnOutput)
        Me.groupBox1.Controls.Add(Me.tbOutput)
        Me.groupBox1.Controls.Add(Me.cbType)
        Me.groupBox1.Controls.Add(Me.btnConvert)
        Me.groupBox1.Controls.Add(Me.label2)
        Me.groupBox1.Controls.Add(Me.btnInput)
        Me.groupBox1.Controls.Add(Me.tbInput)
        Me.groupBox1.Controls.Add(Me.label1)
        Me.groupBox1.Controls.Add(Me.btnSoxPath)
        Me.groupBox1.Controls.Add(Me.tbSoxPath)
        Me.groupBox1.Location = New System.Drawing.Point(14, 14)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(438, 241)
        Me.groupBox1.TabIndex = 3
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Sox Convert"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(8, 152)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(88, 13)
        Me.label3.TabIndex = 10
        Me.label3.Text = "Output Audio File"
        '
        'btnOutput
        '
        Me.btnOutput.Location = New System.Drawing.Point(384, 165)
        Me.btnOutput.Name = "btnOutput"
        Me.btnOutput.Size = New System.Drawing.Size(37, 23)
        Me.btnOutput.TabIndex = 9
        Me.btnOutput.Text = "..."
        Me.btnOutput.UseVisualStyleBackColor = True
        '
        'tbOutput
        '
        Me.tbOutput.Location = New System.Drawing.Point(11, 168)
        Me.tbOutput.Name = "tbOutput"
        Me.tbOutput.Size = New System.Drawing.Size(367, 20)
        Me.tbOutput.TabIndex = 8
        '
        'cbType
        '
        Me.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbType.FormattingEnabled = True
        Me.cbType.Location = New System.Drawing.Point(11, 122)
        Me.cbType.Name = "cbType"
        Me.cbType.Size = New System.Drawing.Size(367, 21)
        Me.cbType.TabIndex = 7
        '
        'btnConvert
        '
        Me.btnConvert.Location = New System.Drawing.Point(11, 204)
        Me.btnConvert.Name = "btnConvert"
        Me.btnConvert.Size = New System.Drawing.Size(410, 23)
        Me.btnConvert.TabIndex = 6
        Me.btnConvert.Text = "Convert"
        Me.btnConvert.UseVisualStyleBackColor = True
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(8, 69)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(80, 13)
        Me.label2.TabIndex = 5
        Me.label2.Text = "Input Audio File"
        '
        'btnInput
        '
        Me.btnInput.Location = New System.Drawing.Point(384, 82)
        Me.btnInput.Name = "btnInput"
        Me.btnInput.Size = New System.Drawing.Size(37, 23)
        Me.btnInput.TabIndex = 4
        Me.btnInput.Text = "..."
        Me.btnInput.UseVisualStyleBackColor = True
        '
        'tbInput
        '
        Me.tbInput.Location = New System.Drawing.Point(11, 85)
        Me.tbInput.Name = "tbInput"
        Me.tbInput.Size = New System.Drawing.Size(367, 20)
        Me.tbInput.TabIndex = 3
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(8, 27)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(50, 13)
        Me.label1.TabIndex = 2
        Me.label1.Text = "Sox Path"
        '
        'btnSoxPath
        '
        Me.btnSoxPath.Location = New System.Drawing.Point(384, 40)
        Me.btnSoxPath.Name = "btnSoxPath"
        Me.btnSoxPath.Size = New System.Drawing.Size(37, 23)
        Me.btnSoxPath.TabIndex = 1
        Me.btnSoxPath.Text = "..."
        Me.btnSoxPath.UseVisualStyleBackColor = True
        '
        'tbSoxPath
        '
        Me.tbSoxPath.Location = New System.Drawing.Point(11, 43)
        Me.tbSoxPath.Name = "tbSoxPath"
        Me.tbSoxPath.Size = New System.Drawing.Size(367, 20)
        Me.tbSoxPath.TabIndex = 0
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 268)
        Me.Controls.Add(Me.groupBox1)
        Me.Name = "MainForm"
        Me.Text = "Sox Convert"
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents btnOutput As System.Windows.Forms.Button
    Private WithEvents tbOutput As System.Windows.Forms.TextBox
    Private WithEvents cbType As System.Windows.Forms.ComboBox
    Private WithEvents btnConvert As System.Windows.Forms.Button
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents btnInput As System.Windows.Forms.Button
    Private WithEvents tbInput As System.Windows.Forms.TextBox
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents btnSoxPath As System.Windows.Forms.Button
    Private WithEvents tbSoxPath As System.Windows.Forms.TextBox
    Private WithEvents ofdSoxPath As System.Windows.Forms.OpenFileDialog
    Private WithEvents ofdInput As System.Windows.Forms.OpenFileDialog
    Private WithEvents sdOutput As System.Windows.Forms.SaveFileDialog

End Class
