Imports captainalm.workerpumper

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

    Private Sub Configure_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If ue Then wp.addEvent(New WorkerEvent(Me, ETs.Load, e))
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

    Private Sub cmbxsniipv4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbxsniipv4.SelectedIndexChanged
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(cmbxsniipv4, l, ETs.SelectedIndexChanged, e))
        End If
    End Sub

    Private Sub nudspudpipv4_ValueChanged(sender As Object, e As EventArgs) Handles nudspudpipv4.ValueChanged
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nudspudpipv4, l, ETs.ValueChanged, e))
        End If
    End Sub

    Private Sub nudsptcpipv4_ValueChanged(sender As Object, e As EventArgs) Handles nudsptcpipv4.ValueChanged
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nudsptcpipv4, l, ETs.ValueChanged, e))
        End If
    End Sub

    Private Sub cmbxsniipv6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbxsniipv6.SelectedIndexChanged
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(cmbxsniipv6, l, ETs.SelectedIndexChanged, e))
        End If
    End Sub

    Private Sub nudspudpipv6_ValueChanged(sender As Object, e As EventArgs) Handles nudspudpipv6.ValueChanged
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nudspudpipv6, l, ETs.ValueChanged, e))
        End If
    End Sub

    Private Sub nudsptcpipv6_ValueChanged(sender As Object, e As EventArgs) Handles nudsptcpipv6.ValueChanged
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nudsptcpipv6, l, ETs.ValueChanged, e))
        End If
    End Sub

    Private Sub nudtcpbl_ValueChanged(sender As Object, e As EventArgs) Handles nudtcpbl.ValueChanged
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nudtcpbl, l, ETs.ValueChanged, e))
        End If
    End Sub

    Private Sub chkbxena_CheckedChanged(sender As Object, e As EventArgs) Handles chkbxena.CheckedChanged
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(chkbxena, l, ETs.CheckedChanged, e))
        End If
    End Sub

    Private Sub txtbxudpextaddIPv4_TextChanged(sender As Object, e As EventArgs) Handles txtbxudpextaddIPv4.TextChanged
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(txtbxudpextaddIPv4, l, ETs.TextChanged, e))
        End If
    End Sub

    Private Sub nududpextpIPv4_ValueChanged(sender As Object, e As EventArgs) Handles nududpextpIPv4.ValueChanged
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nududpextpIPv4, l, ETs.ValueChanged, e))
        End If
    End Sub

    Private Sub txtbxudpextaddIPv6_TextChanged(sender As Object, e As EventArgs) Handles txtbxudpextaddIPv6.TextChanged
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(txtbxudpextaddIPv6, l, ETs.TextChanged, e))
        End If
    End Sub

    Private Sub nududpextpIPv6_ValueChanged(sender As Object, e As EventArgs) Handles nududpextpIPv6.ValueChanged
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(nududpextpIPv6, l, ETs.ValueChanged, e))
        End If
    End Sub

    Private Sub butOK_Click(sender As Object, e As EventArgs) Handles butOK.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butOK, l, ETs.Click, e))
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub butCANCEL_Click(sender As Object, e As EventArgs) Handles butCANCEL.Click
        If ue Then
            Dim l As New List(Of Object)
            l.Add(Me)
            wp.addEvent(New WorkerEvent(butCANCEL, l, ETs.Click, e))
        End If
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class