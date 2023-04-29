Imports System.Text
Imports Avalonia.Controls
Imports Avalonia.Interactivity
Imports Avalonia.Markup.Xaml
Imports Avalonia.Threading

Partial Public Class MainWindow
    Inherits Window
    Dim client As New PipeClient
    Private WithEvents btnHi As Button
    Private WithEvents btnClose As Button
    Private WithEvents lblCommand As Label

    Public Sub New()
        AvaloniaXamlLoader.Load(Me)
        Dim btnClickMe As Button = FindControl(Of Button)("btnClickMe")
        Dim lblNumber As Label = FindControl(Of Label)("lblNumber")
        AddHandler btnClickMe.Click, Sub() lblNumber.Content = Random.Shared.NextInt64(1, 1000)
        btnHi = FindControl(Of Button)("btnHi")
        btnClose = FindControl(Of Button)("btnClose")
        lblCommand = FindControl(Of Label)("lblCommand")
        client.Connect("\\.\pipe\TestMaster1")
        AddHandler client.MessageReceived, AddressOf MessageReceived
    End Sub

    Private Async Sub MessageReceived(message() As Byte)
        Dim cmd As String = Encoding.UTF8.GetString(message)
        Dispatcher.UIThread.Post(Sub() lblCommand.Content = cmd, DispatcherPriority.Background)
        If cmd = $"Close:{PlatformImpl.Handle.Handle}" Then
            CloseSafe()
        End If
    End Sub

    Private Sub CloseSafe()
        client.Disconnect()
        Close()
    End Sub

    Private Sub btnHi_Click(sender As Object, e As RoutedEventArgs) Handles btnHi.Click
        client.SendMessage(Encoding.UTF8.GetBytes("Hi"))
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        client.SendMessage(Encoding.UTF8.GetBytes($"Closing:{PlatformImpl.Handle.Handle}"))
        CloseSafe()
    End Sub
End Class