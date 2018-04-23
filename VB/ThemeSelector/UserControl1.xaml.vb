' Developer Express Code Central Example:
' How to use the Theme Selector that is used in DemoCenter
' 
' This sample illustrates how to use our theme selector that is used in our demos
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3392

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Core

Namespace ThemeSelector
    ''' <summary>
    ''' Interaction logic for UserControl1.xaml
    ''' </summary>
    Partial Public Class UserControl1
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
            AddHandler themeSelectorControl.ActualLoaded, AddressOf themeSelectorControl_ActualLoaded
            AddHandler themeSelectorControl.ThemeSelected, AddressOf themeSelectorControl_ThemeSelected
            themeSelectorControl.SelectDefaultTheme(Theme.Office2007Silver)
        End Sub
        Private Sub themeSelectorControl_ActualLoaded(ByVal sender As Object, ByVal e As EventArgs)
            themeSelectorControl.SetThemes(Theme.Themes)
        End Sub
        Private Sub themeSelectorControl_ThemeSelected(ByVal sender As Object, ByVal e As ThemeSelectedEventArgs)
            ThemeManager.ApplicationThemeName = e.Theme.Name
        End Sub
    End Class
End Namespace
