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
    public class DayContentItem : ContentControlBase
    {



        public double MouseOverOpacity
        {
            get { return (double)GetValue(MouseOverOpacityProperty); }
            set { SetValue(MouseOverOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverOpacityProperty =
            DependencyProperty.Register("MouseOverOpacity", typeof(double), typeof(DayContentItem),
            new PropertyMetadata((s, e) => ((DayContentItem)s).OnMouseOverOpacity_Changed(e)));

        private void OnMouseOverOpacity_Changed(DependencyPropertyChangedEventArgs e)
        {
            MouseOverOpacity = (double)e.NewValue;
        }

        public event EventHandler AddItem_Clicked;

        public DayContentItem()
        {
            DefaultStyleKey = typeof(DayContentItem);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            MouseEnter += new MouseEventHandler(DayContentItem_MouseEnter);
            MouseLeave += new MouseEventHandler(DayContentItem_MouseLeave);
            MouseDblClick += new MouseDblClickHandler(DayContentItem_MouseDblClick);
        }

        void DayContentItem_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseOverOpacity = 0;
        }

        void DayContentItem_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseOverOpacity = 1;            
        }

        void DayContentItem_MouseDblClick(object sender, MouseButtonEventArgs e)
        {
            if (AddItem_Clicked != null)
            {
                AddItem_Clicked(this, null);
            }
        }
    }
}
