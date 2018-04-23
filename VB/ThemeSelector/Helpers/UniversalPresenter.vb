' Developer Express Code Central Example:
' How to use the Theme Selector that is used in DemoCenter
' 
' This sample illustrates how to use our theme selector that is used in our demos
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3392

Imports System.Windows.Controls
Imports DevExpress.Xpf.Core

Namespace ThemeSelector.Helpers
#If SL Then
    Public Class UniversalPresenter
        Inherits DXContentPresenter

    End Class
#Else
    Public Class UniversalPresenter
        Inherits ContentPresenter

    End Class
#End If
End Namespace
