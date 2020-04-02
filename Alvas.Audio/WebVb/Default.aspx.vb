Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO
Imports Alvas.Audio

Public Partial Class _Default
	Inherits System.Web.UI.Page
	Private Const AudioDir As String = "Audio\"
	Protected Sub Page_Load(sender As Object, e As EventArgs)
		If Not IsPostBack Then
			FillFileList()
		End If
	End Sub

	Private Sub FillFileList()
		lbFiles.Items.Clear()
		Dim dir As String = Server.MapPath(AudioDir)
		For Each file As String In Directory.GetFiles(dir)
			lbFiles.Items.Add(Path.GetFileName(file))
		Next
	End Sub

	Private Sub BindFormats(dataSource As Object)
		lbFormats.DataSource = dataSource
		lbFormats.DataBind()
	End Sub

	Private Sub FillFormatList(pathRelative As String)
		Dim fileName As String = Server.MapPath(pathRelative)
		Dim wr As IAudioReader = Nothing
		Dim ext As String = Path.GetExtension(fileName).ToLower()
		If ext <> ".wav" Then
			wr = New DsReader(fileName)
			If Not DirectCast(wr, DsReader).HasAudio Then
				ErrorText(pathRelative)
				Return
				'if (ext == ".mp3")
				'{ 
				'    wr = new Mp3Reader(File.OpenRead(fileName));
				'}
				'else
				'{
				'}
			End If
		Else
			wr = New WaveReader(File.OpenRead(fileName))
		End If
		Dim format As IntPtr
		Try
			'using (WaveReader wr = new WaveReader(File.OpenRead(fileName)))
			If True Then
				format = wr.ReadFormat()
				wr.Close()
			End If
		Catch
			ErrorText(pathRelative)
			Return
		End Try
		Dim fd As FormatDetails = AudioCompressionManager.GetFormatDetails(format)
		fd.ShowFormatTag = True
		' Set label's text
		'"Filename: " + Path.GetFileName(fileName) + "<br>" +
		lblInfo.Text = "Format: " & fd.ToString()
		lblInfo.Visible = True
		'FormatTagDetails[] ftdArr = AudioCompressionManager.GetFormatTagList();
		Dim fdArr As FormatDetails() = AudioCompressionManager.GetCompatibleFormatList(format)
		For i As Integer = 0 To fdArr.Length - 1
			fdArr(i).ShowFormatTag = True
		Next
		BindFormats(fdArr)
	End Sub

	Private Sub ErrorText(pathRelative As String)
		lblInfo.Text = String.Format("<font color=red>Oops! Please <a href='mailto:mail@alvas.net?Subject={0}'>contact us</a>. We will try to quickly resolve the problem.</font>", pathRelative)
		'"Filename: " + Path.GetFileName(fileName) + "<br>" +
		'"Is invalid";
		lblInfo.Visible = True
		BindFormats(New String(-1) {})
	End Sub

	Private Sub Convert()
		Dim fileName As String = Server.MapPath(AudioDir + lbFiles.SelectedValue)
		Try
			Dim wr As IAudioReader = Nothing
			If Path.GetExtension(fileName).ToLower() <> ".wav" Then
				wr = New DsReader(fileName)
			Else
				wr = New WaveReader(File.OpenRead(fileName))
			End If
			'FileStream ms = File.OpenRead(fileName);
			'WaveReader wr = new WaveReader(ms);

			Dim format As IntPtr = wr.ReadFormat()
			Dim ndx As Integer = lbFormats.SelectedIndex
			Dim formatPcm As IntPtr = AudioCompressionManager.GetCompatibleFormatList(format)(ndx).FormatHandle
			Dim dataPcm As Byte() = AudioCompressionManager.Convert(format, formatPcm, wr.ReadData(), False)
			Dim ww As New WaveWriter(File.Create(fileName & ".wav"), AudioCompressionManager.FormatBytes(formatPcm))
			ww.WriteData(dataPcm)
			ww.Close()
			wr.Close()
			lblResult.Text = String.Format("The '{0}' file is converted to '{0}.wav' file successfully.", lbFiles.SelectedValue)
		Catch
			lblResult.Text = "Converting error."
		End Try
		FillFileList()
	End Sub

	Private Sub WriteToFile(strPath As String, ByRef Buffer As Byte())
		Dim newFile As New FileStream(strPath, FileMode.Create)
		newFile.Write(Buffer, 0, Buffer.Length)
		newFile.Close()
	End Sub

	Protected Sub btnConvert_Click(sender As Object, e As EventArgs)
		Convert()
	End Sub

	Protected Sub lbFiles_SelectedIndexChanged(sender As Object, e As EventArgs)
		btnConvert.Enabled = False
		If lbFiles.SelectedIndex > -1 Then
			Dim pathRelative As String = AudioDir + lbFiles.SelectedItem.Text
			HyperLink1.NavigateUrl = pathRelative
			HyperLink1.Text = "Filename: " + lbFiles.SelectedItem.Text
			FillFormatList(pathRelative)
		End If
	End Sub

	Protected Sub cmdSend_Click(sender As Object, e As EventArgs)
		If filMyFile.PostedFile IsNot Nothing Then
			Dim myFile As HttpPostedFile = filMyFile.PostedFile

			Dim nFileLen As Integer = myFile.ContentLength

			If nFileLen > 0 Then
				Dim myData As Byte() = New Byte(nFileLen - 1) {}
				myFile.InputStream.Read(myData, 0, nFileLen)
				Dim strFilename As String = Path.GetFileName(myFile.FileName)

				Dim fileName As String = Server.MapPath(AudioDir & strFilename)
				WriteToFile(fileName, myData)
				FillFileList()
			End If
		End If
	End Sub

	Protected Sub lbFormats_SelectedIndexChanged(sender As Object, e As EventArgs)
		btnConvert.Enabled = lbFormats.SelectedIndex > -1
	End Sub
End Class
