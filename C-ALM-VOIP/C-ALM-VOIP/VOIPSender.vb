﻿
Imports NAudio.Wave

Public NotInheritable Class VOIPSender
    Implements IDisposable

    Protected mic As WaveIn = Nothing
    Public Event dataAvailable(bts As Byte())
    Public Sub New()
        mic = New WaveIn()
        mic.BufferMilliseconds = 50
        mic.DeviceNumber = input_device
        mic.WaveFormat = New WaveFormat(8000, 16, 1)
        AddHandler mic.DataAvailable, AddressOf dataReceived
        mic.StartRecording()
    End Sub

    Private Sub dataReceived(sender As Object, e As WaveInEventArgs)
        RaiseEvent dataAvailable(DeepCopyHelper.deepCopy(Of Byte())(e.Buffer))
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                RemoveHandler mic.DataAvailable, AddressOf dataReceived
                mic.StopRecording()
                mic.Dispose()
                mic = Nothing
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
