using Calc.Desktop;

namespace Calc.Desktop
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : BasePage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           this.AnimateOut();
        }
    }
}
