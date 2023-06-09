﻿Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading

Public Class frmMain
    Dim children As New Dictionary(Of TabPage, Process)
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
                If children(t).MainWindowHandle = handle Then
                    Invoke(Sub() tcApps.TabPages.Remove(t))
                    Exit For
                End If
            Next
        Else
            MessageBox.Show(s)
        End If
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Dim p As Process = Process.Start("C:\CodingCool\Code\Projects\TabbedMultiProcess\ChildProcess\bin\Debug\net7.0-windows\ChildProcess.exe")
        p.WaitForInputIdle()
        p.EnableRaisingEvents = True
        AddHandler p.Exited, AddressOf p_Exited
        Dim t As New TabPage(p.MainWindowTitle)
        tcApps.TabPages.Add(t)
        While WindowsAPI.SetParent(p.MainWindowHandle, t.Handle) = IntPtr.Zero
            Thread.Yield()
        End While
        WindowsAPI.ShowWindow(p.MainWindowHandle, WindowsConstants.SW_MAXIMIZE)
        SetBorderStyle(p)
        children.Add(t, p)
    End Sub

    Private Sub p_Exited(sender As Object, e As EventArgs)
        For Each t As TabPage In tcApps.TabPages
            If children(t) Is sender Then
                tcApps.TabPages.Remove(t)
                Exit For
            End If
        Next
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
        server.SendMessage(Encoding.UTF8.GetBytes($"Close:{children(tcApps.SelectedTab).MainWindowHandle}"))
        tcApps.TabPages.Remove(tcApps.SelectedTab)
    End Sub

    Private Sub tcApps_SizeChanged(sender As Object, e As EventArgs) Handles tcApps.SizeChanged
        For Each t As TabPage In tcApps.TabPages
            Dim p As Process = children(t)
            WindowsAPI.MoveWindow(p.MainWindowHandle, 0, 0, t.Width, t.Height, True)
            SetBorderStyle(p)
        Next
    End Sub

    Private Sub btnBye_Click(sender As Object, e As EventArgs) Handles btnBye.Click
        server.SendMessage(Encoding.UTF8.GetBytes("Bye"))
    End Sub
End Class