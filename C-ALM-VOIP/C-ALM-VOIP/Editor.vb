Imports System.Windows.Forms
Imports captainalm.workerpumper
Imports System.Net

Public Class Editor
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

    Private Sub Editor_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.DialogResult = Windows.Forms.DialogResult.None
        formClosingDone = False
        formClosedDone = False
        txtbxaddr.BackColor = Color.White
        OK_Button.Enabled = True
        Cancel_Button.Enabled = True
        editfin = True
        If ue Then wp.addEvent(Me, ETs.Shown, e)
        While editfin
            Threading.Thread.Sleep(125)
        End While
        'Begin Population
        OK_Button.Select()
        If ceditm = EditorMode.Create Then
            Text = "Create:"
            Label1.Text = "Create:"
            nudport.Value = caddrbs.targetPort
            txtbxaddr.Text = caddrbs.targetAddress
            txtbxname.Text = caddrbs.name
            cmbxipv.SelectedIndex = caddrbs.targetIPVersion - 1
            cmbxstrmode.SelectedIndex = caddrbs.messagePassMode - 1
            cmbxtype.SelectedIndex = caddrbs.type - 1
            cmbxipv.Enabled = True
            cmbxstrmode.Enabled = True
            cmbxtype.Enabled = True
            txtbxaddr.Enabled = True
            txtbxaddr.ReadOnly = False
            nudport.Enabled = True
            nudport.ReadOnly = False
            nudport.Controls(0).Enabled = True
            txtbxmyaddr.ReadOnly = False
            nudmyport.ReadOnly = False
            nudmyport.Controls(0).Enabled = True
            If caddrbs.type = AddressableType.TCP Then
                txtbxmyaddr.Text = ""
                nudmyport.Value = 1
                txtbxmyaddr.Enabled = False
                nudmyport.Enabled = False
            ElseIf caddrbs.type = AddressableType.UDP Then
                txtbxmyaddr.Text = external_UDP_Address_IPv4
                nudmyport.Value = external_UDP_Port_IPv4
                txtbxmyaddr.Enabled = True
                nudmyport.Enabled = True
            End If
        ElseIf ceditm = EditorMode.EditClient Then
            Text = "View:"
            Label1.Text = "View:"
            nudport.Value = caddrbs.targetPort
            txtbxaddr.Text = caddrbs.targetAddress
            txtbxname.Text = caddrbs.name
            cmbxipv.SelectedIndex = caddrbs.targetIPVersion - 1
            cmbxstrmode.SelectedIndex = caddrbs.messagePassMode - 1
            cmbxtype.SelectedIndex = caddrbs.type - 1
            cmbxipv.Enabled = False
            cmbxstrmode.Enabled = False 'Next version changing this on a client will hopefully be allowed.
            cmbxtype.Enabled = False
            txtbxaddr.Enabled = True
            txtbxaddr.ReadOnly = True
            nudport.Enabled = True
            nudport.ReadOnly = True
            nudport.Controls(0).Enabled = False
            txtbxmyaddr.ReadOnly = True
            nudmyport.ReadOnly = True
            nudmyport.Controls(0).Enabled = False
            If caddrbs.type = AddressableType.TCP Then
                txtbxmyaddr.Text = ""
                nudmyport.Value = 1
                txtbxmyaddr.Enabled = False
                nudmyport.Enabled = False
            ElseIf caddrbs.type = AddressableType.UDP Then
                txtbxmyaddr.Text = caddrbs.myAddress
                nudmyport.Value = caddrbs.myPort
                txtbxmyaddr.Enabled = True
                nudmyport.Enabled = True
            End If
        ElseIf ceditm = EditorMode.EditContact Then
            Text = "Edit:"
            Label1.Text = "Edit:"
            nudport.Value = caddrbs.targetPort
            txtbxaddr.Text = caddrbs.targetAddress
            txtbxname.Text = caddrbs.name
            cmbxipv.SelectedIndex = caddrbs.targetIPVersion - 1
            cmbxstrmode.SelectedIndex = caddrbs.messagePassMode - 1
            cmbxtype.SelectedIndex = caddrbs.type - 1
            cmbxipv.Enabled = True
            cmbxstrmode.Enabled = True
            cmbxtype.Enabled = True
            txtbxaddr.Enabled = True
            txtbxaddr.ReadOnly = False
            nudport.Enabled = True
            nudport.ReadOnly = False
            nudport.Controls(0).Enabled = True
            txtbxmyaddr.ReadOnly = False
            nudmyport.ReadOnly = False
            nudmyport.Controls(0).Enabled = True
            If caddrbs.type = AddressableType.TCP Then
                txtbxmyaddr.Text = ""
                nudmyport.Value = 1
                txtbxmyaddr.Enabled = False
                nudmyport.Enabled = False
            ElseIf caddrbs.type = AddressableType.UDP Then
                txtbxmyaddr.Text = caddrbs.myAddress
                nudmyport.Value = caddrbs.myPort
                txtbxmyaddr.Enabled = True
                nudmyport.Enabled = True
            End If
        End If
        OK_Button.Select()
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

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If Not updat() Then Exit Sub
        OK_Button.Enabled = False
        OK_Button.Select()
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(OK_Button, l, ETs.Click, New EventArgsDataContainer(Nothing)))
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Cancel_Button.Enabled = False
        Cancel_Button.Select()
        editfin = True
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(Cancel_Button, l, ETs.Click, New EventArgsDataContainer(Nothing)))
        End If
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub txtbxname_Leave(sender As Object, e As EventArgs) Handles txtbxname.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(txtbxname, l, ETs.Leave, New EventArgsDataContainer(txtbxname.Text)))
        End If
    End Sub

    Private Sub cmbxtype_Leave(sender As Object, e As EventArgs) Handles cmbxtype.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(cmbxtype, l, ETs.Leave, New EventArgsDataContainer(cmbxtype.SelectedIndex)))
        End If
    End Sub

    Private Sub cmbxipv_Leave(sender As Object, e As EventArgs) Handles cmbxipv.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(cmbxipv, l, ETs.Leave, New EventArgsDataContainer(cmbxipv.SelectedIndex)))
        End If
        updat()
    End Sub

    Private Sub txtbxaddr_Leave(sender As Object, e As EventArgs) Handles txtbxaddr.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(txtbxaddr, l, ETs.Leave, New EventArgsDataContainer(txtbxaddr.Text)))
        End If
        updat()
    End Sub

    Private Sub nudport_Leave(sender As Object, e As EventArgs) Handles nudport.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nudport, l, ETs.Leave, New EventArgsDataContainer(nudport.Value)))
        End If
    End Sub

    Private Sub txtbxmyaddr_Leave(sender As Object, e As EventArgs) Handles txtbxmyaddr.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(txtbxmyaddr, l, ETs.Leave, New EventArgsDataContainer(txtbxmyaddr.Text)))
        End If
    End Sub

    Private Sub nudmyport_Leave(sender As Object, e As EventArgs) Handles nudmyport.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nudmyport, l, ETs.Leave, New EventArgsDataContainer(nudmyport.Value)))
        End If
    End Sub

    Private Sub cmbxstrmode_Leave(sender As Object, e As EventArgs) Handles cmbxstrmode.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(cmbxstrmode, l, ETs.Leave, New EventArgsDataContainer(cmbxstrmode.SelectedIndex)))
        End If
    End Sub

    Protected Function updat() As Boolean
        If ceditm = EditorMode.EditClient Then Return True
        Dim striphold As String = txtbxaddr.Text
        Dim iphold As IPAddress = Nothing
        Dim ver As Integer = -1
        ver = cmbxipv.SelectedIndex
        Try
            iphold = IPAddress.Parse(striphold)
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
                txtbxaddr.BackColor = Color.Orange
                Return False
            End If
            For Each ia As IPAddress In ipadd
                If ia.AddressFamily = Sockets.AddressFamily.InterNetwork And ver = 0 Then
                    txtbxaddr.BackColor = Color.White
                    Return True
                ElseIf ia.AddressFamily = Sockets.AddressFamily.InterNetworkV6 And ver = 1 Then
                    txtbxaddr.BackColor = Color.White
                    Return True
                Else
                    txtbxaddr.BackColor = Color.Orange
                    Return False
                End If
            Next
            txtbxaddr.BackColor = Color.Orange
            Return False
        Else
            If iphold.AddressFamily = Sockets.AddressFamily.InterNetwork And ver = 0 Then
                txtbxaddr.BackColor = Color.White
                Return True
            ElseIf iphold.AddressFamily = Sockets.AddressFamily.InterNetworkV6 And ver = 1 Then
                txtbxaddr.BackColor = Color.White
                Return True
            Else
                txtbxaddr.BackColor = Color.Orange
                Return False
            End If
            txtbxaddr.BackColor = Color.Orange
            Return False
        End If
    End Function

    Private Sub cmbxtype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbxtype.SelectedIndexChanged
        If ue Then
            If cmbxtype.SelectedIndex + 1 = AddressableType.TCP Then
                txtbxmyaddr.Enabled = False
                nudmyport.Enabled = False
            ElseIf cmbxtype.SelectedIndex + 1 = AddressableType.UDP Then
                txtbxmyaddr.Enabled = True
                nudmyport.Enabled = True
            End If
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(cmbxtype, l, ETs.SelectedIndexChanged, New EventArgsDataContainer(cmbxtype.SelectedIndex)))
        End If
    End Sub
End Class
