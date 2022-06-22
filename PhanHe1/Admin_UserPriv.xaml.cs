using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data.SqlClient;
namespace QLTT_CSYT
{
    /// <summary>
    /// Interaction logic for Admin_UserPriv.xaml
    /// </summary>
    public partial class Admin_UserPriv : Window
    {
        DataTable tblUser;
        public Admin_UserPriv()
        {
            InitializeComponent();
        }
        private void Admin_UserPriv_Loading(object sender, EventArgs e)
        {
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql= "SELECT GRANTEE,PRIVILEGE,TABLE_NAME FROM USER_TAB_PRIVS";
            tblUser = Class.DB_Config.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvUserPriv.ItemsSource = tblUser.DefaultView; //Nguồn dữ liệu            
            //dgvUserPriv.Columns[0].Header = "1";
            //dgvUserPriv.Columns[1].Header = "2";
            //dgvUserPriv.Columns[2].Header = "TABLE_NAME";
            //dgvUser.Columns[3].Header = "Expiry Date";
            //dgvUser.Columns[4].Header = "Last Login";
            dgvUserPriv.AutoGenerateColumns = true;
        }
       
        private void dgvUserPriv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btn_User_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            sql = "SELECT u.GRANTEE,u.PRIVILEGE,u.TABLE_NAME,r.GRANTED_ROLE AS ROLE FROM USER_TAB_PRIVS u ,DBA_ROLE_PRIVS r  WHERE u.GRANTEE = r.GRANTEE AND 1=1";
            if (txtUser.Text =="")
            {
                MessageBox.Show("Nhập username !!!", "Yêu cầu ...", MessageBoxButton.OK);
                return;
            }
            if (txtUser.Text != "")
                sql = sql + " AND u.GRANTEE LIKE N'%" + txtUser.Text.ToUpper() + "%'";
            tblUser = Class.DB_Config.GetDataToTable(sql);
            dgvUserPriv.ItemsSource = tblUser.DefaultView;
            //if (tblUser.Rows.Count == 0)
            //{
            //    MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!", "Thông báo", MessageBoxButton.OK);
            //}
        }

        private void btn_Role_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            sql = "SELECT ROLE,PRIVILEGE,TABLE_NAME FROM ROLE_TAB_PRIVS  WHERE 1=1";
            if (txtRoleName.Text == "")
            {
                MessageBox.Show("Nhập tên role mong muốn!!!", "Yêu cầu ...", MessageBoxButton.OK);
                return;
            }   
            if (txtRoleName.Text != "")
                sql = sql + " AND ROLE LIKE N'%" + txtRoleName.Text + "%'";
            tblUser = Class.DB_Config.GetDataToTable(sql);
            dgvUserPriv.ItemsSource = tblUser.DefaultView;
            if (tblUser.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!", "Thông báo", MessageBoxButton.OK);
            }
          
        }
    }
}
