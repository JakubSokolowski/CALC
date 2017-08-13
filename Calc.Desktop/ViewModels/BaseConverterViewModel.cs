using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using Calc.PositionalSystem;

namespace Calc.Desktop
{
    public class BaseConverterViewModel : BaseViewModel
    {

        #region Private Members

        private Number InputNumber { get; set; } = NumberConverter.ToBase(0, 10);
        private Number OutputNumber { get; set; } = NumberConverter.ToBase(0, 10);


        private string mInputString = "0.0";
        private string mOutputString = "0.0";

        private string mInputBase = "10";
        private string mOutputBase = "10";

        #endregion

        #region Public Commands

        public ICommand SwitchBaseCommand { get; set; }
        public ICommand ConvertCommand { get; set; }


        public void SwitchBase()
        {
            string temp = InputBase;
            InputBase = OutputBase;
            OutputBase = temp;           
        }

        public void Convert()
        {
            BaseConverter bc = new BaseConverter();
            if (bc.IsValidRadix(System.Convert.ToInt32(InputBase)) && bc.IsValidRadix(System.Convert.ToInt32(OutputBase)))
            {
                if (bc.IsValidString(InputString, System.Convert.ToInt32(InputBase)))
                {
                    InputNumber = NumberConverter.ToBase(InputString, System.Convert.ToInt32(InputBase), System.Convert.ToInt32(InputBase));
                    OutputNumber = NumberConverter.ToBase(InputNumber, System.Convert.ToInt32(OutputBase));

                    InputString = InputNumber.ValueInBase;
                    OutputString = OutputNumber.ValueInBase;


                    ErrorMessage = "";
                }
                else
                    ErrorMessage = "The number does not match it's given radix";
            }
            else
                ErrorMessage = "Radix must be between 2 and 99";
        }


        #endregion

        #region Public Properties

        public string InputString { get => mInputString; set => mInputString = value; }       
        public string OutputString { get { return mOutputString; } set { mOutputString = value; } }

        public string InputBase { get => mInputBase; set => mInputBase = value; }
        public string OutputBase { get => mOutputBase; set => mOutputBase = value; }

        public string InputComplement { get => InputNumber.Complement; set { } }
        public string OutputComplement { get => OutputNumber.Complement; set { } }


        public string ErrorMessage { get; set; } = "";

        #endregion

        #region Constructor

        public BaseConverterViewModel()
        {                     
            SwitchBaseCommand = new RelayCommand( () =>  SwitchBase());
            ConvertCommand = new RelayCommand(() => Convert());
        }

        #endregion


    }
}
