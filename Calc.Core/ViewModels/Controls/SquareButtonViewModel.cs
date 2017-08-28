using System.Windows.Input;

namespace Calc.Core
{
    public class SquareButtonViewModel : BaseViewModel
    {
        private string mContent= "0";

        public string Tag { get; set; } = "DEFAULT CONSTRUCTED";

        public ICommand SquareButtonCommand { get; set; }
        public string SingleCharContent { get => mContent; set => mContent = value; }
        public LinePosition Position { get; set; }

        public SquareButtonViewModel( )
        {
            SquareButtonCommand = new RelayCommand(() => FlipBit());            
        }
        public void FlipBit()
        {            
            SingleCharContent = (SingleCharContent == "0") ? "1" : "0";
            Mediator.Instance.NotifyColleagues(ViewModelMessages.BitFlipped, SingleCharContent);
        }
    }
}
