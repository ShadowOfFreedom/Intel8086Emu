using Intel8086Emu.Objects.CommandArgs;
using Intel8086Emu.ViewModels;
using System;
using System.Linq;

namespace Intel8086Emu.Logic.Commands
{
    public class PopCommand : ICommand
    {
        private const string StackPointerName = "sp";

        private readonly MainWindowViewModel _viewModel;
        private readonly CommandArgFactory _commandArgFactory;

        public PopCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            _commandArgFactory = new CommandArgFactory(_viewModel);
        }

        public void ExecutCommand(string commandArgs)
        {
            if (string.IsNullOrEmpty(commandArgs))
                throw new ArgumentException("Nieprawidłowy argument komendy");

            var destination = _commandArgFactory.CreatCommandArg(commandArgs.ToLower());
            var sourceValue = _viewModel.GetStackValue((0x0700, _viewModel.Register[StackPointerName]));

            if (!new[] {"cs", "ip", "ds", "ss", "sp"}.Contains(destination.StringValue.ToLower())
                && !_viewModel.GeneralRegister.ContainsKey(destination.StringValue))
                switch (destination)
                {
                    case RegArg regDestination:
                        _viewModel.SetValue(regDestination.StringValue, sourceValue);
                        _viewModel.IncreaseStackPointer();
                        break;
                    case MemArg memDestination:
                        _viewModel.SetValueToMemory(memDestination.Key, memDestination.Address, sourceValue);
                        _viewModel.IncreaseStackPointer();
                        break;
                    default:
                        throw new ArgumentException("Nieprawidłowy argument komendy");
                }
            else
                throw new ArgumentException("Nieprawidłowy argument komendy");
        }
    }
}