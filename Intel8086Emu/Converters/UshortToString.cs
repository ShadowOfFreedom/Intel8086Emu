using System;
using System.Globalization;
using System.Windows.Data;

namespace Intel8086Emu.Converters
{
    public class UshortToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ushort ushortValue)
            {
                return ushortValue.ToString("X4");
            }

            return "0000";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(); //it's never used
        }
    }
}
