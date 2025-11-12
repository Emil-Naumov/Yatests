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

namespace Yatests.FolderPage.QuestionFolder
{
    /// <summary>
    /// Логика взаимодействия для ListAnswerPage.xaml
    /// </summary>
    public partial class ListAnswerPage : Page
    {
        List<Question> questions = ListQuestionClass.GetQuestions();
        List<Answer> answers,
                     trueAnswer,
                     MaybeAnswer = new List<Answer>();
        Question questionLoad;
        Answer answerLoad;
        Result resultLoud = new Result();
        int Index = 0,
            PricePoint = 0,
            Count = 0;
        public ListAnswerPage()
        {
            InitializeComponent();
            LoadData();
        }
        private void SkipBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Index < questions.Count)
            {
                LoadData();
            }
            else
            {
                try
                {
                    resultLoud.IdTestMain = questions[0].IdTestMain;
                    resultLoud.DoP = DateTime.Now;
                    resultLoud.Result1 = PricePoint.ToString();
                    Staff staff = DBEntities.GetContext().Staff
                        .FirstOrDefault(s => s.IdUser == VariableClass.IdUser);
                    if (staff == null)
                    {
                        resultLoud.IdStaff = staff.IdStaff;
                        AddResult();
                        NavigationService.Navigate(new StaffPageFolder.TestListPage());
                    }
                    else
                    {
                        resultLoud.IdStaff = staff.IdStaff;
                        AddResult();
                        NavigationService.Navigate(new StaffPageFolder.TestListPage());
                    }
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMessageBox(ex);
                }
            }

        }

        private void TbListOfAnswer_KeyDown(object sender, KeyEventArgs e)
        {
            T1.Text = "";
        }

        private void LvTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Answer answer = LvTest.SelectedItem as Answer;
            if (answer != null)
                TbListOfAnswer.Text = answer.NumberAnswer.ToString();
        }

        private void LvTest_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BtnNex();
        }

        private void BtnNex()
        {
            if (string.IsNullOrWhiteSpace(TbListOfAnswer.Text))
            {
                MBClass.InfoMessageBox("Вы не ввели значение");
            }
            else
            {


                string textAnswer = TbListOfAnswer.Text.Trim();
                string[] MasAnswer = textAnswer.Split(',');
                string text;

                for (int i = 0; i < MasAnswer.Length; i++)
                {
                    text = MasAnswer[i];
                    answerLoad = DBEntities.GetContext().Answer
                        .FirstOrDefault(a => a.NumberAnswer.ToString()
                        .StartsWith(text) && a.IdQuestion == questionLoad.IdQuestion);
                    if (answerLoad != null)
                    {
                        MaybeAnswer.Add(answerLoad);
                    }
                }
                MaybeAnswer = MaybeAnswer.OrderBy(a => a.NumberAnswer).ToList();
                if (EqualityCheck(trueAnswer, MaybeAnswer))
                {
                    Count += 1;
                    PricePoint += questionLoad.Point;
                }
                if (Index < questions.Count)
                {
                    LoadData();
                }
                else
                {
                    try
                    {
                        resultLoud.IdTestMain = questions[0].IdTestMain;
                        resultLoud.DoP = DateTime.Now;
                        resultLoud.Result1 = PricePoint.ToString();
                        Staff staff = DBEntities.GetContext().Staff
                            .FirstOrDefault(s => s.IdUser == VariableClass.IdUser);
                        if (staff == null)
                        {
                            resultLoud.IdStaff = staff.IdStaff;
                            AddResult();
                            NavigationService.Navigate(new StaffPageFolder.TestListPage());
                        }
                        else
                        {
                            resultLoud.IdStaff = staff.IdStaff;
                            AddResult();
                            NavigationService.Navigate(new StaffPageFolder.TestListPage());
                        }
                    }
                    catch (Exception ex)
                    {
                        MBClass.ErrorMessageBox(ex);
                    }
                }
            }
        }
        private void LoadData()
        {
            answers = null;
            trueAnswer = null;
            MaybeAnswer = null;
            MaybeAnswer = new List<Answer>();
            questionLoad = questions[Index];
            TbContentQuestin.Text = questionLoad.ContentQuestion;
            TbListOfAnswer.Text = "";
            answers = DBEntities.GetContext().Answer
                .Where(q => q.IdQuestion == questionLoad.IdQuestion).ToList();
            LvTest.ItemsSource = answers;
            trueAnswer = answers.Where(a => a.IdIsTrueOrFalse == 2).ToList();
            Index++;
        }
        private bool EqualityCheck(List<Answer> listOne, List<Answer> listTwo)
        {
            if (listOne.Count == listTwo.Count)
            {
                for (int Index = 0; Index < listOne.Count; Index++)
                    if (listOne[Index] != listTwo[Index])
                    {
                        return false;
                    }
                return true;
            }
            return false;
        }
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            BtnNex();
        }

        private void AddResult()
        {
            DBEntities.GetContext().Result.Add(resultLoud);
            DBEntities.GetContext().SaveChanges();
            MBClass.InfoMessageBox($"Тест пройден на " +
                    $"{resultLoud.Result1} баллов; правильных " +
                    $"ответов {Count}.");
            ListQuestionClass.Clear();
        }
    }
}
