using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;

namespace Calc.Core
{
    public class WelcomeViewModel : BaseViewModel
    {   
        public ICommand StartCommand { get; set; }
        public WelcomeViewModel()
        {                     
            StartCommand = new RelayCommand( () =>   StartMainPage());
        }
        public void StartMainPage()
        {
            IoC.Get<ApplicationViewModel>().SideMenuVisible ^= true;
            IoC.Get<ApplicationViewModel>().CurrentPage = ApplicationPage.FloatRepresentation;           
        }  

    }
}
