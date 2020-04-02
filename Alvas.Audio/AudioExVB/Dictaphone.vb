Imports System
Imports System.Text
Imports Alvas.Audio
Imports System.IO

Namespace AudioExCS
	Public Class Dictaphone
		Private recEx As Alvas.Audio.RecorderEx
		Private playEx As PlayerEx

		Public Sub New()
			Me.recEx = New Alvas.Audio.RecorderEx()
			Me.playEx = New Alvas.Audio.PlayerEx()
			AddHandler Me.recEx.Close, AddressOf CloseRecorder
			AddHandler Me.recEx.Data, AddressOf DataRecorder
			AddHandler Me.recEx.Open, AddressOf OpenRecorder
			AddHandler Me.playEx.Close, AddressOf playerEx_Close
			AddHandler Me.playEx.Open, AddressOf playerEx_Open
				' 
				' recEx
				' 
				' 
				' playEx
				' 
			AddHandler Me.playEx.Done, AddressOf playerEx_Done
		End Sub

		Public Property RecorderID() As Integer
			Get
				Return recEx.RecorderID
			End Get
			Set
				recEx.RecorderID = value
			End Set
		End Property

		Public Property Format() As IntPtr
			Get
				Return recEx.Format
			End Get
			Set
				recEx.Format = value
			End Set
		End Property

		Private wr As WaveReader = Nothing
		Private ww As WaveWriter = Nothing
		Private _stream As Stream = Nothing

		Public Sub OpenRecorder(ByVal sender As Object, ByVal e As EventArgs)
            If Not (ww Is Nothing) Then
                ww.Close()
            End If
			ww = New WaveWriter(_stream, recEx.FormatBytes())
			OnChangeState(DictaphoneState.Record)
		End Sub

		Public Sub CloseRecorder(ByVal sender As Object, ByVal e As EventArgs)
			vum.Data = Nothing
			OnChangeState(DictaphoneState.Initial)
		End Sub

		Public Sub DataRecorder(ByVal sender As Object, ByVal e As DataEventArgs)
			Dim data As Byte() = e.Data
			ww.WriteData(data)
			Dim pos As Long = recEx.GetPosition(TimeFormat.Milliseconds)
			OnChangePosition(pos)
			Dim buffer As Short() = AudioCompressionManager.RecalculateData(recEx.Format, data, vum.ClientRectangle.Width)
			vum.Data = buffer
		End Sub

		Private Sub playerEx_Done(ByVal sender As Object, ByVal e As DoneEventArgs)
			Dim data As Byte() = e.Data
			Dim pos As Long = playEx.GetPosition(TimeFormat.Milliseconds)
			OnChangePosition(pos)
			Dim buffer As Short() = AudioCompressionManager.RecalculateData(playEx.Format, e.Data, vum.ClientRectangle.Width)
			vum.Data = buffer
			'------------
			If e.IsEndPlaying AndAlso playEx.State <> DeviceState.Closed Then
				playEx.ClosePlayer()
			End If
			'------------
		End Sub

		Public Delegate Sub ChangePositionEventHandler(ByVal sender As Object, ByVal e As PositionEventArgs)

		Public Event ChangePosition As ChangePositionEventHandler

		Protected Overridable Sub OnChangePosition(ByVal pos As Long)
			RaiseEvent ChangePosition(Me, New PositionEventArgs(pos))
		End Sub

		Private Sub playerEx_Close(ByVal sender As Object, ByVal e As EventArgs)
			vum.Data = Nothing
			OnChangeState(DictaphoneState.Initial)
		End Sub

		Private Sub playerEx_Open(ByVal sender As Object, ByVal e As EventArgs)
			OnChangeState(DictaphoneState.Play)
		End Sub

		Public Sub StartRecord(ByVal inMemory As Boolean, ByVal fileName As String)
			If recEx.State = DeviceState.Closed Then
				Dim stream As Stream
				If inMemory Then
					stream = New MemoryStream()
				Else
					stream = New FileStream(fileName, FileMode.Create)
				End If
				_stream = stream
			End If
            Try
                recEx.StartRecord()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
		End Sub

		Public Sub StopRecord()
			recEx.StopRecord()
		End Sub

		Public Sub PauseRecord()
			recEx.PauseRecord()
			OnChangeState(DictaphoneState.PauseRecord)
		End Sub

		Public Sub ClosePlayer()
			playEx.ClosePlayer()
		End Sub

		Public Sub PausePlay()
			playEx.PausePlay()
			OnChangeState(DictaphoneState.PausePlay)
		End Sub

		Private vum As SoundLevelMeter

		Public Property SoundLevelMeter() As SoundLevelMeter
			Get
				Return vum
			End Get
			Set
				vum = value
			End Set
		End Property


		Public Sub StartPlay()
			If playEx.State = DeviceState.Paused Then
				playEx.ResumePlay()
			Else
				wr = New WaveReader(_stream)
				Dim format As IntPtr = wr.ReadFormat()
				playEx.OpenPlayer(format)
				Dim data As Byte() = wr.ReadData()
				playEx.AddData(data)
				playEx.StartPlay()
			End If
		End Sub

		Public Delegate Sub ChangeStateEventHandler(ByVal sender As Object, ByVal e As DictaphoneStateEventArgs)

		Public Event ChangeState As ChangeStateEventHandler

		Protected Overridable Sub OnChangeState(ByVal state As DictaphoneState)
			RaiseEvent ChangeState(Me, New DictaphoneStateEventArgs(state))
		End Sub

		Public Sub SetVolume(ByVal leftVolume As Integer, ByVal rightVolume As Integer)
			playEx.SetVolume(leftVolume, rightVolume)
		End Sub

		Public Sub GetVolume(ByRef leftVolume As Integer, ByRef rightVolume As Integer)
			playEx.GetVolume(leftVolume, rightVolume)
		End Sub

	End Class
End Namespace
