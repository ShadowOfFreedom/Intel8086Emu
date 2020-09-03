using Intel8086Emu.ViewModels;
using System;

namespace Intel8086Emu.Logic
{
    public class CommandFasade
    {
        private readonly MainWindowViewModel _viewModel;
        private readonly CommandFactory _factory;

        public CommandFasade(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            _factory = new CommandFactory(viewModel);
        }

        public void Execut(string commandText)
        {
            if (string.IsNullOrEmpty(commandText)) 
                throw new ArgumentException("Nieprawidłowa komenda");

            var commandArray = commandText.Split(" ", 2);

            _factory.CreatCommand(commandArray[0]).ExecutCommand(commandArray[1]);
        }
    }
}
