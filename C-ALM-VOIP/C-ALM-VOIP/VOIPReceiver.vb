Imports NAudio.Wave
Imports NAudio.Wave.SampleProviders

Public Class VOIPReceiver
    Implements IDisposable

    Protected spk As WaveOut = Nothing
    Protected msp As MixingSampleProvider = Nothing
    Public Sub New()
        spk = New WaveOut()
        msp = New MixingSampleProvider(New WaveFormat(8000, 16, 1))
        msp.ReadFully = True
        spk.Init(msp)
        spk.Play()
    End Sub
    Public Sub addProvider(prov As IWaveProvider)
        msp.AddMixerInput(prov)
    End Sub
    Public Sub removeProvider(prov As IWaveProvider)
        msp.RemoveMixerInput(prov)
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                spk.Dispose()
                spk = Nothing
                msp = Nothing
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
