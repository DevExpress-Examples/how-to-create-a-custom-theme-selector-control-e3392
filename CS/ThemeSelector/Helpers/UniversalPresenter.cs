﻿// Developer Express Code Central Example:
// How to use the Theme Selector that is used in DemoCenter
// 
// This sample illustrates how to use our theme selector that is used in our demos
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3392

using System.Windows.Controls;
using DevExpress.Xpf.Core;

namespace ThemeSelector.Helpers {
#if SL
    public class UniversalPresenter : DXContentPresenter { }
#else
    public class UniversalPresenter : ContentPresenter { }
#endif
}
