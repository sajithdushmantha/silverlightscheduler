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
    public class DaysOfWeekItemsPanel :Panel
    {
        protected override Size ArrangeOverride(Size finalSize)
        {
            int x = 0;
            int ChildWidth = (int)finalSize.Width / this.Children.Count;
            int PreviousChildWidth = 0;

            for (int i = 0; i < this.Children.Count; i++)
            {
                if (i == 0)
                    x = 0;
                else
                    x += PreviousChildWidth;

                PreviousChildWidth = ChildWidth;

                this.Children[i].Arrange(new Rect(x, 0, ChildWidth, finalSize.Height));
            }

            return base.ArrangeOverride(finalSize);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.Children != null)
            {
                int NumberOfChildren = this.Children.Count;
                int ChildWidth = (int)availableSize.Width / NumberOfChildren;

                foreach (UIElement child in this.Children)
                {
                    if (child != null)
                        child.Measure(new Size(ChildWidth, availableSize.Height));
                }
            }

            return availableSize;
        }
    }
}
