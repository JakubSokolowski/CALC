using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calc.Desktop
{
    public class AnimateSlideInFromLeftProperty : AnimateBaseProperty<AnimateSlideInFromLeftProperty>
    {
        protected override async void Animate(FrameworkElement element, bool value)
        {
            if (value)
                await element.SlideAndFadeInFromDirection(FirstLoad ? 0f : 0.4f, SlideDirection.Left, keepMargin: true);
            else
                await element.SlideAndFadeOutToDirection(FirstLoad ? 0: 0.4f, SlideDirection.Right, keepMargin: false);
        }
    }
}
