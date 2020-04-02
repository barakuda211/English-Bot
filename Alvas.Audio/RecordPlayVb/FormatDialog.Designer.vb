Partial Class FormatDialog
	Inherits System.Windows.Forms.Form
	
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.gbSetup = New System.Windows.Forms.GroupBox()
			Me.cbFormat = New System.Windows.Forms.ComboBox()
			Me.label3 = New System.Windows.Forms.Label()
			Me.cbFormatTag = New System.Windows.Forms.ComboBox()
			Me.label5 = New System.Windows.Forms.Label()
			Me.btnOk = New System.Windows.Forms.Button()
			Me.btnCancel = New System.Windows.Forms.Button()
			Me.gbSetup.SuspendLayout()
			Me.SuspendLayout()
			' 
			' gbSetup
			' 
			Me.gbSetup.Controls.Add(Me.cbFormat)
			Me.gbSetup.Controls.Add(Me.label3)
			Me.gbSetup.Controls.Add(Me.cbFormatTag)
			Me.gbSetup.Controls.Add(Me.label5)
			Me.gbSetup.Location = New System.Drawing.Point(12, 12)
			Me.gbSetup.Name = "gbSetup"
			Me.gbSetup.Size = New System.Drawing.Size(217, 120)
			Me.gbSetup.TabIndex = 39
			Me.gbSetup.TabStop = False
			Me.gbSetup.Text = "Recorder Options"
			' 
			' cbFormat
			' 
			Me.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbFormat.Location = New System.Drawing.Point(6, 84)
			Me.cbFormat.Name = "cbFormat"
			Me.cbFormat.Size = New System.Drawing.Size(205, 21)
			Me.cbFormat.TabIndex = 12
			AddHandler Me.cbFormat.SelectedIndexChanged, AddressOf Me.cbFormat_SelectedIndexChanged
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(3, 68)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(101, 13)
			Me.label3.TabIndex = 20
			Me.label3.Text = "Select audio format:"
			' 
			' cbFormatTag
			' 
			Me.cbFormatTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbFormatTag.Location = New System.Drawing.Point(6, 36)
			Me.cbFormatTag.Name = "cbFormatTag"
			Me.cbFormatTag.Size = New System.Drawing.Size(205, 21)
			Me.cbFormatTag.TabIndex = 11
			AddHandler Me.cbFormatTag.SelectedIndexChanged, AddressOf Me.cbFormatTag_SelectedIndexChanged
			' 
			' label5
			' 
			Me.label5.AutoSize = True
			Me.label5.Location = New System.Drawing.Point(3, 20)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(119, 13)
			Me.label5.TabIndex = 19
			Me.label5.Text = "Select audio format tag:"
			' 
			' btnOk
			' 
			Me.btnOk.Location = New System.Drawing.Point(235, 12)
			Me.btnOk.Name = "btnOk"
			Me.btnOk.Size = New System.Drawing.Size(75, 23)
			Me.btnOk.TabIndex = 40
			Me.btnOk.Text = "OK"
			Me.btnOk.UseVisualStyleBackColor = True
			AddHandler Me.btnOk.Click, AddressOf Me.btnOk_Click
			' 
			' btnCancel
			' 
			Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
			Me.btnCancel.Location = New System.Drawing.Point(235, 41)
			Me.btnCancel.Name = "btnCancel"
			Me.btnCancel.Size = New System.Drawing.Size(75, 23)
			Me.btnCancel.TabIndex = 41
			Me.btnCancel.Text = "Cancel"
			Me.btnCancel.UseVisualStyleBackColor = True
			' 
			' NewDialog
			' 
			Me.AcceptButton = Me.btnOk
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(319, 145)
			Me.Controls.Add(Me.btnCancel)
			Me.Controls.Add(Me.btnOk)
			Me.Controls.Add(Me.gbSetup)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
			Me.Name = "NewDialog"
			Me.ShowInTaskbar = False
			Me.Text = "Open"
			AddHandler Me.Load, AddressOf Me.OpenNewDialog_Load
			Me.gbSetup.ResumeLayout(False)
			Me.gbSetup.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private gbSetup As System.Windows.Forms.GroupBox
		Private cbFormat As System.Windows.Forms.ComboBox
		Private label3 As System.Windows.Forms.Label
		Private cbFormatTag As System.Windows.Forms.ComboBox
		Private label5 As System.Windows.Forms.Label
		Private btnOk As System.Windows.Forms.Button
		Private btnCancel As System.Windows.Forms.Button
	End Class
