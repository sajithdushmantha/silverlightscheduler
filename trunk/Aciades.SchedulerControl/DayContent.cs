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

namespace Aciades.BusinessNext.SchedulerControl
{
    public class DayContent : ItemsControl
    {
        private ScrollViewer svContent = null;
        public ucContent Owner = null;
        private TimeSlots timeSlots = null;
        public double timeSlotItemHieght = 0;

        public DayContent()
        {
            DefaultStyleKey = typeof(DayContent);
            this.Loaded += DayContent_Loaded;
        }

        void DayContent_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollToCurrentTime();
        }

        private void ScrollToCurrentTime()
        {
            if (svContent == null) return;
            timeSlots = (TimeSlots)base.GetTemplateChild("PART_TimeSlots");
            if (timeSlots != null && timeSlots.Items.Count > 0)
            {
                TimeSlotItem currentTimeItem = (TimeSlotItem)timeSlots.Items[0];
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

            var dayContentGrid = (Grid)GetTemplateChild("PART_dayContentGrid");
            if (dayContentGrid == null) return;

            dayContentGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(125, GridUnitType.Pixel)
            });
        }
    }
}
