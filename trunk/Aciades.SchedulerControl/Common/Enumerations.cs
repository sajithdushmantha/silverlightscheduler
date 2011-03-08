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

namespace Aciades.BusinessNext.SchedulerControl.Common
{
    public class Enumerations
    {
        public enum DateView
        {
            Day = 1,
            WorkWeek = 2,
            FullWeek = 3
        }

        public enum TimeSlotsPerHour
        {
            Five = 5,
            Six = 6,
            Ten = 10,
            Fifteen = 15,
            Thirty = 30,
            Sixty = 60
        }

        public enum HoursPerDay
        {
            Two = 2,
            Four = 4,
            Eight = 8,
            Twelve = 12,
            TwentyFour = 24
        }

        public enum TimeIn
        {
            Hours = 1,
            Minutes = 2,
            Seconds = 3
        }
    }
}
