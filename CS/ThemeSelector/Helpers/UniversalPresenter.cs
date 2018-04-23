using System.Windows.Controls;
using DevExpress.Xpf.Core;

namespace ThemeSelector.Helpers {
#if SL
    public class UniversalPresenter : DXContentPresenter { }
#else
    public class UniversalPresenter : ContentPresenter { }
#endif
}
