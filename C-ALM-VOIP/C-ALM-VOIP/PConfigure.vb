﻿Imports captainalm.workerpumper
Imports System.Net

Public Class PConfigure
    Implements IEventParser
    Private wp As WorkerPump = Nothing
    Private _input_device As Integer = -1
    Private _selected_interfaceIPv4 As IPAddress = IPAddress.None
    Private _selected_interfaceIPv6 As IPAddress = Nothing
    Private _port_UDP_IPv4 As Integer = 1
    Private _port_UDP_IPv6 As Integer = 1
    Private _port_TCP_IPv4 As Integer = 1
    Private _port_TCP_IPv6 As Integer = 1
    Private _external_UDP_Address_IPv4 As String = IPAddress.None.ToString()
    Private _external_UDP_Address_IPv6 As String = IPAddress.IPv6None.ToString()
    Private _external_UDP_Port_IPv4 As Integer = 1
    Private _external_UDP_Port_IPv6 As Integer = 1
    Private _TCP_backlog As Integer = 1
    Private _TCP_delay As Boolean = False
    Private _TCP_remove_disconnected_clients As Boolean = False

    Public Sub Parse(ev As WorkerEvent) Implements IEventParser.Parse
        If canCastObject(Of Configure)(ev.EventSource.sourceObj) Then
            Dim frm As Configure = castObject(Of Configure)(ev.EventSource.sourceObj)
            If ev.EventType = ETs.Shown Then
                _selected_interfaceIPv4 = selected_interfaceIPv4
                _selected_interfaceIPv6 = selected_interfaceIPv6
                _port_UDP_IPv4 = port_UDP_IPv4
                _port_UDP_IPv6 = port_UDP_IPv6
                _port_TCP_IPv4 = port_TCP_IPv4
                _port_TCP_IPv6 = port_TCP_IPv6
                _external_UDP_Address_IPv4 = external_UDP_Address_IPv4
                _external_UDP_Address_IPv6 = external_UDP_Address_IPv6
                _external_UDP_Port_IPv4 = external_UDP_Port_IPv4
                _external_UDP_Port_IPv6 = external_UDP_Port_IPv6
                _TCP_backlog = TCP_backlog
                _TCP_delay = TCP_delay
                _input_device = input_device
                _TCP_remove_disconnected_clients = TCP_remove_disconnected_clients
                configfin = False
            End If
        ElseIf ev.EventSource.parentObjs IsNot Nothing AndAlso ev.EventSource.parentObjs.Count > 0 Then
            Dim ra As Object = reverseArray(Of Object)(ev.EventSource.parentObjs.ToArray())
            If canCastObject(Of Configure)(ra(0)) Then
                Dim frm As Configure = castObject(Of Configure)(ra(0))
                Dim args As EventArgsDataContainer = castObject(Of EventArgsDataContainer)(ev.EventData)
                If ev.EventSource.sourceObj Is frm.butOK And ev.EventType = ETs.Click Then
                    selected_interfaceIPv4 = _selected_interfaceIPv4
                    selected_interfaceIPv6 = _selected_interfaceIPv6
                    port_UDP_IPv4 = _port_UDP_IPv4
                    port_UDP_IPv6 = _port_UDP_IPv6
                    port_TCP_IPv4 = _port_TCP_IPv4
                    port_TCP_IPv6 = _port_TCP_IPv6
                    external_UDP_Address_IPv4 = _external_UDP_Address_IPv4
                    external_UDP_Address_IPv6 = _external_UDP_Address_IPv6
                    external_UDP_Port_IPv4 = _external_UDP_Port_IPv4
                    external_UDP_Port_IPv6 = _external_UDP_Port_IPv6
                    TCP_backlog = _TCP_backlog
                    TCP_delay = _TCP_delay
                    input_device = _input_device
                    TCP_remove_disconnected_clients = _TCP_remove_disconnected_clients
                    configfin = True
                ElseIf ev.EventSource.sourceObj Is frm.butCANCEL And ev.EventType = ETs.Click Then
                    configfin = True
                ElseIf ev.EventSource.sourceObj Is frm.nudsptcpipv4 And ev.EventType = ETs.Leave Then
                    _port_TCP_IPv4 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudspudpipv4 And ev.EventType = ETs.Leave Then
                    _port_UDP_IPv4 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudsptcpipv6 And ev.EventType = ETs.Leave Then
                    _port_TCP_IPv6 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudspudpipv6 And ev.EventType = ETs.Leave Then
                    _port_UDP_IPv6 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudtcpbl And ev.EventType = ETs.Leave Then
                    _TCP_backlog = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nududpextpIPv4 And ev.EventType = ETs.Leave Then
                    _external_UDP_Port_IPv4 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nududpextpIPv6 And ev.EventType = ETs.Leave Then
                    _external_UDP_Port_IPv6 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.txtbxudpextaddIPv4 And ev.EventType = ETs.Leave Then
                    _external_UDP_Address_IPv4 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.txtbxudpextaddIPv6 And ev.EventType = ETs.Leave Then
                    _external_UDP_Address_IPv6 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.chkbxena And ev.EventType = ETs.Leave Then
                    _TCP_delay = args.held
                ElseIf ev.EventSource.sourceObj Is frm.chkbxrdtcpc And ev.EventType = ETs.Leave Then
                    _TCP_remove_disconnected_clients = args.held
                ElseIf ev.EventSource.sourceObj Is frm.cmbxsid And ev.EventType = ETs.Leave Then
                    _input_device = args.held
                ElseIf ev.EventSource.sourceObj Is frm.cmbxsniipv4 And ev.EventType = ETs.Leave Then
                    If args.held > -1 Then _selected_interfaceIPv4 = _IPv4Interfaces(args.held).Item2
                ElseIf ev.EventSource.sourceObj Is frm.cmbxsniipv6 And ev.EventType = ETs.Leave Then
                    If args.held > -1 Then _selected_interfaceIPv6 = _IPv6Interfaces(args.held).Item2
                End If
            End If
        End If
    End Sub

    Private Function reverseArray(Of t)(arr As t()) As t()
        Dim os As New Stack(Of t)(arr)
        Dim ol As New List(Of t)
        While os.Count > 0
            ol.Add(os.Pop())
        End While
        Return ol.ToArray
    End Function

    Private Function castObject(Of t)(f As Object) As t
        Try
            Dim nf As t = f
            Return nf
        Catch ex As InvalidCastException
            Return Nothing
        End Try
    End Function

    Private Function canCastObject(Of t)(f As Object) As Boolean
        Try
            Dim nf As t = f
            Return True
        Catch ex As InvalidCastException
            Return False
        End Try
    End Function

    Private Function getValueFromControl(Of t)(ctrl As Control, del As [Delegate]) As t
        Return ctrl.Invoke(del)
    End Function

    Public Property WorkerPump As WorkerPump Implements IWorkerPumpReceiver.WorkerPump
        Get
            Return wp
        End Get
        Set(value As WorkerPump)
            wp = value
        End Set
    End Property
End Class
