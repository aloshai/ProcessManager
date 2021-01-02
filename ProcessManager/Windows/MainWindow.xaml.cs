using ProcessManager.Modules;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace ProcessManager
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Process Working Directory.";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var inputWindow = new CommandLine();
                if (inputWindow.ShowDialog() == true)
                {
                    var commandText = inputWindow.inputBox.Text;
                    inputWindow.Close();
                    var processModel = Managers.ProcessManager.StartProcess(dialog.SelectedPath, commandText);
                    ProcessListbox.Items.Add(processModel);
                }
            }
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            var index = ProcessListbox.SelectedIndex;
            var processModel = Managers.ProcessManager.Processes[index];
            Managers.ProcessManager.StartProcess(processModel.Path, processModel.Command);
        }

        private void StopButton_OnClick(object sender, RoutedEventArgs e)
        {
            var index = ProcessListbox.SelectedIndex;
            var processModel = Managers.ProcessManager.Processes[index];
            Managers.ProcessManager.KillProcess(processModel.Process);
        }

        private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var index = ProcessListbox.SelectedIndex;
            var processModel = Managers.ProcessManager.Processes[index];
            Managers.ProcessManager.KillProcess(processModel);
            ProcessListbox.Items.RemoveAt(index);
            Managers.ProcessManager.Processes.RemoveAt(index);
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
