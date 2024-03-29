﻿Imports captainalm.CALMNetMarshal
Imports captainalm.Serialize
Imports System.Xml.Serialization

<Serializable>
Public Structure AudioPacket
    Implements IPacket

    Private senderIP_ As String
    Private senderPort_ As Integer
    Private receiverIP_ As String
    Private receiverPort_ As Integer
    Public bytes As Byte()
    Public timestamp As DateTime
    <NonSerialized, XmlIgnore>
    Public serializer As ISerialize
    <XmlIgnore>
    Public Property data As Object Implements IPacket.data
        Get
            Return New Tuple(Of Byte(), DateTime)(bytes, timestamp)
        End Get
        Set(value As Object)
            Dim val As Tuple(Of Byte(), DateTime) = value
            bytes = val.Item1
            timestamp = val.Item2
        End Set
    End Property

    Public ReadOnly Property dataType As Type Implements IPacket.dataType
        Get
            Return GetType(Tuple(Of Byte(), DateTime))
        End Get
    End Property

    Public ReadOnly Property getData As Byte() Implements IPacket.getData
        Get
            If serializer Is Nothing Then serializer = settings.gserializer
            Return serializer.serializeObject(Of AudioPacket)(Me)
        End Get
    End Property

    Public WriteOnly Property setData As Byte() Implements IPacket.setData
        Set(value As Byte())
            If serializer Is Nothing Then serializer = settings.gserializer
            Dim msg As AudioPacket = serializer.deSerializeObject(Of AudioPacket)(value)
            Me.receiverIP_ = msg.receiverIP_
            Me.receiverPort_ = msg.receiverPort_
            Me.senderIP_ = msg.senderIP_
            Me.senderPort_ = msg.senderPort_
            Me.bytes = msg.bytes
            Me.timestamp = msg.timestamp
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