Public MustInherit Class AddressableBase
    Protected _name As String = ""
    Protected _targaddress As String = ""
    Protected _targport As Integer = 0
    Protected _myaddress As String = ""
    Protected _myport As Integer = 0
    Protected _type As AddressableType = AddressableType.None
    Protected _passmode As MessagePassMode = messagePassMode.Disable

    Public Sub New(other As AddressableBase)
        _name = other._name
        _targaddress = other._targaddress
        _targport = other._targport
        _myaddress = other._myaddress
        _myport = other._myport
        _type = other._type
        _passmode = other._passmode
    End Sub

    Public Sub New(targAddress As String, targPort As Integer, aType As AddressableType, mpMode As MessagePassMode)
        _targaddress = targAddress
        _targport = targPort
        _type = aType
        _passmode = mpMode
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

    Public Overridable Property type As AddressableType
        Get
            Return _type
        End Get
        Protected Set(value As AddressableType)
            _type = value
        End Set
    End Property

    Public Overridable Property messagePassMode As MessagePassMode
        Get
            Return _passmode
        End Get
        Protected Set(value As MessagePassMode)
            _passmode = value
        End Set
    End Property

    Public MustOverride Function duplicateToNew() As AddressableBase
End Class

Public Enum AddressableType As Integer
    None = 0
    TCP = 1
    UDP = 2
End Enum

Public Enum MessagePassMode As Integer
    Disable = 0
    Receive = 1
    Send = 2
    Bidirectional = 3
End Enum
