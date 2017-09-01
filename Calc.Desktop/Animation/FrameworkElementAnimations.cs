

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Calc.Desktop
{
    public static class FrameworkElementAnimations
    {
        public static async Task SlideAndFadeInFromDirection(this FrameworkElement element, float seconds, SlideDirection direction, bool keepMargin)
        {
            var sb = new Storyboard();
            var offset = GetOffset(element, direction);

            if (keepMargin)
                sb.AddSlideFromDirectionAndKeepMargin(seconds, offset, direction);
            else
                sb.AddSlideFromDirectionAndHideMargin(seconds, offset, direction);

            sb.AddFadeIn(seconds);
            sb.Begin(element);
            element.Visibility = Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));
        }
        public static async Task SlideAndFadeOutToDirection(this FrameworkElement element, float seconds, SlideDirection direction, bool keepMargin)
        {
            var sb = new Storyboard();
            var offset = GetOffset(element, direction);

            if (keepMargin)
                sb.AddSlideToDirectionAndKeepMargin(seconds, offset, direction);
            else
                sb.AddSlideToDirectionAndHideMargin(seconds, offset, direction);

            sb.AddFadeOut(seconds);

            sb.Begin(element);
            element.Visibility = Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));
        }     
        
        public static async Task FadeIn(this FrameworkElement element, float seconds)
        {
            var sb = new Storyboard();
            sb.AddFadeIn(seconds);
            sb.Begin(element);
            element.Visibility = Visibility.Visible;
            await Task.Delay((int)(seconds * 1000));
        }
        public static async Task FadeOut(this FrameworkElement element, float seconds)
        {
            var sb = new Storyboard();

            sb.AddFadeOut(seconds);

            sb.Begin(element);

            element.Visibility = Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));
        }

        private static double GetOffset(FrameworkElement element, SlideDirection direction)
        {
            // Special case for pages. Offset is taken from window size
            if(element.GetType().IsSubclassOf(typeof(Page)))
            {
                if (direction == SlideDirection.Left || direction == SlideDirection.Right)
                    return (element as Page).WindowWidth;
                else
                    return (element as Page).WindowHeight;
            }
            else
            {
                if (direction == SlideDirection.Left || direction == SlideDirection.Right)
                    return element.ActualWidth;
                else
                    return element.ActualHeight;
            }
        }        
    }
}
