using Intel8086Emu.ViewModels;

namespace Intel8086Emu.Objects.CommandArgs
{
    public abstract class CommandArg
    {
        protected readonly MainWindowViewModel ViewModel;

        protected CommandArg(MainWindowViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}