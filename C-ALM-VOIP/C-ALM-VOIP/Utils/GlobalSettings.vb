Imports System.Xml.Serialization
Imports System.Net
Imports captainalm.Serialize

<Serializable>
Public Class GlobalSettings
    <XmlIgnore>
    Public selected_interfaceIPv4 As IPAddress = IPAddress.None
    Public Property selectedInterfaceIPv4 As String
        Get
            If selected_interfaceIPv4 Is Nothing Then Return ""
            Return selected_interfaceIPv4.ToString()
        End Get
        Set(value As String)
            If value Is Nothing OrElse value = "" Then selected_interfaceIPv4 = Nothing Else selected_interfaceIPv4 = IPAddress.Parse(value)
        End Set
    End Property
    <XmlIgnore>
    Public selected_interfaceIPv6 As IPAddress = Nothing
    Public Property selectedInterfaceIPv6 As String
        Get
            If selected_interfaceIPv6 Is Nothing Then Return ""
            Return selected_interfaceIPv6.ToString()
        End Get
        Set(value As String)
            If value Is Nothing OrElse value = "" Then selected_interfaceIPv6 = Nothing Else selected_interfaceIPv6 = IPAddress.Parse(value)
        End Set
    End Property
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
    <XmlIgnore>
    Public gserializer As ISerialize = New XSerializer()
    Public Property serializer As Integer
        Get
            If gserializer Is Nothing Then Return 0
            If TypeOf gserializer Is Serializer Then Return 1
            Return 0
        End Get
        Set(value As Integer)
            If value = 1 Then
                gserializer = New Serializer()
            Else
                gserializer = New XSerializer()
            End If
        End Set
    End Property
    Public samplerate As Integer = 12000
    Public buffmdmsecs As Integer = 125
    Public myName As String = ""
    Public setAdvertisedNames As Boolean = True

    Private Sub setupGS()
        If Me.gserializer Is Nothing Then Me.gserializer = New XSerializer()
    End Sub

    Public Sub load(data As String)
        setupGS()
        Dim setting As GlobalSettings = settings.gserializer.deSerialize(Of GlobalSettings)(data)
        If Not setting Is Nothing Then takeACopy(setting)
        setting = Nothing
    End Sub

    Public Sub load(toDuplicate As GlobalSettings)
        If toDuplicate Is Nothing Then Return
        takeACopy(toDuplicate)
    End Sub

    Private Sub takeACopy(setting As GlobalSettings)
        Me.selected_interfaceIPv4 = setting.selected_interfaceIPv4
        Me.selected_interfaceIPv6 = setting.selected_interfaceIPv6
        Me.port_UDP_IPv4 = setting.port_UDP_IPv4
        Me.port_UDP_IPv6 = setting.port_UDP_IPv6
        Me.port_TCP_IPv4 = setting.port_TCP_IPv4
        Me.port_TCP_IPv6 = setting.port_TCP_IPv6
        Me.external_Address_IPv4 = setting.external_Address_IPv4
        Me.external_Address_IPv6 = setting.external_Address_IPv6
        Me.external_UDP_Port_IPv4 = setting.external_UDP_Port_IPv4
        Me.external_UDP_Port_IPv6 = setting.external_UDP_Port_IPv6
        Me.external_TCP_Port_IPv4 = setting.external_TCP_Port_IPv4
        Me.external_TCP_Port_IPv6 = setting.external_TCP_Port_IPv6
        Me.TCP_backlog = setting.TCP_backlog
        Me.TCP_delay = setting.TCP_delay
        Me.input_device = setting.input_device
        Me.TCP_remove_disconnected_clients = setting.TCP_remove_disconnected_clients
        Me.TCP_beat_timeout = setting.TCP_beat_timeout
        Me.gserializer = setting.gserializer
        Me.samplerate = setting.samplerate
        Me.buffmdmsecs = setting.buffmdmsecs
        Me.myName = setting.myName
        Me.setAdvertisedNames = setting.setAdvertisedNames
    End Sub

    Public Function save() As String
        setupGS()
        Return settings.gserializer.serialize(Of GlobalSettings)(Me)
    End Function
End Class
