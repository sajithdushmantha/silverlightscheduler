using System.Windows;
using System.Windows.Controls;

namespace Aciades.BusinessNext.SchedulerControl
{
    public class SchedulerHeader : Control
    {
        public string HeaderName
        {
            get { return (string)GetValue(HeaderNameProperty); }
            set { SetValue(HeaderNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderNameProperty =
            DependencyProperty.Register("HeaderName", typeof(string), typeof(SchedulerHeader), 
            new PropertyMetadata((s,e)=> ((SchedulerHeader)s).OnHeaderChanged(e)));

        private void OnHeaderChanged(DependencyPropertyChangedEventArgs e)
        {
            HeaderName = (string)e.NewValue;
        }


        public SchedulerHeader()
        {
            DefaultStyleKey = typeof(SchedulerHeader);
        }

    }
}
