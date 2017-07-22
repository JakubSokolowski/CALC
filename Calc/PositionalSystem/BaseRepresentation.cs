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
        #region Public Fields and Properties

        /// <summary>
        /// Indicates wchich characters will be used. Needs to be set before converting. The default value is 10.
        /// </summary>
        public int CurrentBase
        {
            get { return currentRadix; }
            set
            {
                if (IsValidRadix(value))
                    currentRadix = value;
                else
                    throw new ArgumentException("Radix must be between 2 and " + MAX_BASE.ToString());
            }
        }
        /// <summary>
        /// Maximum base that is supported.
        /// </summary>
        public const int MAX_BASE = 99;

        #endregion

        #region Private Fields
      
        private int currentRadix = 10;
        /// <summary>
        /// Strings that stores all the digits 0-9 and all the capital letters A-Z
        /// </summary>
        readonly static String digitRepresentationString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        /// <summary>
        /// The dictionary that maps string that represents digits in given positional system to their int values.
        /// When <see cref="CurrentBase"/> is bigger than 36, values and digits are being taken from this dictionary
        /// </summary>
        readonly Dictionary<string, int> digitToValue = new Dictionary<string, int>();
        /// <summary>
        /// The dictionary that maps string that represents digits in given positional system to their int values.
        /// When <see cref="CurrentBase"/> is between 2 and 36, values and digits are being taken from this dictionary
        /// </summary>
        readonly Dictionary<string, int> digitToValueUpToBase36 = new Dictionary<string, int>();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs <see cref="BaseRepresentation"/> that is able to represents numbers from base 2 up to <see cref="MAX_BASE"/>
        /// </summary>
        public BaseRepresentation()
        {           
            for (int i = 0; i < 36; i++)
            {
                digitToValueUpToBase36.Add(digitRepresentationString.ElementAt(i).ToString(), i);
            }

            for (int i = 0; i < MAX_BASE; i++)
            {
                string key = i.ToString();
                // Add 0 in front all the one digit numbers, for consistency
                if (i < 10)
                    key = "0" + key;
                digitToValue.Add(key, i);
            }
        }

        /// <summary>
        /// Constructs <see cref="BaseRepresentation"/> that is able to represents numbers from base 2 up to <paramref name="maxRadix"/>
        /// </summary>
        /// <param name="maxRadix">The max radix, that this <see cref="BaseRepresentation"/> will be able to represent</param>
        public BaseRepresentation(int maxRadix)
        {
            if (IsValidRadix(maxRadix))
            {                
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

        #endregion

        #region Public Methods

        public string GetDigit(int value)
        {
            if (currentRadix <= 36)
                return digitToValueUpToBase36.FirstOrDefault(x => x.Value == value).Key;
            else
                return digitToValue.FirstOrDefault(x => x.Value == value).Key;
        }

        public int GetValue(string key)
        {
            if (currentRadix <= 36)
                return digitToValueUpToBase36[key];
            return digitToValue[key];
        }

        #endregion

        #region Input Validation

        /// <summary>
        /// Returns true if the <paramref name="radix"/> is between 2 and <see cref="MAX_BASE"/>
        /// </summary>
        /// <param name="radix"></param>
        /// <returns></returns>
        private bool IsValidRadix(int radix) { return (radix >= 2 && radix <= MAX_BASE); }

        #endregion
    }
}
