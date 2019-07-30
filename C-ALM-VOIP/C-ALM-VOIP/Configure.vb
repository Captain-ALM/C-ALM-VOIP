Imports captainalm.workerpumper
Imports System.Net

Public Class Configure
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
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub Configure_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not formClosingDone Then
            If Me.Visible Then
                'If close button pressed
                e.Cancel = True
                Me.Hide()
                If Me.DialogResult = Windows.Forms.DialogResult.None Then Me.DialogResult = Windows.Forms.DialogResult.OK
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
        If ue Then wp.addEvent(Me, ETs.Shown, e)
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
    End Sub

    Private Sub nudsptcpipv4_Leave(sender As Object, e As EventArgs) Handles nudsptcpipv4.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nudsptcpipv4, l, ETs.Leave, New EventArgsDataContainer(nudsptcpipv4.Value)))
        End If
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
    End Sub

    Private Sub nudsptcpipv6_Leave(sender As Object, e As EventArgs) Handles nudsptcpipv6.Leave
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nudsptcpipv6, l, ETs.Leave, New EventArgsDataContainer(nudsptcpipv6.Value)))
        End If
    End Sub

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
        butCANCEL.Select()
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
End Class