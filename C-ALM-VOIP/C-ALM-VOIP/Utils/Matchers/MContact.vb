Public Class MContact
    Inherits MAddressableBase
    Implements IMatcher(Of Contact)

    Public type As AddressableType

    Public Sub New(ip As String, port As String, type As AddressableType)
        MyBase.New(ip, port)
        Me.type = type
    End Sub

    Public Function doesMatchContact(input As Contact) As Boolean Implements IMatcher(Of Contact).doesMatch
        Return MyBase.doesMatch(input) And input.type = type
    End Function
End Class
