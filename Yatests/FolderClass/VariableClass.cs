using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatests.FolderWindow;

namespace Yatests.FolderClass
{
    class VariableClass
    {
        public static Registration Window { get; set; }
        public static string Surname { get; set; }
        public static string Name { get; set; }
        public static string Patronymic { get; set; }
        public static string LoginUser { get; set; }
        public static string PasswordUser { get; set; }
        public static int IdRole { get; set; }
        public static int IdStudent { get; set; }
        public static int IdStaff { get; set; }
        public static int IdAdmin { get; set; }
        public static int IdUser { get; set; }
        public static bool IsAccepted { get; set; } = false;

        public static void ClearData()
        {
            Surname = string.Empty;
            Name = string.Empty;
            Patronymic = string.Empty;
            LoginUser = string.Empty;
            PasswordUser = string.Empty;
            IdRole = 0;
        }
    }
}
