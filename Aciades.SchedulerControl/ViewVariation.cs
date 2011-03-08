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

namespace Aciades.Businessnext.SchedulerControl
{
    public class ViewVariation : Control
    {
        public Common.Enumerations.DateView CurrentDateView
        {
            get { return (Common.Enumerations.DateView)GetValue(CurrentDateViewProperty); }
            set { SetValue(CurrentDateViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentDateView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentDateViewProperty =
            DependencyProperty.Register("CurrentDateView", typeof(Common.Enumerations.DateView), typeof(ViewVariation), new PropertyMetadata((s, e) => ((ViewVariation)s).OnCurrentDateView_Changed(e)));

        private void OnCurrentDateView_Changed(DependencyPropertyChangedEventArgs e)
        {
            CurrentDateView = (Common.Enumerations.DateView)e.NewValue;
            PopulateSlider();
        }

        private void PopulateSlider()
        {
            slider = (Slider)GetTemplateChild("viewSlider");
            if (slider == null) return;
            slider.ValueChanged += slider_ValueChanged;

            switch (CurrentDateView)
            {
                case Common.Enumerations.DateView.Day:
                    slider.Value = 0;

                    break;

                case Common.Enumerations.DateView.FullWeek:
                    slider.Value = 100;

                    break;
            }
        }

        private Slider slider = null;

        public event EventHandler OnSliderValue_Changed;

        public ViewVariation()
        {
            this.DefaultStyleKey = typeof(ViewVariation);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PopulateSlider();
        }

        void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (OnSliderValue_Changed != null)
                OnSliderValue_Changed(sender, e);
        }
    }
}
