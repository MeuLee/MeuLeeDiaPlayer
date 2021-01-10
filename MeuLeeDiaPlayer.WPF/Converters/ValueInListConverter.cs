using MeuLeeDiaPlayer.Common.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace MeuLeeDiaPlayer.WPF.Converters
{
    public class ValueInListConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Length == 2
                && values[0] is List<SongDto> list
                && values[1] is SongDto song
                && list.Contains(song);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
