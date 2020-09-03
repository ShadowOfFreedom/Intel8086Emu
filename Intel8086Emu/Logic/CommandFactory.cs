using Intel8086Emu.Logic.Commands;
using Intel8086Emu.ViewModels;
using System;

namespace Intel8086Emu.Logic
{
    public class CommandFactory
    {
        private readonly MainWindowViewModel _viewModel;

        public CommandFactory(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public ICommand CreatCommand(string commandName) =>
            commandName.ToLower() switch
            {
                "mov" => (ICommand) new MovCommand(_viewModel),
                "push" => (ICommand) new PushCommand(_viewModel),
                "pop" => (ICommand) new PopCommand(_viewModel),
                _ => throw new ArgumentException("Niepoprawna nazwa komendy")
            };
    }
}