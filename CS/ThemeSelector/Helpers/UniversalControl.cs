using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ThemeSelector.Helpers {
    public class UniversalControl : Control, ISupportInitialize {
#if SL
        bool onLayoutUpdatedInvoked = false;
        bool onApplyTemplateInvoked = false;
#else
        bool onLoadedInvoked = false;
#endif

        public UniversalControl() {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
#if SL
            LayoutUpdated += OnLayoutUpdated;
#endif
        }
        public event EventHandler ActualLoaded;
        public event EventHandler Initialize;
#if SL
        public void BeginInit() { BeginInitCore(); }
        public void EndInit() { EndInitCore(); }
#else
        public sealed override void BeginInit() {
            base.BeginInit();
            BeginInitCore();
        }
        public sealed override void EndInit() {
            EndInitCore();
            base.EndInit();
        }
#endif
        protected virtual void BeginInitCore() { }
        protected virtual void EndInitCore() { }
        public sealed override void OnApplyTemplate() {
            base.OnApplyTemplate();
            OnApplyTemplateOverride();
            if(Initialize != null)
                Initialize(this, EventArgs.Empty);
#if SL
            onApplyTemplateInvoked = true;
#endif
        }
        protected virtual void OnApplyTemplateOverride() { }
        protected virtual void OnLoaded(object sender, RoutedEventArgs e) {
#if !SL
            if(!onLoadedInvoked) {
                onLoadedInvoked = true;
                RaiseActualLoaded();
            }
#endif
        }
        protected virtual void OnUnloaded(object sender, RoutedEventArgs e) { }
#if SL
        protected virtual void OnLayoutUpdated(object sender, EventArgs e) {
            if(!onLayoutUpdatedInvoked && onApplyTemplateInvoked) {
                onLayoutUpdatedInvoked = true;
                RaiseActualLoaded();
            }
        }
#endif
        protected virtual void RaiseActualLoaded() {
            EventHandler realyLoaded = ActualLoaded;
            if(realyLoaded != null)
                realyLoaded(this, EventArgs.Empty);
        }
    }
}
