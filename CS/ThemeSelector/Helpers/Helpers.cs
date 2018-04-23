using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Collections;

namespace ThemeSelector.Helpers {
    public delegate bool AllowDelegate();
    public class SimpleControl : Control {
        public SimpleControl() { }
    }
    public static class StoryboardProperty {
        public static DependencyProperty Register(string name, Type ownerType, object defaultValue) {
            return Register(name, ownerType, defaultValue, null);
        }
        public static DependencyProperty Register(string name, Type ownerType, object defaultValue, PropertyChangedCallback propertyChangedCallback) {
            return DependencyProperty.Register(name, typeof(Storyboard), ownerType, new PropertyMetadata(defaultValue, propertyChangedCallback
#if !SL
                , CoerceStoryboard
#endif
));
        }
#if !SL
        static object CoerceStoryboard(DependencyObject d, object source) {
            Storyboard sb = (Storyboard)source;
            return sb == null ? null : sb.Clone();
        }
#endif
    }
}