Public Class Contact
    Inherits AddressableBase

    Public Sub New(other As AddressableBase)
        MyBase.New(other)
    End Sub

    Public Sub New(targAddress As String, targPort As Integer, aType As AddressableType, mpMode As MessagePassMode)
        MyBase.New(targAddress, targPort, aType, mpMode)
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

    Public Overloads Property messagePassMode As MessagePassMode
        Get
            Return MyBase.messagePassMode
        End Get
        Set(value As MessagePassMode)
            MyBase.messagePassMode = value
        End Set
    End Property

    Public Overrides Function duplicateToNew() As AddressableBase
        Return New Contact(Me)
    End Function
End Class
