Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading

Public Class frmMain
    Dim children As New Dictionary(Of TabPage, (Process, Integer))
    Dim server As New PipeServer

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        server.Start("\\.\pipe\TestMaster1")
        AddHandler server.MessageReceived, AddressOf server_MessageReceived
    End Sub

    Private Sub server_MessageReceived(message() As Byte)
        Dim s As String = Encoding.UTF8.GetString(message)
        If s.Contains("Closing") Then
            Dim handle As New IntPtr(CInt(s.Split(":")(1)))
            For Each t As TabPage In tcApps.TabPages
                If children(t).Item2 = handle Then
                    Invoke(Sub() tcApps.TabPages.Remove(t))
                    Exit For
                End If
            Next
        Else
            MessageBox.Show(s)
        End If
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Dim id As Integer = Random.Shared.Next
        Dim p As Process = Process.Start("C:\CodingCool\Code\Projects\TabbedMultiProcess\ChildProcess\bin\Debug\net7.0\ChildProcess.exe", id)
        p.EnableRaisingEvents = True
        AddHandler p.Exited, AddressOf p_Exited
        Dim t As New TabPage(p.MainWindowTitle)
        Dim button As New Button With {.Text = "Send Message", .Dock = DockStyle.Top}
        AddHandler button.Click, Sub()
                                     server.SendMessage(Encoding.UTF8.GetBytes($"Message:{id}"))
                                 End Sub
        t.Controls.Add(button)
        tcApps.TabPages.Add(t)
        children.Add(t, (p, id))
    End Sub

    Private Sub p_Exited(sender As Object, e As EventArgs)
        For Each t As TabPage In tcApps.TabPages
            If children(t).Item1 Is sender Then
                Invoke(Sub() tcApps.TabPages.Remove(t))
                Exit For
            End If
        Next
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        server.SendMessage(Encoding.UTF8.GetBytes($"Close:{children(tcApps.SelectedTab).Item2}"))
        tcApps.TabPages.Remove(tcApps.SelectedTab)
    End Sub

    Private Sub btnBye_Click(sender As Object, e As EventArgs) Handles btnBye.Click
        server.SendMessage(Encoding.UTF8.GetBytes("Bye"))
    End Sub
End Class