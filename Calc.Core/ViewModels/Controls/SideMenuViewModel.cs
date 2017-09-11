
using System.Windows.Input;

namespace Calc.Core
{
    public class SideMenuViewModel : BaseViewModel
    {

        public ICommand GoToBaseConverterCommand { get; set; }
        public ICommand GoToFloatConverterCommand { get; set; }


        public SideMenuViewModel()
        {
            GoToBaseConverterCommand = new RelayCommand(() => GoToBaseConverter());
            GoToFloatConverterCommand = new RelayCommand(() => GoToFloatConverter());
        }

        public void GoToBaseConverter()
        {
            IoC.Get<ApplicationViewModel>().SideMenuVisible = true;
            IoC.Get<ApplicationViewModel>().CurrentPage = ApplicationPage.BaseConverter;
        }
        public void GoToCalculator()
        {

        }
        public void GoToFloatConverter()
        {
            IoC.Get<ApplicationViewModel>().SideMenuVisible = true;
            IoC.Get<ApplicationViewModel>().CurrentPage = ApplicationPage.FloatRepresentation;
        }
    }
}
