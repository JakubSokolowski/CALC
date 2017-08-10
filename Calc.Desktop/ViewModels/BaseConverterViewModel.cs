using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using Calc.PositionalSystem;

namespace Calc.Desktop
{
    public class BaseConverterViewModel : BaseViewModel
    {

        private Number InputNumber { get; set; } = NumberConverter.ToBase(0, 10);
        private Number OutputNumber { get; set; } = NumberConverter.ToBase(0, 10);

        


        #region Public Properties

        public ICommand StartCommand { get; set; }

     

        public string InputString
        {
            get { return InputNumber.ValueInBase; }
            set
            {
                InputNumber = NumberConverter.ToBase(value, System.Convert.ToInt32(InputBase), System.Convert.ToInt32(InputBase));
                OutputNumber = NumberConverter.ToBase(InputNumber, System.Convert.ToInt32(OutputBase));                
            }
        }
        public string OutputString { get { return OutputNumber.ValueInBase; } set { } }
        public string InputBase
        {
            get => InputNumber.Radix.ToString();
            set
            {
                BaseConverter bc = new BaseConverter();
                if (bc.IsValidRadix(System.Convert.ToInt32(value)))
                {
                    if (bc.IsValidString(InputString, System.Convert.ToInt32(value)))
                    {
                        InputNumber = NumberConverter.ToBase(InputString, System.Convert.ToInt32(value), System.Convert.ToInt32(InputBase));
                        OutputNumber = NumberConverter.ToBase(InputNumber, System.Convert.ToInt32(OutputBase));
                    }
                    else
                        ErrorMessage = "The number does not match it's given radix";
                }
                else
                    ErrorMessage = "Radix must be between 2 and 99";

            }
        }
        public string OutputBase
        {
            get => OutputNumber.Radix.ToString();
            set
            {
                BaseConverter bc = new BaseConverter();
                if (bc.IsValidRadix(System.Convert.ToInt32(value)))
                {              
                   
                    OutputNumber = NumberConverter.ToBase(InputNumber.DecimalValue, System.Convert.ToInt32(value));                   
                      
                }
                else
                    ErrorMessage = "Radix must be between 2 and 99";

            }
        }

        public string ErrorMessage { get; set; } = "";
        public string Complement
        {
            get { if (OutputNumber.Complement != null)
                    return OutputNumber.Complement;
                else
                    return "";
            }
            set { }
        }





        #endregion

        #region Constructor

        public BaseConverterViewModel()
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
