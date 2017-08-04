using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;

namespace Calc.Desktop
{
    public class WelcomeViewModel : BaseViewModel
    {
        #region Private Members        
       

        #endregion

        #region Public Properties

        public ICommand StartCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Defualt Constructor
        /// </summary>
        /// <param name="window"></param>
        public WelcomeViewModel(Window window)
        {
                       // Create Commands           
            StartCommand = new RelayCommand(async () =>  await StartMainPage() );

            // Fix window resize issue
          

        }

        public async Task StartMainPage()
        {
            await Task.Delay(500);
        }
        #endregion

        #region Private Helpers

       
        #endregion

    }
}
