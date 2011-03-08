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
    public class TimeSlotItem : Control
    {
        public string Hour
        {
            get { return (string)GetValue(HourProperty); }
            set { SetValue(HourProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Hour.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HourProperty =
            DependencyProperty.Register("Hour", typeof(string), typeof(TimeSlotItem), new PropertyMetadata((s, e) => ((TimeSlotItem)s).OnHourChanged(e)));

        private void OnHourChanged(DependencyPropertyChangedEventArgs e)
        {
            Hour = (string)e.NewValue;
        }

        public string Minute
        {
            get { return (string)GetValue(MinuteProperty); }
            set { SetValue(MinuteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Minute.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinuteProperty =
            DependencyProperty.Register("Minute", typeof(string), typeof(TimeSlotItem),
                                        new PropertyMetadata((s, e) => ((TimeSlotItem)s).OnMinuteChanged(e)));

        private void OnMinuteChanged(DependencyPropertyChangedEventArgs e)
        {
            Minute = (string)e.NewValue;
        }

        public TimeSlotItem()
        {
            DefaultStyleKey = typeof(TimeSlotItem);
        }
    }
}
