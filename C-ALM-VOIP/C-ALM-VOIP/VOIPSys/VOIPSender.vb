Imports NAudio.Wave

Public NotInheritable Class VOIPSender
    Implements IDisposable

    Private mic As WaveIn = Nothing
    Private strm As Streamer = Nothing
    Private buff As New SyncLockedList(Of Byte)
    Private buffsiz As Integer = 3000
    Public Event dataAvailable(bts As Byte())
    Public Sub New()
        mic = New WaveIn()
        mic.BufferMilliseconds = buffmdmsecs
        mic.DeviceNumber = input_device
        strm = New Streamer("", False)
        mic.WaveFormat = New WaveFormat(samplerate, 16, 1)
        AddHandler mic.DataAvailable, AddressOf dataReceived
        mic.StartRecording()
    End Sub

    Private Sub dataReceived(sender As Object, e As WaveInEventArgs)
        Dim nb(e.BytesRecorded - 1) As Byte
        Buffer.BlockCopy(e.Buffer, 0, nb, 0, e.BytesRecorded)
        buff.AddRange(nb)
        If buff.Count >= buffsiz Then
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
    Private Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                RemoveHandler mic.DataAvailable, AddressOf dataReceived
                strm.close()
                mic.StopRecording()
                mic.Dispose()
            End If
            strm = Nothing
            mic = Nothing
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
    End Sub
#End Region

End Class
