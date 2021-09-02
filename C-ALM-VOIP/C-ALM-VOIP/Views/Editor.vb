Imports System.Windows.Forms
Imports captainalm.workerpumper
Imports System.Net

Public Class Editor
    Implements IWorkerPumpReceiver

    Private formClosingDone As Boolean = False
    Private formClosedDone As Boolean = False
    Private wp As WorkerPump = Nothing
    Private ue As Boolean = False
    Private slockchker As New Object()
    Private showing As Boolean = False

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

    Private Sub Editor_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If Not formClosedDone Then
            whenClosed()
            If ue Then wp.addEvent(New WorkerEvent(Me, ETs.Closed, e))
            formClosedDone = True
        End If
    End Sub

    Public Sub whenClosed()
        If DialogResult <> Windows.Forms.DialogResult.OK Then
            editfin = True
        End If
    End Sub

    Private Sub Editor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not formClosingDone Then
            SyncLock slockchker
                showing = False
                If Me.Visible Then
                    'If close button pressed
                    e.Cancel = True
                    Me.Hide()
                    If Me.DialogResult = Windows.Forms.DialogResult.None Then Me.DialogResult = Windows.Forms.DialogResult.Cancel
                End If
                If ue Then wp.addEvent(New WorkerEvent(Me, ETs.Closing, e))
                Me.OnFormClosed(New FormClosedEventArgs(e.CloseReason))
                formClosingDone = True
            End SyncLock
        End If
    End Sub

#Region "closeOverride"
    Public Shadows Sub Close()
        Me.Hide()
        Me.OnFormClosing(New FormClosingEventArgs(CloseReason.UserClosing, False))
        If Me.DialogResult = Windows.Forms.DialogResult.None Then Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
