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
    /// Логика взаимодействия для EditTestMainWindow.xaml
    /// </summary>
    public partial class EditTestMainWindow : Window
    {
        TestMain testMain = new TestMain();
        public EditTestMainWindow(int idMainTest)
        {
            InitializeComponent();
            testMain = DBEntities.GetContext().TestMain.FirstOrDefault
                                      (t => t.IdTestMain == idMainTest);
            LoadComboBox();
        }
        private void LoadComboBox()
        {
            StatusCb.ItemsSource = DBEntities.GetContext().Status.ToList();
            NameTb.Text = testMain.NameTest;
            DescriptionTb.Text = testMain.TestDescription;
            StatusCb.SelectedValue = testMain.IdStatus;
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
            if (MBClass.QuestionMB
               ("Хотите отменить изменения?"))
            {
                Close();
            } 
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.QuestionMB("Вы точно хотите сохранить изменения?");
            if (VariableClass.IsAccepted)
            {
                testMain.NameTest = NameTb.Text;
                testMain.TestDescription = DescriptionTb.Text;
                testMain.IdStatus = int.Parse(StatusCb.SelectedValue.ToString());
                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMessageBox("Данные обновлены");
                Close();
            }
        }

        private void Back(object sender, MouseButtonEventArgs e)
        {
            if (MBClass.QuestionMB
               ("Хотите отменить изменения?"))
            {
                Close();
            }
        }

        private void BTCreateATest_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.QuestionMB("Вы точно хотите сохранить изменения?");
            if (VariableClass.IsAccepted)
            {
                testMain.NameTest = NameTb.Text;
                testMain.TestDescription = DescriptionTb.Text;
                testMain.IdStatus = int.Parse(StatusCb.SelectedValue.ToString());
                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMessageBox("Данные обновлены");
                Close();
            }
        }

        private void Logo(object sender, MouseButtonEventArgs e)
        {
            new FolderWindow.MBFolder.Author().Show();
        }
    }
}
