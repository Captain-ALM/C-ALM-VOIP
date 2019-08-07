Imports System.Net
Imports captainalm.CALMNetMarshal
Imports System.Net.Sockets

Public Module [Global]
    Public description As String
    Public license As String
    Public selected_interfaceIPv4 As IPAddress = IPAddress.None
    Public selected_interfaceIPv6 As IPAddress = Nothing
    Public port_UDP_IPv4 As Integer = 1
    Public port_UDP_IPv6 As Integer = 1
    Public port_TCP_IPv4 As Integer = 1
    Public port_TCP_IPv6 As Integer = 1
    Public external_UDP_Address_IPv4 As String = IPAddress.None.ToString()
    Public external_UDP_Address_IPv6 As String = IPAddress.IPv6None.ToString()
    Public external_UDP_Port_IPv4 As Integer = 1
    Public external_UDP_Port_IPv6 As Integer = 1
    Public TCP_backlog As Integer = 1
    Public TCP_delay As Boolean = False
    Public input_device As Integer = -1
    Public TCP_remove_disconnected_clients As Boolean = False
    Public InListening As Boolean = False
    Public tcpmarshalIPv4 As NetMarshalTCP = Nothing
    Public tcpmarshalIPv6 As NetMarshalTCP = Nothing
    Public udpmarshalIPv4 As NetMarshalUDP = Nothing
    Public udpmarshalIPv6 As NetMarshalUDP = Nothing
    Public micVOIP As VOIPSender = Nothing
    Public spkVOIP As VOIPReceiver = Nothing
    Public nomReconReg As New Dictionary(Of Tuple(Of String, Integer), String)
    Public caddrbs As AddressableBase = Nothing
    Public ceditm As EditorMode = EditorMode.None

    Public Function resolve(addr As String, fam As AddressFamily) As IPAddress
        Dim ipadd As IPAddress() = New IPAddress() {}
        Try
            ipadd = Dns.GetHostAddresses(addr)
        Catch ex As Sockets.SocketException
            Return Nothing
        Catch ex As ArgumentException
            Return Nothing
        End Try
        For Each ia As IPAddress In ipadd
            If ia.AddressFamily = fam Then Return ia
        Next
        Return Nothing
    End Function
End Module

Public Enum EditorMode As Integer
    None = 0
    Create = 1
    EditContact = 2
    EditClient = 3
End Enum
