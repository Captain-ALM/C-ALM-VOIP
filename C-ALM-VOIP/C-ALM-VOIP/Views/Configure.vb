Imports captainalm.workerpumper
Imports System.Net
Imports captainalm.CALMNetLib
Imports NAudio.Wave

Public NotInheritable Class Configure
    Implements IWorkerPumpReceiver

    Private formClosingDone As Boolean = False
    Private formClosedDone As Boolean = False
    Private wp As WorkerPump = Nothing
    Private ue As Boolean = False

    'Should not construct externally.
    Sub New(Optional ByRef workerp As WorkerPump = Nothing)
        ' This call is required by the designer.
        InitializeComponent()

        If workerp IsNot Nothing Then
            wp = workerp
            ue = True
        Else
            ue = False
        End If
    End Sub

    Private Sub Configure_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If Not formClosedDone Then
            whenClosed()
            If ue Then wp.addEvent(New WorkerEvent(Me, ETs.Closed, e))
            formClosedDone = True
        End If
    End Sub

    Public Sub whenClosed()
        If DialogResult <> Windows.Forms.DialogResult.OK Then
            configfin = True
        End If
    End Sub

    Private Sub Configure_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not formClosingDone Then
            If Me.Visible Then
                'If close button pressed
                e.Cancel = True
                Me.Hide()
                If Me.DialogResult = Windows.Forms.DialogResult.None Then Me.DialogResult = Windows.Forms.DialogResult.Cancel
            End If
            If ue Then wp.addEvent(New WorkerEvent(Me, ETs.Closing, e))
            Me.OnFormClosed(New FormClosedEventArgs(e.CloseReason))
            formClosingDone = True
        End If
    End Sub

#Region "closeOverride"
    Public Shadows Sub Close()
        Me.Hide()
        Me.OnFormClosing(New FormClosingEventArgs(CloseReason.UserClosing, False))
        If Me.DialogResult = Windows.Forms.DialogResult.None Then Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
