
using Calc.Desktop.Animation;
using System.Windows.Controls;
using System;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Calc.Desktop.Pages
{
    public class BasePage : Page
    {
        public PageAnimation LoadAnimation { get; set; } = PageAnimation.FadeIn;
        public PageAnimation UnloadAnimation { get; set; } = PageAnimation.FadeOut;

        public float AnimationTimeInSeconds { get; set; } = 0.8f;

        public BasePage()
        {
            // If this page has any animation, start with the page hidden
            if (this.LoadAnimation != PageAnimation.None)
                this.Visibility = Visibility.Collapsed;

            // Listen out for page loading
            this.Loaded += BasePage_Loaded;
        }

        private async void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            await AnimateIn();
        }

        public async Task AnimateIn()
        {
            if (this.LoadAnimation == PageAnimation.None)
                return;

            switch(this.LoadAnimation)
            {
                case PageAnimation.SlideAndFadeFromRight:

                    var sb = new Storyboard();                
                    break;
            }
        }
    }
}
