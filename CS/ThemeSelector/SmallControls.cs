using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using DevExpress.Xpf.Core;
using ThemeSelector.Layout;
using System.Windows.Controls;

namespace ThemeSelector {
    public class ThemeButtonControl : Control {
        #region Dependency Properties
        public static readonly DependencyProperty ThemeProperty;
        public static readonly DependencyProperty ThemeSelectorLayoutControlProperty;
        static ThemeButtonControl() {
            Type ownerType = typeof(ThemeButtonControl);
            ThemeProperty = DependencyProperty.Register("Theme", typeof(Theme), ownerType, new PropertyMetadata(null));
            ThemeSelectorLayoutControlProperty = DependencyProperty.Register("ThemeSelectorLayoutControl", typeof(ThemeSelectorLayoutControl), ownerType, new PropertyMetadata(null));
        }
        #endregion
        ButtonBase button;

        public ThemeButtonControl() {
            DefaultStyleKey = typeof(ThemeButtonControl);
        }
        public Theme Theme { get { return (Theme)GetValue(ThemeProperty); } set { SetValue(ThemeProperty, value); } }
        public ThemeSelectorLayoutControl ThemeSelectorLayoutControl { get { return (ThemeSelectorLayoutControl)GetValue(ThemeSelectorLayoutControlProperty); } set { SetValue(ThemeSelectorLayoutControlProperty, value); } }
        void OnButtonClick(object sender, RoutedEventArgs e) {
            Theme theme = ((ButtonBase)sender).DataContext as Theme;
            ThemeSelectorLayoutControl.SelectValue(theme, false);
        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            button = (ButtonBase)GetTemplateChild("Button");
            button.Click += OnButtonClick;
        }
    }
    public class CloseButtonControl : Control {
        ButtonBase button;

        public CloseButtonControl() {
            DefaultStyleKey = typeof(CloseButtonControl);
        }
        protected virtual void OnButtonClick(object sender, RoutedEventArgs e) { }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            button = (ButtonBase)GetTemplateChild("Button");
            button.Click += OnButtonClick;
        }
    }
    public class CloseThemeSelectorButtonControl : CloseButtonControl {
        protected override void OnButtonClick(object sender, RoutedEventArgs e) {
            base.OnButtonClick(sender, e);
            // close
        }
    }
}
