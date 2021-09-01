Imports System.Net
Imports captainalm.CALMNetMarshal
Imports System.Net.Sockets
Imports captainalm.Serialize
Imports System.IO

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

    Public Function loadSettings() As GlobalSettings
        Dim toret As New GlobalSettings()
        Try
            toret.load(File.ReadAllText(settingsStoreLoc))
        Catch ex As IOException
            toret = New GlobalSettings()
        End Try
        Return toret
    End Function

    Public Sub saveSettings(setIn As GlobalSettings)
        If setIn Is Nothing Then Return
        Try
            File.WriteAllText(settingsStoreLoc, setIn.save())
        Catch ex As IOException
        End Try
    End Sub

    Public Function loadContacts() As Contacts
        Dim toret As New Contacts()
        Try
            toret = Contacts.load(File.ReadAllText(contactStoreLoc))
            If toret Is Nothing Then toret = New Contacts()
        Catch ex As IOException
            toret = New Contacts()
        End Try
        Return toret
    End Function

    Public Sub saveContacts(csIn As Contacts)
        If csIn Is Nothing Then Return
        Try
            File.WriteAllText(contactStoreLoc, csIn.save())
        Catch ex As IOException
        End Try
    End Sub
End Module

Public Enum EditorMode As Integer
    None = 0
    Create = 1
    EditContact = 2
    EditClient = 3
    EditBlocker = 4
End Enum
