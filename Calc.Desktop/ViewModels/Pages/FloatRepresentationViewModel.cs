using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using Calc.PositionalSystem;
using System.ComponentModel;
using System;

namespace Calc.Desktop
{
    public class FloatRepresentationViewModel : BaseViewModel
    {
        #region Private Members

        private FloatConverter fConverter = new FloatConverter();
        private Number InputNumber { get; set; } = NumberConverter.ToBase(0m, 10);

        private string mSingleRepresentation = "";

        #endregion


        #region Public Properties


        public string InputString
        {
            get => InputNumber.ValueInBase;
         
            set
            {
                float result;
                if (float.TryParse(value, out result))
                {
                    var rep = new SingleRepresentation(result);
                    WriteRepresentation(rep);
                    InputNumber = NumberConverter.ToBase(rep, InputNumber.Radix);
                    SingleRepresentation = InputNumber.SingleBinaryString;
                    ErrorMessage = "";
                }
                else
                    ErrorMessage = "Invalid Input";
            }
        }

        private string SingleRepresentation
        {
            get => mSingleRepresentation = "";
            set
            {
                mSingleRepresentation = value;
                var rep = new SingleRepresentation(value);
                InputNumber = NumberConverter.ToBase(rep, InputNumber.Radix);
                ErrorMessage = "";                
            }
        }


        public string ErrorMessage { get; set; } = "";

      
        public ButtonListViewModel Sign { get; set; }
        public ButtonListViewModel Exponent { get; set; }
        public ButtonListViewModel Mantissa { get; set; }

        public string SignString => Sign.GetContent();
        public string ExponentString => Exponent.GetContent();
        public string MantissaString => Mantissa.GetContent();

        public string SingleValue { get => SignString + ExponentString + MantissaString; }


        
        public string SignInDecimal { get { return InputNumber.SingleRep.Sign == "1" ? "-1" : "1"; }  set { } }
        public string ExponentInDecimal { get { return InputNumber.SingleRep.ExponentValue.ToString().Split('.')[0]; } }
        public string MantissaInDecimal { get { return InputNumber.SingleRep.MantissaValue.ToString(); } }

        public string SignEncoding { get { return InputNumber.SingleRep.Sign; } set { } }
        public string ExponentEncoding { get { return InputNumber.SingleRep.ExponentEncoding.ToString().Split('.')[0]; } }
        public string MantissaEncoding { get { return InputNumber.SingleRep.MantissaEncoding.ToString(); } }

      
        


        public ICommand StartCommand { get; set; }
        public ICommand GetButtonsContent { get; set; }
        public ICommand AddButton { get; set; }


        #endregion

        #region Constructor

        public FloatRepresentationViewModel()
        {
            AssignCommands();
            CreateSingleRepresentation();
            this.PropertyChanged += new PropertyChangedEventHandler(SubPropertyChanged);
            Mediator.Instance.Register((Object o) =>
            {
                SingleRepresentation = SingleValue;
            }, ViewModelMessages.RepresentationUpdated);
        }

        public void CreateSingleRepresentation()
        {
            Sign = new ButtonListViewModel();
            Exponent = new ButtonListViewModel();
            Mantissa = new ButtonListViewModel();
            WriteRepresentation(InputNumber.SingleRep);

        }

        public async Task StartMainPage()
        {
            await Task.Delay(500);
           
        }
      


      
        #endregion

        #region Private Helpers

        public void AssignCommands()
        {
            StartCommand = new RelayCommand(async () => await StartMainPage());
           
        }

        public void WriteRepresentation(FloatingPointRepresentation representation)
        {
            Sign.WriteText(representation.Sign);
            Exponent.WriteText(representation.Exponent);
            Mantissa.WriteText(representation.Mantissa);
        }

        private void SubPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SingleValue")
                CalculateFromSingleValue();
            if(e.PropertyName == "Sign")
            {

            }

        }

        private void CalculateFromSingleValue()
        {
            var rep = new SingleRepresentation(SingleValue);
            InputNumber = NumberConverter.ToBase(rep, 10);
        }
    
        #endregion

    }
}
