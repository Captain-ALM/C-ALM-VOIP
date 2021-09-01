<Serializable>
Public Class Contact
    Inherits AddressableBase

    Public Sub New()
        MyBase.New("", 0, IPVersion.None, AddressableType.None)
    End Sub

    Public Sub New(other As AddressableBase)
        MyBase.New(other)
    End Sub

    Public Sub New(targAddress As String, targPort As Integer, targVer As IPVersion, aType As AddressableType)
        MyBase.New(targAddress, targPort, targVer, aType)
    End Sub
    <Xml.Serialization.XmlElement(ElementName:="targetAddress")>
    Public Overloads Property targetAddress As String
        Get
            Return MyBase.targetAddress
        End Get
        Set(value As String)
            MyBase.targetAddress = value
        End Set
    End Property
    <Xml.Serialization.XmlElement(ElementName:="targetPort")>
    Public Overloads Property targetPort As Integer
        Get
            Return MyBase.targetPort
        End Get
        Set(value As Integer)
            MyBase.targetPort = value
        End Set
    End Property
    <Xml.Serialization.XmlElement(ElementName:="type")>
    Public Overloads Property type As AddressableType
        Get
            Return MyBase.type
        End Get
        Set(value As AddressableType)
            MyBase.type = value
        End Set
    End Property
    <Xml.Serialization.XmlElement(ElementName:="targetIPVersion")>
    Public Overloads Property targetIPVersion As IPVersion
        Get
            Return MyBase.targetIPVersion
        End Get
        Set(value As IPVersion)
            MyBase.targetIPVersion = value
        End Set
    End Property

    Public Overrides Function duplicateToNew() As AddressableBase
        Return New Contact(Me)
    End Function
End Class

<Serializable, Xml.Serialization.XmlInclude(GetType(Contact))>
Public Class Contacts
    Public contacts As Contact() = New Contact() {}

    Public Shared Function load(strIn As String) As Contacts
        Return sserializer.deSerialize(Of Contacts)(strIn)
    End Function

    Public Function save() As String
        Return sserializer.serialize(Of Contacts)(Me)
    End Function
End Class
