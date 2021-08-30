Imports captainalm.workerpumper
Imports System.Net
Imports captainalm.Serialize

Public Class PConfigure
    Implements IEventParser
    Private wp As WorkerPump = Nothing
    Private _settings As New GlobalSettings()

    Public Sub Parse(ev As WorkerEvent) Implements IEventParser.Parse
        If canCastObject(Of Configure)(ev.EventSource.sourceObj) Then
            Dim frm As Configure = castObject(Of Configure)(ev.EventSource.sourceObj)
            If ev.EventType = ETs.Shown Then
                _settings.selected_interfaceIPv4 = settings.selected_interfaceIPv4
                _settings.selected_interfaceIPv6 = settings.selected_interfaceIPv6
                _settings.port_UDP_IPv4 = settings.port_UDP_IPv4
                _settings.port_UDP_IPv6 = settings.port_UDP_IPv6
                _settings.port_TCP_IPv4 = settings.port_TCP_IPv4
                _settings.port_TCP_IPv6 = settings.port_TCP_IPv6
                _settings.external_Address_IPv4 = settings.external_Address_IPv4
                _settings.external_Address_IPv6 = settings.external_Address_IPv6
                _settings.external_UDP_Port_IPv4 = settings.external_UDP_Port_IPv4
                _settings.external_UDP_Port_IPv6 = settings.external_UDP_Port_IPv6
                _settings.external_TCP_Port_IPv4 = settings.external_TCP_Port_IPv4
                _settings.external_TCP_Port_IPv6 = settings.external_TCP_Port_IPv6
                _settings.TCP_backlog = settings.TCP_backlog
                _settings.TCP_delay = settings.TCP_delay
                _settings.input_device = settings.input_device
                _settings.TCP_remove_disconnected_clients = settings.TCP_remove_disconnected_clients
                _settings.TCP_beat_timeout = settings.TCP_beat_timeout
                _settings.gserializer = settings.gserializer
                _settings.samplerate = settings.samplerate
                _settings.buffmdmsecs = settings.buffmdmsecs
                _settings.myName = settings.myName
                _settings.setAdvertisedNames = settings.setAdvertisedNames
                configfin = False
            End If
        ElseIf ev.EventSource.parentObjs IsNot Nothing AndAlso ev.EventSource.parentObjs.Count > 0 Then
            Dim ra As Object = reverseArray(Of Object)(ev.EventSource.parentObjs.ToArray())
            If canCastObject(Of Configure)(ra(0)) Then
                Dim frm As Configure = castObject(Of Configure)(ra(0))
                Dim args As EventArgsDataContainer = castObject(Of EventArgsDataContainer)(ev.EventData)
                If ev.EventSource.sourceObj Is frm.butOK And ev.EventType = ETs.Click Then
                    settings.selected_interfaceIPv4 = _settings.selected_interfaceIPv4
                    settings.selected_interfaceIPv6 = _settings.selected_interfaceIPv6
                    settings.port_UDP_IPv4 = _settings.port_UDP_IPv4
                    settings.port_UDP_IPv6 = _settings.port_UDP_IPv6
                    settings.port_TCP_IPv4 = _settings.port_TCP_IPv4
                    settings.port_TCP_IPv6 = _settings.port_TCP_IPv6
                    settings.external_Address_IPv4 = _settings.external_Address_IPv4
                    settings.external_Address_IPv6 = _settings.external_Address_IPv6
                    settings.external_UDP_Port_IPv4 = _settings.external_UDP_Port_IPv4
                    settings.external_UDP_Port_IPv6 = _settings.external_UDP_Port_IPv6
                    settings.external_TCP_Port_IPv4 = _settings.external_TCP_Port_IPv4
                    settings.external_TCP_Port_IPv6 = _settings.external_TCP_Port_IPv6
                    settings.TCP_backlog = _settings.TCP_backlog
                    settings.TCP_delay = _settings.TCP_delay
                    settings.input_device = _settings.input_device
                    settings.TCP_remove_disconnected_clients = _settings.TCP_remove_disconnected_clients
                    settings.TCP_beat_timeout = _settings.TCP_beat_timeout
                    If Not InListening Then
                        If settings.gserializer Is Nothing Then
                            settings.gserializer = _settings.gserializer
                        Else
                            If Not Object.ReferenceEquals(settings.gserializer, _settings.gserializer) Then
                                Dim os As ISerialize = settings.gserializer
                                settings.gserializer = _settings.gserializer
                                os.Dispose()
                            End If
                        End If
                    Else
                        If settings.gserializer Is Nothing Then settings.gserializer = New XSerializer()
                    End If
                    settings.samplerate = _settings.samplerate
                    settings.buffmdmsecs = _settings.buffmdmsecs
                    settings.myName = _settings.myName
                    settings.setAdvertisedNames = _settings.setAdvertisedNames
                    configfin = True
                ElseIf ev.EventSource.sourceObj Is frm.butCANCEL And ev.EventType = ETs.Click Then
                    configfin = True
                ElseIf ev.EventSource.sourceObj Is frm.nudsptcpipv4 And ev.EventType = ETs.Leave Then
                    _settings.port_TCP_IPv4 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudspudpipv4 And ev.EventType = ETs.Leave Then
                    _settings.port_UDP_IPv4 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudsptcpipv6 And ev.EventType = ETs.Leave Then
                    _settings.port_TCP_IPv6 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudspudpipv6 And ev.EventType = ETs.Leave Then
                    _settings.port_UDP_IPv6 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudtcpbl And ev.EventType = ETs.Leave Then
                    _settings.TCP_backlog = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nududpextpIPv4 And ev.EventType = ETs.Leave Then
                    _settings.external_UDP_Port_IPv4 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nududpextpIPv6 And ev.EventType = ETs.Leave Then
                    _settings.external_UDP_Port_IPv6 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudtcpextpIPv4 And ev.EventType = ETs.Leave Then
                    _settings.external_TCP_Port_IPv4 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudtcpextpIPv6 And ev.EventType = ETs.Leave Then
                    _settings.external_TCP_Port_IPv6 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.txtbxextaddIPv4 And ev.EventType = ETs.Leave Then
                    _settings.external_Address_IPv4 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.txtbxextaddIPv6 And ev.EventType = ETs.Leave Then
                    _settings.external_Address_IPv6 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.chkbxena And ev.EventType = ETs.Leave Then
                    _settings.TCP_delay = args.held
                ElseIf ev.EventSource.sourceObj Is frm.chkbxrdtcpc And ev.EventType = ETs.Leave Then
                    _settings.TCP_remove_disconnected_clients = args.held
                ElseIf ev.EventSource.sourceObj Is frm.cmbxsid And ev.EventType = ETs.Leave Then
                    _settings.input_device = args.held
                ElseIf ev.EventSource.sourceObj Is frm.cmbxsniipv4 And ev.EventType = ETs.Leave Then
                    If args.held > -1 Then _settings.selected_interfaceIPv4 = _IPv4Interfaces(args.held).Item2
                ElseIf ev.EventSource.sourceObj Is frm.cmbxsniipv6 And ev.EventType = ETs.Leave Then
                    If args.held > -1 Then _settings.selected_interfaceIPv6 = _IPv6Interfaces(args.held).Item2
                ElseIf ev.EventSource.sourceObj Is frm.cmbxis And ev.EventType = ETs.Leave Then
                    If Not InListening Then
                        If args.held = 0 Then
                            If _settings.gserializer IsNot Nothing Then _settings.gserializer.Dispose()
                            _settings.gserializer = New XSerializer()
                        ElseIf args.held = 1 Then
                            If _settings.gserializer IsNot Nothing Then _settings.gserializer.Dispose()
                            _settings.gserializer = New Serializer()
                        End If
                    End If
                ElseIf ev.EventSource.sourceObj Is frm.txtbxcnom And ev.EventType = ETs.Leave Then
                    _settings.myName = args.held
                ElseIf ev.EventSource.sourceObj Is frm.chkbxsan And ev.EventType = ETs.Leave Then
                    _settings.setAdvertisedNames = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudtcpto And ev.EventType = ETs.Leave Then
                    _settings.TCP_beat_timeout = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudsr And ev.EventType = ETs.Leave Then
                    _settings.samplerate = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudrb And ev.EventType = ETs.Leave Then
                    _settings.buffmdmsecs = args.held
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
        Return GetType(t).IsAssignableFrom(f.GetType())
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
