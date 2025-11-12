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

namespace Yatests.FolderPage.AdminPageFolder.QuestionPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListQuestionPage.xaml
    /// </summary>
    public partial class ListQuestionPage : Page
    {

        TestMain testMain;
        List<Question> questions = new List<Question>();
        Question questionLoad;
        ListView listView = new ListView();
        int result;
        List<Answer> ListOfAnswer = new List<Answer>();

        public ListQuestionPage(TestMain testMain1)
        {
            InitializeComponent();
            testMain = testMain1;
            listView = LvQuestionInTest;
            CbStatus.ItemsSource = DBEntities.GetContext().Visiability
                .ToList();
            LblNameTest.Content = testMain.NameTest;
            LoadData();
        }

        private void LoadData()
        {
            questions = DBEntities.GetContext().Question
                .Where(q => q.IdTestMain == testMain.IdTestMain).ToList();
            LvQuestionInTest.ItemsSource = questions;
        }

        private void LoadForOptionQuestionInBrdMenu()
        {
            BrdMenu.Visibility = Visibility.Visible;
            TbChangeContentQuestin.Text = questionLoad.ContentQuestion;
            TbChangePoint.Text = questionLoad.Point.ToString();
            CbStatus.SelectedValue = questionLoad.IdVisiability;
            LoadDgAnswer();
        }
        private void AddQuestion()
        {
            Question question = new Question();
            question.IdTestMain = testMain.IdTestMain;
            question.ContentQuestion = "Вопрос?";
            question.Point = 1;
            question.IdVisiability = 1;
            DBEntities.GetContext().Question.Add(question);
            DBEntities.GetContext().SaveChanges();
            questionLoad = question;
        }
        private void LoadDgAnswer()
        {
            ListOfAnswer = DBEntities.GetContext().Answer
                .Where(a => a.IdQuestion == questionLoad.IdQuestion)
                .OrderBy(c => c.NumberAnswer).ToList();
            DgAnswer.ItemsSource = ListOfAnswer;
        }
        private void LoadQuestion()
        {
            int indexQuestion = questions.IndexOf(questionLoad);
            listView.SelectedItem = listView.Items[indexQuestion];

            listView.UpdateLayout();

            ((ListViewItem)listView.ItemContainerGenerator
                .ContainerFromIndex(indexQuestion)).Focus();

        }
        private void BtnSaveChangeQuestion_Click(object sender, RoutedEventArgs e)
        {
            Visiability visiability = CbStatus.SelectedItem as Visiability;
            if (string.IsNullOrEmpty(TbChangeContentQuestin.Text))
            {
                MBClass.ErrorMessageBox("Введите вопрос.");
            }
            else if (string.IsNullOrEmpty(TbChangePoint.Text))
            {
                MBClass.ErrorMessageBox("Введите баллы.");
            }
            else if (!int.TryParse(TbChangePoint.Text, out result))
            {
                MBClass.ErrorMessageBox("В баллы можно писать только числа.");
            }
            else if (visiability == null)
            {
                MBClass.ErrorMessageBox("Выберете статус вопроса.");
            }
            else
            {
                questionLoad.ContentQuestion = TbChangeContentQuestin.Text;
                questionLoad.Point = result;
                questionLoad.IdVisiability = visiability.IdVisiability;
                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMessageBox("Изменение вопроса сохранены");
                LoadData();
            }
        }
        private void BtnCanselQuestion_Click(object sender, RoutedEventArgs e)
        {
            TbChangeContentQuestin.Text = questionLoad.ContentQuestion;
            TbChangePoint.Text = questionLoad.Point.ToString();
            CbStatus.SelectedValue = questionLoad.IdVisiability;
        }
        private void BtnRemoveQuestion_Click(object sender, RoutedEventArgs e)
        {
            int countAnswer = DBEntities.GetContext().Answer
                .Where(a => a.IdQuestion == questionLoad.IdQuestion)
                .ToList().Count;
            if (countAnswer == 0)
            {
                DBEntities.GetContext().Question.Remove(questionLoad);
                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMessageBox("Вопрос успешно удалён");
                LoadData();
                BrdMenu.Visibility = Visibility.Collapsed;
            }
            else
            {
                MBClass.ErrorMessageBox("Ошибка удаления: у вопроса есть ответы");
            }
        }
        private void BtnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            BrdMenu.Visibility = Visibility.Collapsed;
        }
        private void LvQuestionInTest_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LvQuestionInTest.SelectedItem as Question == null)
            {
                MBClass.ErrorMessageBox("Выберете вопрос");
            }
            else
            {
                questionLoad = LvQuestionInTest.SelectedItem as Question;
                LoadForOptionQuestionInBrdMenu();
            }
        }
        private void AddAnswer_Click(object sender, RoutedEventArgs e)
        {
            new FolderWindow.AdminWindowFolder.QuestionFolder.AnswerFolder.AddAnswerWindow(questionLoad, ListOfAnswer).ShowDialog();
            LoadDgAnswer();
        }
        private void BtnChangeAnswer_Click(object sender, RoutedEventArgs e)
        {
            int IdAnswer = int.Parse((sender as Button)
                    .ToolTip.ToString());
            Answer answer = DBEntities.GetContext().Answer
                .FirstOrDefault(a => a.IdAnswer == IdAnswer);
            ListOfAnswer = ListOfAnswer.Where(l => l.IdAnswer != IdAnswer)
                .ToList();
            new FolderWindow.AdminWindowFolder.QuestionFolder.AnswerFolder
                .ChangeAnswerWindow(answer, ListOfAnswer).ShowDialog();
            LoadDgAnswer();
        }
        private void BtnRemoveAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (MBClass.QuestionMB("Вы точно хотите удалит ответ?"))
            {
                int IdAnswer = int.Parse((sender as Button)
                    .ToolTip.ToString());
                Answer answer = DBEntities.GetContext().Answer
                    .FirstOrDefault(a => a.IdAnswer == IdAnswer);
                DBEntities.GetContext().Answer.Remove(answer);
                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMessageBox("Ответ успешно удалён");
                LoadDgAnswer();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddQuestion();
            questions.Add(questionLoad);
            LoadData();
            LoadQuestion();
            LoadForOptionQuestionInBrdMenu();
        }
    }
}
