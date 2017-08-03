
using Calc.Desktop.Animation;
using System.Windows.Controls;
using System;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Calc.Desktop
{
    public class BasePage : Page
    {
        public AnimationStyles LoadAnimation { get; set; } = AnimationStyles.SlideAndFadeFromBottom;
        public AnimationStyles UnloadAnimation { get; set; } = AnimationStyles.SlideAndFadeOutToBottom;

        public float SlideTimeSeconds { get; set; } = 0.8f;
        public float FadeTimeSeconds { get; set; } = 0.8f;

        public BasePage()
        {
            // If this page has any animation, start with the page hidden
            if (this.LoadAnimation != AnimationStyles.None)
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
            if (this.LoadAnimation == AnimationStyles.None)
                return;

            switch(this.LoadAnimation)
            {
                case AnimationStyles.SlideAndFadeFromBottom:
                    await this.SlideAndFadeInFromTheBottom(this.SlideTimeSeconds);
                    break;
            }
        }

        public async Task AnimateOut()
        {
            if (this.UnloadAnimation == AnimationStyles.None)
                return;

            switch (this.UnloadAnimation)
            {
                case AnimationStyles.SlideAndFadeOutToBottom:
                    await this.SlideAndFadeOutToBottom(this.SlideTimeSeconds);
                    break;
            }
        }
    }
}
