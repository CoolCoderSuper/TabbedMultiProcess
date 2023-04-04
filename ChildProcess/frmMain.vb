Imports System.Text

Public Class frmMain
    Dim client As New PipeClient

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        client.Connect("\\.\pipe\TestMaster1")
        AddHandler client.MessageReceived, AddressOf MessageRecieved
    End Sub

    Private Sub MessageRecieved(message() As Byte)
        Dim coder As New ASCIIEncoding
        Dim cmd As String = coder.GetString(message)
        lblCommand.Text = cmd
        If cmd = $"Close:{Handle}" Then
            CloseSafe()
        End If
    End Sub

    Private Sub btnHi_Click(sender As Object, e As EventArgs) Handles btnHi.Click
        Dim coder As New ASCIIEncoding
        client.SendMessage(coder.GetBytes("Hi"))
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CloseSafe()
    End Sub

    Private Sub CloseSafe()
        client.Disconnect()
        Application.Exit()
    End Sub

End Class
