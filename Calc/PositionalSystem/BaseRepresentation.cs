using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.PositionalSystem
{
    public class BaseRepresentation
    {
        public const int MAX_AVALIBLE_RADIX = 256;
        private int currentRadix = 10;
        readonly Dictionary<string, int> digitToIndexMap = new Dictionary<string, int>();
        readonly Dictionary<string, int> digitToIndexUpToBase36Map = new Dictionary<string, int>();

        public int CurrentRadix
        {
            get { return currentRadix; }
            set
            {
                if (IsValidRadix(value))
                    currentRadix = value;
                else
                    throw new ArgumentException("Radix must be between 2 and " + MAX_AVALIBLE_RADIX.ToString());
            }
        }

        public BaseRepresentation(int maxRadix)
        {
            if (IsValidRadix(maxRadix))
            {
                String digitRepresentationString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                for (int i = 0; i < 36; i++)
                {
                    digitToIndexUpToBase36Map.Add(digitRepresentationString.ElementAt(i).ToString(), i);
                }               
               
                for (int i = 0; i < maxRadix; i++)
                {
                    string key = i.ToString();
                    if (i < 10)
                        key = "0" + key;
                    digitToIndexMap.Add(key, i);
                }               
            }
            else
                throw new ArgumentException("Radix must be between 2 and " + MAX_AVALIBLE_RADIX.ToString());          
        }

        public BaseRepresentation() { }

        public string GetDigit(int value)
        {
            if(currentRadix <= 36)
            {
                return digitToIndexUpToBase36Map.FirstOrDefault(x => x.Value == value).Key;
            }
            else
            {
                return digitToIndexMap.FirstOrDefault(x => x.Value == value).Key;
            }           
        }

        public int GetValue(string key)
        {
            if(currentRadix <= 36)
            {
                return digitToIndexUpToBase36Map[key];
            }
            return digitToIndexMap[key];
        }

        private bool IsValidRadix(int radix) { return (radix >= 2 && radix <= 256); }
    }
}
