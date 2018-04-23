' Developer Express Code Central Example:
' How to use the Theme Selector that is used in DemoCenter
' 
' This sample illustrates how to use our theme selector that is used in our demos
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3392

Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports DevExpress.Xpf.Core
Imports ThemeSelector.Helpers

Namespace ThemeSelector.Layout
    Public Class ThemeSelectorLayoutControl
        Inherits Control

        #Region "Dependency Properties"
        Public Shared ReadOnly SelectedValueProperty As DependencyProperty
        Public Shared ReadOnly BackgroundContentProperty As DependencyProperty
        Public Shared ReadOnly StickContentProperty As DependencyProperty
        Public Shared ReadOnly ValueTemplateProperty As DependencyProperty
        Public Shared ReadOnly CloseContentProperty As DependencyProperty
        Public Shared ReadOnly TextTemplateProperty As DependencyProperty
        Public Shared ReadOnly SelectingProgressProperty As DependencyProperty
        Public Shared ReadOnly Value0Property As DependencyProperty
        Public Shared ReadOnly Value1Property As DependencyProperty
        Public Shared ReadOnly Value2Property As DependencyProperty
        Public Shared ReadOnly Value3Property As DependencyProperty
        Public Shared ReadOnly Value4Property As DependencyProperty
        Public Shared ReadOnly Value5Property As DependencyProperty
        Public Shared ReadOnly DoSelectStoryboardProperty As DependencyProperty
        Shared Sub New()
            Dim ownerType As Type = GetType(ThemeSelectorLayoutControl)
            SelectedValueProperty = DependencyProperty.Register("SelectedValue", GetType(Object), ownerType, New PropertyMetadata(Nothing, Sub(d, e) CType(d, ThemeSelectorLayoutControl).RaiseSelectedValueChanged(e)))
            BackgroundContentProperty = DependencyProperty.Register("BackgroundContent", GetType(Object), ownerType, New PropertyMetadata(Nothing))
            StickContentProperty = DependencyProperty.Register("StickContent", GetType(Object), ownerType, New PropertyMetadata(Nothing))
            ValueTemplateProperty = DependencyProperty.Register("ValueTemplate", GetType(DataTemplate), ownerType, New PropertyMetadata(Nothing))
            CloseContentProperty = DependencyProperty.Register("CloseContent", GetType(Object), ownerType, New PropertyMetadata(Nothing))
            TextTemplateProperty = DependencyProperty.Register("TextTemplate", GetType(DataTemplate), ownerType, New PropertyMetadata(Nothing))
            SelectingProgressProperty = DependencyProperty.Register("SelectingProgress", GetType(Double), ownerType, New PropertyMetadata(0.0, Sub(d, e) CType(d, ThemeSelectorLayoutControl).RaiseSelectingProgressChanged(e)))
            Value0Property = DependencyProperty.Register("Value0", GetType(Object), ownerType, New PropertyMetadata(Nothing))
            Value1Property = DependencyProperty.Register("Value1", GetType(Object), ownerType, New PropertyMetadata(Nothing))
            Value2Property = DependencyProperty.Register("Value2", GetType(Object), ownerType, New PropertyMetadata(Nothing))
            Value3Property = DependencyProperty.Register("Value3", GetType(Object), ownerType, New PropertyMetadata(Nothing))
            Value4Property = DependencyProperty.Register("Value4", GetType(Object), ownerType, New PropertyMetadata(Nothing))
            Value5Property = DependencyProperty.Register("Value5", GetType(Object), ownerType, New PropertyMetadata(Nothing))
            DoSelectStoryboardProperty = StoryboardProperty.Register("DoSelectStoryboard", ownerType, Nothing, Sub(d, e) CType(d, ThemeSelectorLayoutControl).RaiseDoSelectStoryboardChanged(e))
        End Sub
        #End Region
        Private centerPresenter As ContentControl
        Private textPresenter As ContentControl
        Private valuesCanvas As Canvas
        Private valuesPresenters As New List(Of ContentControl)()
        Private valuesPresentersInitialPositions As List(Of Point)
        Private centerPosition As Point
        Private selectedValuePresenter As ContentControl
        Private selectedValuePresenterDestinationRelativePosition As Point
        Private nextSelectedValuePresenter As ContentControl
        Private nextSelectedValuePresenterSourceRelativePosition As Point
        Private isSelectCompleted As Boolean = True
        Private doSelectStoryboardCompletedHandled As Boolean = True

        Public Sub New()
            DefaultStyleKey = GetType(ThemeSelectorLayoutControl)
            InitializeVisualStates()
        End Sub
        Public Property SelectedValue() As Object
            Get
                Return GetValue(SelectedValueProperty)
            End Get
            Private Set(ByVal value As Object)
                SetValue(SelectedValueProperty, value)
            End Set
        End Property
        Public Property BackgroundContent() As Object
            Get
                Return GetValue(BackgroundContentProperty)
            End Get
            Set(ByVal value As Object)
                SetValue(BackgroundContentProperty, value)
            End Set
        End Property
        Public Property StickContent() As Object
            Get
                Return GetValue(StickContentProperty)
            End Get
            Set(ByVal value As Object)
                SetValue(StickContentProperty, value)
            End Set
        End Property
        Public Property ValueTemplate() As DataTemplate
            Get
                Return DirectCast(GetValue(ValueTemplateProperty), DataTemplate)
            End Get
            Set(ByVal value As DataTemplate)
                SetValue(ValueTemplateProperty, value)
            End Set
        End Property
        Public Property Value0() As Object
            Get
                Return GetValue(Value0Property)
            End Get
            Set(ByVal value As Object)
                SetValue(Value0Property, value)
            End Set
        End Property
        Public Property Value1() As Object
            Get
                Return GetValue(Value1Property)
            End Get
            Set(ByVal value As Object)
                SetValue(Value1Property, value)
            End Set
        End Property
        Public Property Value2() As Object
            Get
                Return GetValue(Value2Property)
            End Get
            Set(ByVal value As Object)
                SetValue(Value2Property, value)
            End Set
        End Property
        Public Property Value3() As Object
            Get
                Return GetValue(Value3Property)
            End Get
            Set(ByVal value As Object)
                SetValue(Value3Property, value)
            End Set
        End Property
        Public Property Value4() As Object
            Get
                Return GetValue(Value4Property)
            End Get
            Set(ByVal value As Object)
                SetValue(Value4Property, value)
            End Set
        End Property
        Public Property Value5() As Object
            Get
                Return GetValue(Value5Property)
            End Get
            Set(ByVal value As Object)
                SetValue(Value5Property, value)
            End Set
        End Property
        Public Property CloseContent() As Object
            Get
                Return GetValue(CloseContentProperty)
            End Get
            Set(ByVal value As Object)
                SetValue(CloseContentProperty, value)
            End Set
        End Property
        Public Property TextTemplate() As DataTemplate
            Get
                Return DirectCast(GetValue(TextTemplateProperty), DataTemplate)
            End Get
            Set(ByVal value As DataTemplate)
                SetValue(TextTemplateProperty, value)
            End Set
        End Property
        Public Property DoSelectStoryboard() As Storyboard
            Get
                Return DirectCast(GetValue(DoSelectStoryboardProperty), Storyboard)
            End Get
            Set(ByVal value As Storyboard)
                SetValue(DoSelectStoryboardProperty, value)
            End Set
        End Property
        Public Event SelectedValueChanged As EventHandler
        Public Function SelectValue(ByVal nextSelectedValue As Object, ByVal fast As Boolean) As Boolean
            If Not isSelectCompleted Then
                Return False
            End If
            BeginSelect(nextSelectedValue, fast)
            Return True
        End Function
        Public Property SelectingProgress() As Double
            Get
                Return DirectCast(GetValue(SelectingProgressProperty), Double)
            End Get
            Set(ByVal value As Double)
                SetValue(SelectingProgressProperty, value)
            End Set
        End Property
        Private Sub InitializeVisualStates()
            VisualStateManager.GoToState(Me, "TextIsCollapsed", False)
        End Sub
        Private Sub FillValuesPresentersList()
            Dim isEnabledConverter As New ObjectToBooleanConverter()
            Dim opacityConverter As New BooleanToAnyConverter() With {.InnerConverter = isEnabledConverter, .FalseValue = 0.0, .TrueValue = 1.0}
            For Each d As DependencyObject In Me.valuesCanvas.Children
                Dim valuePresenter As ContentControl = TryCast(d, ContentControl)
                If valuePresenter Is Nothing OrElse (Not valuePresenter.IsEnabled) Then
                    Continue For
                End If
                valuePresenter.RenderTransform = New TranslateTransform()
                AddHandler valuePresenter.MouseEnter, AddressOf OnValuePresenterMouseEnter
                AddHandler valuePresenter.MouseLeave, AddressOf OnValuePresenterMouseLeave
                valuePresenter.SetBinding(ContentControl.IsEnabledProperty, New Binding("Content") With {.Source = valuePresenter, .Converter = isEnabledConverter})
                valuePresenter.SetBinding(ContentControl.OpacityProperty, New Binding("Content") With {.Source = valuePresenter, .Converter = opacityConverter})
                valuesPresenters.Add(valuePresenter)
            Next d
        End Sub
        Private Sub OnValuePresenterMouseEnter(ByVal sender As Object, ByVal e As MouseEventArgs)
            Me.textPresenter.Content = DirectCast(sender, ContentControl).Content.ToString()
            VisualStateManager.GoToState(Me, "TextIsVisible", True)
        End Sub
        Private Sub OnValuePresenterMouseLeave(ByVal sender As Object, ByVal e As MouseEventArgs)
            VisualStateManager.GoToState(Me, "TextIsCollapsed", True)
        End Sub
        Private Function FindValuePresenter(ByVal value As Object) As ContentControl
            For Each valuePresenter As ContentControl In valuesPresenters
                If Object.Equals(valuePresenter.Content, value) Then
                    Return valuePresenter
                End If
            Next valuePresenter
            Return Nothing
        End Function
        Private Function FindInitialPosition(ByVal valuePresenter As ContentControl) As Point
            If valuePresenter Is Nothing Then
                Return New Point(0.0, 0.0)
            End If
            Dim index As Integer = valuesPresenters.IndexOf(valuePresenter)
            Return If(index >= 0, valuesPresentersInitialPositions(index), New Point(0.0, 0.0))
        End Function
        Private Sub FillValuesPresentersInitialPositionsList()
            valuesPresentersInitialPositions = New List(Of Point)()
            For Each valuePresenter As ContentControl In valuesPresenters
                valuesPresentersInitialPositions.Add(GetPosition(valuePresenter))
            Next valuePresenter
            centerPosition = GetPosition(Me.centerPresenter)
        End Sub
        Private Sub SetValuesPresentersHitTestVisibility(ByVal isHitTestVisible As Boolean)
            For Each valuePresenter As ContentControl In valuesPresenters
                valuePresenter.IsHitTestVisible = isHitTestVisible
            Next valuePresenter
        End Sub
        Private Sub BeginSelect(ByVal nextSelectedValue As Object, ByVal fast As Boolean)
            isSelectCompleted = False
            nextSelectedValuePresenter = FindValuePresenter(nextSelectedValue)
            If valuesPresentersInitialPositions Is Nothing Then
                FillValuesPresentersInitialPositionsList()
            End If
            SetValuesPresentersHitTestVisibility(False)
            MoveToFore(nextSelectedValuePresenter)
            Dim nextSelectedValuePresenterPosition As Point = GetPosition(nextSelectedValuePresenter)
            nextSelectedValuePresenterSourceRelativePosition.X = nextSelectedValuePresenterPosition.X - centerPosition.X
            nextSelectedValuePresenterSourceRelativePosition.Y = nextSelectedValuePresenterPosition.Y - centerPosition.Y
            Dim selectedValuePresenterInitialPosition As Point = FindInitialPosition(selectedValuePresenter)
            selectedValuePresenterDestinationRelativePosition.X = selectedValuePresenterInitialPosition.X - centerPosition.X
            selectedValuePresenterDestinationRelativePosition.Y = selectedValuePresenterInitialPosition.Y - centerPosition.Y
