Public NotInheritable Class ListViewedRegistry(Of t As IListViewable)
    Private backedList As New SyncLockedList(Of t)
    Private backedView As ListView = Nothing
    Private slock As New Object()

    Public Sub New(lvIn As ListView)
        SyncLock slock
            backedView = lvIn
            'AddHandler backedView.SelectedIndexChanged, AddressOf backed_selected_indices_changed
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

    Public ReadOnly Property backingList As SyncLockedList(Of t)
        Get
            SyncLock slock
                Return backedList
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
End Class
