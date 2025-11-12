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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Yatests.FolderClass;
using Yatests.WindowsFolder;

namespace Yatests.FolderPage.RegistrationPage
{
    /// <summary>
    /// Логика взаимодействия для RegistrationStep1Page.xaml
    /// </summary>
    public partial class RegistrationStep1Page : Page
    {
        public RegistrationStep1Page()
        {
            InitializeComponent();
        }

        private void SurnameTb_KeyDown(object sender, KeyEventArgs e)
        {
            T1.Text = "";
        }

        private void NameTb_KeyDown(object sender, KeyEventArgs e)
        {
            T2.Text = "";
        }

        private void PatronymicTb_KeyDown(object sender, KeyEventArgs e)
        {
            T3.Text = "";
        }

        private void Enter(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SurnameTb.Text))
            {
                MBClass.ErrorMessageBox("Вы не ввели фамилию!");
                SurnameTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(NameTb.Text))
            {
                MBClass.ErrorMessageBox("Вы не ввели имя!");
                NameTb.Focus();
            }

            else
            {
                VariableClass.Surname = SurnameTb.Text;
                VariableClass.Name = NameTb.Text;
                VariableClass.Patronymic = PatronymicTb.Text;
                NavigationService.Navigate(new RegistrationStep2Page());
            }
        }

        private void Back(object sender, MouseButtonEventArgs e)
        {
            if (MBClass.QuestionMB
               ("Хотите отменить регистрацию?"))
            {
                VariableClass.ClearData();
                new Authorization().Show();
                VariableClass.Window.Close();
                
            }
        }

        private void Logo(object sender, MouseButtonEventArgs e)
        {
            new FolderWindow.MBFolder.Author().Show();
        }
    }
}
