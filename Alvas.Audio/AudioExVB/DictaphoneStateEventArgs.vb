Imports System
Imports System.Text

Namespace AudioExCS
	Public Class DictaphoneStateEventArgs
		Inherits EventArgs
		Public Sub New(ByVal state As DictaphoneState)
			_state = state
		End Sub

		Private _state As DictaphoneState
		Public ReadOnly Property State() As DictaphoneState
			Get
				Return _state
			End Get
		End Property

	End Class
End Namespace
