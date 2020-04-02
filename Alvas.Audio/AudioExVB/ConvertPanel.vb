Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports System.IO
Imports Alvas.Audio
Imports System.Diagnostics

Namespace AudioExCS
	Public Class ConvertPanel
		Inherits UserControl
		Public Sub New()
            InitializeComponent()
            ofdFile.Filter = "*.wav|*.wav|*.mp3|*.mp3|*.avi|*.avi|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.*|*.*"
        End Sub

		Private ofdFile As OpenFileDialog
		Private groupBox1 As GroupBox
		Private lblFileFormat As Label
		Private tbFile2 As TextBox
        Private WithEvents btnFile2 As Button
        Private WithEvents btnMakeMp3 As Button
		Private gbConvert As GroupBox
        Private WithEvents btnDialog As Button
        Private WithEvents rbDialog As RadioButton
		Private rbCombobox As RadioButton
        Private WithEvents btnConvert As Button
        Private WithEvents cbFormatConverted As ComboBox
        Private pbConvert As ProgressBar

		''' <summary> 
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso Not (components Is Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
		End Sub

		#Region "Component Designer generated code"

		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
            Me.ofdFile = New System.Windows.Forms.OpenFileDialog
            Me.groupBox1 = New System.Windows.Forms.GroupBox
            Me.lblFileFormat = New System.Windows.Forms.Label
            Me.tbFile2 = New System.Windows.Forms.TextBox
            Me.btnFile2 = New System.Windows.Forms.Button
            Me.btnMakeMp3 = New System.Windows.Forms.Button
            Me.gbConvert = New System.Windows.Forms.GroupBox
            Me.btnDialog = New System.Windows.Forms.Button
            Me.rbDialog = New System.Windows.Forms.RadioButton
            Me.rbCombobox = New System.Windows.Forms.RadioButton
            Me.btnConvert = New System.Windows.Forms.Button
            Me.cbFormatConverted = New System.Windows.Forms.ComboBox
            Me.pbConvert = New System.Windows.Forms.ProgressBar
            Me.groupBox1.SuspendLayout()
            Me.gbConvert.SuspendLayout()
            Me.SuspendLayout()
            '
            'ofdFile
            '
            Me.ofdFile.DefaultExt = "wav"
            Me.ofdFile.Filter = "*.wav;*.avi;*.mp3|*.wav;*.avi;*.mp3|*.wav|*.wav|*.avi|*.avi|*.mp3|*.mp3"
            '
            'groupBox1
            '
            Me.groupBox1.Controls.Add(Me.lblFileFormat)
            Me.groupBox1.Controls.Add(Me.tbFile2)
            Me.groupBox1.Controls.Add(Me.btnFile2)
            Me.groupBox1.Location = New System.Drawing.Point(3, 3)
            Me.groupBox1.Name = "groupBox1"
            Me.groupBox1.Size = New System.Drawing.Size(215, 66)
            Me.groupBox1.TabIndex = 34
            Me.groupBox1.TabStop = False
            Me.groupBox1.Text = "Select source file name"
            '
            'lblFileFormat
            '
            Me.lblFileFormat.AutoSize = True
            Me.lblFileFormat.Location = New System.Drawing.Point(6, 42)
            Me.lblFileFormat.Name = "lblFileFormat"
            Me.lblFileFormat.Size = New System.Drawing.Size(0, 13)
            Me.lblFileFormat.TabIndex = 29
            '
            'tbFile2
            '
            Me.tbFile2.BackColor = System.Drawing.SystemColors.Window
            Me.tbFile2.Location = New System.Drawing.Point(6, 19)
            Me.tbFile2.Name = "tbFile2"
            Me.tbFile2.ReadOnly = True
            Me.tbFile2.Size = New System.Drawing.Size(183, 20)
            Me.tbFile2.TabIndex = 22
            '
            'btnFile2
            '
            Me.btnFile2.Location = New System.Drawing.Point(188, 19)
            Me.btnFile2.Name = "btnFile2"
            Me.btnFile2.Size = New System.Drawing.Size(23, 20)
            Me.btnFile2.TabIndex = 23
            Me.btnFile2.Text = "..."
            '
            'btnMakeMp3
            '
            Me.btnMakeMp3.Enabled = False
            Me.btnMakeMp3.Location = New System.Drawing.Point(9, 197)
            Me.btnMakeMp3.Name = "btnMakeMp3"
            Me.btnMakeMp3.Size = New System.Drawing.Size(203, 23)
            Me.btnMakeMp3.TabIndex = 33
            Me.btnMakeMp3.Text = "Convert *.wav to *.mp3"
            '
            'gbConvert
            '
            Me.gbConvert.Controls.Add(Me.btnDialog)
            Me.gbConvert.Controls.Add(Me.rbDialog)
            Me.gbConvert.Controls.Add(Me.rbCombobox)
            Me.gbConvert.Controls.Add(Me.btnConvert)
            Me.gbConvert.Controls.Add(Me.cbFormatConverted)
            Me.gbConvert.Enabled = False
            Me.gbConvert.Location = New System.Drawing.Point(3, 75)
            Me.gbConvert.Name = "gbConvert"
            Me.gbConvert.Size = New System.Drawing.Size(215, 107)
            Me.gbConvert.TabIndex = 32
            Me.gbConvert.TabStop = False
            Me.gbConvert.Text = "Select destination format"
            '
            'btnDialog
            '
            Me.btnDialog.Enabled = False
            Me.btnDialog.Location = New System.Drawing.Point(26, 46)
            Me.btnDialog.Name = "btnDialog"
            Me.btnDialog.Size = New System.Drawing.Size(183, 23)
            Me.btnDialog.TabIndex = 29
            Me.btnDialog.Text = "Select destination format"
            '
            'rbDialog
            '
            Me.rbDialog.Location = New System.Drawing.Point(6, 52)
            Me.rbDialog.Name = "rbDialog"
            Me.rbDialog.Size = New System.Drawing.Size(14, 13)
            Me.rbDialog.TabIndex = 28
            '
            'rbCombobox
            '
            Me.rbCombobox.Checked = True
            Me.rbCombobox.Location = New System.Drawing.Point(6, 22)
            Me.rbCombobox.Name = "rbCombobox"
            Me.rbCombobox.Size = New System.Drawing.Size(14, 13)
            Me.rbCombobox.TabIndex = 27
            Me.rbCombobox.TabStop = True
            '
            'btnConvert
            '
            Me.btnConvert.Location = New System.Drawing.Point(7, 75)
            Me.btnConvert.Name = "btnConvert"
            Me.btnConvert.Size = New System.Drawing.Size(202, 23)
            Me.btnConvert.TabIndex = 27
            Me.btnConvert.Text = "Convert"
            '
            'cbFormatConverted
            '
            Me.cbFormatConverted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbFormatConverted.Location = New System.Drawing.Point(26, 19)
            Me.cbFormatConverted.Name = "cbFormatConverted"
            Me.cbFormatConverted.Size = New System.Drawing.Size(183, 21)
            Me.cbFormatConverted.TabIndex = 25
            ' 
            'pbConvert
            ' 
            Me.pbConvert.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pbConvert.Location = New System.Drawing.Point(0, 222)
            Me.pbConvert.Name = "pbConvert"
            Me.pbConvert.Size = New System.Drawing.Size(227, 23)
            Me.pbConvert.TabIndex = 35
            '
            'ConvertPanel
            '
            Me.Controls.Add(Me.pbConvert)
            Me.Controls.Add(Me.groupBox1)
            Me.Controls.Add(Me.btnMakeMp3)
            Me.Controls.Add(Me.gbConvert)
            Me.Name = "ConvertPanel"
            Me.Size = New System.Drawing.Size(227, 245)
            Me.groupBox1.ResumeLayout(False)
            Me.groupBox1.PerformLayout()
            Me.gbConvert.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

		#End Region

        'Private fs As FileStream = Nothing
		Private oldFormat As IntPtr = IntPtr.Zero
		Private newFormat As IntPtr = IntPtr.Zero
        'Private data As Byte() = Nothing
        Private ar As IAudioReader = Nothing


		Private Sub GetFormatsConverted(ByVal format As IntPtr)
			newFormat = IntPtr.Zero
			'cbFormatConverted.DataSource = AudioCompressionManager.GetCompatibleFormatList(format);
			cbFormatConverted.Items.Clear()
			Dim fdArr As FormatDetails() = AudioCompressionManager.GetCompatibleFormatList(format)
			For i As Integer = 0 To fdArr.Length - 1
				Dim fd As FormatDetails = fdArr(i)
				fd.ShowFormatTag = True
				cbFormatConverted.Items.Add(fd)
			Next
		End Sub

        Private Sub btnFile2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFile2.Click
            If ofdFile.ShowDialog(Me) = DialogResult.OK Then
                tbFile2.Text = ofdFile.FileName
                Dim lenExt As Integer = 4
                Dim ext As String = ofdFile.FileName.Substring(ofdFile.FileName.Length - lenExt, _
                    lenExt).ToLower()
                Select Case ext
                    Case ".au"
                    Case ".snd"
                        ar = New AuReader(File.OpenRead(ofdFile.FileName))
                        Exit Select
                    Case ".wav"
                        ar = New WaveReader(File.OpenRead(ofdFile.FileName))
                        Exit Select
                    Case ".avi"
                        ar = New AviReader(File.OpenRead(ofdFile.FileName))
                        If Not CType(ar, AviReader).HasAudio Then
                            MessageBox.Show("Avi stream has not audio track")
                            Return
                        End If
                        Exit Select
                    Case ".mp3"
                        ar = New Mp3Reader(File.OpenRead(ofdFile.FileName))
                        Exit Select
                    Case Else
                        ar = New DsReader(ofdFile.FileName)
                        If Not CType(ar, DsReader).HasAudio Then
                            MessageBox.Show("DirectShow stream has not audio track")
                            Return
                        End If
                        Exit Select
                End Select
                oldFormat = ar.ReadFormat()
                Dim fd As FormatDetails = AudioCompressionManager.GetFormatDetails(oldFormat)
                lblFileFormat.Text = String.Format("{0} {1}", AudioCompressionManager.GetFormatTagDetails(fd.FormatTag).FormatTagName, fd.FormatName)
                GetFormatsConverted(oldFormat)
                gbConvert.Enabled = True
                btnMakeMp3.Enabled = False
            End If
        End Sub

        Private Sub btnConvert_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConvert.Click
            If newFormat.Equals(IntPtr.Zero) Then
                MessageBox.Show("Please, specify destination format for converting")
                Return
            End If
            Dim fileName As String = tbFile2.Text + ".wav"
            Dim size As Integer = ar.Milliseconds2Bytes(1000)
            Dim len As Integer = ar.GetLengthInBytes()
            Dim ac As AcmConverter = New AcmConverter(oldFormat, newFormat, False)
            Dim fs As FileStream = New FileStream(fileName, FileMode.Create)
            Dim ww As WaveWriter = New WaveWriter(fs, AudioCompressionManager.FormatBytes(newFormat))
            pbConvert.Maximum = len
            Dim y As Integer = 0
            While y < len
                pbConvert.Value = y
                Dim data As Byte() = ar.ReadDataInBytes(y, size)
                If data.Length = 0 Then
                    Exit While
                End If
                y += data.Length
                Dim newData As Byte() = ac.Convert(data)
                ww.WriteData(newData)
            End While
            ww.Close()
            ar.Close()
            gbConvert.Enabled = False
            btnMakeMp3.Enabled = tbFile2.Text.ToLower().EndsWith(".wav")
            MessageBox.Show(String.Format("The result is stored in the file '{0}'. Choose next file for the converting.", fileName), "The conversion was executed successfully")
            OpenContainingFolder(fileName)
        End Sub

        Private Sub OpenContainingFolder(ByVal fileName As String)
            Process.Start(Path.GetDirectoryName(fileName))
        End Sub

        Private Sub NewFormatFromComboBox()
            newFormat = IIf((cbFormatConverted.SelectedIndex >= 0), DirectCast(cbFormatConverted.SelectedItem, FormatDetails).FormatHandle, IntPtr.Zero)
        End Sub

        Private Sub cbFormatConverted_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbFormatConverted.SelectedIndexChanged
            NewFormatFromComboBox()
        End Sub

        Private Sub ConvertDialog()
            Dim res As FormatChooseResult = AudioCompressionManager.ChooseCompatibleFormat(Me.Handle, oldFormat)
            If res.Result <> 0 Then
                btnDialog.Text = "Select destination format"
            Else
                newFormat = res.Format
                btnDialog.Text = String.Format("{0} {1}", res.FormatTagName, res.FormatName)
                cbFormatConverted.Enabled = False
            End If
        End Sub

        Private Sub rbDialog_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbDialog.Click
            btnDialog.Enabled = rbDialog.Checked
            cbFormatConverted.Enabled = Not rbDialog.Checked
            If Not rbDialog.Checked Then
                NewFormatFromComboBox()
            End If
        End Sub

        Private Sub btnMakeMp3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMakeMp3.Click
            Dim fileName As String = tbFile2.Text + ".mp3"
            AudioCompressionManager.Wave2Mp3(tbFile2.Text, fileName)
            btnMakeMp3.Enabled = False
            OpenContainingFolder(fileName)
        End Sub

        Private Sub btnDialog_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDialog.Click
            ConvertDialog()
        End Sub

	End Class
End Namespace
