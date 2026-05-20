using System;
using System.Windows.Forms;
using Quan_ly_Homestay.GUI;

namespace Quan_ly_Homestay
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin());
        }
    }
}
