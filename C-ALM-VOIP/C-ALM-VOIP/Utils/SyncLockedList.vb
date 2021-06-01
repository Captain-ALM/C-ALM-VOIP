Public Class SyncLockedList(Of t)
    Implements IList(Of t)

    Protected _list As List(Of t) = Nothing
    Protected _slocklist As New Object()
    Public Sub New()
        _list = New List(Of t)()
    End Sub
    Public Sub New(capacity As Integer)
        _list = New List(Of t)(capacity)
    End Sub
    Public Sub New(collection As IEnumerable(Of t))
        _list = New List(Of t)(collection)
    End Sub

    Public Sub Add(item As t) Implements ICollection(Of t).Add
        SyncLock _slocklist
            _list.Add(item)
        End SyncLock
    End Sub

    Public Sub Clear() Implements ICollection(Of t).Clear
        SyncLock _slocklist
            _list.Clear()
        End SyncLock
    End Sub

    Public Function Contains(item As t) As Boolean Implements ICollection(Of t).Contains
        Dim b As Boolean = False
        SyncLock _slocklist
            b = _list.Contains(item)
        End SyncLock
        Return b
    End Function

    Public Sub CopyTo(array() As t, arrayIndex As Integer) Implements ICollection(Of t).CopyTo
        SyncLock _slocklist
            _list.CopyTo(array, arrayIndex)
        End SyncLock
    End Sub

    Public ReadOnly Property Count As Integer Implements ICollection(Of t).Count
        Get
            Dim i As Integer = 0
            SyncLock _slocklist
                i = _list.Count
            End SyncLock
            Return i
        End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of t).IsReadOnly
        Get
            Return False
        End Get
    End Property

    Public Function Remove(item As t) As Boolean Implements ICollection(Of t).Remove
        Dim b As Boolean = False
        SyncLock _slocklist
            b = _list.Remove(item)
        End SyncLock
        Return b
    End Function

    Public Function GetEnumerator() As IEnumerator(Of t) Implements IEnumerable(Of t).GetEnumerator
        Dim e As IEnumerator(Of t) = Nothing
        SyncLock _slocklist
            e = New SyncLockedGenericEnumerator(_list.GetEnumerator(), _slocklist)
        End SyncLock
        Return e
    End Function

    Public Function IndexOf(item As t) As Integer Implements IList(Of t).IndexOf
        Dim i As Integer = 0
        SyncLock _slocklist
            i = _list.IndexOf(item)
        End SyncLock
        Return i
    End Function

    Public Sub Insert(index As Integer, item As t) Implements IList(Of t).Insert
        SyncLock _slocklist
            _list.Insert(index, item)
        End SyncLock
    End Sub

    Default Public Property Item(index As Integer) As t Implements IList(Of t).Item
        Get
            Dim i As t = Nothing
            SyncLock _slocklist
                i = _list(index)
            End SyncLock
            Return i
        End Get
        Set(value As t)
            SyncLock _slocklist
                _list(index) = value
            End SyncLock
        End Set
    End Property

    Public Sub RemoveAt(index As Integer) Implements IList(Of t).RemoveAt
        SyncLock _slocklist
            _list.RemoveAt(index)
        End SyncLock
    End Sub

    Public Function GetEnumeratorNonGeneric() As IEnumerator Implements IEnumerable.GetEnumerator
        Dim e As IEnumerator = Nothing
        SyncLock _slocklist
            e = New SyncLockedEnumerator(_list.GetEnumerator(), _slocklist)
        End SyncLock
        Return e
    End Function

    Public Class SyncLockedEnumerator
        Implements IEnumerator

        Protected _enum As IEnumerator
        Protected _slocklist As New Object()

        Sub New(enumerator As IEnumerator, ByRef locker As Object)
            _enum = enumerator
            _slocklist = locker
        End Sub

        Public ReadOnly Property Current As Object Implements IEnumerator.Current
            Get
                Dim c As Object = Nothing
                SyncLock _slocklist
                    c = _enum.Current
                End SyncLock
                Return c
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
            Dim b As Boolean = False
            SyncLock _slocklist
                b = _enum.MoveNext()
            End SyncLock
            Return b
        End Function

        Public Sub Reset() Implements IEnumerator.Reset
            SyncLock _slocklist
                _enum.Reset()
            End SyncLock
        End Sub
    End Class

    Public Class SyncLockedGenericEnumerator
        Implements IEnumerator(Of t)

        Protected _enum As IEnumerator(Of t)
        Protected _slocklist As New Object()

        Sub New(enumerator As IEnumerator(Of t), ByRef locker As Object)
            _enum = enumerator
            _slocklist = locker
        End Sub

        Public ReadOnly Property Current As t Implements IEnumerator(Of t).Current
            Get
                Dim c As t = Nothing
                SyncLock _slocklist
                    c = _enum.Current
                End SyncLock
                Return c
            End Get
        End Property

        Public ReadOnly Property CurrentNonGeneric As Object Implements IEnumerator.Current
            Get
                Dim c As Object = Nothing
                SyncLock _slocklist
                    c = _enum.Current
                End SyncLock
                Return c
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
            Dim b As Boolean = False
            SyncLock _slocklist
                b = _enum.MoveNext()
            End SyncLock
            Return b
        End Function

        Public Sub Reset() Implements IEnumerator.Reset
            SyncLock _slocklist
                _enum.Reset()
            End SyncLock
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    _enum.Dispose()
                End If
                _enum = Nothing
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
End Class