#End Region

    Private Sub Configure_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.DialogResult = Windows.Forms.DialogResult.None
        formClosingDone = False
        formClosedDone = False
        nudsptcpipv4.BackColor = Color.White
        nudspudpipv4.BackColor = Color.White
        nudsptcpipv6.BackColor = Color.White
        nudspudpipv6.BackColor = Color.White
        butOK.Enabled = True
        butCANCEL.Enabled = True
        updat()
        configfin = True
        If ue Then wp.addEvent(Me, ETs.Shown, e)
        While configfin
            Threading.Thread.Sleep(125)
        End While
        'Begin Population
        butOK.Select()
        _IPv4Interfaces.Clear()
        Dim lstifan As New List(Of Tuple(Of String, IPAddress))
        lstifan.Add(New Tuple(Of String, IPAddress)("<None>", IPAddress.None))
        lstifan.Add(New Tuple(Of String, IPAddress)("<None>", Nothing))
        lstifan.AddRange(Utilities.GetIPInterfacesAndNames())
        lstifan.Add(New Tuple(Of String, IPAddress)("<All>", IPAddress.Any))
        lstifan.Add(New Tuple(Of String, IPAddress)("<All>", IPAddress.IPv6Any))
        cmbxsniipv4.Items.Clear()
        For Each c As Tuple(Of String, IPAddress) In lstifan
            If c.Item2 Is Nothing Then Continue For
            If c.Item2.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                _IPv4Interfaces.Add(c)
                cmbxsniipv4.Items.Add(c.Item1 & " - " & c.Item2.ToString())
            End If
        Next
        Dim ioi4 As Integer = indexOfIP(selected_interfaceIPv4, _IPv4Interfaces)
        cmbxsniipv4.SelectedIndex = ioi4
        _IPv6Interfaces.Clear()
        cmbxsniipv6.Items.Clear()
        For Each c As Tuple(Of String, IPAddress) In lstifan
            If c.Item2 Is Nothing Then
                _IPv6Interfaces.Add(c)
                cmbxsniipv6.Items.Add(c.Item1 & " - Invalid")
                Continue For
            End If
            If c.Item2.AddressFamily = Sockets.AddressFamily.InterNetworkV6 Then
                _IPv6Interfaces.Add(c)
                cmbxsniipv6.Items.Add(c.Item1 & " - " & c.Item2.ToString())
            End If
        Next
        Dim ioi6 As Integer = indexOfIP(selected_interfaceIPv6, _IPv6Interfaces)
        cmbxsniipv6.SelectedIndex = ioi6
        cmbxsid.Items.Clear()
        For Each c As WaveInCapabilities In waveInDevices()
            cmbxsid.Items.Add(c.ProductName)
        Next
        If input_device > cmbxsid.Items.Count - 1 Then
            input_device = -1
        End If
        cmbxsid.SelectedIndex = input_device
        nudsptcpipv4.Value = port_TCP_IPv4
        nudsptcpipv6.Value = port_TCP_IPv6
        nudspudpipv4.Value = port_UDP_IPv4
        nudspudpipv6.Value = port_UDP_IPv6
        nudtcpbl.Value = TCP_backlog
        nududpextpIPv4.Value = external_UDP_Port_IPv4
        nududpextpIPv6.Value = external_UDP_Port_IPv6
        chkbxena.Checked = TCP_delay
        txtbxudpextaddIPv4.Text = external_Address_IPv4
        txtbxudpextaddIPv6.Text = external_Address_IPv6
        chkbxrdtcpc.Checked = TCP_remove_disconnected_clients
        If InListening Then
            cmbxsniipv4.Enabled = False
            nudsptcpipv4.Enabled = False
            nudspudpipv4.Enabled = False
            cmbxsniipv6.Enabled = False
            nudsptcpipv6.Enabled = False
            nudspudpipv6.Enabled = False
            cmbxsid.Enabled = False
            nudtcpbl.Enabled = False
            chkbxena.Enabled = False
        Else
            cmbxsniipv4.Enabled = True
            nudsptcpipv4.Enabled = True
            nudspudpipv4.Enabled = True
            cmbxsniipv6.Enabled = True
            nudsptcpipv6.Enabled = True
            nudspudpipv6.Enabled = True
            cmbxsid.Enabled = True
            nudtcpbl.Enabled = True
            chkbxena.Enabled = True
        End If
        butOK.Select()
        'End Population
    End Sub

    Public Property WorkerPump As WorkerPump Implements IWorkerPumpReceiver.WorkerPump
        Get
            Return wp
        End Get
        Set(value As WorkerPump)
            If value IsNot Nothing Then
                wp = value
                ue = True
            End If
        End Set
    End Property

    Private Sub cmbxsniipv4_Leave(sender As Object, e As EventArgs) Handles cmbxsniipv4.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(cmbxsniipv4, l, ETs.Leave, New EventArgsDataContainer(cmbxsniipv4.SelectedIndex)))
        End If
    End Sub

    Private Sub nudspudpipv4_Leave(sender As Object, e As EventArgs) Handles nudspudpipv4.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nudspudpipv4, l, ETs.Leave, New EventArgsDataContainer(nudspudpipv4.Value)))
        End If
        updat()
    End Sub

    Private Sub nudsptcpipv4_Leave(sender As Object, e As EventArgs) Handles nudsptcpipv4.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nudsptcpipv4, l, ETs.Leave, New EventArgsDataContainer(nudsptcpipv4.Value)))
        End If
        updat()
    End Sub

    Private Sub cmbxsniipv6_Leave(sender As Object, e As EventArgs) Handles cmbxsniipv6.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(cmbxsniipv6, l, ETs.Leave, New EventArgsDataContainer(cmbxsniipv6.SelectedIndex)))
        End If
    End Sub

    Private Sub nudspudpipv6_Leave(sender As Object, e As EventArgs) Handles nudspudpipv6.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nudspudpipv6, l, ETs.Leave, New EventArgsDataContainer(nudspudpipv6.Value)))
        End If
        updat()
    End Sub

    Private Sub nudsptcpipv6_Leave(sender As Object, e As EventArgs) Handles nudsptcpipv6.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nudsptcpipv6, l, ETs.Leave, New EventArgsDataContainer(nudsptcpipv6.Value)))
        End If
        updat()
    End Sub

    Protected Function updat() As Boolean
        Dim toret As Boolean = True
        If nudspudpipv4.Value = nudsptcpipv4.Value Or nudspudpipv4.Value = nudsptcpipv6.Value Or nudspudpipv4.Value = nudspudpipv6.Value Then
            nudspudpipv4.BackColor = Color.Orange
            toret = False
        Else
            nudspudpipv4.BackColor = Color.White
        End If
        If nudspudpipv6.Value = nudsptcpipv4.Value Or nudspudpipv6.Value = nudsptcpipv6.Value Or nudspudpipv6.Value = nudspudpipv4.Value Then
            nudspudpipv6.BackColor = Color.Orange
            toret = False
        Else
            nudspudpipv6.BackColor = Color.White
        End If
        If nudsptcpipv4.Value = nudspudpipv4.Value Or nudsptcpipv4.Value = nudsptcpipv6.Value Or nudsptcpipv4.Value = nudspudpipv6.Value Then
            nudsptcpipv4.BackColor = Color.Orange
            toret = False
        Else
            nudsptcpipv4.BackColor = Color.White
        End If
        If nudsptcpipv6.Value = nudspudpipv4.Value Or nudsptcpipv6.Value = nudsptcpipv4.Value Or nudsptcpipv6.Value = nudspudpipv6.Value Then
            nudsptcpipv6.BackColor = Color.Orange
            toret = False
        Else
            nudsptcpipv6.BackColor = Color.White
        End If
        Return toret
    End Function

    Private Sub nudtcpbl_Leave(sender As Object, e As EventArgs) Handles nudtcpbl.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nudtcpbl, l, ETs.Leave, New EventArgsDataContainer(nudtcpbl.Value)))
        End If
    End Sub

    Private Sub chkbxena_Leave(sender As Object, e As EventArgs) Handles chkbxena.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(chkbxena, l, ETs.Leave, New EventArgsDataContainer(chkbxena.Checked)))
        End If
    End Sub

    Private Sub txtbxudpextaddIPv4_Leave(sender As Object, e As EventArgs) Handles txtbxudpextaddIPv4.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(txtbxudpextaddIPv4, l, ETs.Leave, New EventArgsDataContainer(txtbxudpextaddIPv4.Text)))
        End If
    End Sub

    Private Sub nududpextpIPv4_Leave(sender As Object, e As EventArgs) Handles nududpextpIPv4.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nududpextpIPv4, l, ETs.Leave, New EventArgsDataContainer(nududpextpIPv4.Value)))
        End If
    End Sub

    Private Sub txtbxudpextaddIPv6_Leave(sender As Object, e As EventArgs) Handles txtbxudpextaddIPv6.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(txtbxudpextaddIPv6, l, ETs.Leave, New EventArgsDataContainer(txtbxudpextaddIPv6.Text)))
        End If
    End Sub

    Private Sub nududpextpIPv6_Leave(sender As Object, e As EventArgs) Handles nududpextpIPv6.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nududpextpIPv6, l, ETs.Leave, New EventArgsDataContainer(nududpextpIPv6.Value)))
        End If
    End Sub

    Private Sub butOK_Click(sender As Object, e As EventArgs) Handles butOK.Click
        If Not updat() Then Exit Sub
        butOK.Enabled = False
        butOK.Select()
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butOK, l, ETs.Click, New EventArgsDataContainer(Nothing)))
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub butCANCEL_Click(sender As Object, e As EventArgs) Handles butCANCEL.Click
        butCANCEL.Enabled = False
        butCANCEL.Select()
        configfin = True
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butCANCEL, l, ETs.Click, New EventArgsDataContainer(Nothing)))
        End If
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmbxsid_Leave(sender As Object, e As EventArgs) Handles cmbxsid.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(cmbxsid, l, ETs.Leave, New EventArgsDataContainer(cmbxsid.SelectedIndex)))
        End If
    End Sub

    Private Sub chkbxrdtcpc_Leave(sender As Object, e As EventArgs) Handles chkbxrdtcpc.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(chkbxrdtcpc, l, ETs.Leave, New EventArgsDataContainer(chkbxrdtcpc.Checked)))
        End If
    End Sub

    Private Function indexOfIP(ip As IPAddress, lst As IList(Of Tuple(Of String, IPAddress)))
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
End Class