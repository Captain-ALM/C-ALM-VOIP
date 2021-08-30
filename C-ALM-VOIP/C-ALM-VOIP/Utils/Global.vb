Imports System.Net
Imports captainalm.CALMNetMarshal
Imports System.Net.Sockets
Imports captainalm.Serialize

Public Module [Global]
    'Global Information
    Public description As String
    Public license As String
    'Global IP Interface Cache
    Public _IPv4Interfaces As New SyncLockedList(Of Tuple(Of String, IPAddress))
    Public _IPv6Interfaces As New SyncLockedList(Of Tuple(Of String, IPAddress))
    'Global Storage Serializer
    Public sserializer As ISerialize = New XSerializer()
    'Global Options
    Public settings As GlobalSettings = New GlobalSettings()
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
    'ListViewedRegistry
    Public clientreg As ListViewedRegistry(Of Client)
    Public contactreg As ListViewedRegistry(Of Contact)
    Public streamreg As ListViewedRegistry(Of Streamer)
    'Config interface storage
    Public configfin As Boolean = True

    Public Function resolve(addr As String, fam As AddressFamily) As IPAddress()
        Dim toret As New List(Of IPAddress)
        Dim ipadd As IPAddress() = New IPAddress() {}
        Try
            ipadd = Dns.GetHostAddresses(addr)
        Catch ex As Sockets.SocketException
            Return Nothing
        Catch ex As ArgumentException
            Return Nothing
        End Try
        For Each ia As IPAddress In ipadd
            If ia.AddressFamily = fam Then toret.Add(ia)
        Next
        Return toret.ToArray()
    End Function

    Public Function returnFirstItemOrNothing(Of t)(input As t()) As t
        If input Is Nothing OrElse input.Length < 1 Then Return Nothing Else Return input(0)
    End Function
End Module

Public Enum EditorMode As Integer
    None = 0
    Create = 1
    EditContact = 2
    EditClient = 3
    EditBlocker = 4
End Enum
