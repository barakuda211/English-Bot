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
	Public Class VoxPanel
		Inherits UserControl
        Private ofdVox As System.Windows.Forms.OpenFileDialog
        Private WithEvents btnConvert As Button
        Private WithEvents btnOpen As Button
		Private cbSamplesPerSec As ComboBox
		Private tbVox As TextBox
        Private WithEvents btnConvert2 As Button
        Private WithEvents btnWav As Button
		Private cbSamplesPerSec2 As ComboBox
		Private tbWav As TextBox
        Private WithEvents cbResample As CheckBox
        Private WithEvents btnVox2Mp3 As System.Windows.Forms.Button

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
            Me.btnConvert = New System.Windows.Forms.Button
            Me.btnOpen = New System.Windows.Forms.Button
            Me.cbSamplesPerSec = New System.Windows.Forms.ComboBox
            Me.tbVox = New System.Windows.Forms.TextBox
            Me.btnConvert2 = New System.Windows.Forms.Button
            Me.btnWav = New System.Windows.Forms.Button
            Me.cbSamplesPerSec2 = New System.Windows.Forms.ComboBox
            Me.tbWav = New System.Windows.Forms.TextBox
            Me.cbResample = New System.Windows.Forms.CheckBox
            Me.btnVox2Mp3 = New System.Windows.Forms.Button
            ofdVox = New System.Windows.Forms.OpenFileDialog
            Me.SuspendLayout()
            '
            'btnConvert
            '
            Me.btnConvert.Location = New System.Drawing.Point(6, 58)
            Me.btnConvert.Name = "btnConvert"
            Me.btnConvert.Size = New System.Drawing.Size(212, 23)
            Me.btnConvert.TabIndex = 5
            Me.btnConvert.Text = "Vox -> Wav"
            '
            'btnOpen
            '
            Me.btnOpen.Location = New System.Drawing.Point(194, 2)
            Me.btnOpen.Name = "btnOpen"
            Me.btnOpen.Size = New System.Drawing.Size(24, 23)
            Me.btnOpen.TabIndex = 4
            Me.btnOpen.Text = "..."
            '
            'cbSamplesPerSec
            '
            Me.cbSamplesPerSec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbSamplesPerSec.Location = New System.Drawing.Point(6, 31)
            Me.cbSamplesPerSec.Name = "cbSamplesPerSec"
            Me.cbSamplesPerSec.Size = New System.Drawing.Size(212, 21)
            Me.cbSamplesPerSec.TabIndex = 7
            '
            'tbVox
            '
            Me.tbVox.Location = New System.Drawing.Point(6, 5)
            Me.tbVox.Name = "tbVox"
            Me.tbVox.Size = New System.Drawing.Size(187, 20)
            Me.tbVox.TabIndex = 6
            '
            'btnConvert2
            '
            Me.btnConvert2.Location = New System.Drawing.Point(6, 196)
            Me.btnConvert2.Name = "btnConvert2"
            Me.btnConvert2.Size = New System.Drawing.Size(212, 23)
            Me.btnConvert2.TabIndex = 9
            Me.btnConvert2.Text = "Wav -> Vox"
            '
            'btnWav
            '
            Me.btnWav.Location = New System.Drawing.Point(194, 117)
            Me.btnWav.Name = "btnWav"
            Me.btnWav.Size = New System.Drawing.Size(24, 23)
            Me.btnWav.TabIndex = 8
            Me.btnWav.Text = "..."
            '
            'cbSamplesPerSec2
            '
            Me.cbSamplesPerSec2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbSamplesPerSec2.Enabled = False
            Me.cbSamplesPerSec2.Location = New System.Drawing.Point(6, 169)
            Me.cbSamplesPerSec2.Name = "cbSamplesPerSec2"
            Me.cbSamplesPerSec2.Size = New System.Drawing.Size(212, 21)
            Me.cbSamplesPerSec2.TabIndex = 11
            '
            'tbWav
            '
            Me.tbWav.Location = New System.Drawing.Point(6, 120)
            Me.tbWav.Name = "tbWav"
            Me.tbWav.Size = New System.Drawing.Size(187, 20)
            Me.tbWav.TabIndex = 10
            '
            'cbResample
            '
            Me.cbResample.Location = New System.Drawing.Point(6, 146)
            Me.cbResample.Name = "cbResample"
            Me.cbResample.Size = New System.Drawing.Size(90, 17)
            Me.cbResample.TabIndex = 12
            Me.cbResample.Text = "Resample"
            '
            'ofdVox
            '
            ofdVox.Filter = "*.vox|*.vox"
            '
            'btnVox2Mp3
            '
            Me.btnVox2Mp3.Location = New System.Drawing.Point(6, 87)
            Me.btnVox2Mp3.Name = "btnVox2Mp3"
            Me.btnVox2Mp3.Size = New System.Drawing.Size(212, 23)
            Me.btnVox2Mp3.TabIndex = 14
            Me.btnVox2Mp3.Text = "Vox -> Mp3"
            '
            'VoxPanel
            '
            Me.Controls.Add(Me.btnVox2Mp3)
            Me.Controls.Add(Me.cbResample)
            Me.Controls.Add(Me.btnConvert2)
            Me.Controls.Add(Me.btnWav)
            Me.Controls.Add(Me.cbSamplesPerSec2)
            Me.Controls.Add(Me.tbWav)
            Me.Controls.Add(Me.btnConvert)
            Me.Controls.Add(Me.btnOpen)
            Me.Controls.Add(Me.cbSamplesPerSec)
            Me.Controls.Add(Me.tbVox)
            Me.Name = "VoxPanel"
            Me.Size = New System.Drawing.Size(224, 243)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

