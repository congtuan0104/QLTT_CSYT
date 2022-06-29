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
            mw.txtTitle.Text = "Quản lý user";
        }

        private void btnPriv_Click(object sender, RoutedEventArgs e)
        {
            Admin_Main mw = Application.Current.Windows[0] as Admin_Main;
            Admin_UserPriv admin_UserPriv = new Admin_UserPriv();
            mw.frBody.Content = admin_UserPriv.Content;
            mw.txtTitle.Text = "Xem quyền chủ thể";
        }

        private void btnGrant_Click(object sender, RoutedEventArgs e)
        {
            Admin_Main mw = Application.Current.Windows[0] as Admin_Main;
            Admin_GrantPrivxaml admin_GrantPriv = new Admin_GrantPrivxaml();
            mw.frBody.Content = admin_GrantPriv.Content;
            mw.txtTitle.Text = "Gán/Thu hồi quyền";
        }

        private void btnMaHoa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAudit_Click(object sender, RoutedEventArgs e)
        {
            Admin_Main mw = Application.Current.Windows[0] as Admin_Main;
            Admin_Audit admin_audit = new Admin_Audit();
            mw.frBody.Content = admin_audit.Content;
            mw.txtTitle.Text = "Audit";
        }
    }
}
