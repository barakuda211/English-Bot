Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports Alvas.Audio

Public Partial Class FormatDialog
		Public Sub New(ByVal isMp3 As Boolean)
			InitializeComponent()
			_isMp3 = isMp3
		End Sub

		Private _isMp3 As Boolean

		Private m_format As IntPtr
		Public ReadOnly Property Format() As IntPtr
			Get
				Return m_format
			End Get
		End Property

		Private Sub EnumFormatTags()
			Dim fdArr As FormatTagDetails() = AudioCompressionManager.GetFormatTagList()
			If Not _isMp3 Then
				cbFormatTag.DataSource = fdArr
			Else
				cbFormatTag.Enabled = False
				For i As Integer = 0 To fdArr.Length - 1
					Dim ftd As FormatTagDetails = fdArr(i)
					If ftd.FormatTag = AudioCompressionManager.MpegLayer3FormatTag Then
						cbFormatTag.DataSource = fdArr
						cbFormatTag.SelectedIndex = i
						Exit For
					End If
				Next
			End If
		End Sub

		Private Sub EnumFormats()
			Dim formatTag As Integer = DirectCast(cbFormatTag.SelectedValue, FormatTagDetails).FormatTag
			cbFormat.DataSource = AudioCompressionManager.GetFormatList(formatTag)
		End Sub

		Private Sub OpenNewDialog_Load(ByVal sender As Object, ByVal e As EventArgs)
			EnumFormatTags()
		End Sub

		Private Sub cbFormatTag_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
			EnumFormats()
		End Sub

		Private Sub cbFormat_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
			Dim pafd As FormatDetails = DirectCast(cbFormat.SelectedItem, FormatDetails)
			m_format = pafd.FormatHandle
		End Sub

		Private Sub btnOk_Click(ByVal sender As Object, ByVal e As EventArgs)

        If m_format = IntPtr.Zero Then
            DialogResult = Windows.Forms.DialogResult.None
        Else
            DialogResult = Windows.Forms.DialogResult.OK
        End If

    End Sub

	End Class
