using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;

namespace ThemeSelector {
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl {
        public UserControl1() {
            InitializeComponent();
            themeSelectorControl.ActualLoaded += new EventHandler(themeSelectorControl_ActualLoaded);
            themeSelectorControl.ThemeSelected += new ThemeSelectedEventHandler(themeSelectorControl_ThemeSelected);
            themeSelectorControl.SelectDefaultTheme(Theme.Office2007Silver);
        }
        void themeSelectorControl_ActualLoaded(object sender, EventArgs e) {
            themeSelectorControl.SetThemes(Theme.Themes);
        }
        void themeSelectorControl_ThemeSelected(object sender, ThemeSelectedEventArgs e) {
            ThemeManager.ApplicationThemeName = e.Theme.Name;
        }
    }
}
