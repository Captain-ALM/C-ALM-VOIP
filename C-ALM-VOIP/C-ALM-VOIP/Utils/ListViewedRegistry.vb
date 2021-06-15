Public NotInheritable Class ListViewedRegistry(Of t As IListViewable)
    Private backedList As New SyncLockedList(Of t)
    Private backedView As ListView = Nothing
    Private slock As New Object()
    Private selectedIdxs As New SyncLockedList(Of Integer)
    Private slocksi As New Object()

    Public Sub New(lvIn As ListView)
        SyncLock slock
            backedView = lvIn
        End SyncLock
    End Sub

    Public Sub add(itemIn As t)
        If backedView.InvokeRequired Then
            backedView.Invoke(Sub() add(itemIn))
        Else
            SyncLock slock
                backedList.Add(itemIn)
                itemIn.updateItem(True)
                backedView.Items.Add(itemIn.item)
            End SyncLock
        End If
    End Sub

    Public Sub remove(itemIn As t)
        If backedView.InvokeRequired Then
            backedView.Invoke(Sub() remove(itemIn))
        Else
            SyncLock slock
                backedView.Items.Remove(itemIn.item)
                backedList.Remove(itemIn)
            End SyncLock
        End If
    End Sub

    Public Function indexOf(itemIn As t) As Integer
        SyncLock slock
            Return backedList.IndexOf(itemIn)
        End SyncLock
    End Function

    Public Function find(matcher As IMatcher(Of t)) As t()
        Dim toret As New List(Of t)
        SyncLock slock
            For Each c As t In backedList
                If matcher.doesMatch(c) Then toret.Add(c)
            Next
        End SyncLock
        Return toret.ToArray()
    End Function

    Default Public ReadOnly Property Item(index As Integer) As t
        Get
            SyncLock slock
                Return backedList(index)
            End SyncLock
        End Get
    End Property

    Public Sub update()
        If backedView.InvokeRequired Then
            backedView.Invoke(Sub() update())
        Else
            SyncLock slock
                backedView.Refresh()
            End SyncLock
        End If
    End Sub

    Public ReadOnly Property count As Integer
        Get
            SyncLock slock
                Return backedList.Count
            End SyncLock
        End Get
    End Property

    Public Sub clear()
        If backedView.InvokeRequired Then
            backedView.Invoke(Sub() clear())
        Else
            SyncLock slock
                backedView.Items.Clear()
                backedList.Clear()
            End SyncLock
        End If
    End Sub

    Public ReadOnly Property selectedIndices As Integer()
        Get
            SyncLock slocksi
                Return selectedIdxs.toArray()
            End SyncLock
        End Get
    End Property

    Public Sub updateCachedIndices()
        SyncLock slocksi
            selectedIdxs.Clear()
            For Each c As Integer In backedView.SelectedIndices
                selectedIdxs.Add(c)
            Next
        End SyncLock
    End Sub
End Class
