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
                await element.SlideAndFadeInFromLeft(FirstLoad ? 0 : 0.3f, keepMargin: false);
            else
                await element.SlideAndFadeOutToLeft(FirstLoad ? 0 : 0.3f, keepMargin: false);
        }
    }
}
