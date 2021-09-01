Public Class MAddressableBase
    Implements IMatcher(Of AddressableBase)

    Public ip As String
    Public port As Integer

    Public Sub New(ip As String, port As String)
        Me.ip = ip
        Me.port = port
    End Sub

    Public Overridable Function doesMatch(input As AddressableBase) As Boolean Implements IMatcher(Of AddressableBase).doesMatch
        Return input.targetAddress = ip And input.targetPort = port
    End Function
End Class
