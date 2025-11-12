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
    /// Логика взаимодействия для ResultsPage.xaml
    /// </summary>
    public partial class ResultsPage : Page
    {
        int currentPage = 1, countInPage = 6, maxPage;
        public ResultsPage()
        {
            InitializeComponent();
            RefreshData();
        }
        private void RefreshData()
        {
            var results = DBEntities.GetContext().Result.OrderBy
                (r => r.DoP).Where
                (r => r.IdStaff == VariableClass.IdStaff)
                .OrderByDescending(r => r.DoP).ToList();
            maxPage = (int)Math.Ceiling(results.Count * 1.0 / countInPage);
            results = results.Skip((currentPage - 1) * countInPage).Take
                (countInPage).ToList();
            LvResult.ItemsSource = results;
            lblNumber.Content = $"{currentPage}/{maxPage}";
            ButtonManageEnable();
        }
        private void ButtonManageEnable()
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
