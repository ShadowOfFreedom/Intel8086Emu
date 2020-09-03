using Intel8086Emu.Objects;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Intel8086Emu.Converters
{
    public class MemoryToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Memory memory)
            {
                return memory.ToString();
            }

            return "Memory not initialaize";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(); //it's never used
        }
    }
}
