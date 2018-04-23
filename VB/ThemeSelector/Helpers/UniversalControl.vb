' Developer Express Code Central Example:
' How to use the Theme Selector that is used in DemoCenter
' 
' This sample illustrates how to use our theme selector that is used in our demos
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3392

Imports System
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls

Namespace ThemeSelector.Helpers
    Public Class UniversalControl
        Inherits Control
        Implements ISupportInitialize

#If SL Then
        Private onLayoutUpdatedInvoked As Boolean = False
        Private onApplyTemplateInvoked As Boolean = False
#Else
        Private onLoadedInvoked As Boolean = False
#End If

        Public Sub New()
            AddHandler Loaded, AddressOf OnLoaded
            AddHandler Unloaded, AddressOf OnUnloaded
#If SL Then
            AddHandler LayoutUpdated, AddressOf OnLayoutUpdated
#End If
        End Sub
        Public Event ActualLoaded As EventHandler
        Public Event Initialize As EventHandler
#If SL Then
        Public Overloads Sub BeginInit() Implements ISupportInitialize.BeginInit
            BeginInitCore()
        End Sub
        Public Overloads Sub EndInit() Implements ISupportInitialize.EndInit
            EndInitCore()
        End Sub
#Else
        Public NotOverridable Overrides Sub BeginInit()
            MyBase.BeginInit()
            BeginInitCore()
        End Sub
        Public NotOverridable Overrides Sub EndInit()
            EndInitCore()
            MyBase.EndInit()
        End Sub
#End If
        Protected Overridable Sub BeginInitCore()
        End Sub
        Protected Overridable Sub EndInitCore()
        End Sub
        Public NotOverridable Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            OnApplyTemplateOverride()
            RaiseEvent Initialize(Me, EventArgs.Empty)
#If SL Then
            onApplyTemplateInvoked = True
#End If
        End Sub
        Protected Overridable Sub OnApplyTemplateOverride()
        End Sub
        Protected Overridable Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
#If Not SL Then
            If Not onLoadedInvoked Then
                onLoadedInvoked = True
                RaiseActualLoaded()
            End If
#End If
        End Sub
        Protected Overridable Sub OnUnloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        End Sub
#If SL Then
        Protected Overridable Sub OnLayoutUpdated(ByVal sender As Object, ByVal e As EventArgs)
            If (Not onLayoutUpdatedInvoked) AndAlso onApplyTemplateInvoked Then
                onLayoutUpdatedInvoked = True
                RaiseActualLoaded()
            End If
        End Sub
#End If
        Protected Overridable Sub RaiseActualLoaded()
            Dim realyLoaded As EventHandler = ActualLoadedEvent
            If realyLoaded IsNot Nothing Then
                realyLoaded(Me, EventArgs.Empty)
            End If
        End Sub
    End Class
End Namespace
