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
    public class DayOfWeekContentItem : Control
    {
        #region Dependency Properties
      
        public string DayString
        {
            get { return (string)GetValue(DayStringProperty); }
            set { SetValue(DayStringProperty, value); }
        }
        
        // Using a DependencyProperty as the backing store for DayString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DayStringProperty =
            DependencyProperty.Register("DayString", typeof(string), typeof(DayOfWeekContentItem), null);

        public string Year
        {
            get { return (string)GetValue(YearProperty); }
            set { SetValue(YearProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Year.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YearProperty =
            DependencyProperty.Register("Year", typeof(string), typeof(DayOfWeekContentItem), new PropertyMetadata(
                DateTime.Now.Year.ToString(),(s,e)=>((DayOfWeekContentItem)s).OnYear_Changed(e)));

        private void OnYear_Changed(DependencyPropertyChangedEventArgs e)
        {
            Year = (string) e.NewValue;
        }

        public string Month
        {
            get { return (string)GetValue(MonthProperty); }
            set { SetValue(MonthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Year.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MonthProperty =
            DependencyProperty.Register("Month", typeof(string), typeof(DayOfWeekContentItem), new PropertyMetadata(
                DateTime.Now.Month.ToString(), (s, e) => ((DayOfWeekContentItem)s).OnMonth_Changed(e)));

        private void OnMonth_Changed(DependencyPropertyChangedEventArgs e)
        {
            Month = (string)e.NewValue;
        }

        public string Day
        {
            get { return (string)GetValue(DayProperty); }
            set { SetValue(DayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Year.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DayProperty =
            DependencyProperty.Register("Day", typeof(string), typeof(DayOfWeekContentItem), new PropertyMetadata(
                DateTime.Now.Day.ToString(), (s, e) => ((DayOfWeekContentItem)s).OnDay_Changed(e)));

        private void OnDay_Changed(DependencyPropertyChangedEventArgs e)
        {
            Day = (string)e.NewValue;
        }

        #endregion

        public DateTime Date
        {
            get { return new DateTime(Int32.Parse(Year), Int32.Parse(Month), Int32.Parse(Day)); }
        }

        public event EventHandler DayOfWeek_Selected;



        public DayOfWeekContentItem()
        {
            DefaultStyleKey = typeof(DayOfWeekContentItem);
        }

        private Border brdRoot;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            brdRoot = (Border) GetTemplateChild("brdRoot");
            if(brdRoot == null) return;

            brdRoot.MouseLeftButtonUp += new MouseButtonEventHandler(brdRoot_MouseLeftButtonUp);

        }

        void brdRoot_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DayOfWeek_Selected != null)
                DayOfWeek_Selected(this, null);
        }
    }
}
