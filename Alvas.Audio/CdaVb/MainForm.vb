Imports Alvas.Audio
Imports System.IO

Public Class MainForm
    Private cda As New CdDrive()
    Private ripping As Boolean = False
    Private m_CancelRipping As Boolean = False


    Private Sub MainWindow_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'cda = New CdDrive()
        Dim Drives As Char() = CdDrive.GetCdDrives()
        For Each drive As Char In Drives
            comboBoxDrives.Items.Add(drive.ToString())
        Next
        If comboBoxDrives.Items.Count > 0 Then
            comboBoxDrives.SelectedIndex = 0
        End If
    End Sub

    Private Sub UpdateVisualControls()
        buttonOpen.Enabled = Not ripping And (comboBoxDrives.SelectedIndex >= 0)
        comboBoxDrives.Enabled = Not ripping And (Not cda.IsOpened)
        groupBoxCDCtrls.Enabled = Not ripping And (cda.IsOpened)
        If listViewTracks.SelectedIndices.Count > 0 Then
            Dim track As Integer = listViewTracks.SelectedIndices(0) ' +1;
            buttonSaveAs.Enabled = Not ripping And cda.IsAudioTrack(track)
        Else
            buttonSaveAs.Enabled = False
        End If
    End Sub

    Private Sub comboBoxDrives_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles comboBoxDrives.SelectedIndexChanged
        UpdateVisualControls()
    End Sub

    Shared dict As New Dictionary(Of String, List(Of CdInfo))()

    Private Shared Function GetCdInfo(ByVal key As String) As List(Of CdInfo)
        If Not dict.ContainsKey(key) Then
            dict(key) = CdDrive.GetCdInfo(key)
        End If
        Return dict(key)
    End Function

    Private Sub buttonOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonOpen.Click
        If cda.IsOpened Then
            cda.Close()
            buttonOpen.Text = "Open"
            statusBar.Text = "CD drive closed"
            listViewTracks.Items.Clear()
        Else
            If cda.Open(comboBoxDrives.Text(0)) Then
                statusBar.Text = "CD drive opened"
                If cda.IsCdReady() Then
                    statusBar.Text += " and ready"
                    If cda.Refresh() Then
                        Dim info As List(Of CdInfo) = GetCdInfo(cda.GetCdQuery())
                        'List<CdInfo> info = cda.GetInfo();
                        Dim Tracks As Integer = cda.GetTrackCount()
                        For i As Integer = 0 To Tracks - 1
                            Dim title As String = IIf((info.Count = 0), "", info(0).Tracks(i).Title)
                            Dim item As New ListViewItem(New String() {(i + 1).ToString(), cda.TrackSize(i).ToString(), IIf(cda.IsAudioTrack(i), "audio", "data"), title})
                            listViewTracks.Items.Add(item)
                        Next
                    End If
                End If
                buttonOpen.Text = "Close"
            Else
                statusBar.Text = "CD drive could not be opened"
            End If
        End If
        progressBar1.Value = 0
        UpdateVisualControls()
    End Sub

    Private Sub listViewTracks_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles listViewTracks.SelectedIndexChanged
        UpdateVisualControls()
    End Sub

    Private Sub listViewTracks_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles listViewTracks.EnabledChanged
        'UpdateVisualControls()
    End Sub

    Private Sub buttonSaveAs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonSaveAs.Click
        If listViewTracks.SelectedIndices.Count > 0 Then
            Dim ndx As Integer = listViewTracks.SelectedIndices(0)
            Dim title As String = String.Format("track{0:00}", ndx + 1)
            saveFileDialog.FileName = String.Format("{0}.wav", title)
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                ripping = True
                Try
                    statusBar.Text = String.Format("Reading track {0}", ndx + 1)
                    UpdateVisualControls()
                    Dim format As IntPtr = CdDrive.GetFormat()
                    Dim ww As New WaveWriter(File.Create(saveFileDialog.FileName), AudioCompressionManager.FormatBytes(format))
                    Dim cr As CdReader = cda.GetReader(ndx)
                    Dim durationInMS As Integer = cr.GetDurationInMS()
                    Dim max As Integer = durationInMS / 1000
                    progressBar1.Minimum = 0
                    progressBar1.Value = 0
                    progressBar1.Maximum = max + 1
                    For i As Integer = 0 To max
                        Dim data As Byte() = cr.ReadData(i, 1)
                        ww.WriteData(data)
                        progressBar1.Value = i + 1
                        Application.DoEvents()
                    Next
                    cr.Close()
                    ww.Close()
                    DsConvert.ToWma(saveFileDialog.FileName, Convert.ToString(saveFileDialog.FileName) & ".wma", DsConvert.WmaProfile.Stereo128)
                Finally
                    ripping = False
                End Try
            End If
        End If
        UpdateVisualControls()
    End Sub

    Private Sub MainWindow_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        If ripping Then
            If MessageBox.Show("Are you to cancel?", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                Me.m_CancelRipping = True
            End If
            e.Cancel = True
        End If
    End Sub

End Class
