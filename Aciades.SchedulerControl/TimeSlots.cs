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
using Aciades.BusinessNext.SchedulerControl.Common;
using Aciades.BusinessNext.SchedulerControl.DataAccess;

namespace Aciades.BusinessNext.SchedulerControl
{
    public class TimeSlots : ItemsControl
    {
        public Enumerations.TimeSlotsPerHour TimeSlotsPerHour
        {
            get { return (Enumerations.TimeSlotsPerHour)GetValue(TimeSlotsPerHourProperty); }
            set { SetValue(TimeSlotsPerHourProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeSlotsPerHour.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeSlotsPerHourProperty =
            DependencyProperty.Register("TimeSlotsPerHour", typeof(Enumerations.TimeSlotsPerHour), typeof(TimeSlots), new PropertyMetadata((s, e) => ((TimeSlots)s).OnTimeSlotPerHour_Changed(e)));

        private void OnTimeSlotPerHour_Changed(DependencyPropertyChangedEventArgs e)
        {
            TimeSlotsPerHour = (Enumerations.TimeSlotsPerHour)e.NewValue;
        }

        public Enumerations.HoursPerDay HoursPerDay
        {
            get { return (Enumerations.HoursPerDay)GetValue(HoursPerDayProperty); }
            set { SetValue(HoursPerDayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoursPerDay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoursPerDayProperty =
            DependencyProperty.Register("HoursPerDay", typeof(Enumerations.HoursPerDay), typeof(TimeSlots), new PropertyMetadata((s, e) => ((TimeSlots)s).OnHoursPerDayChanged(e)));

        private void OnHoursPerDayChanged(DependencyPropertyChangedEventArgs e)
        {
            HoursPerDay = (Enumerations.HoursPerDay)e.NewValue;
        }


        public TimeSlots()
        {
            DefaultStyleKey = typeof(TimeSlots);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PopulateTimeSlots();
        }

        private void PopulateTimeSlots()
        {
            TimeSlotProvider provider = new TimeSlotProvider(this);
            provider.RequestTimeSlotCompleted += OnRequestTimeSlots;
            provider.RequestTimeSlots();
        }

        void OnRequestTimeSlots(object sender, EventArgs e)
        {
            ItemsSource = (sender as TimeSlotProvider);
        }
    }
}