#End Region

        Public Sub New()
            InitializeComponent()
            cbSamplesPerSec.DataSource = New Integer() {6000, 8000, 11025, 12000, 16000, 22050, _
             24000, 32000, 44100, 48000}
            cbSamplesPerSec2.DataSource = New Integer() {6000, 8000, 11025, 12000, 16000, 22050, _
             24000, 32000, 44100, 48000}
        End Sub

        Private Sub btnConvert_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConvert.Click
            Dim voxFile As String = tbVox.Text
            If voxFile = String.Empty Then
                MessageBox.Show("Please, select file for converting")
                Return
            End If
            Dim wavFile As String = Path.ChangeExtension(voxFile, ".wav")
            Vox2Wav(voxFile, wavFile, CInt(cbSamplesPerSec.SelectedValue))
            MessageBox.Show(String.Format("The result is stored in the file '{0}'. Choose next file for the converting.", wavFile), "The conversion was executed successfully")
            OpenContainingFolder(wavFile)
        End Sub

        Private Sub OpenContainingFolder(ByVal fileName As String)
            Process.Start(Path.GetDirectoryName(fileName))
        End Sub

        Private Shared Sub Vox2Wav(ByVal voxFile As String, ByVal wavFile As String, ByVal samplesPerSec As Integer)
            Dim br As New BinaryReader(File.OpenRead(voxFile))
            Dim format As IntPtr = AudioCompressionManager.GetPcmFormat(1, 16, samplesPerSec)
            Dim ww As New WaveWriter(File.Create(wavFile), AudioCompressionManager.FormatBytes(format))
            Vox.Vox2Wav(br, ww)
            br.Close()
            ww.Close()
        End Sub

        Private Sub btnOpen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpen.Click
            OpenFile(VoxFilter, tbVox)
        End Sub

        Private Sub btnWav_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWav.Click
            OpenFile(WavFilter, tbWav)
        End Sub

        Private Sub OpenFile(ByVal filter As String, ByVal tb As TextBox)
            ofdVox.Filter = filter
            If ofdVox.ShowDialog() = DialogResult.OK Then
                tb.Text = ofdVox.FileName
            End If
        End Sub

        Private Const VoxFilter As String = "*.vox|*.vox"
        Private Const WavFilter As String = "*.wav|*.wav"

        Private Sub btnConvert2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConvert2.Click
            Dim wavFile As String = tbWav.Text
            If wavFile = String.Empty Then
                MessageBox.Show("Please, select file for converting")
                Return
            End If
            Dim voxFile As String = Path.ChangeExtension(wavFile, ".vox")
            Wav2Vox(wavFile, voxFile, CInt(cbSamplesPerSec2.SelectedValue), cbResample.Checked)
            MessageBox.Show(String.Format("The result is stored in the file '{0}'. Choose next file for the converting.", voxFile), "The conversion was executed successfully")
            OpenContainingFolder(voxFile)
        End Sub

        Private Shared Sub Wav2Vox(ByVal inFile As String, ByVal outFile As String, ByVal samplesPerSec As Integer, ByVal isResample As Boolean)
            Dim wr As New WaveReader(File.OpenRead(inFile))
            Dim format As IntPtr = wr.ReadFormat()
            Dim data As Byte() = wr.ReadData()
            wr.Close()
            Dim wf As WaveFormat = AudioCompressionManager.GetWaveFormat(format)
            If wf.wFormatTag <> AudioCompressionManager.PcmFormatTag Then
                'Decode if not PCM data 
                Decode2Pcm(format, data, wf)
            End If
            If isResample AndAlso wf.nSamplesPerSec <> samplesPerSec Then
                Resample(format, data, wf, samplesPerSec)
            End If
            Dim bw As New BinaryWriter(File.OpenWrite(outFile))
            Dim br As New BinaryReader(New MemoryStream(data))
            Vox.Raw2Vox(br, bw, wf.wBitsPerSample)
            br.Close()
            bw.Close()
        End Sub

        Private Shared Sub Resample(ByRef format As IntPtr, ByRef data As Byte(), ByRef wf As WaveFormat, ByVal samplesPerSec As Integer)
            Dim newFormat As IntPtr = AudioCompressionManager.GetPcmFormat(wf.nChannels, wf.wBitsPerSample, samplesPerSec)
            Dim buffer As Byte() = AudioCompressionManager.Convert(format, newFormat, data, True)
            format = newFormat
            wf = AudioCompressionManager.GetWaveFormat(newFormat)
            data = buffer
        End Sub

        Private Shared Sub Decode2Pcm(ByRef format As IntPtr, ByRef data As Byte(), ByRef wf As WaveFormat)
            Dim newFormat As IntPtr = AudioCompressionManager.GetCompatibleFormat(format, AudioCompressionManager.PcmFormatTag)
            Dim buffer As Byte() = AudioCompressionManager.Convert(format, newFormat, data, False)
            wf = AudioCompressionManager.GetWaveFormat(newFormat)
            format = newFormat
            data = buffer
        End Sub

        Private Sub cbResample_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbResample.CheckedChanged
            cbSamplesPerSec2.Enabled = cbResample.Checked
        End Sub

        Private Sub btnVox2Mp3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVox2Mp3.Click
            Dim voxFile As String = tbVox.Text
            If voxFile = String.Empty Then
                MessageBox.Show("Please, select file for converting")
                Return
            End If
            Dim mp3File As String = Path.ChangeExtension(voxFile, ".mp3")
            Vox.Vox2Mp3(voxFile, mp3File, CInt(cbSamplesPerSec.SelectedValue))
            MessageBox.Show(String.Format("The result is stored in the file '{0}'. Choose next file for the converting.", mp3File), "The conversion was executed successfully")
            OpenContainingFolder(mp3File)
        End Sub
    End Class
End Namespace
