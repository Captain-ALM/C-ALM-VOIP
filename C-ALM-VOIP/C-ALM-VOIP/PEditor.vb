Imports captainalm.workerpumper

Public Class PEditor
    Implements IEventParser
    Private wp As WorkerPump = Nothing
    Private _caddrbs As AddressableBase = Nothing

    Public Function Parse(ev As WorkerEvent) As Boolean Implements IEventParser.Parse
        Dim toret As Boolean = True
        If canCastObject(Of Editor)(ev.EventSource.sourceObj) Then
            Dim frm As Editor = castObject(Of Editor)(ev.EventSource.sourceObj)
            If ev.EventType = ETs.Shown Then
                _caddrbs = caddrbs.duplicateToNew()
                If ceditm = EditorMode.Create Then
                    frm.Invoke(Sub()
                                   frm.Text = "Create:"
                                   frm.Label1.Text = "Create:"
                                   frm.nudport.Value = 1
                                   frm.txtbxaddr.Text = ""
                                   frm.txtbxname.Text = ""
                                   frm.cmbxipv.SelectedIndex = 0
                                   frm.cmbxstrmode.SelectedIndex = 0
                                   frm.cmbxtype.SelectedIndex = 0
                                   frm.cmbxipv.Enabled = True
                                   frm.cmbxstrmode.Enabled = True
                                   frm.cmbxtype.Enabled = True
                                   frm.txtbxaddr.Enabled = True
                                   frm.txtbxaddr.ReadOnly = False
                                   frm.nudport.Enabled = True
                                   frm.nudport.ReadOnly = False
                                   frm.nudport.Controls(0).Enabled = True
                                   frm.txtbxmyaddr.ReadOnly = False
                                   frm.nudmyport.ReadOnly = False
                                   frm.nudmyport.Controls(0).Enabled = True
                                   If _caddrbs.type = AddressableType.TCP Then
                                       frm.txtbxmyaddr.Text = ""
                                       frm.nudmyport.Value = 1
                                       frm.txtbxmyaddr.Enabled = False
                                       frm.nudmyport.Enabled = False
                                   ElseIf _caddrbs.type = AddressableType.UDP Then
                                       frm.txtbxmyaddr.Text = ""
                                       frm.nudmyport.Value = 1
                                       frm.txtbxmyaddr.Enabled = True
                                       frm.nudmyport.Enabled = True
                                   End If
                               End Sub)
                ElseIf ceditm = EditorMode.EditClient Then
                    frm.Invoke(Sub()
                                   frm.Text = "View:"
                                   frm.Label1.Text = "View:"
                                   frm.nudport.Value = _caddrbs.targetPort
                                   frm.txtbxaddr.Text = _caddrbs.targetAddress
                                   frm.txtbxname.Text = _caddrbs.name
                                   frm.cmbxipv.SelectedIndex = _caddrbs.targetIPVersion - 1
                                   frm.cmbxstrmode.SelectedIndex = _caddrbs.messagePassMode - 2
                                   frm.cmbxtype.SelectedIndex = _caddrbs.type - 1
                                   frm.cmbxipv.Enabled = False
                                   frm.cmbxstrmode.Enabled = False 'Next version changing this on a client will hopefully be allowed.
                                   frm.cmbxtype.Enabled = False
                                   frm.txtbxaddr.Enabled = True
                                   frm.txtbxaddr.ReadOnly = True
                                   frm.nudport.Enabled = True
                                   frm.nudport.ReadOnly = True
                                   frm.nudport.Controls(0).Enabled = False
                                   frm.txtbxmyaddr.ReadOnly = True
                                   frm.nudmyport.ReadOnly = True
                                   frm.nudmyport.Controls(0).Enabled = False
                                   If _caddrbs.type = AddressableType.TCP Then
                                       frm.txtbxmyaddr.Text = ""
                                       frm.nudmyport.Value = 1
                                       frm.txtbxmyaddr.Enabled = False
                                       frm.nudmyport.Enabled = False
                                   ElseIf _caddrbs.type = AddressableType.UDP Then
                                       frm.txtbxmyaddr.Text = _caddrbs.myAddress
                                       frm.nudmyport.Value = _caddrbs.myPort
                                       frm.txtbxmyaddr.Enabled = True
                                       frm.nudmyport.Enabled = True
                                   End If
                               End Sub)
                ElseIf ceditm = EditorMode.EditContact Then
                    frm.Invoke(Sub()
                                   frm.Text = "Edit:"
                                   frm.Label1.Text = "Edit:"
                                   frm.nudport.Value = _caddrbs.targetPort
                                   frm.txtbxaddr.Text = _caddrbs.targetAddress
                                   frm.txtbxname.Text = _caddrbs.name
                                   frm.cmbxipv.SelectedIndex = _caddrbs.targetIPVersion - 1
                                   frm.cmbxstrmode.SelectedIndex = _caddrbs.messagePassMode - 2
                                   frm.cmbxtype.SelectedIndex = _caddrbs.type - 1
                                   frm.cmbxipv.Enabled = True
                                   frm.cmbxstrmode.Enabled = True
                                   frm.cmbxtype.Enabled = True
                                   frm.txtbxaddr.Enabled = True
                                   frm.txtbxaddr.ReadOnly = False
                                   frm.nudport.Enabled = True
                                   frm.nudport.ReadOnly = False
                                   frm.nudport.Controls(0).Enabled = True
                                   frm.txtbxmyaddr.ReadOnly = False
                                   frm.nudmyport.ReadOnly = False
                                   frm.nudmyport.Controls(0).Enabled = True
                                   If _caddrbs.type = AddressableType.TCP Then
                                       frm.txtbxmyaddr.Text = ""
                                       frm.nudmyport.Value = 1
                                       frm.txtbxmyaddr.Enabled = False
                                       frm.nudmyport.Enabled = False
                                   ElseIf _caddrbs.type = AddressableType.UDP Then
                                       frm.txtbxmyaddr.Text = _caddrbs.myAddress
                                       frm.nudmyport.Value = _caddrbs.myPort
                                       frm.txtbxmyaddr.Enabled = True
                                       frm.nudmyport.Enabled = True
                                   End If
                               End Sub)
                End If
            End If
        ElseIf ev.EventSource.parentObjs IsNot Nothing AndAlso ev.EventSource.parentObjs.Count > 0 Then
            Dim ra As Object = reverseArray(Of Object)(ev.EventSource.parentObjs.ToArray())
            If canCastObject(Of Editor)(ra(0)) Then
                Dim frm As Editor = castObject(Of Editor)(ra(0))
                Dim args As EventArgsDataContainer = castObject(Of EventArgsDataContainer)(ev.EventData)
                If ev.EventSource.sourceObj Is frm.OK_Button And ev.EventType = ETs.Click Then
                    If ceditm = EditorMode.Create Then
                        CType(caddrbs, Contact).messagePassMode = _caddrbs.messagePassMode
                        caddrbs.myAddress = _caddrbs.myAddress
                        caddrbs.myPort = _caddrbs.myPort
                        caddrbs.name = _caddrbs.name
                        CType(caddrbs, Contact).targetAddress = _caddrbs.targetAddress
                        CType(caddrbs, Contact).targetIPVersion = _caddrbs.targetIPVersion
                        CType(caddrbs, Contact).targetPort = _caddrbs.targetPort
                        CType(caddrbs, Contact).type = _caddrbs.type
                    ElseIf ceditm = EditorMode.EditClient Then
                        caddrbs.myAddress = _caddrbs.myAddress
                        caddrbs.myPort = _caddrbs.myPort
                        caddrbs.name = _caddrbs.name
                    ElseIf ceditm = EditorMode.EditContact Then
                        CType(caddrbs, Contact).messagePassMode = _caddrbs.messagePassMode
                        caddrbs.myAddress = _caddrbs.myAddress
                        caddrbs.myPort = _caddrbs.myPort
                        caddrbs.name = _caddrbs.name
                        CType(caddrbs, Contact).targetAddress = _caddrbs.targetAddress
                        CType(caddrbs, Contact).targetIPVersion = _caddrbs.targetIPVersion
                        CType(caddrbs, Contact).targetPort = _caddrbs.targetPort
                        CType(caddrbs, Contact).type = _caddrbs.type
                    End If
                    frm.Invoke(Sub()
                                   frm.DialogResult = DialogResult.OK
                                   frm.Close()
                               End Sub)
                ElseIf ev.EventSource.sourceObj Is frm.nudport And ev.EventType = ETs.Leave Then
                    If ceditm <> EditorMode.EditClient Then
                        CType(_caddrbs, Contact).targetPort = args.held
                    End If
                ElseIf ev.EventSource.sourceObj Is frm.nudmyport And ev.EventType = ETs.Leave Then
                    _caddrbs.myPort = args.held
                ElseIf ev.EventSource.sourceObj Is frm.txtbxaddr And ev.EventType = ETs.Leave Then
                    If ceditm <> EditorMode.EditClient Then
                        CType(_caddrbs, Contact).targetAddress = args.held
                    End If
                ElseIf ev.EventSource.sourceObj Is frm.txtbxmyaddr And ev.EventType = ETs.Leave Then
                    _caddrbs.myAddress = args.held
                ElseIf ev.EventSource.sourceObj Is frm.txtbxname And ev.EventType = ETs.Leave Then
                    _caddrbs.name = args.held
                ElseIf ev.EventSource.sourceObj Is frm.cmbxtype And ev.EventType = ETs.Leave Then
                    If ceditm <> EditorMode.EditClient Then
                        CType(_caddrbs, Contact).type = args.held + 1
                        If _caddrbs.type = AddressableType.TCP Then
                            frm.txtbxmyaddr.Enabled = False
                            frm.nudmyport.Enabled = False
                        ElseIf _caddrbs.type = AddressableType.UDP Then
                            frm.txtbxmyaddr.Enabled = True
                            frm.nudmyport.Enabled = True
                        End If
                    End If
                ElseIf ev.EventSource.sourceObj Is frm.cmbxstrmode And ev.EventType = ETs.Leave Then
                    If ceditm <> EditorMode.EditClient Then
                        CType(_caddrbs, Contact).messagePassMode = args.held + 2
                    End If
                ElseIf ev.EventSource.sourceObj Is frm.cmbxipv And ev.EventType = ETs.Leave Then
                    If ceditm <> EditorMode.EditClient Then
                        CType(_caddrbs, Contact).targetIPVersion = args.held + 1
                    End If
                End If
            End If
        End If
        Return toret
    End Function

    Private Function reverseArray(Of t)(arr As t()) As t()
        Dim os As New Stack(Of t)(arr)
        Dim ol As New List(Of t)
        While os.Count > 0
            ol.Add(os.Pop())
        End While
        Return ol.ToArray
    End Function

    Private Function castObject(Of t)(f As Object) As t
        Try
            Dim nf As t = f
            Return nf
        Catch ex As InvalidCastException
            Return Nothing
        End Try
    End Function

    Private Function canCastObject(Of t)(f As Object) As Boolean
        Try
            Dim nf As t = f
            Return True
        Catch ex As InvalidCastException
            Return False
        End Try
    End Function

    Private Function getValueFromControl(Of t)(ctrl As Control, del As [Delegate]) As t
        Return ctrl.Invoke(del)
    End Function

    Public Property WorkerPump As WorkerPump Implements IWorkerPumpReceiver.WorkerPump
        Get
            Return wp
        End Get
        Set(value As WorkerPump)
            wp = value
        End Set
    End Property
End Class
