Imports captainalm.CALMNetMarshal

Public Class BlockClient
    Inherits Client

    Public Sub New(other As Contact)
        MyBase.New(other)
    End Sub

    Public Sub New(other As Contact, targIp As String)
        MyBase.New(other)
        _targaddress = targIp
    End Sub

    Public Overrides Sub forceReceive(msg As IPacket)
    End Sub

    Public Overrides ReadOnly Property connected As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Sub [stop]()
    End Sub

    Public Overrides ReadOnly Property marshal As NetMarshalBase
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides ReadOnly Property stream As Streamer
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Property messagePassMode As MessagePassMode
        Get
            Return voip.MessagePassMode.Disable
        End Get
        Set(value As MessagePassMode)
        End Set
    End Property
End Class
