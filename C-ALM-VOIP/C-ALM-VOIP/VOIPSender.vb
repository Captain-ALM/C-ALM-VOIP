Imports NAudio.Wave

Public NotInheritable Class VOIPSender
    Implements IDisposable

    Protected mic As WaveIn = Nothing
    Protected strm As Streamer = Nothing
    Protected buff As New SyncLockedList(Of Byte)
    Protected buffsiz As Integer = 800
    Public Event dataAvailable(bts As Byte())
    Public Sub New()
        mic = New WaveIn()
        mic.BufferMilliseconds = 50
        mic.DeviceNumber = input_device
        strm = New Streamer("", False)
        mic.WaveFormat = New WaveFormat(8000, 16, 1)
        AddHandler mic.DataAvailable, AddressOf dataReceived
        mic.StartRecording()
    End Sub

    Private Sub dataReceived(sender As Object, e As WaveInEventArgs)
        Dim bts As Byte() = DeepCopyHelper.deepCopy(Of Byte())(e.Buffer)
        For Each b As Byte In bts
            buff.Add(b)
        Next
        If buff.Count > buffsiz Then
            Dim bts2(buff.Count - 1) As Byte
            buff.CopyTo(bts2, 0)
            buff.Clear()
            strm.ingestData(bts2, True)
            RaiseEvent dataAvailable(bts2)
        End If
    End Sub

    Public ReadOnly Property streamer As Streamer
        Get
            Return strm
        End Get
    End Property

    Public Property samplebuffersize As Integer
        Get
            Return buffsiz / 2
        End Get
        Set(value As Integer)
            buffsiz = value * 2
        End Set
    End Property

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                RemoveHandler mic.DataAvailable, AddressOf dataReceived
                strm.close()
                strm = Nothing
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
