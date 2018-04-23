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
Imports System.Windows.Controls
Imports ThemeSelector.Layout
Imports DevExpress.Xpf.Core
Imports ThemeSelector.Helpers
Imports System.Windows

Namespace ThemeSelector
    Public Class ThemeSelectedEventArgs
        Inherits EventArgs

        Public Sub New(ByVal theme As Theme)
            Me.Theme = theme
        End Sub
        Private privateTheme As Theme
        Public Property Theme() As Theme
            Get
                Return privateTheme
            End Get
            Private Set(ByVal value As Theme)
                privateTheme = value
            End Set
        End Property
    End Class
    Public Delegate Sub ThemeSelectedEventHandler(ByVal sender As Object, ByVal e As ThemeSelectedEventArgs)
    Public Class ThemeSelectorControl
        Inherits UniversalControl

        Private defaultTheme As Theme
        Private themeSelectorLayoutControl As ThemeSelectorLayoutControl

        Public Sub New()
            DefaultStyleKey = GetType(ThemeSelectorControl)
        End Sub
        Public Event ThemeSelected As ThemeSelectedEventHandler
        Public Sub SetThemes(ByVal themes As IList(Of Theme))
            themeSelectorLayoutControl.Value0 = If(themes.Count > 0, themes(0), Nothing)
            themeSelectorLayoutControl.Value1 = If(themes.Count > 1, themes(1), Nothing)
            themeSelectorLayoutControl.Value2 = If(themes.Count > 2, themes(2), Nothing)
            themeSelectorLayoutControl.Value3 = If(themes.Count > 3, themes(3), Nothing)
            themeSelectorLayoutControl.Value4 = If(themes.Count > 4, themes(4), Nothing)
            themeSelectorLayoutControl.Value5 = If(themes.Count > 5, themes(5), Nothing)
        End Sub
        Public Sub SelectDefaultTheme(ByVal theme As Theme)
            defaultTheme = theme
            RaiseThemeSelected(theme)
        End Sub
        Public Sub SelectTheme(ByVal theme As Theme)
            themeSelectorLayoutControl.SelectValue(theme, False)
        End Sub
        Public Sub SelectThemeFast(ByVal theme As Theme)
            themeSelectorLayoutControl.SelectValue(theme, True)
        End Sub
        Protected Overrides Sub RaiseActualLoaded()
            MyBase.RaiseActualLoaded()
            themeSelectorLayoutControl.SelectValue(defaultTheme, True)
        End Sub
        Private Sub RaiseThemeSelected(ByVal theme As Theme)
            RaiseEvent ThemeSelected(Me, New ThemeSelectedEventArgs(theme))
        End Sub
        Private Sub OnThemeSelectorLayoutControlSelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            RaiseThemeSelected(DirectCast(themeSelectorLayoutControl.SelectedValue, Theme))
        End Sub
        Protected Overrides Sub OnApplyTemplateOverride()
            MyBase.OnApplyTemplateOverride()
            themeSelectorLayoutControl = CType(GetTemplateChild("ThemeSelectorLayoutControl"), ThemeSelectorLayoutControl)
            AddHandler themeSelectorLayoutControl.SelectedValueChanged, AddressOf OnThemeSelectorLayoutControlSelectedValueChanged
        End Sub
    End Class
End Namespace
