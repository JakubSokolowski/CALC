using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using Calc.PositionalSystem;

namespace Calc.Core
{
    public class BaseConverterViewModel : BaseViewModel
    {

        #region Private Members

        private Number InputNumber { get; set; } = NumberConverter.ToBase(0, 10);
        private Number OutputNumber { get; set; } = NumberConverter.ToBase(0, 10);

        private BaseConverter bc = new BaseConverter();


        private ConversionHistory mHistory = new ConversionHistory();


        private string mInputString = "0.0";
        private string mOutputString = "0.0";

        private int mInputBase = 10;
        private int mOutputBase = 10;

        #endregion

        #region Button Commands and their Methods

        public ICommand SwitchBaseCommand { get; set; }
        public ICommand ConvertCommand { get; set; }
        public ICommand GoBackInHistoryCommand { get; set; }
        public ICommand GoForwardInHistoryCommand { get; set; }

        public void SwitchBase()
        {
            string temp = InputBase;
            InputBase = OutputBase;
            OutputBase = temp;           
        }
        public void Convert()
        {          
            if (bc.IsValidRadix(mInputBase) && bc.IsValidRadix(mOutputBase))
            {
                if (bc.IsValidString(InputString, mInputBase))
                {
                    try
                    {
                        InputNumber = NumberConverter.ToBase(InputString, mInputBase, mOutputBase);
                        OutputNumber = NumberConverter.ToBase(InputNumber, mOutputBase);

                        mHistory.AddEntry(InputNumber, OutputNumber);

                        InputString = InputNumber.ValueInBase;
                        OutputString = OutputNumber.ValueInBase;
                        ErrorMessage = "";
                    }
                    catch(System.OverflowException)
                    {
                        ErrorMessage = "The input number is to large. Max value in base " + InputBase + ": " + NumberConverter.MaxValueForBase(mInputBase);
                        InputNumber = NumberConverter.ToBase(0, 10);
                        OutputNumber = NumberConverter.ToBase(0, 10);
                        InputString = InputNumber.ValueInBase;
                        OutputString = OutputNumber.ValueInBase;
                    }
                }
                else
                    ErrorMessage = "The number does not match it's given radix";
            }
            else
                ErrorMessage = "Radix must be between 2 and 99";
        }
        public void GoBackInHistory()
        {
            if(mHistory.CanBrowseHistory)
            {
                mHistory.GoBackInHistory();
                var entry = mHistory.CurrentEntry;
                FillTextBoxes(entry.Item1, entry.Item2);
            }
        }
        public void GoForwardinHistory()
        {
            if(mHistory.CanBrowseHistory)
            {
                mHistory.GoForwardInHistory();
                var entry = mHistory.CurrentEntry;
                FillTextBoxes(entry.Item1, entry.Item2);
            }
        }

        #endregion

        #region Public Properties

        public string InputString { get => mInputString; set => mInputString = value; }       
        public string OutputString { get { return mOutputString; } set { mOutputString = value; } }

        public string InputBase { get => mInputBase.ToString(); set => mInputBase = System.Convert.ToInt32(value); }
        public string OutputBase { get => mOutputBase.ToString(); set => mOutputBase = System.Convert.ToInt32(value); }

        public string InputComplement { get => InputNumber.Complement; set { } }
        public string OutputComplement { get => OutputNumber.Complement; set { } }


        public string ErrorMessage { get; set; } = "";

        #endregion

        #region Constructor

        public BaseConverterViewModel()
        {
            FillTextBoxes(InputNumber, OutputNumber);

            SwitchBaseCommand = new RelayCommand( () =>  SwitchBase());
            ConvertCommand = new RelayCommand(() => Convert());
            GoBackInHistoryCommand = new RelayCommand(() => GoBackInHistory());
            GoForwardInHistoryCommand = new RelayCommand(() => GoForwardinHistory());
        }

        #endregion

        #region Helpers

        public void FillTextBoxes(Number input, Number output)
        {
            InputNumber = input;
            OutputNumber = output;

            InputString = InputNumber.ValueInBase;
            OutputString = OutputNumber.ValueInBase;

            InputComplement = InputNumber.Complement;
            OutputComplement = OutputNumber.Complement;

            InputBase = InputNumber.Radix.ToString();
            OutputBase = OutputNumber.Radix.ToString();
        }

       
        #endregion

    }
}
