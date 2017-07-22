using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.PositionalSystem
{
    /// <summary>
    /// Represents all the digits and their respective values in given base
    /// </summary>
    public class BaseRepresentation
    {       
        private int currentRadix = 10;
        readonly Dictionary<string, int> digitToValue = new Dictionary<string, int>();
        readonly Dictionary<string, int> digitToValueUpToBase36 = new Dictionary<string, int>();

        public const int MAX_BASE = 99;

        public int CurrentBase
        {
            get { return currentRadix; }
            set
            {
                if (IsValidRadix(value))
                    currentRadix = value;
                else
                    throw new ArgumentException("Base must be between 2 and " + MAX_BASE.ToString());
            }
        }

        public BaseRepresentation()
        {
            String digitRepresentationString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < 36; i++)
            {
                digitToValueUpToBase36.Add(digitRepresentationString.ElementAt(i).ToString(), i);
            }

            for (int i = 0; i < MAX_BASE; i++)
            {
                string key = i.ToString();
                if (i < 10)
                    key = "0" + key;
                digitToValue.Add(key, i);
            }
        }

        public BaseRepresentation(int maxRadix)
        {
            if (IsValidRadix(maxRadix))
            {
                String digitRepresentationString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                for (int i = 0; i < 36; i++)
                {
                    digitToValueUpToBase36.Add(digitRepresentationString.ElementAt(i).ToString(), i);
                }               
               
                for (int i = 0; i < maxRadix; i++)
                {
                    string key = i.ToString();
                    if (i < 10)
                        key = "0" + key;
                    digitToValue.Add(key, i);
                }               
            }
            else
                throw new ArgumentException("Radix must be between 2 and " + MAX_BASE.ToString());          
        }

      

        public string GetDigit(int value)
        {
            if(currentRadix <= 36)            
                return digitToValueUpToBase36.FirstOrDefault(x => x.Value == value).Key;            
            else            
                return digitToValue.FirstOrDefault(x => x.Value == value).Key;                    
        }

        public int GetValue(string key)
        {
            if(currentRadix <= 36)            
                return digitToValueUpToBase36[key];            
            return digitToValue[key];
        }

        private bool IsValidRadix(int radix) { return (radix >= 2 && radix <= 256); }
    }
}
