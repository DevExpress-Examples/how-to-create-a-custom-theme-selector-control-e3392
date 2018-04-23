' Developer Express Code Central Example:
' How to use the Theme Selector that is used in DemoCenter
' 
' This sample illustrates how to use our theme selector that is used in our demos
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3392

Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media
Imports DevExpress.Utils
Imports DevExpress.Xpf.Utils

Namespace ThemeSelector.Helpers
    Public Class StringToVisibilityConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim s As String = TryCast(value, String)
            Return If(String.IsNullOrEmpty(s), Visibility.Collapsed, Visibility.Visible)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
    Public Class ScaleConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim d? As Double = CType(value, Double?)
            If d Is Nothing Then
                Return 0.0
            End If
            Return CDbl(d) * DirectCast(parameter, Double)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
    Public Class TextToResourceUriConverter
        Implements IValueConverter

        Public Property AssemblyMarker() As Object
        Public Property Prefix() As String
        Public Property Suffix() As String
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return Nothing
            End If
            Dim s As String = value.ToString()
            Return AssemblyHelper.GetResourceUri(AssemblyMarker.GetType().Assembly, Prefix & s & Suffix)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
    Public Class BooleanToAnyConverter
        Inherits DependencyObject
        Implements IValueConverter

        #Region "Dependency Properties"
        Public Shared ReadOnly TrueValueProperty As DependencyProperty
        Public Shared ReadOnly FalseValueProperty As DependencyProperty
        Shared Sub New()
            Dim ownerType As Type = GetType(BooleanToAnyConverter)
            TrueValueProperty = DependencyProperty.Register("TrueValue", GetType(Object), ownerType, New PropertyMetadata(Nothing))
            FalseValueProperty = DependencyProperty.Register("FalseValue", GetType(Object), ownerType, New PropertyMetadata(Nothing))
        End Sub
        #End Region

        Public Property TrueValue() As Object
            Get
                Return GetValue(TrueValueProperty)
            End Get
            Set(ByVal value As Object)
                SetValue(TrueValueProperty, value)
            End Set
        End Property
        Public Property FalseValue() As Object
            Get
                Return GetValue(FalseValueProperty)
            End Get
            Set(ByVal value As Object)
                SetValue(FalseValueProperty, value)
            End Set
        End Property
        Public Property InnerConverter() As IValueConverter
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If InnerConverter IsNot Nothing Then
                value = InnerConverter.Convert(value, GetType(Boolean), parameter, culture)
            End If
            Dim b? As Boolean = CType(value, Boolean?)
            If b Is Nothing Then
                Return Nothing
            End If
            Return If(CBool(b), TrueValue, FalseValue)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
End Namespace
