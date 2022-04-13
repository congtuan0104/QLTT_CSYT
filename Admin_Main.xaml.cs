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
using System.Windows.Shapes;

namespace QLTT_CSYT
{
    /// <summary>
    /// Interaction logic for Admin_Main.xaml
    /// </summary>
    public partial class Admin_Main : Window
    {
        public Admin_Main()
        {
            InitializeComponent();
            Admin_Dashboard admin_Dashboard = new Admin_Dashboard();
            frBody.Content = admin_Dashboard.Content;
            txtTitle.Text = "Dashboard";
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            
            Class.Functions.Disconnect();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Environment.Exit(0);
        }

        private void btnUserList_Click(object sender, RoutedEventArgs e)
        {
            Admin_UserList admin_UserList = new Admin_UserList();
            frBody.Content = admin_UserList.Content;
            txtTitle.Text = "User List";
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            Admin_Dashboard admin_Dashboard = new Admin_Dashboard();
            frBody.Content = admin_Dashboard.Content;
            txtTitle.Text = "Dashboard";
        }
    }
}
