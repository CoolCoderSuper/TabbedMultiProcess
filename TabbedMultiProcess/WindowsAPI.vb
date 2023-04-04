Imports System.Runtime.InteropServices

Public Class WindowsAPI
    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function SetParent(hWndChild As IntPtr, hWndNewParent As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function ShowWindow(hWnd As IntPtr, nCmdShow As Integer) As Boolean
    End Function

    Public Shared Function GetWindowLongPtr(hWnd As HandleRef, nIndex As Integer) As IntPtr
        If IntPtr.Size = 8 Then ' This is towork in both 64 bit and 32 bit 
            Return GetWindowLongPtr64(hWnd, nIndex)
        Else
            Return GetWindowLong32(hWnd, nIndex)
        End If
    End Function

    <DllImport("user32.dll", EntryPoint:="GetWindowLong")>
    Private Shared Function GetWindowLong32(hWnd As HandleRef, nIndex As Integer) As IntPtr
    End Function

    <DllImport("user32.dll", EntryPoint:="GetWindowLongPtr")>
    Private Shared Function GetWindowLongPtr64(hWnd As HandleRef, nIndex As Integer) As IntPtr
    End Function

    Public Shared Function SetWindowLongPtr(hWnd As HandleRef, nIndex As Integer, dwNewLong As IntPtr) As IntPtr
        If IntPtr.Size = 8 Then ' This is to work in both 64 bit and 32 bit
            Return SetWindowLongPtr64(hWnd, nIndex, dwNewLong)
        Else
            Return New IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()))
        End If
    End Function

    <DllImport("user32.dll", EntryPoint:="SetWindowLong")>
    Private Shared Function SetWindowLong32(hWnd As HandleRef, nIndex As Integer, dwNewLong As Integer) As Integer
    End Function

    <DllImport("user32.dll", EntryPoint:="SetWindowLongPtr")>
    Private Shared Function SetWindowLongPtr64(hWnd As HandleRef, nIndex As Integer, dwNewLong As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function MoveWindow(hWnd As IntPtr, X As Integer, Y As Integer, nWidth As Integer, nHeight As Integer, bRepaint As Boolean) As Boolean
    End Function

End Class