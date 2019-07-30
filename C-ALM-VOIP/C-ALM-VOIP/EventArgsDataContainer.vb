Public Class EventArgsDataContainer
    Inherits EventArgs

    Private _held As Object

    Public Sub New(heldObject As Object)
        _held = heldObject
    End Sub

    Public ReadOnly Property held As Object
        Get
            Return _held
        End Get
    End Property

    Public ReadOnly Property heldType As Type
        Get
            If _held Is Nothing Then Return GetType(Void)
            Return _held.GetType()
        End Get
    End Property
End Class
