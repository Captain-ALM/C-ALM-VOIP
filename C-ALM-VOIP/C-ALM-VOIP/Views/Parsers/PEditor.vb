﻿Imports captainalm.workerpumper

Public Class PEditor
    Implements IEventParser
    Private wp As WorkerPump = Nothing
    Private _caddrbs As AddressableBase = Nothing

    Public Sub Parse(ev As WorkerEvent) Implements IEventParser.Parse
        If canCastObject(Of Editor)(ev.EventSource.sourceObj) Then
            Dim frm As Editor = castObject(Of Editor)(ev.EventSource.sourceObj)
            If ev.EventType = ETs.Shown Then
                _caddrbs = caddrbs.duplicateToNew()
                editsuccess = False
                editfin = False
            End If
        ElseIf ev.EventSource.parentObjs IsNot Nothing AndAlso ev.EventSource.parentObjs.Count > 0 Then
            Dim ra As Object = reverseArray(Of Object)(ev.EventSource.parentObjs.ToArray())
            If canCastObject(Of Editor)(ra(0)) Then
                Dim frm As Editor = castObject(Of Editor)(ra(0))
                Dim args As EventArgsDataContainer = castObject(Of EventArgsDataContainer)(ev.EventData)
                If ev.EventSource.sourceObj Is frm.OK_Button And ev.EventType = ETs.Click Then
                    If frm.chkipformat() And frm.chkprtnum() Then
                        frm.Invoke(Sub()
                                       frm.DialogResult = DialogResult.OK
                                       frm.Close()
                                   End Sub)
                        caddrbs.name = _caddrbs.name
                        If ceditm <> EditorMode.EditBlocker Then
                            If _caddrbs.type = AddressableType.UDP Then
                                caddrbs.myAddress = _caddrbs.myAddress
                                caddrbs.myPort = _caddrbs.myPort
                            ElseIf _caddrbs.type = AddressableType.TCP And ceditm = EditorMode.EditClient And TypeOf _caddrbs Is Client Then
                                CType(caddrbs, Client).advertisedAddress = CType(_caddrbs, Client).advertisedAddress
                                CType(caddrbs, Client).advertisedPort = CType(_caddrbs, Client).advertisedPort
                            End If
                            If ceditm = EditorMode.Create Or ceditm = EditorMode.EditContact Then
                                CType(caddrbs, Contact).type = _caddrbs.type
                                CType(caddrbs, Contact).targetAddress = _caddrbs.targetAddress
                                CType(caddrbs, Contact).targetIPVersion = _caddrbs.targetIPVersion
                                CType(caddrbs, Contact).targetPort = _caddrbs.targetPort
                            End If
                            caddrbs.messagePassMode = _caddrbs.messagePassMode
                        End If
                        _caddrbs = Nothing
                        editsuccess = True
                        editfin = True
                    Else
                        frm.Invoke(Sub() frm.OK_Button.Enabled = True)
                    End If
                ElseIf ev.EventSource.sourceObj Is frm.Cancel_Button And ev.EventType = ETs.Leave Then
                    editfin = True
                ElseIf ev.EventSource.sourceObj Is frm.nudport And ev.EventType = ETs.Leave Then
                    frm.chkprtnum()
                    If ceditm <> EditorMode.EditClient And ceditm <> EditorMode.EditBlocker Then
                        CType(_caddrbs, Contact).targetPort = args.held
                    End If
                ElseIf ev.EventSource.sourceObj Is frm.nudmyport And ev.EventType = ETs.Leave Then
                    frm.chkprtnum()
                    If ceditm <> EditorMode.EditBlocker Then
                        If _caddrbs.type = AddressableType.UDP Then
                            _caddrbs.myPort = args.held
                        ElseIf _caddrbs.type = AddressableType.TCP And ceditm = EditorMode.EditClient And TypeOf _caddrbs Is Client Then
                            CType(_caddrbs, Client).advertisedPort = args.held
                        End If
                    End If
                ElseIf ev.EventSource.sourceObj Is frm.txtbxaddr And ev.EventType = ETs.Leave Then
                    frm.chkipformat()
                    If ceditm <> EditorMode.EditClient And ceditm <> EditorMode.EditBlocker Then
                        CType(_caddrbs, Contact).targetAddress = args.held
                    End If
                ElseIf ev.EventSource.sourceObj Is frm.txtbxmyaddr And ev.EventType = ETs.Leave Then
                    If ceditm <> EditorMode.EditBlocker Then
                        If _caddrbs.type = AddressableType.UDP Then
                            _caddrbs.myAddress = args.held
                        ElseIf _caddrbs.type = AddressableType.TCP And ceditm = EditorMode.EditClient And TypeOf _caddrbs Is Client Then
                            CType(_caddrbs, Client).advertisedAddress = args.held
                        End If
                    End If
                ElseIf ev.EventSource.sourceObj Is frm.txtbxname And ev.EventType = ETs.Leave Then
                    _caddrbs.name = args.held
                ElseIf ev.EventSource.sourceObj Is frm.cmbxtype And ev.EventType = ETs.Leave Then
                    frm.chkprtnum()
                    If ceditm <> EditorMode.EditClient And ceditm <> EditorMode.EditBlocker Then
                        CType(_caddrbs, Contact).type = args.held + 1
                    End If
                ElseIf ev.EventSource.sourceObj Is frm.cmbxstrmode And ev.EventType = ETs.Leave Then
                    If ceditm <> EditorMode.EditBlocker Then _caddrbs.messagePassMode = args.held
                ElseIf ev.EventSource.sourceObj Is frm.cmbxipv And ev.EventType = ETs.Leave Then
                    frm.chkipformat()
                    If ceditm <> EditorMode.EditClient And ceditm <> EditorMode.EditBlocker Then
                        CType(_caddrbs, Contact).targetIPVersion = args.held + 1
                    End If
                End If
            End If
        End If
    End Sub

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
        Return GetType(t).IsAssignableFrom(f.GetType())
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
