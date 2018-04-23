// Developer Express Code Central Example:
// How to use the Theme Selector that is used in DemoCenter
// 
// This sample illustrates how to use our theme selector that is used in our demos
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3392

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
