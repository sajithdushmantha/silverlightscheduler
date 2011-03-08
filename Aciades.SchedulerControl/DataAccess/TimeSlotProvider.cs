using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Net;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Aciades.BusinessNext.SchedulerControl.DataAccess
{
    /// <summary>
    /// Provider class for TimeSlot.
    /// </summary>
    public class TimeSlotProvider : ObservableCollection<TimeSlotItem>
    {
        private TimeSlots _owner = null;
        private XDocument document = null;

        int TimeSlotHeight = 20;
        int hoursPerDay = 24;
        int loopMinutues;
        int loopHours;

        public TimeSlotProvider(TimeSlots owner)
        {
            this._owner = owner;
        }

        public event EventHandler RequestTimeSlotCompleted;



        public void RequestTimeSlots()
        {
            SettingsProvider provider = new SettingsProvider();
            provider.RequestSettings("");
            provider.SettingsLoaded += OnSettingLoaded;
        }

        int timeSlotPerHour = 15;

        private void OnSettingLoaded(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var provider = (sender as SettingsProvider);
                provider.SettingsLoaded -= OnSettingLoaded;
                TimeSlotHeight = provider.TimeSlotHeight;
                hoursPerDay = provider.HoursPerDay;
                timeSlotPerHour = provider.TimeSlotPerHour;
                GetTimeSlot();
            }
        }

        private void GetTimeSlot()
        {
            loopHours = 24 / hoursPerDay;

            loopMinutues = 60 / timeSlotPerHour;

            TimeSpan tsStart = new TimeSpan(0, 0, 0);
            TimeSpan tsEnd = new TimeSpan(24, 0, 0);
            TimeSpan tsTimeScale = new TimeSpan(loopHours, 0, 0);

            while (tsStart < tsEnd)
            {
                TimeSlotItem tsItem = new TimeSlotItem();

                switch (tsStart.Hours)
                {
                    case 0:
                        tsItem.BorderThickness = new Thickness(0);
                        tsItem.Hour = "12";
                        tsItem.Minute = "AM";
                        break;
                    case 12:
                        tsItem.Hour = "12";
                        tsItem.Minute = "PM";
                        break;
                    default:
                        if (tsStart.Hours > 12)
                        {
                            tsItem.Hour = (tsStart.Hours - 12).ToString();
                            tsItem.Minute = "00";
                        }
                        else
                        {
                            tsItem.Hour = tsStart.Hours.ToString();
                            tsItem.Minute = "00";
                        }
                        break;
                }

                tsItem.Height = (TimeSlotHeight * loopMinutues) * loopHours;

                Add(tsItem);

                tsStart += tsTimeScale;

            }

            if (RequestTimeSlotCompleted != null)
                RequestTimeSlotCompleted(this, null);
        }
    }
}
