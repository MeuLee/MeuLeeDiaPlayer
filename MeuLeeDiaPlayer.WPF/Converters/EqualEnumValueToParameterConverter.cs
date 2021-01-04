using System;
using System.Globalization;
using System.Windows.Data;

namespace MeuLeeDiaPlayer.WPF.Converters
{
    public class EqualEnumValueToParameterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool result =  values.Length == 2 && values[0] is Enum e1 && values[1] is Enum e2 && e1 == e2;
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
