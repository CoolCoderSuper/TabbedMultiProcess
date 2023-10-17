Imports System.Text

Public Module Program
    ReadOnly _client As New PipeClient
    Dim _id As String

    Public Sub Main(args As String())
        _id = args(0)
        _client.Connect("\\.\pipe\TestMaster1")
        AddHandler _client.MessageReceived, AddressOf MessageRecieved
        Console.ReadKey()
    End Sub

    Private Async Sub MessageRecieved(message() As Byte)
        Dim cmd As String = Encoding.UTF8.GetString(message)
        Console.WriteLine($"Received: {cmd}")
        If cmd = $"Close:{_id}" Then
            CloseSafe()
        ElseIf cmd = $"Message:{_id}" Then
            _client.SendMessage(Encoding.UTF8.GetBytes("Hi"))
        End If
    End Sub

    Private Sub CloseSafe()
        _client.Disconnect()
        End
    End Sub
End Module