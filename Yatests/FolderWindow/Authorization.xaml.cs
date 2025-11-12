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
using Yatests.FolderData;
using Yatests.FolderWindow;

namespace Yatests.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            if (MBClass.QuestionMB
              ("Хотите выйти из программы, даже не войдя в неё?"))
            { 
                Close();
            }
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
        private void Enter(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMessageBox("Вы не ввели логин!");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPb.Password))
            {
                MBClass.ErrorMessageBox("Вы не ввели пароль!");
                PasswordPb.Focus();
            }
            else
            {
                try
                {
                    var user = DBEntities.GetContext().User.FirstOrDefault
                        (u => u.LoginUser == LoginTb.Text);
                    if (user == null)
                    {
                        MBClass.ErrorMessageBox("Пользователь не существует!");
                        LoginTb.Clear();
                        LoginTb.Focus();
                    }
                    else if (user.PasswordUser != PasswordPb.Password)
                    {
                        MBClass.ErrorMessageBox("Неверный пароль!");
                        PasswordPb.Clear();
                        PasswordPb.Focus();
                    }
                    else
                    {
                        VariableClass.IdUser = user.IdUser;
                        switch (user.IdRole)
                        {
                            case 1:
                                Admin admin = DBEntities.GetContext()
                                    .Admin.FirstOrDefault(a => a.IdUser == user.IdUser);
                                if (admin == null)
                                {
                                    MBClass.ErrorMessageBox("администратор отсутствует!");
                                }
                                else
                                {
                                    VariableClass.IdAdmin = admin.IdAdmin;
                                    new FolderWindow.AdminWindowFolder.AdminMainWindow().Show();
                                    Close();
                                }
                                break;

                            case 2:
                                Staff staff = DBEntities.GetContext()
                                    .Staff.FirstOrDefault(s => s.IdUser == user.IdUser);
                                if (staff == null)
                                {
                                    MBClass.ErrorMessageBox("Сотрудник отуствует!");
                                }
                                else
                                {
                                    VariableClass.IdStaff = staff.IdStaff;
                                    new FolderWindow.StaffWindowFolder.MainWindow().Show();
                                    Close();
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMessageBox(ex);
                }
            }
        }

        private void PasswordPb_KeyDown(object sender, KeyEventArgs e)
        {
            T2.Text = "";
        }

        private void LoginTb_KeyDown(object sender, KeyEventArgs e)
        {
            T1.Text = "";
        }

        private void AddAccount(object sender, MouseButtonEventArgs e)
        {
            new Registration().Show();
            Close();
        }

        private void Logo(object sender, MouseButtonEventArgs e)
        {
            new FolderWindow.MBFolder.Author().Show();
        }
    }
}
    

