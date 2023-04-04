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
        btnHi = New Button()
        btnClose = New Button()
        lblCommand = New Label()
        SuspendLayout()
        ' 
        ' btnHi
        ' 
        btnHi.Location = New Point(12, 12)
        btnHi.Name = "btnHi"
        btnHi.Size = New Size(75, 23)
        btnHi.TabIndex = 0
        btnHi.Text = "Hi"
        btnHi.UseVisualStyleBackColor = True
        ' 
        ' btnClose
        ' 
        btnClose.Location = New Point(93, 12)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(75, 23)
        btnClose.TabIndex = 1
        btnClose.Text = "Close"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' lblCommand
        ' 
        lblCommand.AutoSize = True
        lblCommand.Location = New Point(12, 38)
        lblCommand.Name = "lblCommand"
        lblCommand.Size = New Size(0, 15)
        lblCommand.TabIndex = 2
        ' 
        ' frmMain
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(880, 534)
        Controls.Add(lblCommand)
        Controls.Add(btnClose)
        Controls.Add(btnHi)
        Name = "frmMain"
        Text = "ChildProcess"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnHi As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents lblCommand As Label
End Class
