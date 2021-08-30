<Serializable>
Public Class Contact
    Inherits AddressableBase

    Public Sub New(other As AddressableBase)
        MyBase.New(other)
    End Sub

    Public Sub New(targAddress As String, targPort As Integer, targVer As IPVersion, aType As AddressableType)
        MyBase.New(targAddress, targPort, targVer, aType)
    End Sub

    Public Overloads Property targetAddress As String
        Get
            Return MyBase.targetAddress
        End Get
        Set(value As String)
            MyBase.targetAddress = value
        End Set
    End Property

    Public Overloads Property targetPort As Integer
        Get
            Return MyBase.targetPort
        End Get
        Set(value As Integer)
            MyBase.targetPort = value
        End Set
    End Property

    Public Overloads Property type As AddressableType
        Get
            Return MyBase.type
        End Get
        Set(value As AddressableType)
            MyBase.type = value
        End Set
    End Property

    Public Overloads Property targetIPVersion As IPVersion
        Get
            Return MyBase.targetIPVersion
        End Get
        Set(value As IPVersion)
            MyBase.targetIPVersion = value
        End Set
    End Property
    <Xml.Serialization.XmlElement(ElementName:="advertisedAddress")>
    Public Overridable Property advertisedAddressValue As String
        Get
            Return _advaddress
        End Get
        Set(value As String)
            _advaddress = value
        End Set
    End Property
    <Xml.Serialization.XmlElement(ElementName:="advertisedPort")>
    Public Overridable Property advertisedPortValue As Integer
        Get
            Return _advport
        End Get
        Set(value As Integer)
            _advport = value
        End Set
    End Property

    Public Overrides Function duplicateToNew() As AddressableBase
        Return New Contact(Me)
    End Function

    Public Shared Function load(strIn As String) As Contact
        Return sserializer.deSerialize(Of Contact)(strIn)
    End Function

    Public Function save() As String
        Return sserializer.serialize(Of Contact)(Me)
    End Function
End Class
