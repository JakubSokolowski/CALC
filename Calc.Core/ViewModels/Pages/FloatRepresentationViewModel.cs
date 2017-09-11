using Calc.FloatingPointNumbers;
using Calc.PositionalSystem;
using System;

namespace Calc.Core
{
    public class FloatRepresentationViewModel : BaseViewModel
    {
        private FloatConverter fConverter = new FloatConverter();
        private BaseConverter bConverter = new BaseConverter();      

        private string mSingleRepresentation = "";
        private string mInputString = "";

        private SingleRepresentation mInputRepresentation;

        public SingleRepresentation InputRepresentation { get => mInputRepresentation; set => mInputRepresentation = value; }

        // Takes value from InputNumber textbox
        public string InputString
        {
            get => mInputString;

            set
            {                
                if (float.TryParse(value, out float result))
                {
                    InputRepresentation = fConverter.ToSingle(result);
                    mInputString = InputRepresentation.DecimalValue.ToString("F20");                   
                    mSingleRepresentation = InputRepresentation.BinaryString;
                    WriteRepresentation(InputRepresentation);
                    ErrorMessage = "";
                }
                else
                    ErrorMessage = "Invalid Input";
            }
        }

        public string ExactValue
        {
            get
            {
                var exp = Math.Pow(2, InputRepresentation.ExponentValue);
                try
                {                    
                    decimal rep = (decimal)(exp * InputRepresentation.MantissaValue);
                    return ConversionHelpers.RemoveTrailingZeros(rep.ToString());
                }
                catch(Exception ex)
                {
                    // UHHHHH
                    ErrorMessage = "The Value is too large";
                    return " ";
                   
                }
               
            }            
        }

        // Takes value from collection of Numbers
        private string SingleRepresentation
        {
            get => mSingleRepresentation = "";
            set
            {
                mSingleRepresentation = value;
                InputRepresentation = fConverter.ToSingle(value);                
                WriteRepresentation(InputRepresentation);
                InputString = InputRepresentation.DecimalValue.ToString();
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

        public string SignInDecimal => InputRepresentation.Sign == "1" ? "-1" : "1"; 
        public string ExponentInDecimal
        {
            get => InputRepresentation.ExponentValue.ToString().Split('.')[0];
            set
            {
                if (Int32.TryParse(value, out int result))
                {
                    var newExponent = fConverter.GetExponentFromValue(result);
                    InputRepresentation = CreateRepresentationFromElementString(newExponent);                   
                    SingleRepresentation = InputRepresentation.BinaryString;
                    ErrorMessage = "";
                }
            }
        }
        public string MantissaInDecimal => InputRepresentation.MantissaValue.ToString(); 
        public string SignEncoding => InputRepresentation.Sign; 
        public string ExponentEncoding
        {
            get => InputRepresentation.ExponentEncoding.ToString().Split('.')[0];
            set
            {
                if (Int32.TryParse(value, out int result))
                {
                    var newExponent = fConverter.GetExponentFromEncoding(result);
                    InputRepresentation = CreateRepresentationFromElementString(newExponent);                   
                    SingleRepresentation = InputRepresentation.BinaryString;
                    ErrorMessage = "";
                }
            }
        }
        public string MantissaEncoding => InputRepresentation.MantissaEncoding.ToString(); 
     

        public string ErrorMessage { get; set; } = "";

        public FloatRepresentationViewModel()
        {
            mInputRepresentation = fConverter.ToSingle(0f);
            mInputString = mInputRepresentation.DecimalValue.ToString();
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
            WriteRepresentation(InputRepresentation);
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
            InputRepresentation = fConverter.ToSingle(SingleValue); 
        }

        private SingleRepresentation CreateRepresentationFromElementString(string elementString)
        {
            switch(elementString.Length)
            {
                case 1:
                    return fConverter.ToSingle(elementString + ExponentString + MantissaString);
                case 8:
                    return fConverter.ToSingle(SignString + elementString + MantissaString);
                case 23:
                    return fConverter.ToSingle(SignString + ExponentString + elementString);
                default:
                    throw new ArgumentException("The elementString is not Sign exponent or mantissa");
            }
        }

        private bool IsSpecialValue(string value)
        {
            switch(value)
            {
                case "NaN":
                    return true;
                case "+Inf":
                    return true;
                case "-Inf":
                    return true;
                case "+Zero":
                    return true;
                case "-Zero":
                    return true;
                default:
                    return false;
            }           
        }

    }
}
