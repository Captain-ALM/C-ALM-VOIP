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

    Private Sub MainProgram_Load(sender As Object, e As EventArgs) Handles Me.Load
        clientreg = New ListViewedRegistry(Of Client)(ListViewcl)
        contactreg = New ListViewedRegistry(Of Contact)(ListViewcl2)
        streamreg = New ListViewedRegistry(Of Streamer)(ListViewsc)
        settings = loadSettings()
        lContacts()
    End Sub

    Private Sub lContacts()
        Dim cs As Contact() = loadContacts().contacts
        contactreg.clear()
        For Each c As Contact In cs
            addContact(c)
        Next
    End Sub

    Private Sub sContacts()
        saveContacts(New Contacts() With {.contacts = contactreg.internalList.toArray()})
    End Sub

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
            wp.addEvent(New WorkerEvent(butabout, New Object() {Me}, ETs.Click, e))
        End If
    End Sub

    Private Sub butrconf_Click(sender As Object, e As EventArgs) Handles butrconf.Click
        If ue Then
            If InListening Then
                wp.showForm(Of Configure)(0, Me)
                While Not configfin
                    Threading.Thread.Sleep(125)
                End While
                saveSettings(settings)
                advToAllClients()
            End If
            wp.addEvent(New WorkerEvent(butrconf, New Object() {Me}, ETs.Click, e))
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
            wp.addEvent(New WorkerEvent(butrset, New Object() {Me}, ETs.Click, e))
        End If
    End Sub

    Private Sub butstp_Click(sender As Object, e As EventArgs) Handles butstp.Click
        If ue Then
            wp.addEvent(New WorkerEvent(butstp, New Object() {Me}, ETs.Click, e))
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
                generateClient(caddrbs)
                editsuccess = False
            End If
            ceditm = EditorMode.None
            caddrbs = Nothing
            wp.addEvent(New WorkerEvent(butcladd, New Object() {Me}, ETs.Click, e))
        End If
    End Sub

    Private Sub butclrem_Click(sender As Object, e As EventArgs) Handles butclrem.Click
        If ue Then
            clientreg.updateCachedIndices()
            If clientreg.selectedIndices.Count > 0 Then
                removeClient(clientreg(clientreg.selectedIndices(0)), True, True)
            End If
            wp.addEvent(New WorkerEvent(butclrem, New Object() {Me}, ETs.Click, e))
        End If
    End Sub

    Private Sub butclcreatec_Click(sender As Object, e As EventArgs) Handles butclcreatec.Click
        If ue Then
            clientreg.updateCachedIndices()
            If clientreg.selectedIndices.Count > 0 Then addContact(clientreg(clientreg.selectedIndices(0)).duplicateToNew())
            wp.addEvent(New WorkerEvent(butclcreatec, New Object() {Me}, ETs.Click, e))
        End If
    End Sub

    Private Sub butclccls_Click(sender As Object, e As EventArgs) Handles butclccls.Click
        If ue Then
            clearAllClients()
            wp.addEvent(New WorkerEvent(butclccls, New Object() {Me}, ETs.Click, e))
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
                addContact(caddrbs)
                editsuccess = False
            End If
            ceditm = EditorMode.None
            caddrbs = Nothing
            wp.addEvent(New WorkerEvent(butcl2add, New Object() {Me}, ETs.Click, e))
        End If
    End Sub

    Private Sub butcl2rem_Click(sender As Object, e As EventArgs) Handles butcl2rem.Click
        If ue Then
            contactreg.updateCachedIndices()
            If contactreg.selectedIndices.Count > 0 Then
                contactreg.remove(contactreg(contactreg.selectedIndices(0)))
            End If
            wp.addEvent(New WorkerEvent(butcl2rem, New Object() {Me}, ETs.Click, e))
        End If
    End Sub

    Private Sub butcl2editc_Click(sender As Object, e As EventArgs) Handles butcl2editc.Click
        If ue Then
            contactreg.updateCachedIndices()
            If contactreg.selectedIndices.Count > 0 Then
                editfin = False
                ceditm = EditorMode.EditContact
                caddrbs = contactreg(contactreg.selectedIndices(0))
                wp.showForm(Of Editor)(0, Me)
                While Not editfin
                    Threading.Thread.Sleep(125)
                End While
                If editsuccess Then
                    caddrbs.updateLVI(True)
                    editsuccess = False
                End If
                ceditm = EditorMode.None
                caddrbs = Nothing
            End If
            wp.addEvent(New WorkerEvent(butcl2editc, New Object() {Me}, ETs.Click, e))
        End If
    End Sub

    Private Sub butcl2ccls_Click(sender As Object, e As EventArgs) Handles butcl2ccls.Click
        If ue Then
            contactreg.clear()
            wp.addEvent(New WorkerEvent(butcl2ccls, New Object() {Me}, ETs.Click, e))
        End If
    End Sub

    Private Sub ListViewsc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewsc.SelectedIndexChanged
        If ue Then
            streamreg.updateCachedIndices()
            If streamreg.selectedIndices.Count > 0 Then
                Dim strm As Streamer = streamreg(streamreg.selectedIndices(0))
                TrackBarvol.Value = strm.volume * 100
            End If
            wp.addEvent(New WorkerEvent(ListViewsc, New Object() {Me}, ETs.SelectedIndexChanged, New EventArgsDataContainer(ListViewsc.SelectedIndices)))
        End If
    End Sub

    Private Sub butscmutes_Click(sender As Object, e As EventArgs) Handles butscmutes.Click
        If ue Then
            muteSelectedStreamer(True)
            wp.addEvent(New WorkerEvent(butscmutes, New Object() {Me}, ETs.Click, e))
        End If
    End Sub

    Private Sub butscunmutes_Click(sender As Object, e As EventArgs) Handles butscunmutes.Click
        If ue Then
            muteSelectedStreamer(False)
            wp.addEvent(New WorkerEvent(butscunmutes, New Object() {Me}, ETs.Click, e))
        End If
    End Sub

    Private Sub TrackBarvol_ValueChanged(sender As Object, e As EventArgs) Handles TrackBarvol.ValueChanged
        If ue Then
            streamreg.updateCachedIndices()
            If streamreg.selectedIndices.Count > 0 Then
                Dim strm As Streamer = streamreg(streamreg.selectedIndices(0))
                strm.volume = TrackBarvol.Value / 100
                strm.updateLVI(True)
            End If
            If NumericUpDownvol.Value <> TrackBarvol.Value Then NumericUpDownvol.Value = TrackBarvol.Value
            wp.addEvent(New WorkerEvent(TrackBarvol, New Object() {Me}, ETs.ValueChanged, New EventArgsDataContainer(TrackBarvol.Value)))
        End If
    End Sub

    Private Sub NumericUpDownvol_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownvol.ValueChanged
        If ue Then
            If NumericUpDownvol.Value <> TrackBarvol.Value Then TrackBarvol.Value = NumericUpDownvol.Value
            wp.addEvent(New WorkerEvent(NumericUpDownvol, New Object() {Me}, ETs.ValueChanged, New EventArgsDataContainer(NumericUpDownvol.Value)))
        End If
    End Sub

    Private Sub butclviewc_Click(sender As Object, e As EventArgs) Handles butclviewc.Click
        If ue Then
            clientreg.updateCachedIndices()
            If clientreg.selectedIndices.Count > 0 Then
                editfin = False
                ceditm = EditorMode.EditClient
                caddrbs = clientreg(clientreg.selectedIndices(0))
                wp.showForm(Of Editor)(0, Me)
                While Not editfin
                    Threading.Thread.Sleep(125)
                End While
                If editsuccess Then
                    caddrbs.updateLVI(True)
                    If Not CType(caddrbs, Client).stream Is Nothing Then CType(caddrbs, Client).stream.updateLVI(True)
                    editsuccess = False
                End If
                ceditm = EditorMode.None
                caddrbs = Nothing
            End If
            wp.addEvent(New WorkerEvent(butclviewc, New Object() {Me}, ETs.Click, e))
        End If
    End Sub

    Private Sub butcl2cc_Click(sender As Object, e As EventArgs) Handles butcl2cc.Click
        If ue Then
            contactreg.updateCachedIndices()
            If contactreg.selectedIndices.Count > 0 Then
                generateClient(contactreg(contactreg.selectedIndices(0)))
            End If
            wp.addEvent(New WorkerEvent(butcl2cc, New Object() {Me}, ETs.Click, e))
        End If
    End Sub

    'Marshal Management

    Private Sub engage()
        saveSettings(settings)
        lContacts()
        micVOIP = New VOIPSender() With {.samplebuffersize = settings.samplerate * (settings.buffmdmsecs / 1000) * 2}
        spkVOIP = New VOIPReceiver()
        If Not settings.selected_interfaceIPv4.Equals(IPAddress.None) Then
            If settings.port_TCP_IPv4 <> 0 Then
                tcpmarshalIPv4 = New NetMarshalTCP(settings.selected_interfaceIPv4, settings.port_TCP_IPv4, settings.TCP_backlog, settings.TCP_delay) With {.beatTimeout = settings.TCP_beat_timeout, .serializer = settings.gserializer, .bufferSize = 65000}
                AddHandler tcpmarshalIPv4.clientConnected, AddressOf conIPv4
                AddHandler tcpmarshalIPv4.clientDisconnected, AddressOf discon
                tcpmarshalIPv4.start()
                InListening = True
            End If
            If settings.port_UDP_IPv4 <> 0 Then
                udpmarshalIPv4 = New NetMarshalUDP(settings.selected_interfaceIPv4, settings.port_UDP_IPv4) With {.serializer = settings.gserializer, .bufferSize = 65000}
                AddHandler udpmarshalIPv4.MessageReceived, AddressOf msgRecIPv4
                udpmarshalIPv4.start()
                InListening = True
            End If
        End If
        If Not settings.selected_interfaceIPv6 Is Nothing Then
            If settings.port_TCP_IPv6 <> 0 Then
                tcpmarshalIPv6 = New NetMarshalTCP(settings.selected_interfaceIPv6, settings.port_TCP_IPv6, settings.TCP_backlog, settings.TCP_delay) With {.beatTimeout = settings.TCP_beat_timeout, .serializer = settings.gserializer, .bufferSize = 65000}
                AddHandler tcpmarshalIPv6.clientConnected, AddressOf conIPv6
                AddHandler tcpmarshalIPv6.clientDisconnected, AddressOf discon
                tcpmarshalIPv6.start()
                InListening = True
            End If
            If settings.port_UDP_IPv6 <> 0 Then
                udpmarshalIPv6 = New NetMarshalUDP(settings.selected_interfaceIPv6, settings.port_UDP_IPv6) With {.serializer = settings.gserializer, .bufferSize = 65000}
                AddHandler udpmarshalIPv6.MessageReceived, AddressOf msgRecIPv6
                udpmarshalIPv6.start()
                InListening = True
            End If
        End If
        streamreg.add(micVOIP.streamer)
        If InListening Then
            lblstatus.Text = "Listening."
        Else
            lblstatus.Text = "Not Listening."
        End If
    End Sub

    Private Sub disengage()
        streamreg.remove(micVOIP.streamer)
        clearAllClients()
        streamreg.clear()
        clientreg.clear()
        If Not tcpmarshalIPv4 Is Nothing Then
            RemoveHandler tcpmarshalIPv4.clientConnected, AddressOf conIPv4
            RemoveHandler tcpmarshalIPv4.clientDisconnected, AddressOf discon
            tcpmarshalIPv4 = terminateMarshal(tcpmarshalIPv4)
        End If
        If Not tcpmarshalIPv6 Is Nothing Then
            RemoveHandler tcpmarshalIPv6.clientConnected, AddressOf conIPv6
            RemoveHandler tcpmarshalIPv6.clientDisconnected, AddressOf discon
            tcpmarshalIPv6 = terminateMarshal(tcpmarshalIPv6)
        End If
        If Not udpmarshalIPv4 Is Nothing Then
            RemoveHandler udpmarshalIPv4.MessageReceived, AddressOf msgRecIPv4
            udpmarshalIPv4 = terminateMarshal(udpmarshalIPv4)
        End If
        If Not udpmarshalIPv6 Is Nothing Then
            RemoveHandler udpmarshalIPv6.MessageReceived, AddressOf msgRecIPv6
            udpmarshalIPv6 = terminateMarshal(udpmarshalIPv6)
        End If
        InListening = False
        micVOIP.Dispose()
        micVOIP = Nothing
        spkVOIP.Dispose()
        spkVOIP = Nothing
        settings = loadSettings()
        sContacts()
    End Sub

    Private Function terminateMarshal(marshalIn As NetMarshalBase) As NetMarshalBase
        If marshalIn.ready Then marshalIn.close()
        Return Nothing
    End Function

    'Connection Management

    Private Sub msgRecIPv4(msg As IPacket)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() msgRecIPv4(msg))
        Else
            Dim cl As Client = returnFirstItemOrNothing(Of Client)(clientreg.find(New MClient(msg.senderIP, msg.senderPort)))
            If cl Is Nothing Then
                cl = New Client(New Contact(returnFirstItemOrNothing(Of IPAddress)(resolve(msg.senderIP, Sockets.AddressFamily.InterNetwork)).ToString(), msg.senderPort, IPVersion.IPv4, AddressableType.UDP) With {.messagePassMode = MessagePassMode.Bidirectional}, udpmarshalIPv4) With {.myAddress = settings.external_Address_IPv4, .myPort = settings.external_UDP_Port_IPv4, .name = msg.senderIP & ":" & msg.senderPort}
                addClient(cl, msg)
            End If
        End If
    End Sub

    Private Sub msgRecIPv6(msg As IPacket)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() msgRecIPv6(msg))
        Else
            Dim cl As Client = returnFirstItemOrNothing(Of Client)(clientreg.find(New MClient(msg.senderIP, msg.senderPort)))
            If cl Is Nothing Then
                cl = New Client(New Contact(returnFirstItemOrNothing(Of IPAddress)(resolve(msg.senderIP, Sockets.AddressFamily.InterNetworkV6)).ToString(), msg.senderPort, IPVersion.IPv6, AddressableType.UDP) With {.messagePassMode = MessagePassMode.Bidirectional}, udpmarshalIPv6) With {.myAddress = settings.external_Address_IPv6, .myPort = settings.external_UDP_Port_IPv6, .name = msg.senderIP & ":" & msg.senderPort}
                addClient(cl, msg)
            End If
        End If
    End Sub

    Private Sub con(ip As String, port As Integer, ipv As IPVersion)
        Dim cl As Client = returnFirstItemOrNothing(Of Client)(clientreg.find(New MClient(ip, port)))
        If cl Is Nothing Then
            Dim clm As NetMarshalTCPClient = Nothing
            If ipv = IPVersion.IPv6 Then clm = tcpmarshalIPv6.client(ip, port) Else clm = tcpmarshalIPv4.client(ip, port)
            If Not clm.internalSocket Is Nothing AndAlso clm.ready Then
                Dim lip As String = CType(clm.internalSocket, NetTCPClient).listenerIPAddress
                Dim lport As Integer = CType(clm.internalSocket, NetTCPClient).listenerPort
                Dim rip As String = CType(clm.internalSocket, NetTCPClient).remoteIPAddress
                Dim rport As String = CType(clm.internalSocket, NetTCPClient).remotePort
                Dim llip As String = CType(tcpmarshalIPv4.internalSocket, NetTCPListener).localIPAddress
                Dim llport As Integer = CType(tcpmarshalIPv4.internalSocket, NetTCPListener).localPort
                If ipv = IPVersion.IPv6 Then
                    llip = CType(tcpmarshalIPv6.internalSocket, NetTCPListener).localIPAddress
                    llport = CType(tcpmarshalIPv6.internalSocket, NetTCPListener).localPort
                End If
                Dim tnom As String = getResvSetName(lip, lport)
                Dim mpm As voip.MessagePassMode = getResvSetPM(lip, lport)
                If tnom <> "" Then remResvSet(lip, lport)
                If lip = llip And lport = llport Then
                    cl = New Client(New Contact(rip, rport, ipv, AddressableType.TCP) With {.messagePassMode = mpm}, clm) With {.name = rip & ":" & rport, .myAddress = rip, .myPort = rport}
                Else
                    If tnom = "" Then
                        tnom = rip & ":" & rport
                    End If
                    cl = New Client(New Contact(lip, lport, ipv, AddressableType.TCP) With {.messagePassMode = mpm}, clm) With {.name = tnom, .myAddress = rip, .myPort = rport}
                End If
                addClient(cl, Nothing)
            End If
        End If
    End Sub

    Private Sub conIPv4(ip As String, port As Integer)
        If Me.InvokeRequired Then Me.Invoke(Sub() con(ip, port, IPVersion.IPv4)) Else con(ip, port, IPVersion.IPv4)
    End Sub

    Private Sub conIPv6(ip As String, port As Integer)
        If Me.InvokeRequired Then Me.Invoke(Sub() con(ip, port, IPVersion.IPv6)) Else con(ip, port, IPVersion.IPv6)
    End Sub

    Private Sub discon(ip As String, port As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() discon(ip, port))
        Else
            removeClient(returnFirstItemOrNothing(Of Client)(clientreg.find(New MClient(ip, port))), settings.TCP_remove_disconnected_clients, False)
        End If
    End Sub

    'Client Management

    Private Sub generateClient(adbIn As AddressableBase)
        If adbIn.name = "" Then adbIn.name = adbIn.targetAddress & ":" & adbIn.targetPort
        If adbIn.type <> AddressableType.Block Then
            If adbIn.targetIPVersion = IPVersion.IPv6 Then
                CType(adbIn, Contact).targetAddress = returnFirstItemOrNothing(Of IPAddress)(resolve(adbIn.targetAddress, Net.Sockets.AddressFamily.InterNetworkV6)).ToString()
            Else
                CType(adbIn, Contact).targetAddress = returnFirstItemOrNothing(Of IPAddress)(resolve(adbIn.targetAddress, Net.Sockets.AddressFamily.InterNetwork)).ToString()
            End If
        End If
        If clientreg.find(New MClient(adbIn.targetAddress, adbIn.targetPort)).Length <> 0 Then Return
        If adbIn.type = AddressableType.TCP Then
            If adbIn.targetIPVersion = IPVersion.IPv4 And Not tcpmarshalIPv4 Is Nothing Then
                Dim tpl As New Tuple(Of String, Integer, String, voip.MessagePassMode)(adbIn.targetAddress, adbIn.targetPort, adbIn.name, adbIn.messagePassMode)
                tcpResvSetReg.Add(tpl)
                If Not tcpmarshalIPv4.connect(adbIn.targetAddress, adbIn.targetPort) Then tcpResvSetReg.Remove(tpl)
            ElseIf adbIn.targetIPVersion = IPVersion.IPv6 And Not tcpmarshalIPv6 Is Nothing Then
                Dim tpl As New Tuple(Of String, Integer, String, voip.MessagePassMode)(adbIn.targetAddress, adbIn.targetPort, adbIn.name, adbIn.messagePassMode)
                tcpResvSetReg.Add(tpl)
                If Not tcpmarshalIPv6.connect(adbIn.targetAddress, adbIn.targetPort) Then tcpResvSetReg.Remove(tpl)
            End If
        ElseIf adbIn.type = AddressableType.UDP Then
            If adbIn.targetIPVersion = IPVersion.IPv4 And Not udpmarshalIPv4 Is Nothing Then
                If adbIn.myAddress = "" Then adbIn.myAddress = settings.external_Address_IPv4
                If adbIn.myPort = 0 Then adbIn.myPort = settings.external_UDP_Port_IPv4
                addClient(New Client(adbIn, udpmarshalIPv4), Nothing)
            ElseIf adbIn.targetIPVersion = IPVersion.IPv6 And Not udpmarshalIPv6 Is Nothing Then
                If adbIn.myAddress = "" Then adbIn.myAddress = settings.external_Address_IPv6
                If adbIn.myPort = 0 Then adbIn.myPort = settings.external_UDP_Port_IPv6
                addClient(New Client(adbIn, udpmarshalIPv6), Nothing)
            End If
        ElseIf adbIn.type = AddressableType.Block Then
            Dim arr As IPAddress() = Nothing
            If adbIn.targetIPVersion = IPVersion.IPv6 Then arr = resolve(adbIn.targetAddress, Net.Sockets.AddressFamily.InterNetworkV6) Else arr = resolve(adbIn.targetAddress, Net.Sockets.AddressFamily.InterNetwork)
            For Each c As IPAddress In arr
                addClient(New BlockClient(adbIn, c.ToString()) With {.messagePassMode = MessagePassMode.Disable}, Nothing)
            Next
        End If
    End Sub

    Private Sub addClient(clIn As Client, forceData As IPacket)
        If clientreg.find(New MClient(clIn.targetAddress, clIn.targetPort)).Length = 0 Then
            clIn.sendAdvertisement()
            clientreg.add(clIn)
            If Not forceData Is Nothing Then clIn.forceReceive(forceData)
            If Not clIn.stream Is Nothing Then streamreg.add(clIn.stream)
        End If
    End Sub

    Private Sub addContact(adbIn As AddressableBase)
        If contactreg.find(New MContact(adbIn.targetAddress, adbIn.targetPort, adbIn.type)).Length = 0 Then contactreg.add(adbIn)
    End Sub

    Private Sub removeClient(cl As Client, removeClient As Boolean, stopClient As Boolean)
        If cl IsNot Nothing Then
            Dim strm As Streamer = cl.stream
            If Not strm Is Nothing Then streamreg.remove(strm)
            If stopClient Then cl.stop()
            If removeClient Then
                clientreg.remove(cl)
            Else
                cl.updateLVI(True)
            End If
        End If
    End Sub

    Private Sub advToAllClients()
        For i As Integer = clientreg.count - 1 To 0 Step -1
            clientreg(i).sendAdvertisement()
        Next
    End Sub

    Private Sub clearAllClients()
        For i As Integer = clientreg.count - 1 To 0 Step -1
            removeClient(clientreg(i), True, True)
        Next
    End Sub

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

    'Audio Management

    Private Sub muteSelectedStreamer(isMuted As Boolean)
        streamreg.updateCachedIndices()
        If streamreg.selectedIndices.Count > 0 Then
            mmuteStreamer(streamreg(streamreg.selectedIndices(0)), isMuted)
        End If
    End Sub

    Private Sub mmuteStreamer(strm As Streamer, isMuted As Boolean)
        strm.muted = isMuted
        strm.updateLVI(True)
    End Sub
End Class