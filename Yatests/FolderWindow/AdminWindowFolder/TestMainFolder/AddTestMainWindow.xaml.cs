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

namespace Yatests.FolderWindow.AdminWindowFolder.TestMainFolder
{
    /// <summary>
    /// Логика взаимодействия для AddTestMainWindow.xaml
    /// </summary>
    public partial class AddTestMainWindow : Window
    {
       
        public AddTestMainWindow()
        {
            InitializeComponent();
            LoadComboBox();
        }
        private void LoadComboBox()
        {
            StatusCb.ItemsSource = DBEntities.GetContext().Status.ToList();
        }
        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            if (MBClass.QuestionMB
               ("Вы передумали создавать тест?"))
            {
                Close();
            }
        }

        private void CreateATest(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTb.Text))
            {
                MBClass.ErrorMessageBox("Вы не ввели название теста!");
                NameTb.Focus();
            }
            else if (StatusCb.SelectedItem == null)
            {
                MBClass.ErrorMessageBox("Вы не выбрали статус!");
                StatusCb.Focus();
            }
            else
            {
                AddTestMain();
                MBClass.InfoMessageBox("Оглавление теста добавлено");
                Close();
            }
        }
        private void AddTestMain()
        {
            try
            {
                var testMain = new TestMain()
                {
                    NameTest = NameTb.Text,
                    IdStatus = int.Parse(StatusCb.SelectedValue.ToString()),
                    TestDescription = DescriptionTb.Text,
                };
                DBEntities.GetContext().TestMain.Add(testMain);
                DBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMessageBox(ex);
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

        private void StatusCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            T3.Text = "";
        }

        private void DescriptionTb_KeyDown(object sender, KeyEventArgs e)
        {
            T2.Text = "";
        }

        private void NameTb_KeyDown(object sender, KeyEventArgs e)
        {
            T1.Text = "";
        }

        private void Back(object sender, MouseButtonEventArgs e)
        {
            if (MBClass.QuestionMB
               ("Вы передумали создавать тест?"))
            {
                Close();
            }
        }

        private void Logo(object sender, MouseButtonEventArgs e)
        {
            new FolderWindow.MBFolder.Author().Show();
        }
    }
}
