<Serializable>
Public MustInherit Class AddressableBase
    Implements IListViewable

    Protected _name As String = ""
    Protected _targaddress As String = ""
    Protected _targport As Integer = 0
    Protected _myaddress As String = ""
    Protected _myport As Integer = 0
    Protected _type As AddressableType = AddressableType.None
    Protected _passmode As MessagePassMode = messagePassMode.Disable
    Protected _targver As IPVersion = IPVersion.None
    Protected _lvi As ListViewItem = Nothing
    <NonSerialized>
    Protected slocklvi As New Object()

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
    <Xml.Serialization.XmlIgnore>
    Public Overridable Property targetAddress As String
        Get
            Return _targaddress
        End Get
        Protected Set(value As String)
            _targaddress = value
        End Set
    End Property
    <Xml.Serialization.XmlIgnore>
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
    <Xml.Serialization.XmlIgnore>
    Public Overridable Property type As AddressableType
        Get
            Return _type
        End Get
        Protected Set(value As AddressableType)
            _type = value
        End Set
    End Property
    <Xml.Serialization.XmlIgnore>
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

    Public Overridable Sub updateLVI(u As Boolean) Implements IListViewable.updateItem
        SyncLock slocklvi
            If (Not _lvi Is Nothing) AndAlso (Not _lvi.ListView Is Nothing) AndAlso _lvi.ListView.InvokeRequired Then
                _lvi.ListView.Invoke(Sub() updateLVIActual(u))
            Else
                updateLVIActual(u)
            End If
        End SyncLock
    End Sub

    Protected Overridable Sub updateLVIActual(u As Boolean)
        If _lvi Is Nothing Then _lvi = New ListViewItem(_name) Else _lvi.Text = name
        If _lvi.SubItems.Count < 2 Then _lvi.SubItems.Add(_targaddress) Else _lvi.SubItems(1).Text = _targaddress
        If _lvi.SubItems.Count < 3 Then _lvi.SubItems.Add(_targport) Else _lvi.SubItems(2).Text = _targport
        If _lvi.SubItems.Count < 4 Then _lvi.SubItems.Add(_type.ToString()) Else _lvi.SubItems(3).Text = _type.ToString()
    End Sub

    Public ReadOnly Property item As ListViewItem Implements IListViewable.item
        Get
            SyncLock slocklvi
                Return _lvi
            End SyncLock
        End Get
    End Property

    Public Sub cleanItem() Implements IListViewable.cleanItem
        SyncLock slocklvi
            _lvi = Nothing
        End SyncLock
    End Sub
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
