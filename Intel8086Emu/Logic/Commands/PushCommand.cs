using Intel8086Emu.Objects.CommandArgs;
using Intel8086Emu.ViewModels;
using System;

namespace Intel8086Emu.Logic.Commands
{
    public class PushCommand : ICommand
    {
        private const string StackPointerName = "sp";

        private readonly MainWindowViewModel _viewModel;
        private readonly CommandArgFactory _commandArgFactory;

        public PushCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            _commandArgFactory = new CommandArgFactory(_viewModel);
        }

        public void ExecutCommand(string commandArgs)
        {
            if (string.IsNullOrEmpty(commandArgs))
                throw new ArgumentException("Nieprawidłowy argument komendy");

            var source = _commandArgFactory.CreatCommandArg(commandArgs.ToLower());
            _viewModel.SetStackValue((0x0700, (ushort) (_viewModel.Register[StackPointerName] - 2)), source.Value);
        }
    }
}