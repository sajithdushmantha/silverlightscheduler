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
using Aciades.Businessnext.SchedulerControl.Common;

namespace Aciades.Businessnext.SchedulerControl.DataAccess
{
    public class DayofWeekProvider :ObservableCollection<UIElement>
    {
        public DaysOfWeek Owner = null;
        public DayofWeekProvider(DaysOfWeek owner)
        {
            Owner = owner;
        }

        public ObservableCollection<UIElement> GetDaysOfWeek()
        {
            Clear();
            DayOfWeekContentItem item = null;

            switch (Owner.CurrentDateView)
            {
                case Enumerations.DateView.Day:
                    item = new DayOfWeekContentItem();
                    item.DayString = Owner.CurrentDate.DayOfWeek.ToString();
                    item.Month = Owner.CurrentDate.Month.ToString();
                    item.Year = Owner.CurrentDate.Year.ToString();
                    item.Cursor = Cursors.Hand;
                    Add(item);
                    break;
                case Enumerations.DateView.FullWeek:
                    int dayOffSet = 7;

                    DateTime startDate =
                        Owner.CurrentDate.AddDays((int) System.DayOfWeek.Monday - (int) Owner.CurrentDate.DayOfWeek);

                    for (int i = 0; i < dayOffSet; i++)
                    {
                        item = new DayOfWeekContentItem();
                        item.DayString = startDate.AddDays(i).DayOfWeek.ToString();
                        item.Month = Owner.CurrentDate.Month.ToString();
                        item.Year = Owner.CurrentDate.Year.ToString();
                        item.Cursor = Cursors.Hand;
                        Add(item);
                    }
                    break;
            }
            return this;
        }
    }
}
