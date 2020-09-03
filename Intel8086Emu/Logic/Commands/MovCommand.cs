using Intel8086Emu.Objects.CommandArgs;
using Intel8086Emu.ViewModels;
using System;
using System.Linq;

namespace Intel8086Emu.Logic.Commands
{
    public class MovCommand : ICommand
    {
        private readonly MainWindowViewModel _viewModel;
        private readonly CommandArgFactory _commandArgFactory;

        public MovCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            _commandArgFactory = new CommandArgFactory(_viewModel);
        }

        public void ExecutCommand(string commandArgs)
        {
            if (string.IsNullOrEmpty(commandArgs)) 
                throw new ArgumentException("Nieprawidłowy argument komendy");

            var commandArgsArray = commandArgs.Split(",");

            var destination = _commandArgFactory.CreatCommandArg(commandArgsArray[0].Trim().ToLower());
            var source = _commandArgFactory.CreatCommandArg(commandArgsArray[1].Trim().ToLower());

            if (!new[] {"cs", "ip", "ds", "ss", "sp"}.Contains(destination.StringValue.ToLower()))
            {
                if (destination is RegArg regDestination)
                {
                    _viewModel.SetValue(regDestination.StringValue, source.Value);
                }
                else if (destination is MemArg memDestination && !(source is MemArg))
                {
                    if (source.Size == ArgSize._16Bit)
                    {
                        _viewModel.SetValueToMemory(memDestination.Key, memDestination.Address, source.Value);
                    }
                    else if (source.Size == ArgSize._8Bit)
                    {
                        _viewModel.SetValueToMemory(memDestination.Key, memDestination.Address, (byte) source.Value);
                    }
                }
            }

            _viewModel.IncreaseIpValue(2);
        }
    }
}