' Developer Express Code Central Example:
' How to use the Theme Selector that is used in DemoCenter
' 
' This sample illustrates how to use our theme selector that is used in our demos
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3392

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Imaging
Imports System.Collections

Namespace ThemeSelector.Helpers
    Public Delegate Function AllowDelegate() As Boolean
    Public Class SimpleControl
        Inherits Control

        Public Sub New()
        End Sub
    End Class
    Public NotInheritable Class StoryboardProperty

        Private Sub New()
        End Sub

        Public Shared Function Register(ByVal name As String, ByVal ownerType As Type, ByVal defaultValue As Object) As DependencyProperty
            Return Register(name, ownerType, defaultValue, Nothing)
        End Function
        Public Shared Function Register(ByVal name As String, ByVal ownerType As Type, ByVal defaultValue As Object, ByVal propertyChangedCallback As PropertyChangedCallback) As DependencyProperty
#If Not SL Then
            Return DependencyProperty.Register(name, GetType(Storyboard), ownerType, New PropertyMetadata(defaultValue, propertyChangedCallback, AddressOf CoerceStoryboard))
#Else
            Return DependencyProperty.Register(name, GetType(Storyboard), ownerType, New PropertyMetadata(defaultValue, propertyChangedCallback))
#End If
        End Function
#If Not SL Then
        Private Shared Function CoerceStoryboard(ByVal d As DependencyObject, ByVal source As Object) As Object
            Dim sb As Storyboard = DirectCast(source, Storyboard)
            Return If(sb Is Nothing, Nothing, sb.Clone())
        End Function
#End If
    End Class
End Namespace