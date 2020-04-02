Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports Alvas.Audio
Imports System.IO

Partial Public Class MainForm
    Inherits Form
    Private Const filter As String = "*.wav|*.wav|*.mp3|*.mp3|*.avi|*.avi|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.*|*.*"
    Private Const OpenOrDragFile As String = "Open a media file or drag the mouse from Windows Explorer"
    Private ofdAudio As New OpenFileDialog()

    Public Sub New()
        InitializeComponent()
        Init()
    End Sub

    Private Sub Init()
        ofdAudio.Filter = filter
        tbResult.Text = OpenOrDragFile
        tbResult.AllowDrop = True
    End Sub

    Private Sub miExit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles miExit.Click
        Close()
    End Sub

    Private Sub miOpen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles miOpen.Click
        If ofdAudio.ShowDialog() = DialogResult.OK Then
            tbResult.Lines = IntspectFile(ofdAudio.FileName)
        End If
    End Sub

    Private Function IntspectFile(ByVal fileName As String) As String()
        Dim m_id3 As ID3v1
        Dim m_waveTags As Dictionary(Of WaveInfo, String) = Nothing
        Dim ar As IAudioReader = Nothing
        Dim ext As String = Path.GetExtension(fileName.ToLower())
        m_id3 = Nothing
        m_waveTags = Nothing
        Select Case ext
            Case ".au"
            Case ".snd"
                ar = New AuReader(File.OpenRead(fileName))
                Exit Select
            Case ".avi"
                ar = New AviReader(File.OpenRead(fileName))
                If Not DirectCast(ar, AviReader).HasAudio Then
                    Return New String() {String.Format("'{0}' file is not contains audio data", fileName)}
                End If
                Exit Select
            Case ".wav"
                ar = New WaveReader(File.OpenRead(fileName))
                m_waveTags = TryCast(ar, WaveReader).ReadInfoTag()
                Exit Select
            Case ".mp3"
                ar = New Mp3Reader(File.OpenRead(fileName))

                Dim mrID3 As New Mp3Reader(File.OpenRead(fileName))
                m_id3 = mrID3.ReadID3v1Tag()
                Exit Select
            Case Else
                ar = New DsReader(fileName)
                If Not DirectCast(ar, DsReader).HasAudio Then
                    Return New String() {String.Format("'{0}' file is not contains audio data", fileName)}
                End If
                Exit Select
        End Select
        Dim format As IntPtr = ar.ReadFormat()
        Dim wf As WaveFormat = AudioCompressionManager.GetWaveFormat(format)
        Dim list As New List(Of String)()
        list.Add(String.Format("Opening {0}", fileName))
        list.Add(String.Format("{0}, {1} Hz, {2} channels, {3} bits per sample", GetFormatTagName(wf.wFormatTag), wf.nSamplesPerSec, wf.nChannels, wf.wBitsPerSample))
        list.Add(String.Format("Block Align: {0}, Average Bytes Per Second: {1}", wf.nBlockAlign, wf.nAvgBytesPerSec))
        Dim duration As TimeSpan = TimeSpan.FromMilliseconds(ar.GetDurationInMS())
        list.Add(String.Format("Duration: {0}", duration))
        If m_id3 IsNot Nothing Then
            list.Add("--------- ID3 -----------")
            list.Add(String.Format("Title: {0}", m_id3.Title))
            list.Add(String.Format("Artist: {0}", m_id3.Artist))
            list.Add(String.Format("Album: {0}", m_id3.Album))
            list.Add(String.Format("Year: {0}", m_id3.Year))
            list.Add(String.Format("Genre: {0}", m_id3.Genre.ToString()))

            list.Add(String.Format("Comment: {0}", m_id3.Comment))
        End If
        If m_waveTags IsNot Nothing Then
            list.Add("--------- Wave tags -----------")
            For Each key As WaveInfo In m_waveTags.Keys
                list.Add(String.Format("{0}: {1}", key.ToString(), m_waveTags(key)))

            Next
        End If
        ar.Close()
        Return list.ToArray()
    End Function

    Private Function GetFormatTagName(ByVal tag As Integer) As String
        Dim ftd As FormatTagDetails = AudioCompressionManager.GetFormatTagDetails(tag)
        Return ftd.FormatTagName
    End Function

    Private Sub tbResult_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles tbResult.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub tbResult_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles tbResult.DragDrop
        Try
            Dim a As Array = DirectCast(e.Data.GetData(DataFormats.FileDrop), Array)

            If a IsNot Nothing Then
                Dim s As String = a.GetValue(0).ToString()
                tbResult.Lines = IntspectFile(s)
                Me.Activate()
            End If
        Catch ex As Exception
            Console.WriteLine("Error in DragDrop function: " & ex.Message)
        End Try
    End Sub

End Class
