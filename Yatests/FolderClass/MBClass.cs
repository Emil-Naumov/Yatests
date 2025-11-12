using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Yatests.FolderClass
{
    internal class MBClass
    {
        public static void ErrorMessageBox(string error)
        {
            new FolderWindow.MBFolder.ErrorWindow(error).ShowDialog();
        }
        public static void ErrorMessageBox(Exception ex)
        {
            new FolderWindow.MBFolder.ErrorWindow(ex.Message).ShowDialog();
        }
        public static void InfoMessageBox(string text)
        {
            new FolderWindow.MBFolder.InfoWindow(text).ShowDialog();
        }
        public static bool QuestionMB(string question)
        {
            new FolderWindow.MBFolder.QuestionWindow(question).ShowDialog();
            return VariableClass.IsAccepted;
        }
        public static void ExitMB()
        {
            if (QuestionMB("Вы действительно хотите выйти?"))
            {
                Application.Current.Shutdown();
            }
        }
    }
}
