Imports Microsoft.VisualBasic
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
        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim s As String = TryCast(value, String)
            Return If(String.IsNullOrEmpty(s), Visibility.Collapsed, Visibility.Visible)
        End Function
        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
    Public Class ScaleConverter
        Implements IValueConverter
        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim d As System.Nullable(Of Double) = CType(value, System.Nullable(Of Double))
            If d Is Nothing Then
                Return 0.0
            End If
            Return CDbl(d) * CDbl(parameter)
        End Function
        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
    Public Class TextToResourceUriConverter
        Implements IValueConverter
        Public Property AssemblyMarker() As Object
            Get
                Return m_AssemblyMarker
            End Get
            Set(value As Object)
                m_AssemblyMarker = value
            End Set
        End Property
        Private m_AssemblyMarker As Object
        Public Property Prefix() As String
            Get
                Return m_Prefix
            End Get
            Set(value As String)
                m_Prefix = Value
            End Set
        End Property
        Private m_Prefix As String
        Public Property Suffix() As String
            Get
                Return m_Suffix
            End Get
            Set(value As String)
                m_Suffix = Value
            End Set
        End Property
        Private m_Suffix As String
        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return Nothing
            End If
            Dim s As String = value.ToString()
            Return AssemblyHelper.GetResourceUri(AssemblyMarker.[GetType]().Assembly, Prefix & s & Suffix)
        End Function
        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
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
            Set(value As Object)
                SetValue(TrueValueProperty, value)
            End Set
        End Property
        Public Property FalseValue() As Object
            Get
                Return GetValue(FalseValueProperty)
            End Get
            Set(value As Object)
                SetValue(FalseValueProperty, value)
            End Set
        End Property
        Public Property InnerConverter() As IValueConverter
            Get
                Return m_InnerConverter
            End Get
            Set(value As IValueConverter)
                m_InnerConverter = Value
            End Set
        End Property
        Private m_InnerConverter As IValueConverter
        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If InnerConverter IsNot Nothing Then
                value = InnerConverter.Convert(value, GetType(Boolean), parameter, culture)
            End If
            Dim b As System.Nullable(Of Boolean) = CType(value, System.Nullable(Of Boolean))
            If b Is Nothing Then
                Return Nothing
            End If
            Return If(CBool(b), TrueValue, FalseValue)
        End Function
        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class

End Namespace
