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
    /// Логика взаимодействия для AddAnswerWindow.xaml
    /// </summary>
    public partial class AddAnswerWindow : Window
    {
        List<Answer> ListAnswers = new List<Answer>();
        Question questionLoad;
        IsTrueOrFalse isTrueOrFalse;
        int number;
        public AddAnswerWindow(Question question, List<Answer> answers)
        {
            InitializeComponent();
            ListAnswers = answers;
            questionLoad = question;
            LoadData();
        }
        private void LoadData()
        {
            CbTrueOfFalse.ItemsSource = DBEntities.GetContext().IsTrueOrFalse
                .ToList();
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
            isTrueOrFalse = CbTrueOfFalse.SelectedItem as IsTrueOrFalse;
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
                MBClass.ErrorMessageBox("Введите ответ");
                TbContentAnswer.Focus();
            }
            else if (isTrueOrFalse == null)
            {
                MBClass.ErrorMessageBox("Выберете правдивость ответа");
                TbContentAnswer.Focus();
            }
            else
            {
                AddAnswer();
            }
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
        private void AddAnswer()
        {
            Answer answer = new Answer()
            {
                NumberAnswer = int.Parse(TbNumberAnswer.Text),
                IdQuestion = questionLoad.IdQuestion,
                ContentAnswer = TbContentAnswer.Text,
                IdIsTrueOrFalse = isTrueOrFalse.IdIsTrueOrFalse,
            };
            DBEntities.GetContext().Answer.Add(answer);
            DBEntities.GetContext().SaveChanges();
            MBClass.InfoMessageBox("Ответ успешно добавлен");
            Close();
        }

        private void Back(object sender, MouseButtonEventArgs e)
        {
            if (MBClass.QuestionMB
               ("Вы передумали создавать ответ?"))
            {
                Close();
            }
        }

        private void X(object sender, MouseButtonEventArgs e)
        {
            if (MBClass.QuestionMB
               ("Вы передумали создавать ответ?"))
            {
                Close();
            }
        }

        private void TbNumberAnswer_KeyDown(object sender, KeyEventArgs e)
        {
            T1.Text = "";
        }

        private void TbContentAnswer_KeyDown(object sender, KeyEventArgs e)
        {
            T2.Text = "";
        }

        private void T3_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void CbTrueOfFalse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            T3.Text = "";
        }

        private void Logo(object sender, MouseButtonEventArgs e)
        {
            new FolderWindow.MBFolder.Author().Show();
        }
    }
}
