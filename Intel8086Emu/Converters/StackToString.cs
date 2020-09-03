using Intel8086Emu.Objects;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Intel8086Emu.Converters
{
    public class StackToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MemoryStack stack)
            {
                return stack.ToString();
            }

            return "Memory not initialized";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(); //it's never used
        }
    }
}
