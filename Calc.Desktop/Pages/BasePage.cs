
using Calc.Desktop.Animation;
using System.Windows.Controls;
using System.Windows;
using System.Threading.Tasks;
using Calc.Core;

namespace Calc.Desktop
{
    public class BasePage<VM> : Page
        where VM : BaseViewModel, new()
    {
        private VM mViewModel;

        public AnimationStyles LoadAnimation { get; set; } = AnimationStyles.SlideAndFadeFromBottom;
        public AnimationStyles UnloadAnimation { get; set; } = AnimationStyles.SlideAndFadeOutToBottom;


        public float SlideTimeSeconds { get; set; } = 0.4f;
        public float FadeTimeSeconds { get; set; } = 0.8f;

        public VM ViewModel
        {
            get => ViewModel;
            set
            {
                // If nothing has changed, return
                if (mViewModel == value)
                    return;

                // Update the value
                mViewModel = value;

                // Set the data context for this page
                this.DataContext = mViewModel;
            }
        }

        
        public BasePage()
        {
            // If this page has any animation, start with the page hidden
            if (LoadAnimation != AnimationStyles.None)
                Visibility = Visibility.Collapsed;

            // Listen out for page loading
            Loaded += BasePage_Loaded;
            //ViewModel = new VM();
            ViewModel = IoC.Get<VM>();
        }

        private async void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            await AnimateIn();
        }

        public async Task AnimateIn()
        {
            if (LoadAnimation == AnimationStyles.None)
                return;

            switch(LoadAnimation)
            {
                case AnimationStyles.SlideAndFadeFromBottom:
                    await this.SlideAndFadeInFromDirection(SlideTimeSeconds, SlideDirection.Bottom, keepMargin: true);
                    break;
                case AnimationStyles.FadeIn:
                    await this.FadeIn(SlideTimeSeconds);
                    break;
            }
        }

        public async Task AnimateOut()
        {
            if (UnloadAnimation == AnimationStyles.None)
                return;

            switch (UnloadAnimation)
            {
                case AnimationStyles.SlideAndFadeOutToBottom:
                    await this.SlideAndFadeOutToDirection(SlideTimeSeconds, SlideDirection.Bottom, keepMargin: true);
                    break;
                case AnimationStyles.FadeIn:
                    await this.FadeOut(SlideTimeSeconds);
                    break;
            }
        }
    }
}