#End Region

    Private Sub Editor_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.DialogResult = Windows.Forms.DialogResult.None
        formClosingDone = False
        formClosedDone = False
        txtbxaddr.BackColor = Color.White
        nudport.BackColor = Color.White
        nudmyport.BackColor = Color.White
        OK_Button.Enabled = True
        Cancel_Button.Enabled = True
        editfin = True
        If ue Then wp.addEvent(Me, ETs.Shown, e)
        While editfin
            Threading.Thread.Sleep(125)
        End While
        SyncLock slockchker
            'Begin Population
            Label7.Text = "My Address:"
            Label8.Text = "My Port:"
            If ceditm = EditorMode.Create Or ceditm = EditorMode.EditContact Then
                cmbxipv.Enabled = True
                cmbxtype.Enabled = True
                txtbxaddr.Enabled = True
                txtbxaddr.ReadOnly = False
                nudport.Enabled = True
                nudport.ReadOnly = False
                nudport.Controls(0).Enabled = True
                txtbxmyaddr.ReadOnly = False
                nudmyport.ReadOnly = False
                nudmyport.Controls(0).Enabled = True
                If caddrbs.type = AddressableType.UDP Then
                    txtbxmyaddr.Text = settings.external_Address_IPv4
                    nudmyport.Value = settings.external_UDP_Port_IPv4
                    txtbxmyaddr.Enabled = True
                    nudmyport.Enabled = True
                    cmbxstrmode.Enabled = True
                ElseIf caddrbs.type = AddressableType.TCP Then
                    txtbxmyaddr.Enabled = False
                    nudmyport.Enabled = False
                    cmbxstrmode.Enabled = True
                Else
                    txtbxmyaddr.Enabled = False
                    nudmyport.Enabled = False
                    cmbxstrmode.Enabled = False
                End If
                If ceditm = EditorMode.Create Then
                    Text = "Create:"
                    Label1.Text = "Create:"
                ElseIf ceditm = EditorMode.EditContact Then
                    Text = "Edit Contact:"
                    Label1.Text = "Edit Contact:"
                End If
            ElseIf ceditm = EditorMode.EditBlocker Or ceditm = EditorMode.EditClient Then
                cmbxipv.Enabled = False
                cmbxtype.Enabled = False
                txtbxaddr.Enabled = True
                txtbxaddr.ReadOnly = True
                nudport.Enabled = True
                nudport.ReadOnly = True
                nudport.Controls(0).Enabled = False
                If ceditm = EditorMode.EditClient Then
                    If caddrbs.type = AddressableType.TCP And TypeOf caddrbs Is Client Then
                        Label7.Text = "Actual Address:"
                        Label8.Text = "Actual Port:"
                        txtbxmyaddr.Text = CType(caddrbs, Client).advertisedAddress
                        nudmyport.Value = CType(caddrbs, Client).advertisedPort
                    Else
                        txtbxmyaddr.Text = caddrbs.myAddress
                        nudmyport.Value = caddrbs.myPort
                    End If
                    txtbxmyaddr.Enabled = True
                    nudmyport.Enabled = True
                    cmbxstrmode.Enabled = True
                    Text = "Edit Client:"
                    Label1.Text = "Edit Client:"
                ElseIf ceditm = EditorMode.EditBlocker Then
                    txtbxmyaddr.Text = ""
                    nudmyport.Value = 0
                    txtbxmyaddr.Enabled = False
                    nudmyport.Enabled = False
                    cmbxstrmode.Enabled = False
                    Text = "Edit Blocker:"
                    Label1.Text = "Edit Blocker:"
                End If
            End If
            nudport.Value = caddrbs.targetPort
            txtbxaddr.Text = caddrbs.targetAddress
            txtbxname.Text = caddrbs.name
            cmbxipv.SelectedIndex = caddrbs.targetIPVersion - 1
            cmbxstrmode.SelectedIndex = caddrbs.messagePassMode
            cmbxtype.SelectedIndex = caddrbs.type - 1
            OK_Button.Select()
            'End Population
        End SyncLock
        showing = True
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

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        OK_Button.Enabled = False
        OK_Button.Select()
        If ue Then
            wp.addEvent(New WorkerEvent(OK_Button, New Object() {Me}, ETs.Click, New EventArgsDataContainer(Nothing)))
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Cancel_Button.Enabled = False
        Cancel_Button.Select()
        editfin = True
        If ue Then
            wp.addEvent(New WorkerEvent(Cancel_Button, New Object() {Me}, ETs.Click, New EventArgsDataContainer(Nothing)))
        End If
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub txtbxname_Leave(sender As Object, e As EventArgs) Handles txtbxname.Leave
        If ue Then
            wp.addEvent(New WorkerEvent(txtbxname, New Object() {Me}, ETs.Leave, New EventArgsDataContainer(txtbxname.Text)))
        End If
    End Sub

    Private Sub cmbxtype_Leave(sender As Object, e As EventArgs) Handles cmbxtype.Leave
        If ue Then
            updateIDTST()
            updateSelectedTypeIN()
            wp.addEvent(New WorkerEvent(cmbxtype, New Object() {Me}, ETs.Leave, New EventArgsDataContainer(cmbxtype.SelectedIndex)))
        End If
    End Sub

    Private Sub cmbxipv_Leave(sender As Object, e As EventArgs) Handles cmbxipv.Leave
        If ue Then
            updateSelectedTypeIN()
            wp.addEvent(New WorkerEvent(cmbxipv, New Object() {Me}, ETs.Leave, New EventArgsDataContainer(cmbxipv.SelectedIndex)))
        End If
    End Sub

    Private Sub txtbxaddr_Leave(sender As Object, e As EventArgs) Handles txtbxaddr.Leave
        If ue Then
            wp.addEvent(New WorkerEvent(txtbxaddr, New Object() {Me}, ETs.Leave, New EventArgsDataContainer(txtbxaddr.Text)))
        End If
    End Sub

    Private Sub nudport_Leave(sender As Object, e As EventArgs) Handles nudport.Leave
        If ue Then
            wp.addEvent(New WorkerEvent(nudport, New Object() {Me}, ETs.Leave, New EventArgsDataContainer(nudport.Value)))
        End If
    End Sub

    Private Sub txtbxmyaddr_Leave(sender As Object, e As EventArgs) Handles txtbxmyaddr.Leave
        If ue Then
            wp.addEvent(New WorkerEvent(txtbxmyaddr, New Object() {Me}, ETs.Leave, New EventArgsDataContainer(txtbxmyaddr.Text)))
        End If
    End Sub

    Private Sub nudmyport_Leave(sender As Object, e As EventArgs) Handles nudmyport.Leave
        If ue Then
            wp.addEvent(New WorkerEvent(nudmyport, New Object() {Me}, ETs.Leave, New EventArgsDataContainer(nudmyport.Value)))
        End If
    End Sub

    Private Sub cmbxstrmode_Leave(sender As Object, e As EventArgs) Handles cmbxstrmode.Leave
        If ue Then
            wp.addEvent(New WorkerEvent(cmbxstrmode, New Object() {Me}, ETs.Leave, New EventArgsDataContainer(cmbxstrmode.SelectedIndex)))
        End If
    End Sub

    Private Sub cmbxtype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbxtype.SelectedIndexChanged
        If ue Then
            updateIDTST()
            updateSelectedTypeIN()
            chkprtnum()
            wp.addEvent(New WorkerEvent(cmbxtype, New Object() {Me}, ETs.SelectedIndexChanged, New EventArgsDataContainer(cmbxtype.SelectedIndex)))
        End If
    End Sub

    Private Sub cmbxipv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbxipv.SelectedIndexChanged
        If ue Then
            updateSelectedTypeIN()
            wp.addEvent(New WorkerEvent(cmbxipv, New Object() {Me}, ETs.SelectedIndexChanged, New EventArgsDataContainer(cmbxipv.SelectedIndex)))
        End If
    End Sub

    Protected Sub updateIDTST()
        If cmbxtype.SelectedIndex + 1 = AddressableType.Block Then
            txtbxmyaddr.Enabled = False
            nudmyport.Enabled = False
            cmbxstrmode.Enabled = False
        ElseIf cmbxtype.SelectedIndex + 1 = AddressableType.TCP And ceditm <> EditorMode.EditClient Then
            txtbxmyaddr.Enabled = False
            nudmyport.Enabled = False
            cmbxstrmode.Enabled = True
        Else
            txtbxmyaddr.Enabled = True
            nudmyport.Enabled = True
            cmbxstrmode.Enabled = True
        End If
    End Sub

    Protected Sub updateSelectedTypeIN()
        If ceditm = EditorMode.Create Then
            If cmbxipv.SelectedIndex + 1 = IPVersion.IPv4 Then
                If cmbxtype.SelectedIndex + 1 = AddressableType.UDP Then
                    If udpmarshalIPv4 Is Nothing OrElse Not udpmarshalIPv4.ready Then
                        If tcpmarshalIPv4 Is Nothing OrElse Not tcpmarshalIPv4.ready Then
                            cmbxtype.SelectedIndex = AddressableType.Block - 1
                        Else
                            cmbxtype.SelectedIndex = AddressableType.TCP - 1
                        End If
                    Else
                        cmbxtype.SelectedIndex = AddressableType.UDP - 1
                    End If
                ElseIf cmbxtype.SelectedIndex + 1 = AddressableType.TCP Then
                    If tcpmarshalIPv4 Is Nothing OrElse Not tcpmarshalIPv4.ready Then
                        If udpmarshalIPv4 Is Nothing OrElse Not udpmarshalIPv4.ready Then
                            cmbxtype.SelectedIndex = AddressableType.Block - 1
                        Else
                            cmbxtype.SelectedIndex = AddressableType.UDP - 1
                        End If
                    Else
                        cmbxtype.SelectedIndex = AddressableType.TCP - 1
                    End If
                End If
            ElseIf cmbxipv.SelectedIndex + 1 = IPVersion.IPv6 Then
                If cmbxtype.SelectedIndex + 1 = AddressableType.UDP Then
                    If udpmarshalIPv6 Is Nothing OrElse Not udpmarshalIPv6.ready Then
                        If tcpmarshalIPv6 Is Nothing OrElse Not tcpmarshalIPv6.ready Then
                            cmbxtype.SelectedIndex = AddressableType.Block - 1
                        Else
                            cmbxtype.SelectedIndex = AddressableType.TCP - 1
                        End If
                    Else
                        cmbxtype.SelectedIndex = AddressableType.UDP - 1
                    End If
                ElseIf cmbxtype.SelectedIndex + 1 = AddressableType.TCP Then
                    If tcpmarshalIPv6 Is Nothing OrElse Not tcpmarshalIPv6.ready Then
                        If udpmarshalIPv6 Is Nothing OrElse Not udpmarshalIPv6.ready Then
                            cmbxtype.SelectedIndex = AddressableType.Block - 1
                        Else
                            cmbxtype.SelectedIndex = AddressableType.UDP - 1
                        End If
                    Else
                        cmbxtype.SelectedIndex = AddressableType.TCP - 1
                    End If
                End If
            End If
        End If
    End Sub

    Public Function chkipformat() As Boolean
        If ceditm = EditorMode.EditClient Or ceditm = EditorMode.EditBlocker Or Not showing Then Return True
        Dim toret As Boolean = False
        SyncLock slockchker
            Dim striphold As String
            If Me.InvokeRequired Then
                striphold = Me.Invoke(Function() As String
                                          Return txtbxaddr.Text
                                      End Function)
            Else
                striphold = txtbxaddr.Text
            End If
            Dim iphold As IPAddress = Nothing
            Dim ver As Integer
            If Me.InvokeRequired Then
                ver = Me.Invoke(Function() As Integer
                                    Return cmbxipv.SelectedIndex
                                End Function)
            Else
                ver = cmbxipv.SelectedIndex
            End If
            Try
                iphold = IPAddress.Parse(striphold)
                toret = True
            Catch ex As ArgumentNullException
                iphold = Nothing
            Catch ex As FormatException
                iphold = Nothing
            End Try
            If iphold Is Nothing Then
                Dim ipadd As IPAddress() = New IPAddress() {}
                Try
                    ipadd = Dns.GetHostAddresses(striphold)
                Catch ex As Sockets.SocketException
                Catch ex As ArgumentException
                End Try
                If ipadd.Length < 1 Then
                    setBackColor(txtbxaddr, Color.Orange)
                    toret = False
                Else
                    For Each ia As IPAddress In ipadd
                        If ia.AddressFamily = Sockets.AddressFamily.InterNetwork And ver = 0 Then
                            setBackColor(txtbxaddr, Color.White)
                            toret = True
                            Exit For
                        ElseIf ia.AddressFamily = Sockets.AddressFamily.InterNetworkV6 And ver = 1 Then
                            setBackColor(txtbxaddr, Color.White)
                            toret = True
                            Exit For
                        Else
                            setBackColor(txtbxaddr, Color.Orange)
                            toret = False
                        End If
                    Next
                End If
            Else
                If iphold.AddressFamily = Sockets.AddressFamily.InterNetwork And ver = 0 Then
                    setBackColor(txtbxaddr, Color.White)
                    toret = True
                ElseIf iphold.AddressFamily = Sockets.AddressFamily.InterNetworkV6 And ver = 1 Then
                    setBackColor(txtbxaddr, Color.White)
                    toret = True
                Else
                    setBackColor(txtbxaddr, Color.Orange)
                    toret = False
                End If
            End If
            If ceditm = EditorMode.EditContact Then toret = True
        End SyncLock
        Return toret
    End Function

    Public Function chkprtnum() As Boolean
        If Not showing Then Return True
        Dim toret As Boolean = False
        SyncLock slockchker
            Dim ctyp As AddressableType
            If Me.InvokeRequired Then
                ctyp = Me.Invoke(Function() As Integer
                                     Return cmbxtype.SelectedIndex + 1
                                 End Function)
            Else
                ctyp = cmbxtype.SelectedIndex + 1
            End If
            Dim iblkm As Boolean = (ceditm = EditorMode.EditBlocker) Or ((ceditm = EditorMode.Create Or ceditm = EditorMode.EditContact) And (ctyp = AddressableType.Block Or ctyp = AddressableType.TCP))
            Dim ptr1 As Integer
            If Me.InvokeRequired Then
                ptr1 = Me.Invoke(Function() As Integer
                                     Return nudport.Value
                                 End Function)
            Else
                ptr1 = nudport.Value
            End If
            Dim ptr2 As Integer
            If Me.InvokeRequired Then
                ptr2 = Me.Invoke(Function() As Integer
                                     Return nudmyport.Value
                                 End Function)
            Else
                ptr2 = nudmyport.Value
            End If
            If ptr1 < 1 And (Not iblkm) Then setBackColor(nudport, Color.Orange) Else setBackColor(nudport, Color.White)
            If ptr2 < 1 And (Not iblkm) Then setBackColor(nudmyport, Color.Orange) Else setBackColor(nudmyport, Color.White)
            toret = Not (ptr1 < 1 Or ptr2 < 1)
            If iblkm Then toret = True
        End SyncLock
        Return toret
    End Function

    Protected Sub setBackColor(ctrl As Control, bc As Color)
        If ctrl.InvokeRequired Then
            ctrl.Invoke(Sub() ctrl.BackColor = bc)
        Else
            ctrl.BackColor = bc
        End If
    End Sub
End Class
