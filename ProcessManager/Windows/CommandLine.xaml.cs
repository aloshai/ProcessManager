using System.Windows;

namespace ProcessManager.Modules
{
    /// <summary>
    /// CommandLine.xaml etkileşim mantığı
    /// </summary>
    public partial class CommandLine : Window
    {
        public CommandLine()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
