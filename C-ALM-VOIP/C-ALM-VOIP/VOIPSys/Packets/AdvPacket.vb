Imports captainalm.CALMNetMarshal
Imports captainalm.Serialize
Imports System.Xml.Serialization

<Serializable>
Public Structure AdvPacket
    Implements IPacket

    Private senderIP_ As String
    Private senderPort_ As Integer
    Private receiverIP_ As String
    Private receiverPort_ As Integer
    <XmlIgnore>
    Public advName As String
    Public Property advNameSafe As String
        Get
            If advName Is Nothing Then Return ""
            Return advName
        End Get
        Set(value As String)
            advName = value
        End Set
    End Property
    <XmlIgnore>
    Public advIP As String
    Public Property advIPSafe As String
        Get
            If advIP Is Nothing Then Return ""
            Return advIP
        End Get
        Set(value As String)
            advIP = value
        End Set
    End Property
    Public advPort As Integer
    <NonSerialized, XmlIgnore>
    Public serializer As ISerialize
    <XmlIgnore>
    Public Property data As Object Implements IPacket.data
        Get
            Return New Tuple(Of String, String, Integer)(advName, advIP, advPort)
        End Get
        Set(value As Object)
            Dim val As Tuple(Of String, String, Integer) = value
            advName = val.Item1
            advIP = val.Item2
            advPort = val.Item3
        End Set
    End Property

    Public ReadOnly Property dataType As Type Implements IPacket.dataType
        Get
            Return GetType(Tuple(Of String, String, Integer))
        End Get
    End Property

    Public ReadOnly Property getData As Byte() Implements IPacket.getData
        Get
            If serializer Is Nothing Then serializer = settings.gserializer
            Return serializer.serializeObject(Of AdvPacket)(Me)
        End Get
    End Property

    Public WriteOnly Property setData As Byte() Implements IPacket.setData
        Set(value As Byte())
            If serializer Is Nothing Then serializer = settings.gserializer
            Dim msg As AdvPacket = serializer.deSerializeObject(Of AdvPacket)(value)
            Me.receiverIP_ = msg.receiverIP_
            Me.receiverPort_ = msg.receiverPort_
            Me.senderIP_ = msg.senderIP_
            Me.senderPort_ = msg.senderPort_
            Me.advName = msg.advName
            Me.advIP = msg.advIP
            Me.advPort = msg.advPort
            msg = Nothing
        End Set
    End Property

    Public Property receiverIP As String Implements IPacket.receiverIP
        Get
            Return receiverIP_
        End Get
        Set(value As String)
            receiverIP_ = value
        End Set
    End Property

    Public Property receiverPort As Integer Implements IPacket.receiverPort
        Get
            Return receiverPort_
        End Get
        Set(value As Integer)
            receiverPort_ = value
        End Set
    End Property

    Public Property senderIP As String Implements IPacket.senderIP
        Get
            Return senderIP_
        End Get
        Set(value As String)
            senderIP_ = value
        End Set
    End Property

    Public Property senderPort As Integer Implements IPacket.senderPort
        Get
            Return senderPort_
        End Get
        Set(value As Integer)
            senderPort_ = value
        End Set
    End Property
End Structure
