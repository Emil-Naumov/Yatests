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

namespace Yatests.FolderPage.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListStaffPage.xaml
    /// </summary>
    public partial class ListStaffPage : Page
    {
        public ListStaffPage()
        {
            InitializeComponent();
            LoadEntity();
        }
        private void LoadEntity()
        {
            DgListStaff.ItemsSource =
                DBEntities.GetContext().Staff.ToList();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DgListStaff.ItemsSource = DBEntities.GetContext().Staff
                .Where(s => s.Surname.StartsWith(TbSearch.Text)).ToList();
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            Staff staff = DgListStaff.SelectedItem as Staff;
            if (staff == null)
            {
                MBClass.ErrorMessageBox("Выберите сотрудника!");
            }
            else
            {
                if (MBClass.QuestionMB("Хотите удалить сотрудника?"))
                {
                    var results = DBEntities.GetContext().Result.Where
                        (r => r.IdStaff == staff.IdStaff).ToList();
                    var user = DBEntities.GetContext().User.FirstOrDefault
                        (u => u.IdUser == staff.IdUser);
                    foreach (var result in results)
                    {
                        DBEntities.GetContext().Result.Remove(result);
                        DBEntities.GetContext().SaveChanges();
                    }
                    DBEntities.GetContext().Staff.Remove(staff);
                    DBEntities.GetContext().User.Remove(user);
                    DBEntities.GetContext().SaveChanges();
                    LoadEntity();
                    MBClass.InfoMessageBox("Сотрудник удалён");
                }
            }
        }

        private void TbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            T1.Text = "";
        }
    }
}
