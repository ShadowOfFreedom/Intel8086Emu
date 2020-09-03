using System.Linq;
using System.Text;

namespace Intel8086Emu.Helpers
{
    static class ByteArrayExtension
    {
        public static string ToMemoryString(this byte[] bytesArray)
        {
            var sBuilder = new StringBuilder();

            foreach (var b in bytesArray.Select((value, i) => new { i, value }))
            {
                sBuilder.Append(b.i == 7 ? $"{b.value:X2}-" : $"{b.value:X2} ");
            }

            return sBuilder.ToString().TrimEnd();
        }
    }
}
