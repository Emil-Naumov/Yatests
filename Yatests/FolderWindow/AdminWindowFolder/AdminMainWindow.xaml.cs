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

namespace Yatests.FolderWindow.AdminWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new FolderPage.AdminPageFolder.ListTestPage();
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            if (MBClass.QuestionMB
            ("Хотите выйти из программы?"))
            {
                Close();
            }
        }

        private void Exit(object sender, MouseButtonEventArgs e)
        {
            if (MBClass.QuestionMB
            ("Хотите выйти с аккаунта?"))
            {
                new Authorization().Show();
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
        private void ButtonLostFocus(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            button.FontSize = 17;
            button.FontWeight = FontWeights.SemiBold;
        }
        private void BtnListOfStaff_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new FolderPage.AdminPageFolder.ListStaffPage();
        }
        private void BtnResult_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new FolderPage.AdminPageFolder.ResultsPageFolder.ResultsStaffPage();
        }

        private void BtnListOfTests_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new FolderPage.AdminPageFolder.ListTestPage();
        }

        private void Question(object sender, MouseButtonEventArgs e)
        {
            new MBFolder.Admin().Show();
        }

        private void Logo(object sender, MouseButtonEventArgs e)
        {
            new MBFolder.Author().Show();
        }
    }
}
