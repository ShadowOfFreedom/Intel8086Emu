using Intel8086Emu.ViewModels;

namespace Intel8086Emu.Objects.CommandArgs
{
    internal class RegArg : CommandArg, ICommandArg
    {
        public string StringValue { get; set; }
        public ushort Value { get; }
        public ArgSize Size { get; }

        public RegArg(string regName, MainWindowViewModel viewModel) : base(viewModel)
        {
            StringValue = regName;

            if (viewModel.GeneralRegister.Is16RegisterKey(regName))
            {
                Value = viewModel.GeneralRegister.Get16RegisterValue(regName);
                Size = ArgSize._16Bit;
            }
            else if (viewModel.GeneralRegister.ContainsKey(regName))
            {
                Value = viewModel.GeneralRegister[regName];
                Size = ArgSize._8Bit;
            }
            else if (viewModel.Register.ContainsKey(regName))
            {
                Value = viewModel.Register[regName];
                Size = ArgSize._16Bit;
            }
        }
    }
}
