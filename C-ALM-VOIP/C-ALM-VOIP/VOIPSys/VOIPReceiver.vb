Imports NAudio.Wave
Imports NAudio.Wave.SampleProviders

Public Class VOIPReceiver
    Implements IDisposable

    Protected spk As WaveOut = Nothing
    Protected msp As MixingSampleProvider = Nothing
    Protected slockprov As New Object()
    Protected slockcs As New Object()
    Public Sub New()
        spk = New WaveOut()
        msp = New MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(samplerate, 1))
        msp.ReadFully = True
        spk.Init(msp)
        spk.Play()
    End Sub
    Public Sub addProvider(prov As ISampleProvider)
        SyncLock slockprov
            msp.AddMixerInput(prov)
        End SyncLock
    End Sub
    Public Sub removeProvider(prov As ISampleProvider)
        SyncLock slockprov
            msp.RemoveMixerInput(prov)
        End SyncLock
    End Sub
    Public Function createStreamer(name As String) As Streamer
        Dim toret As Streamer = Nothing
        SyncLock slockcs
            toret = New Streamer(name, True)
            addProvider(toret.volumeprovider)
        End SyncLock
        Return toret
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                SyncLock slockprov
                    msp.RemoveAllMixerInputs()
                End SyncLock
                spk.Dispose()
            End If
            spk = Nothing
            msp = Nothing
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
