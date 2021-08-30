Public Class MClient
    Inherits MAddressableBase
    Implements IMatcher(Of Client)

    Public Sub New(ip As String, port As String)
        MyBase.New(ip, port)
    End Sub

    Public Overrides Function doesMatch(input As AddressableBase) As Boolean
        Return doesMatchClient(input)
    End Function

    Public Function doesMatchClient(input As Client) As Boolean Implements IMatcher(Of Client).doesMatch
        If Input.type = AddressableType.TCP Then
            Return Input.marshal.duplicatedInternalSocketConfig.remoteIPAddress = ip And Input.marshal.duplicatedInternalSocketConfig.remotePort = port
        ElseIf Input.type = AddressableType.UDP Then
            Return MyBase.doesMatch(input)
        Else
            If Input.targetAddress = ip Then
                If Input.targetPort = 0 OrElse Input.targetPort = port Then Return True
            End If
            Return False
        End If
    End Function
End Class
