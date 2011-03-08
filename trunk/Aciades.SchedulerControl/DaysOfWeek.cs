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
    public class DaysOfWeek : ItemsControl
    {
        #region Dependency Property
        public Common.Enumerations.DateView CurrentDateView
        {
            get { return (Common.Enumerations.DateView)GetValue(CurrentDateViewProperty); }
            set { SetValue(CurrentDateViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentDateView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentDateViewProperty =
            DependencyProperty.Register("CurrentDateView", typeof(Common.Enumerations.DateView), typeof(DaysOfWeek), new PropertyMetadata((s, e) => ((DaysOfWeek)s).OnCurrentDateViewChanged(e)));

        private void OnCurrentDateViewChanged(DependencyPropertyChangedEventArgs e)
        {
            CurrentDateView = (Common.Enumerations.DateView)e.NewValue;
            PopulateHeader();
        }

        public DateTime CurrentDate
        {
            get { return (DateTime)GetValue(CurrentDateProperty); }
            set { SetValue(CurrentDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentDateProperty =
            DependencyProperty.Register("CurrentDate", typeof(DateTime), typeof(DaysOfWeek), new PropertyMetadata((s, e) => ((DaysOfWeek)s).OnCurrentDateChanged(e)));

        private void OnCurrentDateChanged(DependencyPropertyChangedEventArgs e)
        {
            CurrentDate = (DateTime)e.NewValue;
            PopulateHeader();
        }
        #endregion

        public DaysOfWeek()
        {
            DefaultStyleKey = typeof(DaysOfWeek);
        }
        public event EventHandler DayOfWeek_Clicked;

        private void PopulateHeader()
        {
            DayofWeekProvider provider = new DayofWeekProvider(this);
            ObservableCollection<UIElement> collection = null;
            switch (CurrentDateView)
            {
                case Enumerations.DateView.Day:
                case Enumerations.DateView.WorkWeek:
                case Enumerations.DateView.FullWeek:
                    Margin = new Thickness(50, 0, 13, 0);
                    break;
            }

            collection = provider.GetDaysOfWeek();
            ItemsSource = collection;

            if (collection != null && collection.Count > 0)
            {
                foreach (var uiElement in collection)
                {
                    (uiElement as DayOfWeekContentItem).DayOfWeek_Selected += DayOfWeek_Selected;
                }
            }
        }

        void DayOfWeek_Selected(object sender, EventArgs e)
        {
            if (DayOfWeek_Clicked != null)
                DayOfWeek_Clicked(sender, null);
        }
    }
}
