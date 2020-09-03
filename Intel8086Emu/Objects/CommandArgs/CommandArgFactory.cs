using System;
using Intel8086Emu.Helpers;
using Intel8086Emu.ViewModels;

namespace Intel8086Emu.Objects.CommandArgs
{
    public class CommandArgFactory
    {
        private readonly MainWindowViewModel _viewModel;

        public CommandArgFactory(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public ICommandArg CreatCommandArg(string stringValue)
        {
            if (stringValue.IsMem())
                return new MemArg(stringValue, _viewModel);

            if (stringValue.IsReg())
                return new RegArg(stringValue, _viewModel);

            if (stringValue.IsImmediate())
                return new ImmediateArg(stringValue);

            throw new ArgumentException("Nieprawidłowy argument komendy");
        }
    }
}