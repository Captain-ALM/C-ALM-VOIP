Imports captainalm.workerpumper
Imports System.Net
Imports captainalm.CALMNetLib
Imports NAudio.Wave

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
    Private _IPv4Interfaces As New List(Of Tuple(Of String, IPAddress))
    Private _IPv6Interfaces As New List(Of Tuple(Of String, IPAddress))

    Public Function Parse(ev As WorkerEvent) As Boolean Implements IEventParser.Parse
        Dim toret As Boolean = True
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
                _IPv4Interfaces.Clear()
                frm.Invoke(Sub() frm.butOK.Select())
                Dim lstifan As New List(Of Tuple(Of String, IPAddress))
                lstifan.Add(New Tuple(Of String, IPAddress)("<None>", IPAddress.None))
                lstifan.Add(New Tuple(Of String, IPAddress)("<None>", Nothing))
                lstifan.AddRange(Utilities.GetIPInterfacesAndNames())
                lstifan.Add(New Tuple(Of String, IPAddress)("<All>", IPAddress.Any))
                lstifan.Add(New Tuple(Of String, IPAddress)("<All>", IPAddress.IPv6Any))
                frm.Invoke(Sub() frm.cmbxsniipv4.Items.Clear())
                For Each c As Tuple(Of String, IPAddress) In lstifan
                    If c.Item2 Is Nothing Then Continue For
                    If c.Item2.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                        _IPv4Interfaces.Add(c)
                        frm.Invoke(Sub() frm.cmbxsniipv4.Items.Add(c.Item1 & " - " & c.Item2.ToString()))
                    End If
                Next
                Dim ioi4 As Integer = indexOfIP(_selected_interfaceIPv4, _IPv4Interfaces)
                frm.Invoke(Sub() frm.cmbxsniipv4.SelectedIndex = ioi4)
                _IPv6Interfaces.Clear()
                frm.Invoke(Sub() frm.cmbxsniipv6.Items.Clear())
                For Each c As Tuple(Of String, IPAddress) In lstifan
                    If c.Item2 Is Nothing Then
                        _IPv6Interfaces.Add(c)
                        frm.Invoke(Sub() frm.cmbxsniipv6.Items.Add(c.Item1 & " - Invalid"))
                        Continue For
                    End If
                    If c.Item2.AddressFamily = Sockets.AddressFamily.InterNetworkV6 Then
                        _IPv6Interfaces.Add(c)
                        frm.Invoke(Sub() frm.cmbxsniipv6.Items.Add(c.Item1 & " - " & c.Item2.ToString()))
                    End If
                Next
                Dim ioi6 As Integer = indexOfIP(_selected_interfaceIPv6, _IPv6Interfaces)
                frm.Invoke(Sub() frm.cmbxsniipv6.SelectedIndex = ioi6)
                frm.Invoke(Sub() frm.cmbxsid.Items.Clear())
                For Each c As WaveInCapabilities In waveInDevices()
                    frm.Invoke(Sub() frm.cmbxsid.Items.Add(c.ProductName))
                Next
                If _input_device > getValueFromControl(Of Integer)(frm.cmbxsid, Function() As Integer
                                                                                    Return frm.cmbxsid.Items.Count - 1
                                                                                End Function) Then
                    _input_device = -1
                End If
                frm.Invoke(Sub() frm.cmbxsid.SelectedIndex = _input_device)
                frm.Invoke(Sub()
                               frm.nudsptcpipv4.Value = _port_TCP_IPv4
                               frm.nudsptcpipv6.Value = _port_TCP_IPv6
                               frm.nudspudpipv4.Value = _port_UDP_IPv4
                               frm.nudspudpipv6.Value = _port_UDP_IPv6
                               frm.nudtcpbl.Value = _TCP_backlog
                               frm.nududpextpIPv4.Value = _external_UDP_Port_IPv4
                               frm.nududpextpIPv6.Value = _external_UDP_Port_IPv6
                               frm.chkbxena.Checked = _TCP_delay
                               frm.txtbxudpextaddIPv4.Text = _external_UDP_Address_IPv4
                               frm.txtbxudpextaddIPv6.Text = _external_UDP_Address_IPv6
                               frm.chkbxrdtcpc.Checked = _TCP_remove_disconnected_clients
                               If InListening Then
                                   frm.cmbxsniipv4.Enabled = False
                                   frm.nudsptcpipv4.Enabled = False
                                   frm.nudspudpipv4.Enabled = False
                                   frm.cmbxsniipv6.Enabled = False
                                   frm.nudsptcpipv6.Enabled = False
                                   frm.nudspudpipv6.Enabled = False
                                   frm.cmbxsid.Enabled = False
                                   frm.nudtcpbl.Enabled = False
                                   frm.chkbxena.Enabled = False
                               Else
                                   frm.cmbxsniipv4.Enabled = True
                                   frm.nudsptcpipv4.Enabled = True
                                   frm.nudspudpipv4.Enabled = True
                                   frm.cmbxsniipv6.Enabled = True
                                   frm.nudsptcpipv6.Enabled = True
                                   frm.nudspudpipv6.Enabled = True
                                   frm.cmbxsid.Enabled = True
                                   frm.nudtcpbl.Enabled = True
                                   frm.chkbxena.Enabled = True
                               End If
                               frm.butOK.Select()
                           End Sub)
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
                    frm.Invoke(Sub()
                                   frm.DialogResult = DialogResult.OK
                                   frm.Close()
                               End Sub)
                ElseIf ev.EventSource.sourceObj Is frm.nudsptcpipv4 And ev.EventType = ETs.Leave Then
                    _port_TCP_IPv4 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudspudpipv4 And ev.EventType = ETs.Leave Then
                    _port_UDP_IPv4 =args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudsptcpipv6 And ev.EventType = ETs.Leave Then
                    _port_TCP_IPv6 =args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudspudpipv6 And ev.EventType = ETs.Leave Then
                    _port_UDP_IPv6 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nudtcpbl And ev.EventType = ETs.Leave Then
                    _TCP_backlog = args.held
                ElseIf ev.EventSource.sourceObj Is frm.nududpextpIPv4 And ev.EventType = ETs.Leave Then
                    _external_UDP_Port_IPv4 =args.held
                ElseIf ev.EventSource.sourceObj Is frm.nududpextpIPv6 And ev.EventType = ETs.Leave Then
                    _external_UDP_Port_IPv6 = args.held
                ElseIf ev.EventSource.sourceObj Is frm.txtbxudpextaddIPv4 And ev.EventType = ETs.Leave Then
                    _external_UDP_Address_IPv4 =args.held
                ElseIf ev.EventSource.sourceObj Is frm.txtbxudpextaddIPv6 And ev.EventType = ETs.Leave Then
                    _external_UDP_Address_IPv6 =args.held
                ElseIf ev.EventSource.sourceObj Is frm.chkbxena And ev.EventType = ETs.Leave Then
                    _TCP_delay = args.held
                ElseIf ev.EventSource.sourceObj Is frm.chkbxrdtcpc And ev.EventType = ETs.Leave Then
                    _TCP_remove_disconnected_clients =args.held
                ElseIf ev.EventSource.sourceObj Is frm.cmbxsid And ev.EventType = ETs.Leave Then
                    _input_device = args.held
                ElseIf ev.EventSource.sourceObj Is frm.cmbxsniipv4 And ev.EventType = ETs.Leave Then
                    If args.held > -1 Then _selected_interfaceIPv4 = _IPv4Interfaces(args.held).Item2
                ElseIf ev.EventSource.sourceObj Is frm.cmbxsniipv6 And ev.EventType = ETs.Leave Then
                    If args.held > -1 Then _selected_interfaceIPv6 = _IPv6Interfaces(args.held).Item2
                End If
            End If
        End If
        Return toret
    End Function

    Private Function indexOfIP(ip As IPAddress, lst As List(Of Tuple(Of String, IPAddress)))
        For i As Integer = 0 To lst.Count - 1 Step 1
            Dim c As Tuple(Of String, IPAddress) = lst(i)
            If c.Item2 Is Nothing Or ip Is Nothing Then
                If c.Item2 Is Nothing And ip Is Nothing Then
                    Return i
                End If
            Else
                If c.Item2.Equals(ip) And ip.Equals(c.Item2) Then
                    Return i
                End If
            End If
        Next
        Return 0
    End Function

    Private Function waveInDevices() As WaveInCapabilities()
        Dim wic As New List(Of WaveInCapabilities)
        For i As Integer = 0 To WaveIn.DeviceCount - 1 Step 1
            wic.Add(WaveIn.GetCapabilities(i))
        Next
        Return wic.ToArray()
    End Function

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
