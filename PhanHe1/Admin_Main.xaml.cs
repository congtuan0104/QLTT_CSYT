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
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        public Admin_Main()
        {
            InitializeComponent();
            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();

            Admin_Dashboard admin_Dashboard = new Admin_Dashboard();
            frBody.Content = admin_Dashboard.Content;
            txtTitle.Text = "Trang chủ";
        }

        private void Timer_Click(object sender, EventArgs e)
        {
            DateTime d;
            d = DateTime.Now;
            txtTime.Text = d.ToString("dd") + "-" + d.ToString("MM") + "-" + d.ToString("yyyy")
                + "         |         " + d.ToString("hh") + ":" 
                + d.ToString("mm") + ":" + d.ToString("ss") + " " + d.ToString("tt");
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


            if (select == "Trang chủ")
            {
                Admin_Dashboard admin_Dashboard = new Admin_Dashboard();
                frBody.Content = admin_Dashboard.Content;
                txtTitle.Text = "Trang chủ";
            }
            else if (select == "Danh sách đối tượng")
            {
                Admin_Object admin_ob = new Admin_Object();
                frBody.Content = admin_ob.Content;
                txtTitle.Text = "Danh sách đối tượng";
            }
            else if (select == "Quản lý user và role")
            {
                Admin_UserList admin_UserList = new Admin_UserList();
                frBody.Content = admin_UserList.Content;
                txtTitle.Text = "Quản lý user và role";
            }
            else if(select == "Xem quyền chủ thể")
            {
                Admin_UserPriv admin_UserPriv = new Admin_UserPriv();
                frBody.Content = admin_UserPriv.Content;
                txtTitle.Text = "Xem quyền chủ thể";
            }
            else if (select == "Gán/Thu hồi quyền")
            {
                Admin_GrantPrivxaml admin_GrantPrivxaml = new Admin_GrantPrivxaml();
                frBody.Content = admin_GrantPrivxaml.Content;
                txtTitle.Text = "Gán/Thu hồi quyền";
            }
            else if (select == "Mã hoá")
            {

            }
            else if (select == "Audit")
            {
                Admin_Audit admin_Audit = new Admin_Audit();
                frBody.Content = admin_Audit.Content;
                txtTitle.Text = "Audit";
            }
            else if (select == "Đăng xuất")
            {
                Class.DB_Config.Disconnect();
                Login mainWindow = new Login();
                mainWindow.Show();
                this.Close();
            }

            drawer.IsLeftDrawerOpen = false;

        }

    }
}
