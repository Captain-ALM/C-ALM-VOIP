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
        ElseIf input.type = AddressableType.UDP Then
            Return input.targetAddress = ip And input.targetPort = port
        Else
            If input.targetAddress = ip Then
                If input.targetPort = 0 OrElse input.targetPort = port Then Return True
            End If
            Return False
        End If
    End Function
End Class
