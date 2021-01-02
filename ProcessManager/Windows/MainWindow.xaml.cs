using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Management;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Microsoft.Win32;
using ProcessManager.Modules;
using Application = System.Windows.Application;
using ListBox = System.Windows.Controls.ListBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

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

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void InformationButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            var processes = Managers.ProcessManager.Processes;
            foreach (var process in processes)
            {
                Managers.ProcessManager.KillProcessAndChildrens(process.Id);
            }
            Application.Current.Shutdown();
        }
    }
}
