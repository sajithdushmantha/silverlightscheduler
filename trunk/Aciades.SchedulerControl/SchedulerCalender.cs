using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Aciades.Businessnext.SchedulerControl.Common;

namespace Aciades.Businessnext.SchedulerControl
{
    public class SchedulerCalender : Control
    {

        #region Dependency Property
        public DateTime CurrentDate
        {
            get { return (DateTime)GetValue(CurrentDateProperty); }
            set { SetValue(CurrentDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentDateProperty =
            DependencyProperty.Register("CurrentDate", typeof(DateTime), typeof(SchedulerCalender), new PropertyMetadata((s, e) => ((SchedulerCalender)s).OnCurrentDateChanged(e)));

        private void OnCurrentDateChanged(DependencyPropertyChangedEventArgs e)
        {
            CurrentDate = (DateTime)e.NewValue;
        }

        public Common.Enumerations.DateView CurrentDateView
        {
            get { return (Common.Enumerations.DateView)GetValue(CurrentDateViewProperty); }
            set { SetValue(CurrentDateViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentDateView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentDateViewProperty =
            DependencyProperty.Register("CurrentDateView", typeof(Common.Enumerations.DateView), typeof(SchedulerCalender), new PropertyMetadata((s, e) => ((SchedulerCalender)s).OnCurrentDateView_Changed(e)));

        private void OnCurrentDateView_Changed(DependencyPropertyChangedEventArgs e)
        {
            CurrentDateView = (Common.Enumerations.DateView)e.NewValue;
        }



        public Enumerations.TimeSlotsPerHour TimeSlotPerHour
        {
            get { return (Enumerations.TimeSlotsPerHour)GetValue(TimeSlotPerHourProperty); }
            set { SetValue(TimeSlotPerHourProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeSlotPerHour.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeSlotPerHourProperty =
            DependencyProperty.Register("TimeSlotPerHour", typeof(Enumerations.TimeSlotsPerHour), typeof(SchedulerCalender), new PropertyMetadata((s, e) => ((SchedulerCalender)s).OnTimeSlotPerHour_Changed(e)));

        private void OnTimeSlotPerHour_Changed(DependencyPropertyChangedEventArgs e)
        {
            TimeSlotPerHour = (Enumerations.TimeSlotsPerHour)e.NewValue;
        }



        public Enumerations.HoursPerDay HoursPerDay
        {
            get { return (Enumerations.HoursPerDay)GetValue(HoursPerDayProperty); }
            set { SetValue(HoursPerDayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoursPerDay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoursPerDayProperty =
            DependencyProperty.Register("HoursPerDay", typeof(Enumerations.HoursPerDay), typeof(SchedulerCalender), new PropertyMetadata((s, e) => ((SchedulerCalender)s).OnHoursPerDay_Changed(e)));

        private void OnHoursPerDay_Changed(DependencyPropertyChangedEventArgs e)
        {
            HoursPerDay = (Enumerations.HoursPerDay)e.NewValue;
        }

        #endregion


        private ViewVariation viewVariation = null;
        private DaysOfWeek daysofweek = null;
        private ucContent _Content = null;

        public SchedulerCalender()
        {
            DefaultStyleKey = typeof(SchedulerCalender);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            daysofweek = (DaysOfWeek)GetTemplateChild("_DOW");

            if (daysofweek == null) return;

            daysofweek.DayOfWeek_Clicked += new EventHandler(DayOfWeek_Clicked);

            viewVariation = (ViewVariation)GetTemplateChild("_VV");

            if (viewVariation == null) return;

            viewVariation.OnSliderValue_Changed += new EventHandler(viewVariation_OnSliderValue_Changed);

            _Content = (ucContent)GetTemplateChild("_content");
            if (_Content == null) return;

            _Content.AddItem_Clicked += new EventHandler(_Content_AddItem_Clicked);

            SetBindings();
        }

        void _Content_AddItem_Clicked(object sender, EventArgs e)
        {

        }

        void viewVariation_OnSliderValue_Changed(object sender, EventArgs e)
        {
            if ((sender as Slider).Value == 0)
                CurrentDateView = Enumerations.DateView.Day;
            else
                CurrentDateView = Enumerations.DateView.FullWeek;
        }

        void DayOfWeek_Clicked(object sender, EventArgs e)
        {
            CurrentDateView = Enumerations.DateView.Day;
            CurrentDate = (sender as DayOfWeekContentItem).Date;
        }

        private void SetBindings()
        {
            var view = new ViewModel.SchedulerModel()
                           {
                               CurrentDate = DateTime.Now,
                               CurrentDateView = Enumerations.DateView.Day
                           };

            DataContext = view;

            Binding b1 = new Binding() { Path = new PropertyPath("TimeSlotPerHour"), Mode = BindingMode.TwoWay, Source = view };
            SetBinding(TimeSlotPerHourProperty, b1);
            _Content.SetBinding(ucContent.TimeSlotPerHourProperty, b1);

            Binding b2 = new Binding { Path = new PropertyPath("HoursPerDay"), Mode = BindingMode.TwoWay, Source = view };
            this.SetBinding(HoursPerDayProperty, b2);
            _Content.SetBinding(ucContent.HoursPerDayProperty, b2);

            Binding b3 = new Binding { Path = new PropertyPath("CurrentDateView"), Mode = BindingMode.TwoWay, Source = view };
            this.SetBinding(CurrentDateViewProperty, b3);
            viewVariation.SetBinding(ViewVariation.CurrentDateViewProperty, b3);

            _Content.SetBinding(ucContent.CurrentDateViewProperty, b3);
            daysofweek.SetBinding(DaysOfWeek.CurrentDateViewProperty, b3);

            Binding b4 = new Binding { Path = new PropertyPath("CurrentDate"), Mode = BindingMode.TwoWay, Source = view };
            this.SetBinding(CurrentDateProperty, b4);

            daysofweek.SetBinding(DaysOfWeek.CurrentDateProperty, b3);


        }
    }
}
