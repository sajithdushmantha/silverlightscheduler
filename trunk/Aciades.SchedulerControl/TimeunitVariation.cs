using System;
using System.Windows;
using System.Windows.Controls;

namespace Aciades.Businessnext.SchedulerControl
{
    public class TimeunitVariation : Control
    {
        #region Dependency Property
        public Common.Enumerations.TimeIn TimeIn
        {
            get { return (Common.Enumerations.TimeIn)GetValue(TimeInProperty); }
            set { SetValue(TimeInProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeIn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeInProperty =
            DependencyProperty.Register("TimeIn", typeof(Common.Enumerations.TimeIn), typeof(TimeunitVariation),
            new PropertyMetadata((s, e) => ((TimeunitVariation)s).OnTimeIn_Chanbged(e)));

        private void OnTimeIn_Chanbged(DependencyPropertyChangedEventArgs e)
        {
            TimeIn = (Common.Enumerations.TimeIn)e.NewValue;
        }

        #endregion

        public TimeunitVariation()
        {
            DefaultStyleKey = typeof(TimeunitVariation);
        }
    }
}
