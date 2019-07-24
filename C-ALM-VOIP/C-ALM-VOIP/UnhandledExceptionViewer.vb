Public Class UnhandledExceptionViewer
    Private cc As Boolean = False
    Private cr As Boolean = False
    Private ct As Boolean = True
    Private ex As Exception = Nothing
    Private rl As String = ""
    Private Const crlf As String = ChrW(13) & ChrW(10)

    Public Sub New(canContinue As Boolean, canReport As Boolean, canTerminate As Boolean, exc As Exception, Optional reportLink As String = "https://github.com/Captain-ALM/Flashcard-Maker/issues/new", Optional reportLinkAppensioner As IAppensioner = Nothing)
        InitializeComponent()
        cc = canContinue
        cr = canReport
        ct = canTerminate
        ex = exc
        rl = reportLink
        If reportLinkAppensioner IsNot Nothing Then rl = reportLinkAppensioner.appendText(rl)
        Me.DialogResult = Windows.Forms.DialogResult.None
    End Sub

    Private Sub UnhandledExceptionViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        butcont.Enabled = cc
        butr.Enabled = cr
        butt.Enabled = ct
        Dim extxt As String = ex.GetType.ToString & crlf & crlf
        extxt &= ex.Message & crlf & crlf
        extxt &= ex.TargetSite.ToString() & crlf & crlf
        extxt &= ex.Source & crlf & crlf
        extxt &= ex.StackTrace & crlf & crlf
        txtbxex.Text = extxt
    End Sub

    Private Sub butcont_Click(sender As Object, e As EventArgs) Handles butcont.Click
        If cc Then
            Me.DialogResult = Windows.Forms.DialogResult.Ignore
            Me.Close()
        End If
    End Sub

    Private Sub butt_Click(sender As Object, e As EventArgs) Handles butt.Click
        If ct Then
            Me.DialogResult = Windows.Forms.DialogResult.Abort
            Me.Close()
        End If
    End Sub

    Private Sub butr_Click(sender As Object, e As EventArgs) Handles butr.Click
        If cr Then
            Dim startInfo As New ProcessStartInfo()
            startInfo.FileName = rl
            startInfo.WindowStyle = ProcessWindowStyle.Maximized
            Process.Start(startInfo)
        End If
    End Sub

    Private Sub UnhandledExceptionViewer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.DialogResult <> Windows.Forms.DialogResult.Abort And Me.DialogResult <> Windows.Forms.DialogResult.Ignore Then
            If cc Then
                Me.DialogResult = Windows.Forms.DialogResult.Ignore
            Else
                Me.DialogResult = Windows.Forms.DialogResult.Abort
            End If
        End If
    End Sub
End Class
''' <summary>
''' Defines an Appensioner that appends data to the text it is passed.
''' </summary>
''' <remarks></remarks>
Public Interface IAppensioner
    ''' <summary>
    ''' Called to appened text to the passed text.
    ''' </summary>
    ''' <param name="text">The passed Text.</param>
    ''' <returns>The Text with the appension.</returns>
    ''' <remarks></remarks>
    Function appendText(text As String) As String
End Interface