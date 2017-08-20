using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;

namespace Calc.Desktop
{
    public class BaseConverterMenuViewModel : BaseViewModel
    {     

        #region Public Properties

        public ICommand StartCommand { get; set; }
        public ApplicationPage MenuPage { get; set; } = ApplicationPage.MenuPage;
        public ApplicationPage BaseConverterPage { get; set; } = ApplicationPage.BaseConverter;


        #endregion

        #region Constructor
     
        public BaseConverterMenuViewModel()
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
