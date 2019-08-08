Imports captainalm.workerpumper
Imports captainalm.CALMNetMarshal
Imports System.Net
Imports captainalm.CALMNetLib

Public NotInheritable Class MainProgram
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

    Private Sub MainProgram_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If ue Then wp.addEvent(New WorkerEvent(Me, ETs.Load, e))
    End Sub

    Private Sub MainProgram_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If Not formClosedDone Then
            whenClosed()
            If ue Then wp.addEvent(New WorkerEvent(Me, ETs.Closed, e))
            formClosedDone = True
        End If
    End Sub

    Public Sub whenClosed()
        disengage()
    End Sub

    Private Sub MainProgram_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not formClosingDone Then
            If Me.Visible Then
                'If close button pressed
                e.Cancel = True
                Me.Hide()
                Me.DialogResult = Windows.Forms.DialogResult.OK
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
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
#End Region

    Private Sub MainProgram_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.DialogResult = Windows.Forms.DialogResult.None
        formClosingDone = False
        formClosedDone = False
        If ue Then wp.addEvent(Me, ETs.Shown, e)
        InListening = False
        wp.showForm(Of Configure)(0, Me)
        engage()
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

    Private Sub butabout_Click(sender As Object, e As EventArgs) Handles butabout.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butabout, l, ETs.Click, e))
            wp.showForm(Of AboutBx)(0, Me)
        End If
    End Sub

    Private Sub butrconf_Click(sender As Object, e As EventArgs) Handles butrconf.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butrconf, l, ETs.Click, e))
            wp.showForm(Of Configure)(0, Me)
        End If
    End Sub

    Private Sub butrset_Click(sender As Object, e As EventArgs) Handles butrset.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butrset, l, ETs.Click, e))
            disengage()
            wp.showForm(Of Configure)(0, Me)
            engage()
        End If
    End Sub

    Private Sub butstp_Click(sender As Object, e As EventArgs) Handles butstp.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butstp, l, ETs.Click, e))
        End If
        Me.Close()
    End Sub

    Private Sub butcladd_Click(sender As Object, e As EventArgs) Handles butcladd.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butcladd, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butclrem_Click(sender As Object, e As EventArgs) Handles butclrem.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butclrem, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butclcreatec_Click(sender As Object, e As EventArgs) Handles butclcreatec.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butclcreatec, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butclccls_Click(sender As Object, e As EventArgs) Handles butclccls.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butclccls, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butcl2add_Click(sender As Object, e As EventArgs) Handles butcl2add.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butcl2add, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butcl2rem_Click(sender As Object, e As EventArgs) Handles butcl2rem.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butcl2rem, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butcl2editc_Click(sender As Object, e As EventArgs) Handles butcl2editc.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butcl2editc, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butcl2ccls_Click(sender As Object, e As EventArgs) Handles butcl2ccls.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butcl2ccls, l, ETs.Click, e))
        End If
    End Sub

    Private Sub ListViewcl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewcl.SelectedIndexChanged
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(ListViewcl, l, ETs.SelectedIndexChanged, New EventArgsDataContainer(ListViewcl.SelectedIndices)))
        End If
    End Sub

    Private Sub ListViewcl2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewcl2.SelectedIndexChanged
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(ListViewcl2, l, ETs.SelectedIndexChanged, New EventArgsDataContainer(ListViewcl2.SelectedIndices)))
        End If
    End Sub

    Private Sub ListViewsc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewsc.SelectedIndexChanged
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(ListViewsc, l, ETs.SelectedIndexChanged, New EventArgsDataContainer(ListViewsc.SelectedIndices)))
        End If
    End Sub

    Private Sub butscmutes_Click(sender As Object, e As EventArgs) Handles butscmutes.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butscmutes, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butscunmutes_Click(sender As Object, e As EventArgs) Handles butscunmutes.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butscunmutes, l, ETs.Click, e))
        End If
    End Sub

    Private Sub TrackBarvol_Leave(sender As Object, e As EventArgs) Handles TrackBarvol.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(TrackBarvol, l, ETs.Scroll, New EventArgsDataContainer(TrackBarvol.Value)))
        End If
    End Sub

    Private Sub NumericUpDownvol_Leave(sender As Object, e As EventArgs) Handles NumericUpDownvol.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(NumericUpDownvol, l, ETs.ValueChanged, New EventArgsDataContainer(NumericUpDownvol.Value)))
        End If
    End Sub

    Private Sub butclviewc_Click(sender As Object, e As EventArgs) Handles butclviewc.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butclviewc, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butcl2cc_Click(sender As Object, e As EventArgs) Handles butcl2cc.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butcl2cc, l, ETs.Click, e))
        End If
    End Sub

    Private Sub engage()
        If Not selected_interfaceIPv4.Equals(IPAddress.None) Then
            tcpmarshalIPv4 = New NetMarshalTCP(selected_interfaceIPv4, port_TCP_IPv4, TCP_backlog, TCP_delay) With {.beatTimeout = 2500}
            udpmarshalIPv4 = New NetMarshalUDP(selected_interfaceIPv4, port_UDP_IPv4)
            AddHandler tcpmarshalIPv4.clientConnected, AddressOf conIPv4
            AddHandler tcpmarshalIPv4.clientDisconnected, AddressOf discon
            AddHandler udpmarshalIPv4.MessageReceived, AddressOf msgRecIPv4
            tcpmarshalIPv4.start()
            udpmarshalIPv4.start()
            InListening = True
        End If
        If Not selected_interfaceIPv6 Is Nothing Then
            tcpmarshalIPv6 = New NetMarshalTCP(selected_interfaceIPv6, port_TCP_IPv6, TCP_backlog, TCP_delay) With {.beatTimeout = 2500}
            udpmarshalIPv6 = New NetMarshalUDP(selected_interfaceIPv6, port_UDP_IPv6)
            AddHandler tcpmarshalIPv6.clientConnected, AddressOf conIPv6
            AddHandler tcpmarshalIPv6.clientDisconnected, AddressOf discon
            AddHandler udpmarshalIPv6.MessageReceived, AddressOf msgRecIPv6
            tcpmarshalIPv6.start()
            udpmarshalIPv6.start()
            InListening = True
        End If
    End Sub

    Private Sub disengage()
        For Each c As Client In clients
            c.stop()
        Next
        clients.Clear()
        If Not tcpmarshalIPv4 Is Nothing Then
            RemoveHandler tcpmarshalIPv4.clientConnected, AddressOf conIPv4
            RemoveHandler tcpmarshalIPv4.clientDisconnected, AddressOf discon
            If tcpmarshalIPv4.ready Then
                tcpmarshalIPv4.close()
            End If
            tcpmarshalIPv4 = Nothing
            InListening = False
        End If
        If Not tcpmarshalIPv6 Is Nothing Then
            RemoveHandler tcpmarshalIPv6.clientConnected, AddressOf conIPv6
            RemoveHandler tcpmarshalIPv6.clientDisconnected, AddressOf discon
            If tcpmarshalIPv6.ready Then
                tcpmarshalIPv6.close()
            End If
            tcpmarshalIPv6 = Nothing
            InListening = False
        End If
        If Not udpmarshalIPv4 Is Nothing Then
            RemoveHandler udpmarshalIPv4.MessageReceived, AddressOf msgRecIPv4
            If udpmarshalIPv4.ready Then
                udpmarshalIPv4.close()
            End If
            udpmarshalIPv4 = Nothing
            InListening = False
        End If
        If Not udpmarshalIPv6 Is Nothing Then
            RemoveHandler udpmarshalIPv6.MessageReceived, AddressOf msgRecIPv6
            If udpmarshalIPv6.ready Then
                udpmarshalIPv6.close()
            End If
            udpmarshalIPv6 = Nothing
            InListening = False
        End If
    End Sub

    Private Sub msgRecIPv4(msg As IPacket)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() msgRecIPv4(msg))
        Else
            Dim cl As Client = retRegCl(msg.senderIP, msg.senderPort)
            If cl Is Nothing Then
                cl = New Client(New Contact(resolve(msg.senderIP, Sockets.AddressFamily.InterNetwork).ToString(), msg.senderPort, AddressableType.UDP, MessagePassMode.Bidirectional, IPVersion.IPv4), udpmarshalIPv4) With {.myAddress = external_UDP_Address_IPv4, .myPort = external_UDP_Port_IPv4, .name = msg.senderIP & ":" & msg.senderPort}
                clients.Add(cl)
                Dim lvc As New ListViewItem(cl.name)
                lvc.SubItems.Add(cl.targetAddress)
                lvc.SubItems.Add(cl.targetPort)
                lvc.SubItems.Add("UDP")
                lvc.SubItems.Add("Ready")
                ListViewcl.Items.Add(lvc)
                cl.forceReceive(msg)
            End If
        End If
    End Sub

    Private Sub msgRecIPv6(msg As IPacket)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() msgRecIPv6(msg))
        Else
            Dim cl As Client = retRegCl(msg.senderIP, msg.senderPort)
            If cl Is Nothing Then
                cl = New Client(New Contact(resolve(msg.senderIP, Sockets.AddressFamily.InterNetworkV6).ToString(), msg.senderPort, AddressableType.UDP, MessagePassMode.Bidirectional, IPVersion.IPv6), udpmarshalIPv6) With {.myAddress = external_UDP_Address_IPv6, .myPort = external_UDP_Port_IPv6, .name = msg.senderIP & ":" & msg.senderPort}
                clients.Add(cl)
                Dim lvc As New ListViewItem(cl.name)
                lvc.SubItems.Add(cl.targetAddress)
                lvc.SubItems.Add(cl.targetPort)
                lvc.SubItems.Add("UDP")
                lvc.SubItems.Add("Ready")
                ListViewcl.Items.Add(lvc)
                cl.forceReceive(msg)
            End If
        End If
    End Sub

    Private Sub conIPv4(ip As String, port As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() conIPv4(ip, port))
        Else
            Dim cl As Client = retRegCl(ip, port)
            If cl Is Nothing Then
                Dim clm As NetMarshalTCPClient = tcpmarshalIPv4.client(ip, port)
                Dim lip As String = CType(clm.internalSocket, NetTCPClient).listenerIPAddress
                Dim lport As Integer = CType(clm.internalSocket, NetTCPClient).listenerPort
                Dim rip As String = CType(clm.internalSocket, NetTCPClient).remoteIPAddress
                Dim rport As String = CType(clm.internalSocket, NetTCPClient).remotePort
                Dim llip As String = CType(tcpmarshalIPv4.internalSocket, NetTCPListener).localIPAddress
                Dim llport As Integer = CType(tcpmarshalIPv4.internalSocket, NetTCPListener).localPort
                Dim tnom As String = getName(lip, lport)
                If tnom <> "" Then remName(lip, lport)
                If lip = llip And lport = llport Then
                    cl = New Client(New Contact(rip, rport, AddressableType.TCP, MessagePassMode.Bidirectional, IPVersion.IPv4), clm) With {.name = rip & ":" & rport, .myAddress = rip, .myPort = rport}
                Else
                    If tnom = "" Then
                        tnom = rip & ":" & rport
                    End If
                    cl = New Client(New Contact(lip, lport, AddressableType.TCP, MessagePassMode.Bidirectional, IPVersion.IPv4), clm) With {.name = tnom, .myAddress = rip, .myPort = rport}
                End If
                clients.Add(cl)
                Dim lvc As New ListViewItem(cl.name)
                lvc.SubItems.Add(cl.myAddress)
                lvc.SubItems.Add(cl.myPort)
                lvc.SubItems.Add("TCP")
                lvc.SubItems.Add(clm.ready)
                ListViewcl.Items.Add(lvc)
            End If
        End If
    End Sub

    Private Sub conIPv6(ip As String, port As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() conIPv6(ip, port))
        Else
            Dim cl As Client = retRegCl(ip, port)
            If cl Is Nothing Then
                Dim clm As NetMarshalTCPClient = tcpmarshalIPv6.client(ip, port)
                Dim lip As String = CType(clm.internalSocket, NetTCPClient).listenerIPAddress
                Dim lport As Integer = CType(clm.internalSocket, NetTCPClient).listenerPort
                Dim rip As String = CType(clm.internalSocket, NetTCPClient).remoteIPAddress
                Dim rport As String = CType(clm.internalSocket, NetTCPClient).remotePort
                Dim llip As String = CType(tcpmarshalIPv6.internalSocket, NetTCPListener).localIPAddress
                Dim llport As Integer = CType(tcpmarshalIPv6.internalSocket, NetTCPListener).localPort
                Dim tnom As String = getName(lip, lport)
                If tnom <> "" Then remName(lip, lport)
                If lip = llip And lport = llport Then
                    cl = New Client(New Contact(rip, rport, AddressableType.TCP, MessagePassMode.Bidirectional, IPVersion.IPv6), clm) With {.name = rip & ":" & rport, .myAddress = rip, .myPort = rport}
                Else
                    If tnom = "" Then
                        tnom = rip & ":" & rport
                    End If
                    cl = New Client(New Contact(lip, lport, AddressableType.TCP, MessagePassMode.Bidirectional, IPVersion.IPv6), clm) With {.name = tnom, .myAddress = rip, .myPort = rport}
                End If
                clients.Add(cl)
                Dim lvc As New ListViewItem(cl.name)
                lvc.SubItems.Add(cl.myAddress)
                lvc.SubItems.Add(cl.myPort)
                lvc.SubItems.Add("TCP")
                lvc.SubItems.Add(clm.ready)
                ListViewcl.Items.Add(lvc)
            End If
        End If
    End Sub

    Private Sub discon(ip As String, port As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() discon(ip, port))
        Else
            Dim cl As Client = retRegCl(ip, port)
            If cl IsNot Nothing Then
                Dim ind As Integer = clients.IndexOf(cl)
                If ListViewcl.SelectedIndices.Contains(ind) Then _
                    ListViewcl.SelectedIndices.Remove(ind)
                ListViewcl.Items.RemoveAt(ind)
                clients.RemoveAt(ind)
            End If
        End If
    End Sub

    Private Sub visUpdat()
        If InListening Then
            lblstatus.Text = "Listening."
        Else
            lblstatus.Text = "Not Listening."
        End If
    End Sub

    Private Function retRegCl(ip As String, port As Integer) As Client
        Dim toret As Client = Nothing
        For Each c As Client In clients
            If c.type = AddressableType.UDP Then
                If c.targetAddress = ip And c.targetPort = port Then
                    toret = c
                    Exit For
                End If
            ElseIf c.type = AddressableType.TCP Then
                If CType(c.marshal.internalSocket, INetConfig).remoteIPAddress = ip And CType(c.marshal.internalSocket, INetConfig).remotePort = port Then
                    toret = c
                    Exit For
                End If
            End If
        Next
        Return toret
    End Function

    Private Function getName(ip As String, port As Integer) As String
        Dim toret As String = ""
        For Each c As Tuple(Of String, Integer, String) In nomReconReg
            If c.Item1 = ip And c.Item2 = port Then
                toret = c.Item3
                Exit For
            End If
        Next
        Return toret
    End Function

    Private Sub remName(ip As String, port As Integer)
        For i As Integer = 0 To nomReconReg.Count - 1 Step 1
            Dim c As Tuple(Of String, Integer, String) = nomReconReg(i)
            If c.Item1 = ip And c.Item2 = port Then
                nomReconReg.RemoveAt(i)
                Exit For
            End If
        Next
    End Sub
End Class