#If SL Then
            VisualStateManager.GoToState(Me, "TextIsCollapsed", True)
#End If
            If Not fast Then
                doSelectStoryboardCompletedHandled = False
                Storyboard.SetTarget(DoSelectStoryboard, Me)
                DoSelectStoryboard.Begin()
            Else
                EndSelect()
            End If
        End Sub
        Private Sub EndSelect()
            DoSelectStoryboard.Stop()
            SelectingProgress = 0.0
            MoveToBack(nextSelectedValuePresenter)
            UpdateMovingValuesPresentersPosition(1.0)
            selectedValuePresenter = nextSelectedValuePresenter
            isSelectCompleted = True
            SelectedValue = If(selectedValuePresenter Is Nothing, Nothing, selectedValuePresenter.Content)
            BackgroundHelper.DoInBackground(Nothing, Sub() SetValuesPresentersHitTestVisibility(True))
        End Sub
        Private Function GetPosition(ByVal valuePresenter As ContentControl) As Point
            If valuePresenter Is Nothing Then
                valuePresenter = Me.centerPresenter
            End If
            Return New Point(Canvas.GetLeft(valuePresenter), Canvas.GetTop(valuePresenter))
        End Function
        Private Sub SetPosition(ByVal valuePresenter As ContentControl, ByVal position As Point)
            If valuePresenter Is Nothing Then
                Return
            End If
            Canvas.SetTop(valuePresenter, position.Y)
            Canvas.SetLeft(valuePresenter, position.X)
        End Sub
        Private Sub MoveToFore(ByVal valuePresenter As ContentControl)
            If valuePresenter Is Nothing Then
                Return
            End If
            Canvas.SetZIndex(valuePresenter, 100)
        End Sub
        Private Sub MoveToBack(ByVal valuePresenter As ContentControl)
            If valuePresenter Is Nothing Then
                Return
            End If
            Canvas.SetZIndex(valuePresenter, 1)
        End Sub
        Private Sub RaiseSelectingProgressChanged(ByVal e As DependencyPropertyChangedEventArgs)

            Dim selectingProgress_Renamed As Double = DirectCast(e.NewValue, Double)
            UpdateMovingValuesPresentersPosition(selectingProgress_Renamed)
        End Sub
        Private Sub OnDoSelectStoryboardCompleted(ByVal sender As Object, ByVal e As EventArgs)
            If doSelectStoryboardCompletedHandled Then
                Return
            End If
            doSelectStoryboardCompletedHandled = True
            EndSelect()
        End Sub
        Private Sub UpdateMovingValuesPresentersPosition(ByVal selectingProgress As Double)
            If selectingProgress = 0.0 Then
                Return
            End If
            Dim selectedValuePresenterPosition As New Point()
            selectedValuePresenterPosition.X = centerPosition.X + selectedValuePresenterDestinationRelativePosition.X * selectingProgress
            selectedValuePresenterPosition.Y = centerPosition.Y + selectedValuePresenterDestinationRelativePosition.Y * selectingProgress
            Dim nextSelectedValuePresenterPosition As New Point()
            nextSelectedValuePresenterPosition.X = centerPosition.X + nextSelectedValuePresenterSourceRelativePosition.X * (1.0 - selectingProgress)
            nextSelectedValuePresenterPosition.Y = centerPosition.Y + nextSelectedValuePresenterSourceRelativePosition.Y * (1.0 - selectingProgress)
            SetPosition(selectedValuePresenter, selectedValuePresenterPosition)
            SetPosition(nextSelectedValuePresenter, nextSelectedValuePresenterPosition)
        End Sub
        Private Sub RaiseSelectedValueChanged(ByVal e As DependencyPropertyChangedEventArgs)
            RaiseEvent SelectedValueChanged(Me, EventArgs.Empty)
        End Sub
        Private Sub RaiseDoSelectStoryboardChanged(ByVal e As DependencyPropertyChangedEventArgs)
            Dim oldValue As Storyboard = DirectCast(e.OldValue, Storyboard)
            Dim newValue As Storyboard = DirectCast(e.NewValue, Storyboard)
            If oldValue IsNot Nothing Then
                RemoveHandler oldValue.Completed, AddressOf OnDoSelectStoryboardCompleted
            End If
            If newValue IsNot Nothing Then
                AddHandler newValue.Completed, AddressOf OnDoSelectStoryboardCompleted
            End If
        End Sub
        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            Me.centerPresenter = CType(GetTemplateChild("CenterPresenter"), ContentControl)
            Me.textPresenter = CType(GetTemplateChild("TextPresenter"), ContentControl)
            Me.valuesCanvas = CType(GetTemplateChild("ValuesCanvas"), Canvas)
            FillValuesPresentersList()
        End Sub
    End Class
End Namespace
