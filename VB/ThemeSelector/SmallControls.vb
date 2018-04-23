' Developer Express Code Central Example:
' How to use the Theme Selector that is used in DemoCenter
' 
' This sample illustrates how to use our theme selector that is used in our demos
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3392

Imports System
Imports System.Windows
Imports System.Windows.Controls.Primitives
Imports DevExpress.Xpf.Core
Imports ThemeSelector.Layout
Imports System.Windows.Controls

Namespace ThemeSelector
    Public Class ThemeButtonControl
        Inherits Control

        #Region "Dependency Properties"
        Public Shared ReadOnly ThemeProperty As DependencyProperty
        Public Shared ReadOnly ThemeSelectorLayoutControlProperty As DependencyProperty
        Shared Sub New()
            Dim ownerType As Type = GetType(ThemeButtonControl)
            ThemeProperty = DependencyProperty.Register("Theme", GetType(Theme), ownerType, New PropertyMetadata(Nothing))
            ThemeSelectorLayoutControlProperty = DependencyProperty.Register("ThemeSelectorLayoutControl", GetType(ThemeSelectorLayoutControl), ownerType, New PropertyMetadata(Nothing))
        End Sub
        #End Region
        Private button As ButtonBase

        Public Sub New()
            DefaultStyleKey = GetType(ThemeButtonControl)
        End Sub
        Public Property Theme() As Theme
            Get
                Return DirectCast(GetValue(ThemeProperty), Theme)
            End Get
            Set(ByVal value As Theme)
                SetValue(ThemeProperty, value)
            End Set
        End Property
        Public Property ThemeSelectorLayoutControl() As ThemeSelectorLayoutControl
            Get
                Return DirectCast(GetValue(ThemeSelectorLayoutControlProperty), ThemeSelectorLayoutControl)
            End Get
            Set(ByVal value As ThemeSelectorLayoutControl)
                SetValue(ThemeSelectorLayoutControlProperty, value)
            End Set
        End Property
        Private Sub OnButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Dim theme_Renamed As Theme = TryCast(DirectCast(sender, ButtonBase).DataContext, Theme)
            ThemeSelectorLayoutControl.SelectValue(theme_Renamed, False)
        End Sub
        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            button = CType(GetTemplateChild("Button"), ButtonBase)
            AddHandler button.Click, AddressOf OnButtonClick
        End Sub
    End Class
    Public Class CloseButtonControl
        Inherits Control

        Private button As ButtonBase

        Public Sub New()
            DefaultStyleKey = GetType(CloseButtonControl)
        End Sub
        Protected Overridable Sub OnButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
        End Sub
        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            button = CType(GetTemplateChild("Button"), ButtonBase)
            AddHandler button.Click, AddressOf OnButtonClick
        End Sub
    End Class
    Public Class CloseThemeSelectorButtonControl
        Inherits CloseButtonControl

        Protected Overrides Sub OnButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MyBase.OnButtonClick(sender, e)
            ' close
        End Sub
    End Class
End Namespace
