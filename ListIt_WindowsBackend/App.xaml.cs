using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ListIt_WindowsBackend
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private List<Window> windows = new List<Window>();
        public void Logout(object sender)
        {
            var window = new AdminLogin();
            windows.Add(window);
            window.Show();
            Close(sender);
        }

        public void Back_To_Menu(object sender)
        {
            var window = new MenuWindow();
            window.Show();
            Close(sender);
        }

        private void Close(object sender)
        {
            foreach (var w in windows)
                if ((Window)sender == w)
                {
                    w.Close();
                    windows.Remove(w);
                    break;
                }
        }
    }   
}
