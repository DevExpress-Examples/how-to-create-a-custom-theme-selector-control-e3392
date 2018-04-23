using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using DevExpress.Utils;
using DevExpress.Xpf.Utils;

namespace ThemeSelector.Helpers {
    public class StringToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string s = value as string;
            return string.IsNullOrEmpty(s) ? Visibility.Collapsed : Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class ScaleConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            double? d = value as double?;
            if(d == null) return 0.0;
            return (double)d * (double)parameter;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class TextToResourceUriConverter : IValueConverter {
        public object AssemblyMarker { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null) return null;
            string s = value.ToString();
            return AssemblyHelper.GetResourceUri(AssemblyMarker.GetType().Assembly, Prefix + s + Suffix);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class BooleanToAnyConverter : DependencyObject, IValueConverter {
        #region Dependency Properties
        public static readonly DependencyProperty TrueValueProperty;
        public static readonly DependencyProperty FalseValueProperty;
        static BooleanToAnyConverter() {
            Type ownerType = typeof(BooleanToAnyConverter);
            TrueValueProperty = DependencyProperty.Register("TrueValue", typeof(object), ownerType, new PropertyMetadata(null));
            FalseValueProperty = DependencyProperty.Register("FalseValue", typeof(object), ownerType, new PropertyMetadata(null));
        }
        #endregion

        public object TrueValue { get { return GetValue(TrueValueProperty); } set { SetValue(TrueValueProperty, value); } }
        public object FalseValue { get { return GetValue(FalseValueProperty); } set { SetValue(FalseValueProperty, value); } }
        public IValueConverter InnerConverter { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(InnerConverter != null)
                value = InnerConverter.Convert(value, typeof(bool), parameter, culture);
            bool? b = value as bool?;
            if(b == null) return null;
            return (bool)b ? TrueValue : FalseValue;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
