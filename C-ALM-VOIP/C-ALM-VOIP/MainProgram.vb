Imports captainalm.workerpumper
Imports captainalm.CALMNetMarshal

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
End Class