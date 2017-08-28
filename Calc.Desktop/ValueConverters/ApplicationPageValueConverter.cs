
using Calc.Core;
using System;
using System.Diagnostics;
using System.Globalization;

namespace Calc.Desktop
{   
    /// <summary>
    /// Convertes the <see cref="ApplicationPage"/> to actual view/page
    /// </summary>
    public class ApplicationPageValueConverter : ValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Find the appropriate page
            switch((ApplicationPage)value)
            {
                case ApplicationPage.Welcome:
                    return new WelcomePage();            
                case ApplicationPage.BaseConverter:
                    return new BaseConverterPage();
                case ApplicationPage.FloatRepresentation:
                    return new FloatRepresentationPage();                
                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
