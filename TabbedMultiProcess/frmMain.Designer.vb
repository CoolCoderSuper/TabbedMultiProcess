<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        MenuStrip1 = New MenuStrip()
        btnStart = New ToolStripMenuItem()
        btnClose = New ToolStripMenuItem()
        tcApps = New TabControl()
        btnBye = New ToolStripMenuItem()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {btnStart, btnClose, btnBye})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(911, 24)
        MenuStrip1.TabIndex = 0
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' btnStart
        ' 
        btnStart.Name = "btnStart"
        btnStart.Size = New Size(43, 20)
        btnStart.Text = "Start"
        ' 
        ' btnClose
        ' 
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(48, 20)
        btnClose.Text = "Close"
        ' 
        ' tcApps
        ' 
        tcApps.Dock = DockStyle.Fill
        tcApps.Location = New Point(0, 24)
        tcApps.Name = "tcApps"
        tcApps.SelectedIndex = 0
        tcApps.Size = New Size(911, 535)
        tcApps.TabIndex = 1
        ' 
        ' btnBye
        ' 
        btnBye.Name = "btnBye"
        btnBye.Size = New Size(38, 20)
        btnBye.Text = "Bye"
        ' 
        ' frmMain
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(911, 559)
        Controls.Add(tcApps)
        Controls.Add(MenuStrip1)
        MainMenuStrip = MenuStrip1
        Name = "frmMain"
        Text = "MultiProcessApp"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents btnStart As ToolStripMenuItem
    Friend WithEvents tcApps As TabControl
    Friend WithEvents btnClose As ToolStripMenuItem
    Friend WithEvents btnBye As ToolStripMenuItem
End Class
