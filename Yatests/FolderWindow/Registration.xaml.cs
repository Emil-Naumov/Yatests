using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Yatests.FolderClass;
using Yatests.WindowsFolder;

namespace Yatests.FolderWindow
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
            VariableClass.Window = this;
            MainFrame.Content = new FolderPage.RegistrationPage.
                RegistrationStep1Page();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {
                System.Threading.Thread.Sleep(10);
            }
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            if (MBClass.QuestionMB("Хотите отмениь регистрацию?"))
            {
                new Authorization().Show();
                Close();
            }
        }
    }
}
