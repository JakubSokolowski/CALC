
namespace Calc.Core
{
    public class ApplicationViewModel : BaseViewModel
    {
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Welcome;
        public bool SideMenuVisible { get; set; } = false;
    }
}
