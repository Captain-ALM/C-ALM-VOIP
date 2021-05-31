Imports captainalm.CALMNetMarshal
Imports captainalm.Serialize

<Serializable>
Public Structure AudioPacket
    Implements IPacket

    Private senderIP_ As String
    Private senderPort_ As Integer
    Private receiverIP_ As String
    Private receiverPort_ As Integer
    Public bytes As Byte()
    Public year As Integer
    Public day As Integer
    Public millisecond As Integer

    Public Property data As Object Implements IPacket.data
        Get
            Return New Tuple(Of Byte(), Integer, Integer, Integer)(bytes, year, day, millisecond)
        End Get
        Set(value As Object)
            Dim val As Tuple(Of Byte(), Integer, Integer, Integer) = value
            bytes = val.Item1
            year = val.Item2
            day = val.Item3
            millisecond = val.Item4
        End Set
    End Property

    Public ReadOnly Property dataType As Type Implements IPacket.dataType
        Get
            Return GetType(Tuple(Of Byte(), Integer, Integer, Integer))
        End Get
    End Property

    Public ReadOnly Property getData As Byte() Implements IPacket.getData
        Get
            Return New Serializer().serializeObject(Of AudioPacket)(Me)
        End Get
    End Property

    Public WriteOnly Property setData As Byte() Implements IPacket.setData
        Set(value As Byte())
            Dim msg As AudioPacket = New Serializer().deSerializeObject(Of AudioPacket)(value)
            Me.receiverIP_ = msg.receiverIP_
            Me.receiverPort_ = msg.receiverPort_
            Me.senderIP_ = msg.senderIP_
            Me.senderPort_ = msg.senderPort_
            Me.bytes = msg.bytes
            Me.year = msg.year
            Me.day = msg.day
            Me.millisecond = msg.millisecond
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