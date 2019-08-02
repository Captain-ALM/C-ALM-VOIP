Imports System.Net

Public Module [Global]
    Public description As String
    Public license As String
    Public selected_interfaceIPv4 As IPAddress = IPAddress.None
    Public selected_interfaceIPv6 As IPAddress = Nothing
    Public port_UDP_IPv4 As Integer = 1
    Public port_UDP_IPv6 As Integer = 1
    Public port_TCP_IPv4 As Integer = 1
    Public port_TCP_IPv6 As Integer = 1
    Public external_UDP_Address_IPv4 As String = IPAddress.None.ToString()
    Public external_UDP_Address_IPv6 As String = IPAddress.IPv6None.ToString()
    Public external_UDP_Port_IPv4 As Integer = 1
    Public external_UDP_Port_IPv6 As Integer = 1
    Public TCP_backlog As Integer = 1
    Public TCP_delay As Boolean = False
    Public input_device As Integer = -1
    Public TCP_remove_disconnected_clients As Boolean = False
    Public InListening As Boolean = False
End Module
