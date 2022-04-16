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
            txtTitle.Text = "Home";
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Environment.Exit(0);
        }


        private void menu(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;
            ListBoxItem lbi = lb.SelectedItem as ListBoxItem;
            TextBlock tb = (TextBlock)lbi.Content;
            string select = tb.Text;


            if (select == "HOME")
            {
                Admin_Dashboard admin_Dashboard = new Admin_Dashboard();
                frBody.Content = admin_Dashboard.Content;
                txtTitle.Text = "Home";
            }
            else if (select == "ALL USERS")
            {
                Admin_UserList admin_UserList = new Admin_UserList();
                frBody.Content = admin_UserList.Content;
                txtTitle.Text = "All users";
            }
            else if(select == "USER/ROLE PRIVILEGES")
            {
                Admin_UserPriv admin_UserPriv = new Admin_UserPriv();
                frBody.Content = admin_UserPriv.Content;
                txtTitle.Text = "User/Role Privileges";
            }
            else if (select == "GRANT/REVOKE PRIVILEGES")
            {
                Admin_GrantPrivxaml admin_GrantPrivxaml = new Admin_GrantPrivxaml();
                frBody.Content = admin_GrantPrivxaml.Content;
                txtTitle.Text = "Grant/Revoke Privileges";
            }
            else
            {
                Class.Functions.Disconnect();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }

            drawer.IsLeftDrawerOpen = false;

        }
    }
}
