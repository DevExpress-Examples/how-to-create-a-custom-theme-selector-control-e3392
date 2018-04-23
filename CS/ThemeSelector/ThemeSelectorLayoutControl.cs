// Developer Express Code Central Example:
// How to use the Theme Selector that is used in DemoCenter
// 
// This sample illustrates how to use our theme selector that is used in our demos
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3392

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Core;
using ThemeSelector.Helpers;

namespace ThemeSelector.Layout {
    public class ThemeSelectorLayoutControl : Control {
        #region Dependency Properties
        public static readonly DependencyProperty SelectedValueProperty;
        public static readonly DependencyProperty BackgroundContentProperty;
        public static readonly DependencyProperty StickContentProperty;
        public static readonly DependencyProperty ValueTemplateProperty;
        public static readonly DependencyProperty CloseContentProperty;
        public static readonly DependencyProperty TextTemplateProperty;
        public static readonly DependencyProperty SelectingProgressProperty;
        public static readonly DependencyProperty Value0Property;
        public static readonly DependencyProperty Value1Property;
        public static readonly DependencyProperty Value2Property;
        public static readonly DependencyProperty Value3Property;
        public static readonly DependencyProperty Value4Property;
        public static readonly DependencyProperty Value5Property;
        public static readonly DependencyProperty DoSelectStoryboardProperty;
        static ThemeSelectorLayoutControl() {
            Type ownerType = typeof(ThemeSelectorLayoutControl);
            SelectedValueProperty = DependencyProperty.Register("SelectedValue", typeof(object), ownerType, new PropertyMetadata(null,
                (d, e) => ((ThemeSelectorLayoutControl)d).RaiseSelectedValueChanged(e)));
            BackgroundContentProperty = DependencyProperty.Register("BackgroundContent", typeof(object), ownerType, new PropertyMetadata(null));
            StickContentProperty = DependencyProperty.Register("StickContent", typeof(object), ownerType, new PropertyMetadata(null));
            ValueTemplateProperty = DependencyProperty.Register("ValueTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null));
            CloseContentProperty = DependencyProperty.Register("CloseContent", typeof(object), ownerType, new PropertyMetadata(null));
            TextTemplateProperty = DependencyProperty.Register("TextTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null));
            SelectingProgressProperty = DependencyProperty.Register("SelectingProgress", typeof(double), ownerType, new PropertyMetadata(0.0,
                (d, e) => ((ThemeSelectorLayoutControl)d).RaiseSelectingProgressChanged(e)));
            Value0Property = DependencyProperty.Register("Value0", typeof(object), ownerType, new PropertyMetadata(null));
            Value1Property = DependencyProperty.Register("Value1", typeof(object), ownerType, new PropertyMetadata(null));
            Value2Property = DependencyProperty.Register("Value2", typeof(object), ownerType, new PropertyMetadata(null));
            Value3Property = DependencyProperty.Register("Value3", typeof(object), ownerType, new PropertyMetadata(null));
            Value4Property = DependencyProperty.Register("Value4", typeof(object), ownerType, new PropertyMetadata(null));
            Value5Property = DependencyProperty.Register("Value5", typeof(object), ownerType, new PropertyMetadata(null));
            DoSelectStoryboardProperty = StoryboardProperty.Register("DoSelectStoryboard", ownerType, null,
                (d, e) => ((ThemeSelectorLayoutControl)d).RaiseDoSelectStoryboardChanged(e));
        }
        #endregion
        ContentControl centerPresenter;
        ContentControl textPresenter;
        Canvas valuesCanvas;
        List<ContentControl> valuesPresenters = new List<ContentControl>();
        List<Point> valuesPresentersInitialPositions;
        Point centerPosition;
        ContentControl selectedValuePresenter;
        Point selectedValuePresenterDestinationRelativePosition;
        ContentControl nextSelectedValuePresenter;
        Point nextSelectedValuePresenterSourceRelativePosition;
        bool isSelectCompleted = true;
        bool doSelectStoryboardCompletedHandled = true;

        public ThemeSelectorLayoutControl() {
            DefaultStyleKey = typeof(ThemeSelectorLayoutControl);
            InitializeVisualStates();
        }
        public object SelectedValue { get { return GetValue(SelectedValueProperty); } private set { SetValue(SelectedValueProperty, value); } }
        public object BackgroundContent { get { return GetValue(BackgroundContentProperty); } set { SetValue(BackgroundContentProperty, value); } }
        public object StickContent { get { return GetValue(StickContentProperty); } set { SetValue(StickContentProperty, value); } }
        public DataTemplate ValueTemplate { get { return (DataTemplate)GetValue(ValueTemplateProperty); } set { SetValue(ValueTemplateProperty, value); } }
        public object Value0 { get { return GetValue(Value0Property); } set { SetValue(Value0Property, value); } }
        public object Value1 { get { return GetValue(Value1Property); } set { SetValue(Value1Property, value); } }
        public object Value2 { get { return GetValue(Value2Property); } set { SetValue(Value2Property, value); } }
        public object Value3 { get { return GetValue(Value3Property); } set { SetValue(Value3Property, value); } }
        public object Value4 { get { return GetValue(Value4Property); } set { SetValue(Value4Property, value); } }
        public object Value5 { get { return GetValue(Value5Property); } set { SetValue(Value5Property, value); } }
        public object CloseContent { get { return GetValue(CloseContentProperty); } set { SetValue(CloseContentProperty, value); } }
        public DataTemplate TextTemplate { get { return (DataTemplate)GetValue(TextTemplateProperty); } set { SetValue(TextTemplateProperty, value); } }
        public Storyboard DoSelectStoryboard { get { return (Storyboard)GetValue(DoSelectStoryboardProperty); } set { SetValue(DoSelectStoryboardProperty, value); } }
        public event EventHandler SelectedValueChanged;
        public bool SelectValue(object nextSelectedValue, bool fast) {
            if(!isSelectCompleted) return false;
            BeginSelect(nextSelectedValue, fast);
            return true;
        }
        public double SelectingProgress { get { return (double)GetValue(SelectingProgressProperty); } set { SetValue(SelectingProgressProperty, value); } }
        void InitializeVisualStates() {
            VisualStateManager.GoToState(this, "TextIsCollapsed", false);
        }
        void FillValuesPresentersList() {
            ObjectToBooleanConverter isEnabledConverter = new ObjectToBooleanConverter();
            BooleanToAnyConverter opacityConverter = new BooleanToAnyConverter() { InnerConverter = isEnabledConverter, FalseValue = 0.0, TrueValue = 1.0 };
            foreach(DependencyObject d in this.valuesCanvas.Children) {
                ContentControl valuePresenter = d as ContentControl;
                if(valuePresenter == null || !valuePresenter.IsEnabled) continue;
                valuePresenter.RenderTransform = new TranslateTransform();
                valuePresenter.MouseEnter += OnValuePresenterMouseEnter;
                valuePresenter.MouseLeave += OnValuePresenterMouseLeave;
                valuePresenter.SetBinding(ContentControl.IsEnabledProperty, new Binding("Content") { Source = valuePresenter, Converter = isEnabledConverter });
                valuePresenter.SetBinding(ContentControl.OpacityProperty, new Binding("Content") { Source = valuePresenter, Converter = opacityConverter });
                valuesPresenters.Add(valuePresenter);
            }
        }
        void OnValuePresenterMouseEnter(object sender, MouseEventArgs e) {
            this.textPresenter.Content = ((ContentControl)sender).Content.ToString();
            VisualStateManager.GoToState(this, "TextIsVisible", true);
        }
        void OnValuePresenterMouseLeave(object sender, MouseEventArgs e) {
            VisualStateManager.GoToState(this, "TextIsCollapsed", true);
        }
        ContentControl FindValuePresenter(object value) {
            foreach(ContentControl valuePresenter in valuesPresenters) {
                if(object.Equals(valuePresenter.Content, value))
                    return valuePresenter;
            }
            return null;
        }
        Point FindInitialPosition(ContentControl valuePresenter) {
            if(valuePresenter == null) return new Point(0.0, 0.0);
            int index = valuesPresenters.IndexOf(valuePresenter);
            return index >= 0 ? valuesPresentersInitialPositions[index] : new Point(0.0, 0.0);
        }
        void FillValuesPresentersInitialPositionsList() {
            valuesPresentersInitialPositions = new List<Point>();
            foreach(ContentControl valuePresenter in valuesPresenters) {
                valuesPresentersInitialPositions.Add(GetPosition(valuePresenter));
            }
            centerPosition = GetPosition(this.centerPresenter);
        }
        void SetValuesPresentersHitTestVisibility(bool isHitTestVisible) {
            foreach(ContentControl valuePresenter in valuesPresenters) {
                valuePresenter.IsHitTestVisible = isHitTestVisible;
            }
        }
        void BeginSelect(object nextSelectedValue, bool fast) {
            isSelectCompleted = false;
            nextSelectedValuePresenter = FindValuePresenter(nextSelectedValue);
            if(valuesPresentersInitialPositions == null)
                FillValuesPresentersInitialPositionsList();
            SetValuesPresentersHitTestVisibility(false);
            MoveToFore(nextSelectedValuePresenter);
            Point nextSelectedValuePresenterPosition = GetPosition(nextSelectedValuePresenter);
            nextSelectedValuePresenterSourceRelativePosition.X = nextSelectedValuePresenterPosition.X - centerPosition.X;
            nextSelectedValuePresenterSourceRelativePosition.Y = nextSelectedValuePresenterPosition.Y - centerPosition.Y;
            Point selectedValuePresenterInitialPosition = FindInitialPosition(selectedValuePresenter);
            selectedValuePresenterDestinationRelativePosition.X = selectedValuePresenterInitialPosition.X - centerPosition.X;
            selectedValuePresenterDestinationRelativePosition.Y = selectedValuePresenterInitialPosition.Y - centerPosition.Y;
#if SL
            VisualStateManager.GoToState(this, "TextIsCollapsed", true);
#endif
            if(!fast) {
                doSelectStoryboardCompletedHandled = false;
                Storyboard.SetTarget(DoSelectStoryboard, this);
                DoSelectStoryboard.Begin();
            } else {
                EndSelect();
            }
        }
        void EndSelect() {
            DoSelectStoryboard.Stop();
            SelectingProgress = 0.0;
            MoveToBack(nextSelectedValuePresenter);
            UpdateMovingValuesPresentersPosition(1.0);
            selectedValuePresenter = nextSelectedValuePresenter;
            isSelectCompleted = true;
            SelectedValue = selectedValuePresenter == null ? null : selectedValuePresenter.Content;
            BackgroundHelper.DoInBackground(null, delegate() {
                SetValuesPresentersHitTestVisibility(true);
            });
        }
        Point GetPosition(ContentControl valuePresenter) {
            if(valuePresenter == null)
                valuePresenter = this.centerPresenter;
            return new Point(Canvas.GetLeft(valuePresenter), Canvas.GetTop(valuePresenter));
        }
        void SetPosition(ContentControl valuePresenter, Point position) {
            if(valuePresenter == null) return;
            Canvas.SetTop(valuePresenter, position.Y);
            Canvas.SetLeft(valuePresenter, position.X);
        }
        void MoveToFore(ContentControl valuePresenter) {
            if(valuePresenter == null) return;
            Canvas.SetZIndex(valuePresenter, 100);
        }
        void MoveToBack(ContentControl valuePresenter) {
            if(valuePresenter == null) return;
            Canvas.SetZIndex(valuePresenter, 1);
        }
        void RaiseSelectingProgressChanged(DependencyPropertyChangedEventArgs e) {
            double selectingProgress = (double)e.NewValue;
            UpdateMovingValuesPresentersPosition(selectingProgress);
        }
        void OnDoSelectStoryboardCompleted(object sender, EventArgs e) {
            if(doSelectStoryboardCompletedHandled) return;
            doSelectStoryboardCompletedHandled = true;
            EndSelect();
        }
        void UpdateMovingValuesPresentersPosition(double selectingProgress) {
            if(selectingProgress == 0.0) return;
            Point selectedValuePresenterPosition = new Point();
            selectedValuePresenterPosition.X = centerPosition.X + selectedValuePresenterDestinationRelativePosition.X * selectingProgress;
            selectedValuePresenterPosition.Y = centerPosition.Y + selectedValuePresenterDestinationRelativePosition.Y * selectingProgress;
            Point nextSelectedValuePresenterPosition = new Point();
            nextSelectedValuePresenterPosition.X = centerPosition.X + nextSelectedValuePresenterSourceRelativePosition.X * (1.0 - selectingProgress);
            nextSelectedValuePresenterPosition.Y = centerPosition.Y + nextSelectedValuePresenterSourceRelativePosition.Y * (1.0 - selectingProgress);
            SetPosition(selectedValuePresenter, selectedValuePresenterPosition);
            SetPosition(nextSelectedValuePresenter, nextSelectedValuePresenterPosition);
        }
        void RaiseSelectedValueChanged(DependencyPropertyChangedEventArgs e) {
            if(SelectedValueChanged != null)
                SelectedValueChanged(this, EventArgs.Empty);
        }
        void RaiseDoSelectStoryboardChanged(DependencyPropertyChangedEventArgs e) {
            Storyboard oldValue = (Storyboard)e.OldValue;
            Storyboard newValue = (Storyboard)e.NewValue;
            if(oldValue != null)
                oldValue.Completed -= OnDoSelectStoryboardCompleted;
            if(newValue != null)
                newValue.Completed += OnDoSelectStoryboardCompleted;
        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            this.centerPresenter = (ContentControl)GetTemplateChild("CenterPresenter");
            this.textPresenter = (ContentControl)GetTemplateChild("TextPresenter");
            this.valuesCanvas = (Canvas)GetTemplateChild("ValuesCanvas");
            FillValuesPresentersList();
        }
    }
}
