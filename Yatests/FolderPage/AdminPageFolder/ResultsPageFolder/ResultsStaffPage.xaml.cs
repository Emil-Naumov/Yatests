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

namespace Yatests.FolderPage.AdminPageFolder.ResultsPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ResultsStaffPage.xaml
    /// </summary>
    public partial class ResultsStaffPage : Page
    {
        int currentPage = 1, countInPage = 10, maxPage;
        public ResultsStaffPage()
        {
            InitializeComponent();
            RefreshData();
        }
        private void RefreshData()
        {
            var staffResults = DBEntities.GetContext().Result.ToList();
            maxPage = (int)Math.Ceiling(staffResults.Count * 1.0 / countInPage);
            staffResults = staffResults.Skip((currentPage - 1) * countInPage).
                Take(countInPage).ToList();
            LvResultStaff.ItemsSource = staffResults;
            lblNumber.Content = $"{currentPage}/{maxPage}";
            ManageButtonEnable();
        }
        private void TbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            T1.Text = "";
        }
        private void ManageButtonEnable()
        {
            PreviousPageBtn.IsEnabled = true;
            NextPageBtn.IsEnabled = true;
            if (currentPage == 1)
                PreviousPageBtn.IsEnabled = false;
            if (currentPage == maxPage)
                NextPageBtn.IsEnabled = false;
        }
        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LvResultStaff.ItemsSource = DBEntities.GetContext().Result.Where
                (r => r.IdStaff == null && r.Staff.Surname.StartsWith
                (TbSearch.Text) || r.TestMain.NameTest.StartsWith
                (TbSearch.Text)).ToList();
            if (string.IsNullOrWhiteSpace(TbSearch.Text))
            {
                RefreshData();
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
    }
}
