using Intel8086Emu.Helpers;
using Intel8086Emu.ViewModels;
using System;
using System.Linq;

namespace Intel8086Emu.Objects.CommandArgs
{
    internal class MemArg : CommandArg, ICommandArg
    {
        private static readonly string[] ValidGenRegNames = { "bx", "bp" };
        private static readonly string[] ValidRegNames = { "si", "di" };

        public string StringValue { get; set; }
        public ArgSize Size { get; } = ArgSize._16Bit;

        public ushort Value => ViewModel.Memory.GetValue(Key, Address);

        public ushort AddressValue
        {
            get
            {
                var memAdressArray = StringValue
                    .Trim('[')
                    .TrimEnd(']')
                    .ToLower()
                    .Replace(" ", "")
                    .Split('+');

                switch (memAdressArray.Length)
                {
                    case 1 when ValidRegNames.Contains(memAdressArray[0]) || ValidGenRegNames.Contains(memAdressArray[0]):
                        return ViewModel.GetValue(memAdressArray[0]);
                    case 1 when memAdressArray[0].IsImmediate():
                        return GetImmidietValue(memAdressArray[0]);
                    case 2 when (ValidRegNames.Contains(memAdressArray[0]) || ValidGenRegNames.Contains(memAdressArray[0])) && memAdressArray[1].IsImmediate():
                        return (ushort) (ViewModel.GetValue(memAdressArray[0]) + GetImmidietValue(memAdressArray[1]));
                    case 2 when ValidGenRegNames.Contains(memAdressArray[0]) && ValidRegNames.Contains(memAdressArray[1]):
                        return (ushort) (ViewModel.GetValue(memAdressArray[0]) + ViewModel.GetValue(memAdressArray[1]));
                    case 3 when ValidGenRegNames.Contains(memAdressArray[0]) && ValidRegNames.Contains(memAdressArray[1]) && memAdressArray[2].IsImmediate():
                        return (ushort) (ViewModel.GetValue(memAdressArray[0])
                                         + ViewModel.GetValue(memAdressArray[1])
                                         + GetImmidietValue(memAdressArray[2]));
                    default:
                        throw new ArgumentException("Nieprawidłowy adres pamięci");
                }
            }
        }
        
        public (ushort, ushort) Key 
            => (0x0700, Convert.ToUInt16(
                AddressValue.ToString("X4")
                    .Remove(3, 1)
                    .Insert(3, "0"),
                16));

        public byte Address => Convert.ToByte(AddressValue.ToString("X4")[3].ToString(), 16);

        public MemArg(string memAdress, MainWindowViewModel viewModel) : base(viewModel) => StringValue = memAdress;

        private ushort GetImmidietValue(string sValue)
        {
            if (sValue.IsBinaryValue())
                return Convert.ToUInt16(sValue.Replace("b", ""), 2);

            if (!sValue.IsHexValue()) 
                return sValue.IsNumber() ? Convert.ToUInt16(sValue) : (ushort) 0;

            var hexString = sValue.Replace("h", "");
            if (hexString.Length > 4) 
                hexString = hexString.Remove(0, 1);

            return Convert.ToUInt16(hexString, 16);

        }
    }
}