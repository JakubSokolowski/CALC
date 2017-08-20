using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;

namespace Calc.Desktop
{
    public class FloatRepresentationViewModel : BaseViewModel
    {     

        #region Public Properties

        public ICommand StartCommand { get; set; }
        public ICommand GetButtonsContent { get; set; }
        public ICommand AddButton { get; set; }
        public ButtonListViewModel Test { get; set; }


        private string mListValue = "";
        public string ListContent { get { return Test.GetContent(); } set {}}
     


        #endregion

        #region Constructor
     
        public FloatRepresentationViewModel()
        {                     
            StartCommand = new RelayCommand(async () =>  await StartMainPage());
            GetButtonsContent = new RelayCommand(() => TestButtons());
            AddButton = new RelayCommand(() => AddButt());
            Test = new ButtonListViewModel();
            Test.WriteText("GROOVY");
        }

        public async Task StartMainPage()
        {
            await Task.Delay(500);
           
        }

        public void TestButtons()
        {
            ListContent = Test.GetContent();
        }

        public void AddButt()
        {
            Test.AddButtons(1,"Y");
        }
        #endregion

        #region Private Helpers

       
        #endregion

    }
}
