Imports System.Net
Imports captainalm.CALMNetMarshal
Imports System.Net.Sockets
Imports captainalm.Serialize

Public Module [Global]
    'Global Information
    Public description As String
    Public license As String
    'Global Options
    Public selected_interfaceIPv4 As IPAddress = IPAddress.None
    Public selected_interfaceIPv6 As IPAddress = Nothing
    Public _IPv4Interfaces As New SyncLockedList(Of Tuple(Of String, IPAddress))
    Public _IPv6Interfaces As New SyncLockedList(Of Tuple(Of String, IPAddress))
    Public port_UDP_IPv4 As Integer = 0
    Public port_UDP_IPv6 As Integer = 0
    Public port_TCP_IPv4 As Integer = 0
    Public port_TCP_IPv6 As Integer = 0
    Public external_Address_IPv4 As String = IPAddress.None.ToString()
    Public external_Address_IPv6 As String = IPAddress.IPv6None.ToString()
    Public external_UDP_Port_IPv4 As Integer = 0
    Public external_UDP_Port_IPv6 As Integer = 0
    Public external_TCP_Port_IPv4 As Integer = 0
    Public external_TCP_Port_IPv6 As Integer = 0
    Public TCP_backlog As Integer = 1
    Public TCP_delay As Boolean = False
    Public input_device As Integer = -1
    Public TCP_remove_disconnected_clients As Boolean = False
    Public TCP_beat_timeout As Integer = 0
    Public gserializer As ISerialize = New XSerializer()
    Public samplerate As Integer = 12000
    Public buffmdmsecs As Integer = 125
    Public myName As String = ""
    Public setAdvertisedNames As Boolean = True
    'Global marshals, VOIP units and status
    Public InListening As Boolean = False
    Public tcpmarshalIPv4 As NetMarshalTCP = Nothing
    Public tcpmarshalIPv6 As NetMarshalTCP = Nothing
    Public udpmarshalIPv4 As NetMarshalUDP = Nothing
    Public udpmarshalIPv6 As NetMarshalUDP = Nothing
    Public micVOIP As VOIPSender = Nothing
    Public spkVOIP As VOIPReceiver = Nothing
    Public tcpResvSetReg As New SyncLockedList(Of Tuple(Of String, Integer, String, voip.MessagePassMode))
    'Editor interface storage
    Public caddrbs As AddressableBase = Nothing
    Public ceditm As EditorMode = EditorMode.None
    Public editsuccess As Boolean = False
    Public editfin As Boolean = True
    'Listview backed lists
    Public clients As New SyncLockedList(Of Client)
    Public contacts As New SyncLockedList(Of Contact)
    Public streams As New SyncLockedList(Of Streamer)
    Public configfin As Boolean = True

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
    EditBlocker = 4
End Enum
