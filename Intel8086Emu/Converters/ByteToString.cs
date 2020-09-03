using System;
using System.Globalization;
using System.Windows.Data;

namespace Intel8086Emu.Converters
{
    public class ByteToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte byteValue)
            {
                return byteValue.ToString("X2");
            }
            return "00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return System.Convert.ToByte(stringValue, 16);
            }
            return (byte) 0;
        }
    }
}
