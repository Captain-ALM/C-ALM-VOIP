Public Interface IListViewable
    ReadOnly Property item As ListViewItem
    Sub updateItem(refresh As Boolean)
    Sub cleanItem()
End Interface