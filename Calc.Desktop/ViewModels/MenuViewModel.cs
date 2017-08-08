using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;

namespace Calc.Desktop
{
    public class MenuViewModel : BaseViewModel
    {     

        #region Public Properties

        public ICommand StartCommand { get; set; }
     


        #endregion

        #region Constructor
     
        public MenuViewModel()
        {                     
            StartCommand = new RelayCommand(async () =>  await StartMainPage());
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
