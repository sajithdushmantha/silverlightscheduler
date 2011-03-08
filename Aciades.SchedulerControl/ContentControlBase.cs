using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Aciades.BusinessNext.SchedulerControl
{
    public class ContentControlBase : ContentControl
    {
        private DispatcherTimer DoubleClickTimer = new DispatcherTimer();

        public delegate void MouseDblClickHandler(object sender, MouseButtonEventArgs e);

        public event MouseDblClickHandler MouseDblClick;

        public ContentControlBase()
        {
            DoubleClickTimer.Interval = new TimeSpan(0, 0, 0, 3000);
            DoubleClickTimer.Tick += new EventHandler(DoubleClickTimer_Tick);
            MouseLeftButtonDown += new MouseButtonEventHandler(ContentControlBase_MouseLeftButtonDown);
            MouseLeftButtonUp += new MouseButtonEventHandler(ContentControlBase_MouseLeftButtonUp);
            MouseMove += new MouseEventHandler(ContentControlBase_MouseMove);
        }

        void ContentControlBase_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        void ContentControlBase_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
            base.OnMouseLeftButtonUp(e);
        }

        void ContentControlBase_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DoubleClickTimer.IsEnabled)
            {
                DoubleClickTimer.Stop();
                if (MouseDblClick != null)
                    MouseDblClick(sender, e);
            }
            else
            {
                DoubleClickTimer.Start();
            }

            OnMouseLeftButtonDown(e);
        }

        void DoubleClickTimer_Tick(object sender, EventArgs e)
        {
            DoubleClickTimer.Stop();
        }
    }
}
