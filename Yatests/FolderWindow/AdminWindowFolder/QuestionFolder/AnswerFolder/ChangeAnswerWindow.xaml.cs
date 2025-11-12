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

namespace Yatests.FolderWindow.AdminWindowFolder.QuestionFolder.AnswerFolder
{
    /// <summary>
    /// Логика взаимодействия для ChangeAnswerWindow.xaml
    /// </summary>
    public partial class ChangeAnswerWindow : Window
    {
        List<Answer> ListAnswers = new List<Answer>();
        Answer answerLoad;
        int number;
        public ChangeAnswerWindow(Answer answer, List<Answer> answers)
        {
            InitializeComponent();
            answerLoad = answer;
            ListAnswers = answers;
            LoadData();
            //DataContext = answerLoad;
        }
        private void X(object sender, MouseButtonEventArgs e)
        {
            if (MBClass.QuestionMB
               ("Вы передумали редактировать ответ?"))
            {
                Close();
            }
        }
        private void LoadData()
        {
            CbTrueOfFalse.ItemsSource = DBEntities.GetContext().IsTrueOrFalse
                .ToList();
            TbNumberAnswer.Text = answerLoad.NumberAnswer.ToString();
            TbContentAnswer.Text = answerLoad.ContentAnswer;
            CbTrueOfFalse.SelectedItem = answerLoad.IdIsTrueOrFalse.ToString();
        }
        private bool EquilNumber(int A)
        {
            if (ListAnswers.Count != 0)
            {
                for (int i = 0; i < ListAnswers.Count; i++)
                {
                    if (ListAnswers[i].NumberAnswer == A)
                    {
                        return true;
                    }
                }
                return false;
            }
            return false;
        }
        private void Enter(object sender, MouseButtonEventArgs e)
        {
            if (MBClass.QuestionMB("Сохранить изменения?"))
            {
                IsTrueOrFalse isTrueOrFalse =
                    CbTrueOfFalse.SelectedItem as IsTrueOrFalse;
                if (string.IsNullOrEmpty(TbNumberAnswer.Text))
                {
                    MBClass.ErrorMessageBox("Введите номер ответа.");
                    TbNumberAnswer.Focus();
                }
                else if (!int.TryParse(TbNumberAnswer.Text, out number))
                {
                    MBClass.ErrorMessageBox
                        ("Номером ответа может быть только цифра.");
                    TbNumberAnswer.Clear();
                    TbNumberAnswer.Focus();
                }
                else if (EquilNumber(number))
                {
                    MBClass.ErrorMessageBox("У каждого ответа свой уникальный номер");
                }
                else if (string.IsNullOrEmpty(TbContentAnswer.Text))
                {
                    MBClass.ErrorMessageBox("Введите ответ.");
                    TbContentAnswer.Focus();
                }
                else if (isTrueOrFalse == null)
                {
                    MBClass.ErrorMessageBox("Выберете правдивость ответа.");
                    TbContentAnswer.Focus();
                }
                else
                {
                    answerLoad.NumberAnswer = int.Parse(TbNumberAnswer.Text);
                    answerLoad.ContentAnswer = TbContentAnswer.Text;
                    answerLoad.IdIsTrueOrFalse = isTrueOrFalse.IdIsTrueOrFalse;
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InfoMessageBox("Ответ успешно изменён.");
                    Close();
                }
            }
        }

        private void Back(object sender, MouseButtonEventArgs e)
        {
            if (MBClass.QuestionMB
               ("Вы передумали редактировать ответ?"))
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

        private void CbTrueOfFalse_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void CbTrueOfFalse_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void Logo(object sender, MouseButtonEventArgs e)
        {
            new FolderWindow.MBFolder.Author().Show();
        }
    }
}
