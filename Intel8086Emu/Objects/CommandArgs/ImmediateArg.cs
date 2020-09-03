using Intel8086Emu.Helpers;
using System;

namespace Intel8086Emu.Objects.CommandArgs
{
    internal class ImmediateArg : ICommandArg
    {
        public string StringValue { get; set; }

        public ushort Value =>
            StringValue.IsBinaryValue()
                ? Convert.ToUInt16(StringValue.Replace("b", ""), 2)
                : StringValue.IsHexValue()
                    ? Convert.ToUInt16(
                        StringValue.StartsWith('0')
                            ? StringValue.Remove(0, 1).Replace("h", "")
                            : StringValue.Replace("h", "")
                        , 16)
                    : StringValue.IsNumber()
                        ? ushort.Parse(StringValue)
                        : (ushort) 0;

        public ArgSize Size
        {
            get
            {
                try
                {
                    Convert.ToByte(Value);
                    return ArgSize._8Bit;
                }
                catch (Exception)
                {
                    return ArgSize._16Bit;
                }
            }
        }

        public ImmediateArg(string stringValue) => StringValue = stringValue;
    }
}