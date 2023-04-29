﻿Imports Avalonia

Module Program
    ' Initialization code. Don't use any Avalonia, third-party APIs or any
    ' SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    ' yet and stuff might break.
    <STAThread>
    Public Sub Main(args As String())
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args)
    End Sub

    ' Avalonia configuration, don't remove; also used by visual designer.
    Public Function BuildAvaloniaApp() As AppBuilder
        Return AppBuilder.Configure(Of App)().UsePlatformDetect().LogToTrace()
    End Function
End Module