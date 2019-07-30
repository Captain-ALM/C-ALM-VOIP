Imports captainalm.workerpumper

Friend Class ETs
    Inherits EventTypes
    Public Shared SelectedIndexChanged As New EventType("SelectedIndexChanged")
    Public Shared ValueChanged As New EventType("ValueChanged")
    Public Shared Scroll As New EventType("Scroll")
    Public Shared CheckedChanged As New EventType("CheckedChanged")
    Public Shared TextChanged As New EventType("TextChanged")
    Public Shared Leave As New EventType("Leave")
End Class