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
using Yatests.FolderPage.StaffPageFolder;
using Yatests.WindowsFolder;

namespace Yatests.FolderWindow.StaffWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new TestListPage();
        }

        private void Exit(object sender, MouseButtonEventArgs e)
        {
            if (MBClass.QuestionMB
               ("Хотите выйти из аккаунта?"))
            {
                new Authorization().Show();
                Close();
            }
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            if (MBClass.QuestionMB
               ("Хотите выйти из программы?"))
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
        private void ButtonLostFocus(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            button.FontSize = 17;
            button.FontWeight = FontWeights.SemiBold;
        }

        private void BtnListOfTests_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new TestListPage();
        }

        private void BtnResult_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ResultsPage();
        }

        private void Question(object sender, MouseButtonEventArgs e)
        {
            new MBFolder.Staff().Show();
        }

        private void Logo(object sender, MouseButtonEventArgs e)
        {
            new FolderWindow.MBFolder.Author().Show();
        }
    }
}
