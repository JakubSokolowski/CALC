using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;

namespace Calc.Desktop
{
    public class FloatRepresentationMenuViewModel : BaseViewModel
    {     

        #region Public Properties

        public ICommand StartCommand { get; set; }
        public ApplicationPage MenuPage { get; set; } = ApplicationPage.MenuPage;
        public ApplicationPage FloatRepresentationPage { get; set; } = ApplicationPage.FloatRepresentation;


        #endregion

        #region Constructor
     
        public FloatRepresentationMenuViewModel()
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
