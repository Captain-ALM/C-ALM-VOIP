Public Class MClient
    Implements IMatcher(Of Client)

    Public ip As String
    Public port As Integer

    Public Sub New(ip As String, port As String)
        Me.ip = ip
        Me.port = port
    End Sub

    Public Function doesMatch(input As Client) As Boolean Implements IMatcher(Of Client).doesMatch
        If input.type = AddressableType.TCP Then
            Return input.marshal.duplicatedInternalSocketConfig.remoteIPAddress = ip And input.marshal.duplicatedInternalSocketConfig.remotePort = port
        Else
            Return input.targetAddress = ip And input.targetPort = port
        End If
    End Function
End Class
