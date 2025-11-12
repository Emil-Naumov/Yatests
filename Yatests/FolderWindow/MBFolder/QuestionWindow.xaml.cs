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

namespace Yatests.FolderWindow.MBFolder
{
    /// <summary>
    /// Логика взаимодействия для QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow : Window
    {
        public QuestionWindow(string question)
        {
            InitializeComponent();
            QuestionTb.Text = question;
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
            VariableClass.IsAccepted = false;
            Close();
        }

        private void YesWindow(object sender, MouseButtonEventArgs e)
        {
            VariableClass.IsAccepted = true;
            Close();
        }
    }
}
