using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Runtime.InteropServices;
using DevExpress.Data.Utils;
using DevExpress.Xpf.Utils.Native;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Reflection;

namespace ThemeSelector {
    public static class Program {
        class App : Application {
            public App() {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }
        [STAThread]
        public static void Main(string[] args) {
            App app = new App();
            app.Run();
        }
    }
}
