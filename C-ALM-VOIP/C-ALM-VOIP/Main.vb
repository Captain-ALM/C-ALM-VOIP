Imports System.Reflection
Imports System.IO
Imports captainalm.workerpumper
Imports System.Threading
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization


''' <summary>
''' The main static class - internal.
''' </summary>
''' <remarks></remarks>
Module Main
    Public Const quote As Char = """c"
    Public programAssembly As Assembly = Assembly.GetEntryAssembly()
    Public programPath As String = programAssembly.Location
    Public execdir As String = Path.GetDirectoryName(programPath)
    Public worker As WorkerPump = Nothing
    Public forms As New List(Of Form)
    Public parsers As New List(Of IEventParser)

    Public Sub main()
        Try
            init()
            Try
                runtime()
            Catch ex As Exception
                Dim r As DialogResult = New UnhandledExceptionBooter(New UnhandledExceptionViewer(False, True, True, ex)).showForm()
                Environment.Exit(1)
            End Try
            shutdown()
        Catch ex As Exception
            Dim r As DialogResult = New UnhandledExceptionBooter(New UnhandledExceptionViewer(False, True, True, ex)).showForm()
            Environment.Exit(1)
        End Try
        Environment.Exit(0)
    End Sub

    Public Sub init()
        Application.EnableVisualStyles()
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)
        AddHandler Application.ThreadException, AddressOf oae

        Try
            If File.Exists(execdir & "\license.txt") Then
                license = File.ReadAllText(execdir & "\license.txt")
            End If
            If license = "" Then
                license = My.Resources.LICENSE
            End If
        Catch ex As IOException
        End Try
        Try
            If File.Exists(execdir & "\description.txt") Then
                description = File.ReadAllText(execdir & "\description.txt")
            End If
        Catch ex As IOException
        End Try

        worker = New WorkerPump()
        AddHandler worker.OnPumpException, AddressOf ope
        forms.Add(New AboutBx(worker))
        worker.addFormInstance(Of AboutBx)(forms(0))
        forms.Add(New MainProgram(worker))
        worker.addFormInstance(Of MainProgram)(forms(1))
        forms.Add(New Configure(worker))
        worker.addFormInstance(Of Configure)(forms(2))
        forms.Add(New Editor(worker))
        worker.addFormInstance(Of Editor)(forms(3))
        parsers.Add(New PConfigure())
        parsers.Add(New PEditor())
        For Each c As IEventParser In parsers
            worker.addParser(c)
        Next
    End Sub

    Public Sub runtime()
        worker.startPump()
        worker.showForm(Of MainProgram)()
        While worker.PumpBusy
            Thread.Sleep(125)
        End While
        worker.stopPump()
    End Sub

    Public Sub shutdown()
        If worker.Pumping() Then
            worker.joinPump(2500)
            worker.stopPumpForce()
        End If
        RemoveHandler worker.OnPumpException, AddressOf ope
        If Not worker.Disposing And Not worker.IsDisposed Then worker.Dispose()
        worker = Nothing
        RemoveHandler Application.ThreadException, AddressOf oae
    End Sub

    Function convertStringToInteger(str As String) As Integer
        Dim toret As Integer = Integer.MinValue
        Try
            toret = Integer.Parse(str)
        Catch ex As InvalidCastException
            toret = Integer.MinValue
        Catch ex As ArgumentException
            toret = Integer.MinValue
        Catch ex As OverflowException
            toret = Integer.MinValue
        End Try
        Return toret
    End Function

    Sub ope(ex As Exception)
        Dim r As DialogResult = New UnhandledExceptionBooter(New UnhandledExceptionViewer(True, True, False, ex)).showForm()
    End Sub

    Sub oae(sender As Object, ex As ThreadExceptionEventArgs)
        Dim r As DialogResult = New UnhandledExceptionBooter(New UnhandledExceptionViewer(True, True, True, ex.Exception)).showForm()
        If r = DialogResult.Abort Then Environment.Exit(1)
    End Sub
End Module

Friend NotInheritable Class UnhandledExceptionBooter
    Private expviewer As UnhandledExceptionViewer = Nothing
    Public Sub New(ByRef exv As UnhandledExceptionViewer)
        expviewer = exv
    End Sub
    Public Function showForm(Optional parent As IWin32Window = Nothing) As DialogResult
        If Not expviewer.Disposing And Not expviewer.IsDisposed Then
            Dim r As System.Windows.Forms.DialogResult = expviewer.ShowDialog(parent)
            If Not expviewer.Disposing And Not expviewer.IsDisposed Then
                expviewer.Dispose()
            End If
            expviewer = Nothing
            Return r
        Else
            Return DialogResult.None
        End If
    End Function
End Class
