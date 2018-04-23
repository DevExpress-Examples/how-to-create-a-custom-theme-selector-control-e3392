using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace ThemeSelector.Helpers {
    public static class BackgroundHelper {
        static Dispatcher dispatcher;

        public static void DoInBackground(Action backgroundAction, Action mainThreadAction) {
            DoInBackground(backgroundAction, mainThreadAction, 30);
        }
        public static void DoInBackground(Action backgroundAction, Action mainThreadAction, int milliseconds) {
            Thread thread = new Thread(delegate() {
                Thread.Sleep(milliseconds);
                if(backgroundAction != null)
                    backgroundAction();
                if(mainThreadAction != null)
                    Dispatcher.BeginInvoke(mainThreadAction);
            });
            thread.Start();
        }
        public static void DoInMainThread(Action action) {
            AutoResetEvent done = new AutoResetEvent(false);
            Dispatcher.BeginInvoke((Action)delegate() {
                action();
                done.Set();
            });
            done.WaitOne();
        }
        public static Dispatcher Dispatcher {
            get {
                if(dispatcher == null)
                    dispatcher = DefaultDispatcher;
                return dispatcher;
            }
            set { dispatcher = value; }
        }
        static Dispatcher DefaultDispatcher {
            get {
#if SL
                return Deployment.Current.Dispatcher;
#else
                return Application.Current.Dispatcher;
#endif
            }
        }
    }
}
