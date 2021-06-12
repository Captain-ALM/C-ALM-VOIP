<Serializable>
Public MustInherit Class AddressableBase
    Protected _name As String = ""
    Protected _targaddress As String = ""
    Protected _targport As Integer = 0
    Protected _myaddress As String = ""
    Protected _myport As Integer = 0
    Protected _advaddress As String = ""
    Protected _advport As Integer = 0
    Protected _type As AddressableType = AddressableType.None
    Protected _passmode As MessagePassMode = messagePassMode.Disable
    Protected _targver As IPVersion = IPVersion.None

    Public Sub New(other As AddressableBase)
        _name = other._name
        _targaddress = other._targaddress
        _targport = other._targport
        _myaddress = other._myaddress
        _myport = other._myport
        _type = other._type
        _passmode = other._passmode
        _targver = other._targver
    End Sub

    Public Sub New(targAddress As String, targPort As Integer, targVer As IPVersion, aType As AddressableType)
        _targaddress = targAddress
        _targport = targPort
        _type = aType
        _targver = targVer
    End Sub

    Public Overridable Property name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Public Overridable Property targetAddress As String
        Get
            Return _targaddress
        End Get
        Protected Set(value As String)
            _targaddress = value
        End Set
    End Property

    Public Overridable Property targetPort As Integer
        Get
            Return _targport
        End Get
        Protected Set(value As Integer)
            _targport = value
        End Set
    End Property

    Public Overridable Property myAddress As String
        Get
            Return _myaddress
        End Get
        Set(value As String)
            _myaddress = value
        End Set
    End Property

    Public Overridable Property myPort As Integer
        Get
            Return _myport
        End Get
        Set(value As Integer)
            _myport = value
        End Set
    End Property

    Public Overridable Property advertisedAddress As String
        Get
            If _advaddress Is Nothing OrElse _advaddress = "" Then Return _myaddress
            Return _advaddress
        End Get
        Set(value As String)
            _advaddress = value
        End Set
    End Property

    Public Overridable Property advertisedPort As Integer
        Get
            If _advport = 0 Then Return _myport
            Return _advport
        End Get
        Set(value As Integer)
            _advport = value
        End Set
    End Property

    Public Overridable Property type As AddressableType
        Get
            Return _type
        End Get
        Protected Set(value As AddressableType)
            _type = value
        End Set
    End Property

    Public Overridable Property targetIPVersion As IPVersion
        Get
            Return _targver
        End Get
        Protected Set(value As IPVersion)
            _targver = value
        End Set
    End Property

    Public Overridable Property messagePassMode As MessagePassMode
        Get
            Return _passmode
        End Get
        Set(value As MessagePassMode)
            _passmode = value
        End Set
    End Property

    Public MustOverride Function duplicateToNew() As AddressableBase
End Class

Public Enum AddressableType As Integer
    None = 0
    TCP = 1
    UDP = 2
    Block = 3
End Enum

Public Enum MessagePassMode As Integer
    Disable = 0
    Receive = 1
    Send = 2
    Bidirectional = 3
End Enum

Public Enum IPVersion As Integer
    None = 0
    IPv4 = 1
    IPv6 = 2
End Enum
