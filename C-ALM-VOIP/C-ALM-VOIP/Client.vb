﻿Imports captainalm.CALMNetMarshal
Imports NAudio.Wave
Imports captainalm.CALMNetLib
Imports System.Net
Imports NAudio.Wave.SampleProviders

Public Class Client
    Inherits AddressableBase

    Protected _str As Streamer = Nothing
    Protected _cl As NetMarshalBase = Nothing
    Protected _m As Boolean = False

    Public Sub New(other As Contact, client As NetMarshalBase)
        MyBase.New(other)
        _str = spkVOIP.createStreamer(other.name)
        _cl = client
        AddHandler _cl.MessageReceived, AddressOf msgrec
        AddHandler micVOIP.streamer.dataExgest, AddressOf msgsnd
    End Sub

    Public Overrides Function duplicateToNew() As AddressableBase
        Return New Contact(Me)
    End Function

    Public Overridable Sub forceReceive(msg As IPacket)
        Me.msgrec(msg)
    End Sub

    Protected Overridable Sub msgrec(msg As IPacket)
        If _passmode = voip.MessagePassMode.Disable Or _passmode = voip.MessagePassMode.Send Then Exit Sub
        If isForMe(msg) And Not _m Then
            If msg.dataType = GetType(Byte()) Then
                _str.ingestData(msg.data, False)
            End If
        End If
    End Sub

    Protected Overridable Function isForMe(msg As IPacket) As Boolean
        If _type = AddressableType.TCP Then
            Return msg.senderIP = CType(_cl.internalSocket, INetConfig).remoteIPAddress And msg.senderPort = CType(_cl.internalSocket, INetConfig).remotePort
        ElseIf _type = AddressableType.UDP Then
            Return isResolveEqual(msg.senderIP, _targaddress) And msg.senderPort = _targport
        End If
        Return False
    End Function

    Protected Overridable Sub msgsnd(bts As Byte())
        If _passmode = voip.MessagePassMode.Disable Or _passmode = voip.MessagePassMode.Receive Then Exit Sub
        Dim ap As AudioPacket = Nothing
        If _type = AddressableType.TCP Then
            ap = New AudioPacket() With {.bytes = bts, .receiverIP = CType(_cl.internalSocket, INetConfig).remoteIPAddress, .receiverPort = CType(_cl.internalSocket, INetConfig).remotePort, .senderIP = CType(_cl.internalSocket, INetConfig).localIPAddress, .senderPort = CType(_cl.internalSocket, INetConfig).localPort}
        ElseIf _type = AddressableType.UDP Then
            ap = New AudioPacket() With {.bytes = bts, .receiverIP = _targaddress, .receiverPort = _targport, .senderIP = _myaddress, .senderPort = _myport}
        End If
        _cl.sendMessage(ap)
    End Sub

    Public Overridable ReadOnly Property connected As Boolean
        Get
            Return _cl.ready
        End Get
    End Property

    Public Overridable Sub [stop]()
        RemoveHandler _cl.MessageReceived, AddressOf msgrec
        RemoveHandler micVOIP.streamer.dataExgest, AddressOf msgsnd
        spkVOIP.removeProvider(_str.volumeprovider)
        If _type = AddressableType.TCP Then _
            _cl.close()
        _str.close()
        _str = Nothing
        _cl = Nothing
    End Sub

    Protected Overridable Function isResolveEqual(addr1 As String, addr2 As String) As Boolean
        Dim _1ip As IPAddress = Nothing
        Try
            _1ip = IPAddress.Parse(addr1)
        Catch ex As FormatException
            _1ip = Nothing
        Catch ex As ArgumentNullException
            _1ip = Nothing
        End Try
        Dim _2ip As IPAddress = Nothing
        Try
            _2ip = IPAddress.Parse(addr2)
        Catch ex As FormatException
            _2ip = Nothing
        Catch ex As ArgumentNullException
            _2ip = Nothing
        End Try
        If _1ip IsNot Nothing And _2ip IsNot Nothing Then
            Return _1ip.Equals(_2ip) And _2ip.Equals(_1ip)
        End If
        If _1ip IsNot Nothing Then
            Dim ipadd As IPAddress() = New IPAddress() {}
            Try
                ipadd = Dns.GetHostAddresses(addr2)
            Catch ex As Sockets.SocketException
                Return False
            Catch ex As ArgumentException
                Return False
            End Try
            If ipadd.Length < 1 Then Return False
            For Each ia As IPAddress In ipadd
                If ia.Equals(_1ip) And _1ip.Equals(ia) Then Return True
            Next
            Return False
        End If
        If _2ip IsNot Nothing Then
            Dim ipadd As IPAddress() = New IPAddress() {}
            Try
                ipadd = Dns.GetHostAddresses(addr1)
            Catch ex As Sockets.SocketException
                Return False
            Catch ex As ArgumentException
                Return False
            End Try
            If ipadd.Length < 1 Then Return False
            For Each ia As IPAddress In ipadd
                If ia.Equals(_2ip) And _2ip.Equals(ia) Then Return True
            Next
            Return False
        End If
        Return False
    End Function

    Public Overridable Property muted As Boolean
        Get
            Return _str.muted
        End Get
        Set(value As Boolean)
            _str.muted = value
        End Set
    End Property

    Public Overridable Property volume As Single
        Get
            Return _str.volume
        End Get
        Set(value As Single)
            _str.volume = value
        End Set
    End Property

    Public Overridable ReadOnly Property stream As Streamer
        Get
            Return _str
        End Get
    End Property

    Public Overridable ReadOnly Property marshal As NetMarshalBase
        Get
            Return _cl
        End Get
    End Property
End Class
