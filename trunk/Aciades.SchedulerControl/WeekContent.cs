using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Aciades.Businessnext.SchedulerControl
{
    public class WeekContent : ItemsControl
    {
        public ucContent Owner = null;
        public double DayContentWidth = 0;
        private ScrollViewer svContent=null;

        public WeekContent()
        {
            DefaultStyleKey = typeof (WeekContent);
            this.Loaded += WeekContent_Loaded;
        }

        void WeekContent_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollToCurrentTime();
        }

        private void ScrollToCurrentTime()
        {
            if (svContent == null) return;
            TimeSlots timeSlots = (TimeSlots)base.GetTemplateChild("PART_TimeSlots");
            if (timeSlots != null && timeSlots.Items.Count > 0)
            {
                TimeSlotItem currentTimeItem = (TimeSlotItem)(timeSlots).Items[0];
                double i = currentTimeItem.Height;
                svContent.ScrollToVerticalOffset(i * DateTime.Now.Hour);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            svContent = (ScrollViewer)base.GetTemplateChild("svContent");
            if (svContent == null) return;

            ScrollViewerExtensions.SetIsMouseWheelScrollingEnabled(svContent, true);
        }
    }
}
