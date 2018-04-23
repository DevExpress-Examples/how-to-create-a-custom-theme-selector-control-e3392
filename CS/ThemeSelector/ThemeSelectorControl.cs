using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ThemeSelector.Layout;
using DevExpress.Xpf.Core;
using ThemeSelector.Helpers;
using System.Windows;

namespace ThemeSelector {
    public class ThemeSelectedEventArgs : EventArgs {
        public ThemeSelectedEventArgs(Theme theme) {
            Theme = theme;
        }
        public Theme Theme { get; private set; }
    }
    public delegate void ThemeSelectedEventHandler(object sender, ThemeSelectedEventArgs e);
    public class ThemeSelectorControl : UniversalControl {
        Theme defaultTheme;
        ThemeSelectorLayoutControl themeSelectorLayoutControl;

        public ThemeSelectorControl() {
            DefaultStyleKey = typeof(ThemeSelectorControl);
        }
        public event ThemeSelectedEventHandler ThemeSelected;
        public void SetThemes(IList<Theme> themes) {
            themeSelectorLayoutControl.Value0 = themes.Count > 0 ? themes[0] : null;
            themeSelectorLayoutControl.Value1 = themes.Count > 1 ? themes[1] : null;
            themeSelectorLayoutControl.Value2 = themes.Count > 2 ? themes[2] : null;
            themeSelectorLayoutControl.Value3 = themes.Count > 3 ? themes[3] : null;
            themeSelectorLayoutControl.Value4 = themes.Count > 4 ? themes[4] : null;
            themeSelectorLayoutControl.Value5 = themes.Count > 5 ? themes[5] : null;
        }
        public void SelectDefaultTheme(Theme theme) {
            defaultTheme = theme;
            RaiseThemeSelected(theme);
        }
        public void SelectTheme(Theme theme) {
            themeSelectorLayoutControl.SelectValue(theme, false);
        }
        public void SelectThemeFast(Theme theme) {
            themeSelectorLayoutControl.SelectValue(theme, true);
        }
        protected override void RaiseActualLoaded() {
            base.RaiseActualLoaded();
            themeSelectorLayoutControl.SelectValue(defaultTheme, true);
        }
        void RaiseThemeSelected(Theme theme) {
            if(ThemeSelected != null)
                ThemeSelected(this, new ThemeSelectedEventArgs(theme));
        }
        void OnThemeSelectorLayoutControlSelectedValueChanged(object sender, EventArgs e) {
            RaiseThemeSelected((Theme)themeSelectorLayoutControl.SelectedValue);
        }
        protected override void OnApplyTemplateOverride() {
            base.OnApplyTemplateOverride();
            themeSelectorLayoutControl = (ThemeSelectorLayoutControl)GetTemplateChild("ThemeSelectorLayoutControl");
            themeSelectorLayoutControl.SelectedValueChanged += OnThemeSelectorLayoutControlSelectedValueChanged;
        }
    }
}
