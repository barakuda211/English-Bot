Imports Alvas.Audio

Public Class MainForm

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ofdSoxPath.Filter = "*sox.exe|sox.exe"
        ofdInput.Filter = "*.Aifc;*.Aiff;*.Au;*.Hcom;*.Voc;*.Wav;*.Gsm;*.Lpc10;*.Ogg;*.Wv|.Aifc;*.Aiff;*.Au;*.Hcom;*.Voc;*.Wav;*.Gsm;*.Lpc10;*.Ogg;*.Wv|*.*|*.*"
        Dim arr As String() = New String() {"Aifc", "Aiff", "Au", "Hcom", "Voc", "Wav", _
         "Gsm", "Lpc10", "Ogg", "Wv"}
        cbType.DataSource = arr
    End Sub

    Private _AudioFileIndex As Integer

    Public Property AudioFileIndex() As Integer
        Get
            Return _AudioFileIndex
        End Get
        Set(ByVal value As Integer)
            _AudioFileIndex = value
        End Set
    End Property

    Private Sub btnConvert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConvert.Click
        Dim soxPath As String = tbSoxPath.Text
        Dim inputFile As String = tbInput.Text
        Dim outputFile As String = tbOutput.Text
        If String.IsNullOrEmpty(soxPath) Then
            MessageBox.Show("Please specify path to Sox executable file")
            Return
        End If
        If String.IsNullOrEmpty(inputFile) Then
            MessageBox.Show("Please specify path to ")
            Return
        End If
        If String.IsNullOrEmpty(outputFile) Then
            MessageBox.Show("Please specify path to ")
            Return
        End If
        Try
            Dim OutputType As SoxAudioFileType = DirectCast(AudioFileIndex, SoxAudioFileType)
            Sox.Convert(soxPath, inputFile, outputFile, OutputType)
            MessageBox.Show("Complete!")
        Catch ex As SoxException
            MessageBox.Show([String].Format("{0}: {1}", ex.Code, ex.Message))
        End Try
    End Sub

    Private Sub btnOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutput.Click
        sdOutput.FileName = tbOutput.Text
        If sdOutput.ShowDialog() = DialogResult.OK Then
            tbOutput.Text = sdOutput.FileName
        End If
    End Sub

    Private Sub btnSoxPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSoxPath.Click
        ofdSoxPath.FileName = tbSoxPath.Text
        If ofdSoxPath.ShowDialog() = DialogResult.OK Then
            tbSoxPath.Text = ofdSoxPath.FileName
        End If
    End Sub

    Private Sub btnInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInput.Click
        ofdInput.FileName = tbInput.Text
        If ofdInput.ShowDialog() = DialogResult.OK Then
            tbInput.Text = ofdInput.FileName
        End If
    End Sub

    Private Sub tbInput_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbInput.TextChanged
        GenerateOutputFileName()
    End Sub

    Private Sub GenerateOutputFileName()
        tbOutput.Text = tbInput.Text + String.Format(".{0}", cbType.Items(AudioFileIndex))
    End Sub

    Private Sub cbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbType.SelectedIndexChanged
        AudioFileIndex = cbType.SelectedIndex
        GenerateOutputFileName()
    End Sub
End Class
