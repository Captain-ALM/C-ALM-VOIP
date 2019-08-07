Imports captainalm.workerpumper

Friend Class ETs
    Inherits EventTypes
    Public Shared SelectedIndexChanged As New EventType("SelectedIndexChanged")
    Public Shared ValueChanged As New EventType("ValueChanged")
    Public Shared Scroll As New EventType("Scroll")
    Public Shared Leave As New EventType("Leave")
End Class