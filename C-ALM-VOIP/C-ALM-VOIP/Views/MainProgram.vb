Imports captainalm.workerpumper
Imports captainalm.CALMNetMarshal
Imports System.Net
Imports captainalm.CALMNetLib
Imports captainalm.Serialize

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
        InListening = False
        wp.showForm(Of Configure)(0, Me)
        While Not configfin
            Threading.Thread.Sleep(125)
        End While
        If ue Then wp.addEvent(Me, ETs.Shown, e)
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
            wp.showForm(Of AboutBx)(0, Me)
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butabout, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butrconf_Click(sender As Object, e As EventArgs) Handles butrconf.Click
        If ue Then
            If InListening Then
                wp.showForm(Of Configure)(0, Me)
                While Not configfin
                    Threading.Thread.Sleep(125)
                End While
            End If
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butrconf, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butrset_Click(sender As Object, e As EventArgs) Handles butrset.Click
        If ue Then
            disengage()
            wp.showForm(Of Configure)(0, Me)
            While Not configfin
                Threading.Thread.Sleep(125)
            End While
            engage()
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butrset, l, ETs.Click, e))
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
            editfin = False
            ceditm = EditorMode.Create
            caddrbs = New Contact("", 0, IPVersion.IPv4, AddressableType.UDP) With {.messagePassMode = MessagePassMode.Bidirectional}
            wp.showForm(Of Editor)(0, Me)
            While Not editfin
                Threading.Thread.Sleep(125)
            End While
            If editsuccess Then
                If caddrbs.name = "" Then caddrbs.name = caddrbs.targetAddress & ":" & caddrbs.targetPort
                If caddrbs.type = AddressableType.TCP Then
                    If caddrbs.targetIPVersion = IPVersion.IPv4 And Not tcpmarshalIPv4 Is Nothing Then
                        CType(caddrbs, Contact).targetAddress = resolve(caddrbs.targetAddress, Net.Sockets.AddressFamily.InterNetwork).ToString()
                        Dim tpl As New Tuple(Of String, Integer, String, voip.MessagePassMode)(caddrbs.targetAddress, caddrbs.targetPort, caddrbs.name, caddrbs.messagePassMode)
                        tcpResvSetReg.Add(tpl)
                        Dim chk As Boolean = tcpmarshalIPv4.connect(caddrbs.targetAddress, caddrbs.targetPort)
                        If Not chk Then tcpResvSetReg.Remove(tpl)
                    ElseIf caddrbs.targetIPVersion = IPVersion.IPv6 And Not tcpmarshalIPv6 Is Nothing Then
                        CType(caddrbs, Contact).targetAddress = resolve(caddrbs.targetAddress, Net.Sockets.AddressFamily.InterNetworkV6).ToString()
                        Dim tpl As New Tuple(Of String, Integer, String, voip.MessagePassMode)(caddrbs.targetAddress, caddrbs.targetPort, caddrbs.name, caddrbs.messagePassMode)
                        tcpResvSetReg.Add(tpl)
                        Dim chk As Boolean = tcpmarshalIPv6.connect(caddrbs.targetAddress, caddrbs.targetPort)
                        If Not chk Then tcpResvSetReg.Remove(tpl)
                    End If
                ElseIf caddrbs.type = AddressableType.UDP Then
                    If caddrbs.targetIPVersion = IPVersion.IPv4 And Not udpmarshalIPv4 Is Nothing Then
                        CType(caddrbs, Contact).targetAddress = resolve(caddrbs.targetAddress, Net.Sockets.AddressFamily.InterNetwork).ToString()
                        If caddrbs.myAddress = "" Then caddrbs.myAddress = external_Address_IPv4
                        If caddrbs.myPort = 0 Then caddrbs.myPort = external_UDP_Port_IPv4
                        Dim cl As New Client(caddrbs, udpmarshalIPv4)
                        addCl(cl)
                        addStrm(cl.stream)
                    ElseIf caddrbs.targetIPVersion = IPVersion.IPv6 And Not udpmarshalIPv6 Is Nothing Then
                        CType(caddrbs, Contact).targetAddress = resolve(caddrbs.targetAddress, Net.Sockets.AddressFamily.InterNetworkV6).ToString()
                        If caddrbs.myAddress = "" Then caddrbs.myAddress = external_Address_IPv6
                        If caddrbs.myPort = 0 Then caddrbs.myPort = external_UDP_Port_IPv6
                        Dim cl As New Client(caddrbs, udpmarshalIPv6)
                        addCl(cl)
                        addStrm(cl.stream)
                    End If
                End If
                editsuccess = False
            End If
            ceditm = EditorMode.None
            caddrbs = Nothing
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butcladd, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butclrem_Click(sender As Object, e As EventArgs) Handles butclrem.Click
        If ue Then
            If ListViewcl.SelectedIndices.Count > 0 Then
                Dim indx As Integer = ListViewcl.SelectedIndices(0)
                Dim cl As Client = indxCl(indx)
                Dim strm As Streamer = cl.stream
                remStrm(strm)
                cl.stop()
                remCl(cl)
            End If
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butclrem, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butclcreatec_Click(sender As Object, e As EventArgs) Handles butclcreatec.Click
        If ue Then
            If ListViewcl.SelectedIndices.Count > 0 Then
                Dim indx As Integer = ListViewcl.SelectedIndices(0)
                Dim cl As Client = indxCl(indx)
                Dim ab As AddressableBase = cl.duplicateToNew()
                addCon(ab)
            End If
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butclcreatec, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butclccls_Click(sender As Object, e As EventArgs) Handles butclccls.Click
        If ue Then
            For i As Integer = clients.Count - 1 To 0 Step -1
                Dim c As Client = clients(i)
                If Not c.stream Is Nothing Then _
                    remStrm(c.stream)
                c.stop()
                remCl(c)
            Next
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butclccls, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butcl2add_Click(sender As Object, e As EventArgs) Handles butcl2add.Click
        If ue Then
            editfin = False
            ceditm = EditorMode.Create
            caddrbs = New Contact("", 0, IPVersion.IPv4, AddressableType.UDP) With {.messagePassMode = MessagePassMode.Bidirectional}
            wp.showForm(Of Editor)(0, Me)
            While Not editfin
                Threading.Thread.Sleep(125)
            End While
            If editsuccess Then
                If caddrbs.name = "" Then caddrbs.name = caddrbs.targetAddress & ":" & caddrbs.targetPort
                addCon(caddrbs)
                editsuccess = False
            End If
            ceditm = EditorMode.None
            caddrbs = Nothing
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butcl2add, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butcl2rem_Click(sender As Object, e As EventArgs) Handles butcl2rem.Click
        If ue Then
            If ListViewcl2.SelectedIndices.Count > 0 Then
                Dim indx As Integer = ListViewcl2.SelectedIndices(0)
                Dim cl As Contact = indxCon(indx)
                remCon(cl)
            End If
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butcl2rem, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butcl2editc_Click(sender As Object, e As EventArgs) Handles butcl2editc.Click
        If ue Then
            If ListViewcl2.SelectedIndices.Count > 0 Then
                Dim indx As Integer = ListViewcl2.SelectedIndices(0)
                editfin = False
                ceditm = EditorMode.EditContact
                caddrbs = indxCon(indx)
                wp.showForm(Of Editor)(0, Me)
                While Not editfin
                    Threading.Thread.Sleep(125)
                End While
                If editsuccess Then
                    upCon(caddrbs)
                    editsuccess = False
                End If
                ceditm = EditorMode.None
                caddrbs = Nothing
            End If
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butcl2editc, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butcl2ccls_Click(sender As Object, e As EventArgs) Handles butcl2ccls.Click
        If ue Then
            For i As Integer = contacts.Count - 1 To 0 Step -1
                Dim c As Contact = contacts(i)
                remCon(c)
            Next
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butcl2ccls, l, ETs.Click, e))
        End If
    End Sub

    Private Sub ListViewsc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewsc.SelectedIndexChanged
        If ue Then
            If ListViewsc.SelectedIndices.Count > 0 Then
                Dim indx As Integer = ListViewsc.SelectedIndices(0)
                Dim strm As Streamer = indxStrm(indx)
                TrackBarvol.Value = strm.volume * 100
                NumericUpDownvol.Value = strm.volume * 100
            End If
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(ListViewsc, l, ETs.SelectedIndexChanged, New EventArgsDataContainer(ListViewsc.SelectedIndices)))
        End If
    End Sub

    Private Sub butscmutes_Click(sender As Object, e As EventArgs) Handles butscmutes.Click
        If ue Then
            If ListViewsc.SelectedIndices.Count > 0 Then
                Dim indx As Integer = ListViewsc.SelectedIndices(0)
                Dim strm As Streamer = indxStrm(indx)
                strm.muted = True
                upStrm(strm)
            End If
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butscmutes, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butscunmutes_Click(sender As Object, e As EventArgs) Handles butscunmutes.Click
        If ue Then
            If ListViewsc.SelectedIndices.Count > 0 Then
                Dim indx As Integer = ListViewsc.SelectedIndices(0)
                Dim strm As Streamer = indxStrm(indx)
                strm.muted = False
                upStrm(strm)
            End If
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butscunmutes, l, ETs.Click, e))
        End If
    End Sub

    Private Sub TrackBarvol_Leave(sender As Object, e As EventArgs) Handles TrackBarvol.Leave
        If ue Then
            NumericUpDownvol.Value = TrackBarvol.Value
            If ListViewsc.SelectedIndices.Count > 0 Then
                Dim indx As Integer = ListViewsc.SelectedIndices(0)
                Dim strm As Streamer = indxStrm(indx)
                strm.volume = TrackBarvol.Value / 100
                upStrm(strm)
            End If
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(TrackBarvol, l, ETs.Leave, New EventArgsDataContainer(TrackBarvol.Value)))
        End If
    End Sub

    Private Sub NumericUpDownvol_Leave(sender As Object, e As EventArgs) Handles NumericUpDownvol.Leave
        If ue Then
            TrackBarvol.Value = NumericUpDownvol.Value
            If ListViewsc.SelectedIndices.Count > 0 Then
                Dim indx As Integer = ListViewsc.SelectedIndices(0)
                Dim strm As Streamer = indxStrm(indx)
                strm.volume = NumericUpDownvol.Value / 100
                upStrm(strm)
            End If
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(NumericUpDownvol, l, ETs.Leave, New EventArgsDataContainer(NumericUpDownvol.Value)))
        End If
    End Sub

    Private Sub butclviewc_Click(sender As Object, e As EventArgs) Handles butclviewc.Click
        If ue Then
            If ListViewcl.SelectedIndices.Count > 0 Then
                Dim indx As Integer = ListViewcl.SelectedIndices(0)
                editfin = False
                ceditm = EditorMode.EditClient
                caddrbs = indxCl(indx)
                wp.showForm(Of Editor)(0, Me)
                While Not editfin
                    Threading.Thread.Sleep(125)
                End While
                If editsuccess Then
                    upCl(caddrbs)
                    upStrm(CType(caddrbs, Client).stream)
                    editsuccess = False
                End If
                ceditm = EditorMode.None
                caddrbs = Nothing
            End If
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butclviewc, l, ETs.Click, e))
        End If
    End Sub

    Private Sub butcl2cc_Click(sender As Object, e As EventArgs) Handles butcl2cc.Click
        If ue Then
            If ListViewcl2.SelectedIndices.Count > 0 Then
                Dim indx As Integer = ListViewcl2.SelectedIndices(0)
                Dim cl As Contact = indxCon(indx)
                If cl.name = "" Then cl.name = cl.targetAddress & ":" & cl.targetPort
                If cl.type = AddressableType.TCP Then
                    If cl.targetIPVersion = IPVersion.IPv4 And Not tcpmarshalIPv4 Is Nothing Then
                        CType(cl, Contact).targetAddress = resolve(cl.targetAddress, Net.Sockets.AddressFamily.InterNetwork).ToString()
                        Dim tpl As New Tuple(Of String, Integer, String, voip.MessagePassMode)(cl.targetAddress, cl.targetPort, cl.name, cl.messagePassMode)
                        tcpResvSetReg.Add(tpl)
                        Dim chk As Boolean = tcpmarshalIPv4.connect(cl.targetAddress, cl.targetPort)
                        If Not chk Then tcpResvSetReg.Remove(tpl)
                    ElseIf cl.targetIPVersion = IPVersion.IPv6 And Not tcpmarshalIPv6 Is Nothing Then
                        CType(cl, Contact).targetAddress = resolve(cl.targetAddress, Net.Sockets.AddressFamily.InterNetworkV6).ToString()
                        Dim tpl As New Tuple(Of String, Integer, String, voip.MessagePassMode)(cl.targetAddress, cl.targetPort, cl.name, cl.messagePassMode)
                        tcpResvSetReg.Add(tpl)
                        Dim chk As Boolean = tcpmarshalIPv6.connect(cl.targetAddress, cl.targetPort)
                        If Not chk Then tcpResvSetReg.Remove(tpl)
                    End If
                ElseIf cl.type = AddressableType.UDP Then
                    If cl.targetIPVersion = IPVersion.IPv4 And Not udpmarshalIPv4 Is Nothing Then
                        CType(cl, Contact).targetAddress = resolve(cl.targetAddress, Net.Sockets.AddressFamily.InterNetwork).ToString()
                        If cl.myAddress = "" Then cl.myAddress = external_Address_IPv4
                        If cl.myPort = 0 Then cl.myPort = external_UDP_Port_IPv4
                        Dim cl2 As New Client(cl, udpmarshalIPv4)
                        addCl(cl2)
                        addStrm(cl2.stream)
                    ElseIf cl.targetIPVersion = IPVersion.IPv6 And Not udpmarshalIPv6 Is Nothing Then
                        CType(cl, Contact).targetAddress = resolve(cl.targetAddress, Net.Sockets.AddressFamily.InterNetworkV6).ToString()
                        If cl.myAddress = "" Then cl.myAddress = external_Address_IPv6
                        If cl.myPort = 0 Then cl.myPort = external_UDP_Port_IPv6
                        Dim cl2 As New Client(cl, udpmarshalIPv6)
                        addCl(cl2)
                        addStrm(cl2.stream)
                    End If
                End If
            End If
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butcl2cc, l, ETs.Click, e))
        End If
    End Sub

    Private Sub engage()
        micVOIP = New VOIPSender() With {.samplebuffersize = samplerate \ (1000 \ buffmdmsecs)}
        spkVOIP = New VOIPReceiver()
        If Not selected_interfaceIPv4.Equals(IPAddress.None) Then
            tcpmarshalIPv4 = New NetMarshalTCP(selected_interfaceIPv4, port_TCP_IPv4, TCP_backlog, TCP_delay) With {.beatTimeout = 0, .serializer = gserializer}
            udpmarshalIPv4 = New NetMarshalUDP(selected_interfaceIPv4, port_UDP_IPv4) With {.serializer = gserializer}
            AddHandler tcpmarshalIPv4.clientConnected, AddressOf conIPv4
            AddHandler tcpmarshalIPv4.clientDisconnected, AddressOf discon
            AddHandler udpmarshalIPv4.MessageReceived, AddressOf msgRecIPv4
            tcpmarshalIPv4.start()
            udpmarshalIPv4.start()
            InListening = True
        End If
        If Not selected_interfaceIPv6 Is Nothing Then
            tcpmarshalIPv6 = New NetMarshalTCP(selected_interfaceIPv6, port_TCP_IPv6, TCP_backlog, TCP_delay) With {.beatTimeout = 0, .serializer = gserializer}
            udpmarshalIPv6 = New NetMarshalUDP(selected_interfaceIPv6, port_UDP_IPv6) With {.serializer = gserializer}
            AddHandler tcpmarshalIPv6.clientConnected, AddressOf conIPv6
            AddHandler tcpmarshalIPv6.clientDisconnected, AddressOf discon
            AddHandler udpmarshalIPv6.MessageReceived, AddressOf msgRecIPv6
            tcpmarshalIPv6.start()
            udpmarshalIPv6.start()
            InListening = True
        End If
        addStrm(micVOIP.streamer)
        If InListening Then
            lblstatus.Text = "Listening."
        Else
            lblstatus.Text = "Not Listening."
        End If
    End Sub

    Private Sub disengage()
        remStrm(micVOIP.streamer)
        For i As Integer = clients.Count - 1 To 0 Step -1
            Dim c As Client = clients(i)
            If Not c.stream Is Nothing Then _
                remStrm(c.stream)
            c.stop()
            remCl(c)
        Next
        streams.Clear()
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
        micVOIP.Dispose()
        micVOIP = Nothing
        spkVOIP.Dispose()
        spkVOIP = Nothing
    End Sub

    Private Sub msgRecIPv4(msg As IPacket)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() msgRecIPv4(msg))
        Else
            Dim cl As Client = retRegCl(msg.senderIP, msg.senderPort)
            If cl Is Nothing Then
                cl = New Client(New Contact(resolve(msg.senderIP, Sockets.AddressFamily.InterNetwork).ToString(), msg.senderPort, IPVersion.IPv4, AddressableType.UDP) With {.messagePassMode = MessagePassMode.Bidirectional}, udpmarshalIPv4) With {.myAddress = external_Address_IPv4, .myPort = external_UDP_Port_IPv4, .name = msg.senderIP & ":" & msg.senderPort}
                addCl(cl)
                cl.forceReceive(msg)
                addStrm(cl.stream)
            End If
        End If
    End Sub

    Private Sub msgRecIPv6(msg As IPacket)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() msgRecIPv6(msg))
        Else
            Dim cl As Client = retRegCl(msg.senderIP, msg.senderPort)
            If cl Is Nothing Then
                cl = New Client(New Contact(resolve(msg.senderIP, Sockets.AddressFamily.InterNetworkV6).ToString(), msg.senderPort, IPVersion.IPv6, AddressableType.UDP), udpmarshalIPv6) With {.myAddress = external_Address_IPv6, .myPort = external_UDP_Port_IPv6, .name = msg.senderIP & ":" & msg.senderPort}
                addCl(cl)
                cl.forceReceive(msg)
                addStrm(cl.stream)
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
                If Not clm.internalSocket Is Nothing AndAlso clm.ready Then
                    Dim lip As String = CType(clm.internalSocket, NetTCPClient).listenerIPAddress
                    Dim lport As Integer = CType(clm.internalSocket, NetTCPClient).listenerPort
                    Dim rip As String = CType(clm.internalSocket, NetTCPClient).remoteIPAddress
                    Dim rport As String = CType(clm.internalSocket, NetTCPClient).remotePort
                    Dim llip As String = CType(tcpmarshalIPv4.internalSocket, NetTCPListener).localIPAddress
                    Dim llport As Integer = CType(tcpmarshalIPv4.internalSocket, NetTCPListener).localPort
                    Dim tnom As String = getResvSetName(lip, lport)
                    Dim mpm As voip.MessagePassMode = getResvSetPM(lip, lport)
                    If tnom <> "" Then remResvSet(lip, lport)
                    If lip = llip And lport = llport Then
                        cl = New Client(New Contact(rip, rport, IPVersion.IPv4, AddressableType.TCP) With {.messagePassMode = mpm}, clm) With {.name = rip & ":" & rport, .myAddress = rip, .myPort = rport}
                    Else
                        If tnom = "" Then
                            tnom = rip & ":" & rport
                        End If
                        cl = New Client(New Contact(lip, lport, IPVersion.IPv4, AddressableType.TCP) With {.messagePassMode = mpm}, clm) With {.name = tnom, .myAddress = rip, .myPort = rport}
                    End If
                    addCl(cl)
                    addStrm(cl.stream)
                End If
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
                If Not clm.internalSocket Is Nothing AndAlso clm.ready Then
                    Dim lip As String = CType(clm.internalSocket, NetTCPClient).listenerIPAddress
                    Dim lport As Integer = CType(clm.internalSocket, NetTCPClient).listenerPort
                    Dim rip As String = CType(clm.internalSocket, NetTCPClient).remoteIPAddress
                    Dim rport As String = CType(clm.internalSocket, NetTCPClient).remotePort
                    Dim llip As String = CType(tcpmarshalIPv6.internalSocket, NetTCPListener).localIPAddress
                    Dim llport As Integer = CType(tcpmarshalIPv6.internalSocket, NetTCPListener).localPort
                    Dim tnom As String = getResvSetName(lip, lport)
                    Dim mpm As voip.MessagePassMode = getResvSetPM(lip, lport)
                    If tnom <> "" Then remResvSet(lip, lport)
                    If lip = llip And lport = llport Then
                        cl = New Client(New Contact(rip, rport, IPVersion.IPv6, AddressableType.TCP) With {.messagePassMode = mpm}, clm) With {.name = rip & ":" & rport, .myAddress = rip, .myPort = rport}
                    Else
                        If tnom = "" Then
                            tnom = rip & ":" & rport
                        End If
                        cl = New Client(New Contact(lip, lport, IPVersion.IPv4, AddressableType.TCP) With {.messagePassMode = mpm}, clm) With {.name = tnom, .myAddress = rip, .myPort = rport}
                    End If
                    addCl(cl)
                    addStrm(cl.stream)
                End If
            End If
        End If
    End Sub

    Private Sub discon(ip As String, port As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() discon(ip, port))
        Else
            Dim cl As Client = retRegCl(ip, port)
            If cl IsNot Nothing Then
                Dim strm As Streamer = cl.stream
                If Not strm Is Nothing Then _
                    remStrm(strm)
                If TCP_remove_disconnected_clients Then
                    remCl(cl)
                Else
                    upCl(cl)
                End If
            End If
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
                If c.marshal.duplicatedInternalSocketConfig.remoteIPAddress = ip And c.marshal.duplicatedInternalSocketConfig.remotePort = port Then
                    toret = c
                    Exit For
                End If
            End If
        Next
        Return toret
    End Function

    'TCP Client Reserved Settings Management

    Public Function getResvSetName(ip As String, port As Integer) As String
        Dim toret As String = ""
        For Each c As Tuple(Of String, Integer, String, voip.MessagePassMode) In tcpResvSetReg
            If c.Item1 = ip And c.Item2 = port Then
                toret = c.Item3
                Exit For
            End If
        Next
        Return toret
    End Function

    Public Function getResvSetPM(ip As String, port As Integer) As voip.MessagePassMode
        Dim toret As voip.MessagePassMode = MessagePassMode.Bidirectional
        For Each c As Tuple(Of String, Integer, String, voip.MessagePassMode) In tcpResvSetReg
            If c.Item1 = ip And c.Item2 = port Then
                toret = c.Item4
                Exit For
            End If
        Next
        Return toret
    End Function

    Public Sub remResvSet(ip As String, port As Integer)
        For i As Integer = 0 To tcpResvSetReg.Count - 1 Step 1
            Dim c As Tuple(Of String, Integer, String, voip.MessagePassMode) = tcpResvSetReg(i)
            If c.Item1 = ip And c.Item2 = port Then
                tcpResvSetReg.RemoveAt(i)
                Exit For
            End If
        Next
    End Sub

    'Stream Management

    Private slockstrm As New Object()

    Public Sub addStrm(strm As Streamer)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() addStrm(strm))
        Else
            SyncLock slockstrm
                streams.Add(strm)
                Dim lvi As New ListViewItem(strm.name)
                lvi.SubItems.Add(strm.muted)
                lvi.SubItems.Add(strm.volume * 100)
                ListViewsc.Items.Add(lvi)
            End SyncLock
        End If
    End Sub

    Public Function indxStrm(strm As Streamer) As Integer
        Dim toret As Integer = -1
        SyncLock slockstrm
            toret = streams.IndexOf(strm)
        End SyncLock
        Return toret
    End Function

    Public Function indxStrm(indx As Integer) As Streamer
        Dim toret As Streamer = Nothing
        SyncLock slockstrm
            If indx > -1 And indx < streams.Count Then _
                toret = streams(indx)
        End SyncLock
        Return toret
    End Function

    Public Sub upStrm(strm As Streamer)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() upStrm(strm))
        Else
            SyncLock slockstrm
                Dim indx As Integer = indxStrm(strm)
                If indx > -1 Then
                    Dim lvi As New ListViewItem(strm.name)
                    lvi.SubItems.Add(strm.muted)
                    lvi.SubItems.Add(strm.volume * 100)
                    ListViewsc.Items(indx) = lvi
                End If
            End SyncLock
        End If
    End Sub

    Public Sub remStrm(strm As Streamer)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() remStrm(strm))
        Else
            SyncLock slockstrm
                Dim indx As Integer = indxStrm(strm)
                If indx > -1 Then
                    ListViewsc.Items.RemoveAt(indx)
                    streams.RemoveAt(indx)
                End If
            End SyncLock
        End If
    End Sub

    'Client Management

    Private slockCl As New Object()

    Public Sub addCl(Cl As Client)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() addCl(Cl))
        Else
            SyncLock slockCl
                clients.Add(Cl)
                Dim lvi As New ListViewItem(Cl.name)
                If Cl.type = AddressableType.TCP Then
                    lvi.SubItems.Add(Cl.myAddress)
                    lvi.SubItems.Add(Cl.myPort)
                    lvi.SubItems.Add("TCP")
                    lvi.SubItems.Add(Cl.connected)
                ElseIf Cl.type = AddressableType.UDP Then
                    lvi.SubItems.Add(Cl.targetAddress)
                    lvi.SubItems.Add(Cl.targetPort)
                    lvi.SubItems.Add("UDP")
                    lvi.SubItems.Add("True")
                End If
                ListViewcl.Items.Add(lvi)
            End SyncLock
        End If
    End Sub

    Public Function indxCl(Cl As Client) As Integer
        Dim toret As Integer = -1
        SyncLock slockCl
            toret = clients.IndexOf(Cl)
        End SyncLock
        Return toret
    End Function

    Public Function indxCl(indx As Integer) As Client
        Dim toret As Client = Nothing
        SyncLock slockCl
            If indx > -1 And indx < clients.Count Then _
                toret = clients(indx)
        End SyncLock
        Return toret
    End Function

    Public Sub upCl(Cl As Client)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() upCl(Cl))
        Else
            SyncLock slockCl
                Dim indx As Integer = indxCl(Cl)
                If indx > -1 Then
                    Dim lvi As New ListViewItem(Cl.name)
                    If Cl.type = AddressableType.TCP Then
                        lvi.SubItems.Add(Cl.myAddress)
                        lvi.SubItems.Add(Cl.myPort)
                        lvi.SubItems.Add("TCP")
                        lvi.SubItems.Add(Cl.connected)
                    ElseIf Cl.type = AddressableType.UDP Then
                        lvi.SubItems.Add(Cl.targetAddress)
                        lvi.SubItems.Add(Cl.targetPort)
                        lvi.SubItems.Add("UDP")
                        lvi.SubItems.Add("True")
                    End If
                    ListViewcl.Items(indx) = lvi
                End If
            End SyncLock
        End If
    End Sub

    Public Sub remCl(Cl As Client)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() remCl(Cl))
        Else
            SyncLock slockCl
                Dim indx As Integer = indxCl(Cl)
                If indx > -1 Then
                    ListViewcl.Items.RemoveAt(indx)
                    clients.RemoveAt(indx)
                End If
            End SyncLock
        End If
    End Sub

    'Contact Management

    Private slockCon As New Object()

    Public Sub addCon(Con As Contact)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() addCon(Con))
        Else
            SyncLock slockCon
                contacts.Add(Con)
                Dim lvi As New ListViewItem(Con.name)
                lvi.SubItems.Add(Con.targetAddress)
                lvi.SubItems.Add(Con.targetPort)
                If Con.type = AddressableType.TCP Then
                    lvi.SubItems.Add("TCP")
                ElseIf Con.type = AddressableType.UDP Then
                    lvi.SubItems.Add("UDP")
                End If
                ListViewcl2.Items.Add(lvi)
            End SyncLock
        End If
    End Sub

    Public Function indxCon(Con As Contact) As Integer
        Dim toret As Integer = -1
        SyncLock slockCon
            toret = contacts.IndexOf(Con)
        End SyncLock
        Return toret
    End Function

    Public Function indxCon(indx As Integer) As Contact
        Dim toret As Contact = Nothing
        SyncLock slockCon
            If indx > -1 And indx < contacts.Count Then _
                toret = contacts(indx)
        End SyncLock
        Return toret
    End Function

    Public Sub upCon(Con As Contact)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() upCon(Con))
        Else
            SyncLock slockCon
                Dim indx As Integer = indxCon(Con)
                If indx > -1 Then
                    Dim lvi As New ListViewItem(Con.name)
                    lvi.SubItems.Add(Con.targetAddress)
                    lvi.SubItems.Add(Con.targetPort)
                    If Con.type = AddressableType.TCP Then
                        lvi.SubItems.Add("TCP")
                    ElseIf Con.type = AddressableType.UDP Then
                        lvi.SubItems.Add("UDP")
                    End If
                    ListViewcl2.Items(indx) = lvi
                End If
            End SyncLock
        End If
    End Sub

    Public Sub remCon(Con As Contact)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() remCon(Con))
        Else
            SyncLock slockCon
                Dim indx As Integer = indxCon(Con)
                If indx > -1 Then
                    ListViewcl2.Items.RemoveAt(indx)
                    contacts.RemoveAt(indx)
                End If
            End SyncLock
        End If
    End Sub
End Class