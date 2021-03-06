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
    /// Interaction logic for Admin_Dashboard.xaml
    /// </summary>
    public partial class Admin_Dashboard : Window
    {
        public Admin_Dashboard()
        {
            InitializeComponent();
        }

        private void btnPH2_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            for (int i = 0; i < Application.Current.Windows.Count - 1; i++)
                Application.Current.Windows[i].Close();
            menu.Show();
            Application.Current.Windows[0].Close();
        }

        private void btnAllUser_Click(object sender, RoutedEventArgs e)
        {
            Admin_Main mw = Application.Current.Windows[0] as Admin_Main;
            Admin_UserList admin_UserList = new Admin_UserList();
            mw.frBody.Content = admin_UserList.Content;
            mw.txtTitle.Text = "Quản lý user và role";
        }

        private void btnPriv_Click(object sender, RoutedEventArgs e)
        {
            Admin_Main mw = Application.Current.Windows[0] as Admin_Main;
            Admin_UserPriv admin_UserPriv = new Admin_UserPriv();
            mw.frBody.Content = admin_UserPriv.Content;
            mw.txtTitle.Text = "Quản lý quyền";
        }


        private void btnMaHoa_Click(object sender, RoutedEventArgs e)
        {
            Admin_Main mw = Application.Current.Windows[0] as Admin_Main;
            Admin_MaHoa admin_MaHoa = new Admin_MaHoa();
            mw.frBody.Content = admin_MaHoa.Content;
            mw.txtTitle.Text = "Mã hoá";
        }

        private void btnAudit_Click(object sender, RoutedEventArgs e)
        {
            Admin_Main mw = Application.Current.Windows[0] as Admin_Main;
            Admin_Audit admin_audit = new Admin_Audit();
            mw.frBody.Content = admin_audit.Content;
            mw.txtTitle.Text = "Audit";
        }

        private void btnObject_Click(object sender, RoutedEventArgs e)
        {
            Admin_Main mw = Application.Current.Windows[0] as Admin_Main;
            Admin_Object admin_ob = new Admin_Object();
            mw.frBody.Content = admin_ob.Content;
            mw.txtTitle.Text = "Danh sách đối tượng";
        }
    }
}
