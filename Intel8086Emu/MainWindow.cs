using Intel8086Emu.Logic;
using Intel8086Emu.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace Intel8086Emu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CommandFasade _commandFasade;
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            _commandFasade = new CommandFasade(DataContext as MainWindowViewModel);

            CommandTextBox.Focus();
        }
        
        private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {
            ExecuteCommand();
        }

        private void CommandTextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            ExecuteCommand();
            e.Handled = true;
        }

        private void ExecuteCommand()
        {
            try
            {
                _commandFasade.Execut(CommandTextBox.Text.ToLower());
            }
            catch (Exception e)
            {
                MessageBox.Show( e.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
