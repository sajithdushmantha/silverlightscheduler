using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Aciades.Businessnext.SchedulerControl.DataAccess
{
    /// <summary>
    /// Provider class for TimeSlot.
    /// </summary>
    public class TimeSlotProvider : ObservableCollection<TimeSlotItem>
    {
        private TimeSlots _owner = null;

        public TimeSlotProvider(TimeSlots owner)
        {
            this._owner = owner;
        }

        public ObservableCollection<TimeSlotItem> GetTimeSlot()
        {
            int TimeSlotHeight = 20;
            
            int loopMinutues = 60 / (int)Common.Enumerations.TimeSlotsPerHour.Fifteen;
            int loopHours = 24 / (int)Common.Enumerations.HoursPerDay.TwentyFour;

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
            return this;
        }
    }
}
