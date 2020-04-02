Imports System
Imports System.Text

Namespace AudioExCS
	Public Class PositionEventArgs
		Inherits EventArgs
		Public Sub New(ByVal position As Long)
			_position = position
		End Sub

		Private _position As Long
		Public ReadOnly Property Position() As Long
			Get
				Return _position
			End Get
		End Property

	End Class
End Namespace
