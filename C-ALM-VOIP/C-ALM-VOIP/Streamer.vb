Imports NAudio.Wave
Imports NAudio.Wave.SampleProviders

Public Class Streamer
    Protected _vsp As VolumeSampleProvider = Nothing
    Protected _wp As BufferedWaveProvider = Nothing
    Protected _datalen As Integer = 0
    Protected _m As Boolean = False
    Protected _name As String = ""
    Public Event dataExgest(data As Byte())
    Public Event dataExgestWithVolume(data As Single())

    Public Sub New(name As String, havevolume As Boolean)
        _wp = New BufferedWaveProvider(New WaveFormat(8000, 16, 1))
        If havevolume Then
            _vsp = New VolumeSampleProvider(_wp)
        End If
        _name = name
    End Sub

    Public Sub ingestData(data As Byte(), exportViaEvent As Boolean)
        If Not _m Then
            _wp.AddSamples(data, 0, data.Length)
            If Not exportViaEvent Then Exit Sub
            If (_vsp Is Nothing) Then
                Dim bts As Byte() = New Byte(data.Length - 1) {}
                _wp.Read(bts, 0, bts.Length)
                RaiseEvent dataExgest(bts)
            Else
                Dim bts As Single() = New Single(data.Length - 1) {}
                _vsp.Read(bts, 0, bts.Length)
                RaiseEvent dataExgestWithVolume(bts)
            End If
        End If
    End Sub

    Public Sub [close]()
        _vsp = Nothing
        _wp.ClearBuffer()
        _wp = Nothing
    End Sub

    Public Overridable Property muted As Boolean
        Get
            Return _m
        End Get
        Set(value As Boolean)
            _m = value
        End Set
    End Property

    Public Overridable Property volume As Single
        Get
            Return _vsp.Volume
        End Get
        Set(value As Single)
            _vsp.Volume = value
        End Set
    End Property

    Public Overridable ReadOnly Property bufferedprovider As BufferedWaveProvider
        Get
            Return _wp
        End Get
    End Property

    Public Overridable ReadOnly Property volumeprovider As VolumeSampleProvider
        Get
            Return _vsp
        End Get
    End Property

    Public Overridable Property name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property
End Class


