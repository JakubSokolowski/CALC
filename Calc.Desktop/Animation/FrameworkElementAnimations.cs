

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Calc.Desktop
{
    public static class FrameworkElementAnimations
    {
        public static async Task SlideAndFadeInFromTheBottom(this FrameworkElement element, float seconds, bool keepMargin = true)
        {
            var sb = new Storyboard();          
            sb.AddSlideFromBottom(seconds, element.ActualHeight, keepMargin: keepMargin);
            sb.AddFadeIn(seconds);
            sb.Begin(element);
            element.Visibility = Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));
        }
        public static async Task SlideAndFadeOutToBottom(this FrameworkElement element, float seconds, bool keepMargin = true)
        {
            var sb = new Storyboard();
            sb.AddSlideToBottom(seconds, element.ActualHeight, keepMargin: keepMargin);
            sb.AddFadeOut(seconds);
            sb.Begin(element);
            element.Visibility = Visibility.Visible;
            await Task.Delay((int)(seconds * 1000));
        }

        public static async Task SlideAndFadeInFromLeft(this FrameworkElement element, float seconds, bool keepMargin = true)
        {
            var sb = new Storyboard();
            sb.AddSlideFromLeft(seconds, element.ActualWidth, keepMargin: keepMargin);
            sb.AddFadeIn(seconds);
            sb.Begin(element);
            element.Visibility = Visibility.Visible;
            await Task.Delay((int)(seconds * 1000));
        }
        public static async Task SlideAndFadeOutToLeft(this FrameworkElement element, float seconds, bool keepMargin = true)
        {
            var sb = new Storyboard();
            sb.AddSlideToLeft(seconds, element.ActualWidth, keepMargin: keepMargin);
            sb.AddFadeOut(seconds);
            sb.Begin(element);
            element.Visibility = Visibility.Visible;
            await Task.Delay((int)(seconds * 1000));
        }

        public static async Task SlideAndFadeInFromRight(this FrameworkElement element, float seconds, bool keepMargin = true)
        {
            var sb = new Storyboard();
            sb.AddSlideFromRight(seconds, element.ActualWidth, keepMargin: keepMargin);
            sb.AddFadeIn(seconds);
            sb.Begin(element);
            element.Visibility = Visibility.Visible;
            await Task.Delay((int)(seconds * 1000));
        }
        public static async Task SlideAndFadeOutToRight(this FrameworkElement element, float seconds, bool keepMargin = true)
        {
            var sb = new Storyboard();
            sb.AddSlideToLeft(seconds, element.ActualWidth, keepMargin: keepMargin);
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
    }
}
