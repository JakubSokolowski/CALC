
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Calc.Desktop
{
    // The same as static resource, bindings 
    // TODO read more

    /// <summary>
    /// Base Value Converter that allows direct XAML usage 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {

        private static T mConverter = null;

        #region Value Converter Methods

        
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConverter ?? (mConverter = new T());
        }

        #endregion

    }
}
