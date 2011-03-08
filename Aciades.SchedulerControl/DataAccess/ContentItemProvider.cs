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
    public class ContentItemProvider : ObservableCollection<UIElement>
    {
        private DependencyObject Owner;

        public ContentItemProvider(DependencyObject owner)
        {
            Owner = owner;
        }

        public ObservableCollection<UIElement> GetContent()
        {
            Clear();
            ucContent contentOwner = (Owner as ucContent);
            int TimeSlotPerHour = 60 / (int)contentOwner.TimeSlotPerHour;
            int TimeSlotPerDay = TimeSlotPerHour * (int)contentOwner.HoursPerDay;

            switch (contentOwner.CurrentDateView)
            {
                case Enumerations.DateView.Day:
                    int currentMinute = 0;

                    for (int i = 0; i < TimeSlotPerDay; i++)
                    {
                        Add(CreateDayView(contentOwner, currentMinute));
                        currentMinute += (int)contentOwner.TimeSlotPerHour;
                    }
                    break;
                case Enumerations.DateView.FullWeek:
                case Enumerations.DateView.WorkWeek:
                    int dayOffset = 0;

                    if (contentOwner.CurrentDateView == Enumerations.DateView.FullWeek)
                        dayOffset = 7;
                    else if (contentOwner.CurrentDateView == Enumerations.DateView.WorkWeek)
                        dayOffset = 5;

                    DateTime _MondaysDate = contentOwner.CurrentDate.AddDays((int)System.DayOfWeek.Monday - (int)contentOwner.CurrentDate.DayOfWeek);

                    double width = (contentOwner.ActualWidth - 100) / dayOffset;

                    for (int i = 0; i < dayOffset; i++)
                    {
                        StackPanel sp = new StackPanel();
                        Border brd = new Border();

                        brd.Width = width;
                        var dt = _MondaysDate.AddDays(i);
                        currentMinute = 0;

                        for (int j = 0; j < TimeSlotPerDay; j++)
                        {
                            sp.Children.Add(CreateDayView(contentOwner, currentMinute));
                            currentMinute += (int)contentOwner.TimeSlotPerHour;

                        }

                        brd.Child = sp;
                        Add(brd);
                    }
                    break;
            }


            return this;
        }

        private UIElement CreateDayView(ucContent contentOwner, int currentMinute)
        {
            DayContentItem dayContentItem = new DayContentItem
            {

            };
            TimeSpan currentHour = new TimeSpan(0, currentMinute, 0);

            return dayContentItem;
        }
    }
}
