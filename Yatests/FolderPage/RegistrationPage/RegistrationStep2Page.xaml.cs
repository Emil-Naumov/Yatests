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
using Yatests.FolderData;
using Yatests.WindowsFolder;

namespace Yatests.FolderPage.RegistrationPage
{
    /// <summary>
    /// Логика взаимодействия для RegistrationStep2Page.xaml
    /// </summary>
    public partial class RegistrationStep2Page : Page
    {
        User user;
        public RegistrationStep2Page()
        {
            InitializeComponent();
        }

        private void Back(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Enter(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMessageBox("Вы не ввели логин!");
                LoginTb.Focus();
            }
            else if (LoginTb.Text.Length < 6)
            {
                MBClass.ErrorMessageBox
                    ("Логин должен состоять из 6 символов!");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPb.Password))
            {
                MBClass.ErrorMessageBox("Вы не ввели пароль!");
                PasswordPb.Focus();
            }
            else if (PasswordPb.Password.Length < 6)
            {
                MBClass.ErrorMessageBox
                    ("Пароль должен состоять из 6 символов!");
                PasswordPb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(RPasswordPb.Password))
            {
                MBClass.ErrorMessageBox("Вы не повторили пароль!");
                RPasswordPb.Focus();
            }
            else if (PasswordPb.Password != RPasswordPb.Password)
            {
                MBClass.ErrorMessageBox("Пароли должны совпадать!");
                PasswordPb.Clear();
                RPasswordPb.Clear();
                PasswordPb.Focus();
            }
            else if (LoginTb.Text.Equals(PasswordPb.Password))
            {
                MBClass.ErrorMessageBox
                    ("Логин и пароль не должны совпадать!");
                PasswordPb.Clear();
                RPasswordPb.Clear();
                PasswordPb.Focus();
            }
            else
            {
                try
                {
                    var user = DBEntities.GetContext().User.FirstOrDefault
                       (u => u.LoginUser == LoginTb.Text);
                    if (user != null)
                    {
                        MBClass.ErrorMessageBox
                            ("Пользователь с таким логином уже существует!");
                        LoginTb.Clear();
                        LoginTb.Focus();
                    }
                    else
                    {
                        AddUser();
                        AddEmployee();
                        VariableClass.ClearData();
                        MBClass.InfoMessageBox("Регистрация прошла успешно");
                        new Authorization().Show();
                        VariableClass.Window.Close();
                    }

                }
                catch (Exception ex)
                {
                    MBClass.ErrorMessageBox(ex);
                }
            }
        }
        private void AddUser()
        {
            try
            {
                user = new User()
                {
                    LoginUser = LoginTb.Text,
                    PasswordUser = PasswordPb.Password,
                    IdRole = 2,
                };
                DBEntities.GetContext().User.Add(user);
                DBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMessageBox(ex);
            }

        }
        private void AddEmployee()
        {
            try
            {
                var addEmployee = new Staff()
                {
                    Surname = VariableClass.Surname,
                    Name = VariableClass.Name,
                    Patronymic = VariableClass.Patronymic,
                    IdUser = user.IdUser,
                };
                DBEntities.GetContext().Staff.Add(addEmployee);
                DBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMessageBox(ex);
            }

        }

        private void LoginTb_KeyDown(object sender, KeyEventArgs e)
        {
            T1.Text = "";
        }

        private void PasswordPb_KeyDown(object sender, KeyEventArgs e)
        {
            T2.Text = "";
        }

        private void RPasswordPb_KeyDown(object sender, KeyEventArgs e)
        {
            T3.Text = "";
        }

        private void Logo(object sender, MouseButtonEventArgs e)
        {
            new FolderWindow.MBFolder.Author().Show();
        }
    }
}
