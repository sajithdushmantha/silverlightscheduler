using System;
using System.Collections.ObjectModel;
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
    public class ucContent : ContentControl
    {
        #region Dependency Property

        public Enumerations.DateView CurrentDateView
        {
            get { return (Enumerations.DateView)GetValue(CurrentDateViewProperty); }
            set { SetValue(CurrentDateViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentDateView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentDateViewProperty =
            DependencyProperty.Register("CurrentDateView", typeof(Enumerations.DateView), typeof(ucContent), new PropertyMetadata((s, e) => ((ucContent)s).OnCurrentDateView_Changed(e)));

        private void OnCurrentDateView_Changed(DependencyPropertyChangedEventArgs e)
        {
            CurrentDateView = (Enumerations.DateView)e.NewValue;
            PopulateContent();
        }

        public Enumerations.TimeSlotsPerHour TimeSlotPerHour
        {
            get { return (Enumerations.TimeSlotsPerHour)GetValue(TimeSlotPerHourProperty); }
            set { SetValue(TimeSlotPerHourProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeSlotPerHour.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeSlotPerHourProperty =
            DependencyProperty.Register("TimeSlotPerHour", typeof(Enumerations.TimeSlotsPerHour), typeof(ucContent), new PropertyMetadata((s, e) => ((ucContent)s).OnTimeSlotPerHour_Changed(e)));

        private void OnTimeSlotPerHour_Changed(DependencyPropertyChangedEventArgs e)
        {
            TimeSlotPerHour = (Enumerations.TimeSlotsPerHour)e.NewValue;
            PopulateContent();
        }

        public Enumerations.HoursPerDay HoursPerDay
        {
            get { return (Enumerations.HoursPerDay)GetValue(HoursPerDayProperty); }
            set { SetValue(HoursPerDayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoursPerDay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoursPerDayProperty =
            DependencyProperty.Register("HoursPerDay", typeof(Enumerations.HoursPerDay), typeof(ucContent), new PropertyMetadata((s, e) => ((ucContent)s).OnHoursPerDay_Changed(e)));

        private void OnHoursPerDay_Changed(DependencyPropertyChangedEventArgs e)
        {
            HoursPerDay = (Enumerations.HoursPerDay)e.NewValue;
            PopulateContent();
        }



        public DateTime CurrentDate
        {
            get { return (DateTime)GetValue(CurrentDateProperty); }
            set { SetValue(CurrentDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentDateProperty =
            DependencyProperty.Register("CurrentDate", typeof(DateTime), typeof(ucContent), new PropertyMetadata((s, e) => ((ucContent)s).OnCurrentDate_Changed(e)));

        private void OnCurrentDate_Changed(DependencyPropertyChangedEventArgs e)
        {
            CurrentDate = (DateTime)e.NewValue;
        }

        #endregion

        public event EventHandler AddItem_Clicked;

        public ucContent()
        {
            DefaultStyleKey = typeof(ucContent);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PopulateContent();
        }

        private void PopulateContent()
        {
            SettingsProvider settingsProvider = new SettingsProvider();
            settingsProvider.SettingsLoaded += OnSettingLoaded;
            settingsProvider.RequestSettings("");
        }

        private void OnSettingLoaded(object sender, DownloadStringCompletedEventArgs e)
        {
            ContentItemProvider provider = new ContentItemProvider(this);
            ObservableCollection<UIElement> items = null;
            //TODO: Remove Hardcoding

            var settings = (sender as SettingsProvider);
            settings.SettingsLoaded -= OnSettingLoaded;

            var loopHours = 24 / settings.HoursPerDay;
            var loopMinutues = 60 / settings.TimeSlotPerHour;
            var TimeSlotItemHeight = (settings.TimeSlotHeight * loopMinutues) * loopHours; ;
            
            var parts = 60 / settings.TimeSlotPerHour;
            
            var dayContentItemHeight = TimeSlotItemHeight / parts;

            switch (CurrentDateView)
            {
                case Enumerations.DateView.Day:
                    DayContent day = new DayContent();
                    day.Owner = this;
                    items = provider.GetContent();
                    day.ItemsSource = items;
                    if (items != null && items.Count > 0)
                    {
                        foreach (var uiElement in items)
                        {
                            if (uiElement.GetType() != typeof(Grid))
                            {
                                (uiElement as DayContentItem).AddItem_Clicked += ucContent_AddItem_Clicked;
                                (uiElement as DayContentItem).Height = dayContentItemHeight;

                            }

                        }
                    }
                    Content = day;
                    break;
                case Enumerations.DateView.FullWeek:
                    WeekContent weekContent = new WeekContent();
                    weekContent.Owner = this;
                    items = provider.GetContent();
                    weekContent.ItemsSource = items;

                    if (items != null && items.Count > 0)
                    {
                        foreach (var uiElement in items)
                        {
                            if (uiElement.GetType() != typeof(Grid) && uiElement.GetType() == typeof(Border))
                                foreach (var element in ((uiElement as Border).Child as StackPanel).Children)
                                {
                                    (element as DayContentItem).AddItem_Clicked += ucContent_AddItem_Clicked;
                                    (element as DayContentItem).Height = dayContentItemHeight;
                                    weekContent.Width = (element as DayContentItem).Width;
                                }
                        }
                    }

                    Content = weekContent;
                    break;
            }
        }

        void ucContent_AddItem_Clicked(object sender, EventArgs e)
        {
            if (AddItem_Clicked != null)
                AddItem_Clicked(sender, null);
        }
    }
}
