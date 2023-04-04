Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading

Public Class frmMain
    Dim children As New Dictionary(Of TabPage, Process)
    Dim server As New PipeServer

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        server.Start("\\.\pipe\TestMaster1")
        AddHandler server.MessageReceived, AddressOf server_MessageReceived
    End Sub

    Private Sub server_MessageReceived(message() As Byte)
        MessageBox.Show(New ASCIIEncoding().GetString(message))
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Dim p As Process = Process.Start("C:\CodingCool\Code\Projects\TabbedMultiProcess\ChildProcess\bin\Debug\net7.0-windows\ChildProcess.exe")
        p.WaitForInputIdle()
        Dim t As New TabPage(p.MainWindowTitle)
        tcApps.TabPages.Add(t)
        While WindowsAPI.SetParent(p.MainWindowHandle, t.Handle) = IntPtr.Zero
            Thread.Yield()
        End While
        WindowsAPI.ShowWindow(p.MainWindowHandle, WindowsConstants.SW_MAXIMIZE)
        SetBorderStyle(p)
        children.Add(t, p)
    End Sub

    Private Sub SetBorderStyle(p As Process)
        Dim style As Long = WindowsAPI.GetWindowLongPtr(New HandleRef(Me, p.MainWindowHandle), WindowsConstants.GWL_STYLE).ToInt64()
        style = style And Not (WindowsConstants.WS_CAPTION Or WindowsConstants.WS_BORDER Or WindowsConstants.WS_DLGFRAME)
        Dim styleValue As New IntPtr(style)
        WindowsAPI.SetWindowLongPtr(New HandleRef(Me, p.MainWindowHandle), WindowsConstants.GWL_STYLE, styleValue)
        style = WindowsAPI.GetWindowLongPtr(New HandleRef(Me, p.MainWindowHandle), WindowsConstants.GWL_STYLE)
        style = style And Not WindowsConstants.WS_POPUP
        style = style Or WindowsConstants.WS_CHILD
        styleValue = New IntPtr(style)
        WindowsAPI.SetWindowLongPtr(New HandleRef(Me, p.MainWindowHandle), WindowsConstants.GWL_STYLE, styleValue)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        server.SendMessage(New ASCIIEncoding().GetBytes($"Close:{children(tcApps.SelectedTab).MainWindowHandle}"))
        tcApps.TabPages.Remove(tcApps.SelectedTab)
    End Sub

    Private Sub tcApps_SizeChanged(sender As Object, e As EventArgs) Handles tcApps.SizeChanged
        For Each t As TabPage In tcApps.TabPages
            Dim p As Process = children(t)
            WindowsAPI.MoveWindow(p.MainWindowHandle, 0, 0, t.Width, t.Height, True)
            SetBorderStyle(p)
        Next
    End Sub
End Class