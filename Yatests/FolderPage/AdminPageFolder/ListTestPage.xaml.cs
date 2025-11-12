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
using Yatests.FolderPage.AdminPageFolder.QuestionPageFolder;

namespace Yatests.FolderPage.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListTestPage.xaml
    /// </summary>
    public partial class ListTestPage : Page
    {
        private int currentPage = 1;
        private int countInPage = 7;
        private int maxPage;
        TestMain testMain;
        public ListTestPage()
        {
            InitializeComponent();
            RefreshData();
        }
        private void RefreshData()
        {
            List<TestMain> testMain =
                DBEntities.GetContext().TestMain.ToList();
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

        private void BtnAddTest_Click(object sender, RoutedEventArgs e)
        {
            new FolderWindow.AdminWindowFolder.TestMainFolder.AddTestMainWindow().ShowDialog();
            RefreshData();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LvTest.ItemsSource =
                DBEntities.GetContext().TestMain.Where(c => c.NameTest.StartsWith
                (TbSearch.Text)).ToList();
        }
        private void RemoveTestMain(TestMain testMain)
        {
            DBEntities.GetContext().TestMain.Remove(testMain);
            DBEntities.GetContext().SaveChanges();
        }
        private void RemoveQuestion(Question question)
        {
            DBEntities.GetContext().Question.Remove(question);
            DBEntities.GetContext().SaveChanges();
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            LvTest.ItemsSource =
            DBEntities.GetContext().TestMain.Where(c => c.NameTest.StartsWith
              (TbSearch.Text)).ToList();
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            TestMain testMain = LvTest.SelectedItem as TestMain;
            if (testMain == null)
            {
                MBClass.ErrorMessageBox("Выберите тест");
            }
            else
            {
                new FolderWindow.AdminWindowFolder.TestMainFolder.EditTestMainWindow(testMain.IdTestMain).ShowDialog();
                RefreshData();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            testMain = LvTest.SelectedItem as TestMain;
            if (testMain == null)
            {
                MBClass.ErrorMessageBox("Выберите тест, нажав по нему");
            }
            else
            {
                if (MBClass.QuestionMB("Хотите удалить тест?"))
                {
                    var questions = DBEntities.GetContext().Question.Where
                        (q => q.IdTestMain == testMain.IdTestMain).ToList();
                    var results = DBEntities.GetContext().Result.Where
                        (r => r.IdTestMain == testMain.IdTestMain).ToList();
                    foreach (var result in results)
                    {
                        DBEntities.GetContext().Result.Remove(result);
                        DBEntities.GetContext().SaveChanges();
                    }
                    foreach (var question in questions)
                    {
                        var answers = DBEntities.GetContext().Answer.Where
                            (a => a.IdQuestion == question.IdQuestion).ToList();

                        foreach (var answer in answers)
                        {
                            DBEntities.GetContext().Answer.Remove(answer);
                            DBEntities.GetContext().SaveChanges();
                        }
                        RemoveQuestion(question);
                    }
                    RemoveTestMain(testMain);

                    MBClass.InfoMessageBox("Тест удален");
                    RefreshData();
                    if (LvTest.Items.Count <= 0)
                    {
                        if (currentPage > 0)
                        {
                            currentPage--;
                            RefreshData();
                        }
                    }

                }
            }
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

        private void TbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            T1.Text = "";
        }

        private void GoTestBtn(object sender, RoutedEventArgs e)
        {
            int id = int.Parse((sender as Button).ToolTip.ToString());
            TestMain testMain = DBEntities.GetContext().TestMain
                .FirstOrDefault(c => c.IdTestMain == id);
            NavigationService.Navigate(new QuestionPageFolder
                  .ListQuestionPage(testMain));
        }
    }
}
