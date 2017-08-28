
using Calc.PositionalSystem;
using System;

namespace Calc.Core
{
    public class FloatRepresentationViewModel : BaseViewModel
    {
        private FloatConverter fConverter = new FloatConverter();
        private BaseConverter bConverter = new BaseConverter();      

        private string mSingleRepresentation = "";    

        public Number InputNumber { get; set; } = NumberConverter.ToBase(0.0, 10);

        // Takes value from InputNumber textbox
        public string InputString
        {
            get => InputNumber.ValueInBase;

            set
            {
                if (float.TryParse(value, out float result))
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
        // Takes value from collection of Numbers
        private string SingleRepresentation
        {
            get => mSingleRepresentation = "";
            set
            {
                mSingleRepresentation = value;
                var rep = new SingleRepresentation(value);
                WriteRepresentation(rep);
                InputNumber = NumberConverter.ToBase(rep, InputNumber.Radix);
                ErrorMessage = "";
            }
        }        

        public ButtonListViewModel Sign { get; set; }
        public ButtonListViewModel Exponent { get; set; }
        public ButtonListViewModel Mantissa { get; set; }

        public string SingleValue => SignString + ExponentString + MantissaString;

        public string SignString => Sign.GetAllButtonsText();
        public string ExponentString => Exponent.GetAllButtonsText();
        public string MantissaString => Mantissa.GetAllButtonsText();   

        public string SignInDecimal => InputNumber.SingleRep.Sign == "1" ? "-1" : "1"; 
        public string ExponentInDecimal
        {
            get => InputNumber.SingleRep.ExponentValue.ToString().Split('.')[0];
            set
            {
                if (Int32.TryParse(value, out int result))
                {
                    var newExponent = fConverter.GetExponentFromValue(result);
                    var rep = CreateRepresentationFromElementString(newExponent);
                    InputNumber = NumberConverter.ToBase(rep, InputNumber.Radix);
                    SingleRepresentation = InputNumber.SingleBinaryString;
                    ErrorMessage = "";
                }
            }
        }
        public string MantissaInDecimal { get { return InputNumber.SingleRep.MantissaValue.ToString(); } }

        public string SignEncoding => InputNumber.SingleRep.Sign; 
        public string ExponentEncoding
        {
            get => InputNumber.SingleRep.ExponentEncoding.ToString().Split('.')[0];
            set
            {
                int result;
                if (Int32.TryParse(value, out result))
                {
                    var newExponent = fConverter.GetExponentFromEncoding(result);
                    var rep = CreateRepresentationFromElementString(newExponent);
                    InputNumber = NumberConverter.ToBase(rep, InputNumber.Radix);
                    SingleRepresentation = InputNumber.SingleBinaryString;
                    ErrorMessage = "";
                }
            }
        }
        public string MantissaEncoding { get { return InputNumber.SingleRep.MantissaEncoding.ToString(); } }
     

        public string ErrorMessage { get; set; } = "";

        public FloatRepresentationViewModel()
        {
            AssignCommands();
            CreateSingleRepresentation();
            RegisterForEvents();
        }

        public void AssignCommands()
        {

        }
        public void CreateSingleRepresentation()
        {
            Sign = new ButtonListViewModel();
            Exponent = new ButtonListViewModel();
            Mantissa = new ButtonListViewModel();
            WriteRepresentation(InputNumber.SingleRep);
        }
        public void CreateDoubleRepresentation()
        {

        }

        public void RegisterForEvents()
        {      
            // Register the value change, when buttons in Collection are clicked
            Mediator.Instance.Register((Object o) =>
            {
                SingleRepresentation = SingleValue;
            },  ViewModelMessages.RepresentationUpdated);
        }                     

        public void WriteRepresentation(FloatingPointRepresentation representation)
        {
            Sign.WriteOnButtons(representation.Sign);
            Exponent.WriteOnButtons(representation.Exponent);
            Mantissa.WriteOnButtons(representation.Mantissa);
        }
        private void CalculateFromSingleValue()
        {
            var rep = new SingleRepresentation(SingleValue);
            InputNumber = NumberConverter.ToBase(rep, 10);
        }

        private SingleRepresentation CreateRepresentationFromElementString(string elementString)
        {
            switch(elementString.Length)
            {
                case 1:
                    return new SingleRepresentation(elementString + ExponentString + MantissaString);
                case 8:
                    return new SingleRepresentation(SignString + elementString + MantissaString);
                case 23:
                    return new SingleRepresentation(SignString + ExponentString + elementString);
                default:
                    throw new ArgumentException("The elementString is not Sign exponent or mantissa");
            }
        }

    }
}
