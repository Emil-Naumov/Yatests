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

namespace Yatests.FolderPage.StaffPageFolder
{
    /// <summary>
    /// Логика взаимодействия для TestListPage.xaml
    /// </summary>
    public partial class TestListPage : Page
    {
        int currentPage = 1, countInPage = 7, maxPage;
        public TestListPage()
        {
            InitializeComponent();
            RefreshData();
        }
        private void RefreshData()
        {
            List<TestMain> testMain =
                DBEntities.GetContext().TestMain.Where(s => s.IdStatus == 2).ToList();
            maxPage = (int)Math.Ceiling(testMain.Count * 1.0 / countInPage);
            testMain = testMain.Skip((currentPage - 1) * countInPage).Take(countInPage).ToList();
            LvTest.ItemsSource = testMain;
            lblNumber.Content = $"{currentPage}/{maxPage}";
            ManageButtonEnable();
        }
        private void ManageButtonEnable()
        {
            PreviousPageBtn.IsEnabled = true;
            NextPageBtn.IsEnabled = true;

            if (currentPage == 1)
            {
                PreviousPageBtn.IsEnabled = false;
            }
            if (currentPage == maxPage)
            {
                NextPageBtn.IsEnabled = false;
            }
        }
        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            LvTest.ItemsSource = DBEntities.GetContext().TestMain.Where
                (c => c.NameTest.StartsWith(SearchTb.Text)).ToList();
        }
        private void ChoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            int Id = int.Parse((sender as Button).ToolTip.ToString());
            TestMain testMain = DBEntities.GetContext().TestMain
                .FirstOrDefault(t => t.IdTestMain == Id);

            ListQuestionClass.Clear();

            List<Question> questions = DBEntities.GetContext().Question
                .Where(q => q.IdTestMain == testMain.IdTestMain
                && q.IdVisiability == 2).ToList();

            ListQuestionClass.SetQuestions(questions);

            if (questions.Count > 0)
            {
                NavigationService.Navigate(new QuestionFolder.ListAnswerPage());
            }
            else
            {
                MBClass.ErrorMessageBox("В тест отсутствуют вопросы");
            }
        }

        private void SearchTb_KeyDown(object sender, KeyEventArgs e)
        {
            T1.Text = "";
        }

        private void PreviousPageBtn_Click(object sender, RoutedEventArgs e)
        {
            currentPage--;
            RefreshData();
        }

        private void NextPageBtn_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
            RefreshData();
        }
    }
}
