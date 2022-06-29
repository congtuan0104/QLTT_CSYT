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
            LoadDataGridView();
            GetAllObject();
            GetAllRole();
        }

        private void GetAllRole()
        {
            string sql = "SELECT GRANTED_ROLE AS ROLE FROM USER_ROLE_PRIVS WHERE GRANTED_ROLE != 'RESOURCE'";
            DataTable dt = new DataTable();
            dt = Class.DB_Config.GetDataToTable(sql);
            cbRole.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbRole.Items.Add(dt.Rows[i]["ROLE"]);
            }
        }

        private void GetAllObject()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT OBJECT_NAME" +
                " FROM USER_OBJECTS " +
                "WHERE OBJECT_TYPE != 'SEQUENCE' AND " +
                "OBJECT_TYPE != 'INDEX' AND " +
                "OBJECT_TYPE != 'TRIGGER'";
            dt = Class.DB_Config.GetDataToTable(sql);
            cbTable.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbTable.Items.Add(dt.Rows[i]["OBJECT_NAME"]);
            }
        }

        private void LoadDataGridView()
        {
            string sql;
            sql= "SELECT GRANTEE,PRIVILEGE,TABLE_NAME FROM USER_TAB_PRIVS";
            tblUser = Class.DB_Config.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvUserPriv.ItemsSource = null;
            dgvUserPriv.ItemsSource = tblUser.DefaultView; //Nguồn dữ liệu            

        }

        private void btn_User_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            if (txtUser.Text =="" || txtUser.Text == null)
            {
                MessageBox.Show("Nhập user hoặc role !!!", "Yêu cầu ...", MessageBoxButton.OK);
                return;
            }


            if(rd1.IsChecked == false)
            {
                sql = "SELECT GRANTEE,PRIVILEGE,TABLE_NAME AS OBJECT_NAME, " +
                "GRANTOR, TYPE FROM USER_TAB_PRIVS" +
                " WHERE GRANTEE ='" + txtUser.Text.ToUpper() + "'";
            }
            else
            {
                sql = "SELECT DISTINCT GRANTEE, GRANTED_ROLE " +
                    "FROM DBA_ROLE_PRIVS WHERE GRANTEE = '" + txtUser.Text.ToUpper() + "'";
            }

            tblUser = Class.DB_Config.GetDataToTable(sql);
            dgvUserPriv.ItemsSource = null;
            dgvUserPriv.ItemsSource = tblUser.DefaultView;

        }

       

        private void btn_GrantPriv_Click(object sender, RoutedEventArgs e)
        {
            if (cbRole.IsEnabled == false)
            {
                string priv = cbPriv.Text;
                string tab = cbTable.Text;
                string grantee = tbGrantee.Text.ToUpper();
                string sql;

                if (priv == "Xem (Select)")
                    priv = "SELECT";
                else if (priv == "Xoá (Delete)")
                    priv = "DELETE";
                else if (priv == "Thêm (Insert)")
                    priv = "INSERT";
                else if (priv == "Sửa (Update)")
                    priv = "UPDATE";
                else if (priv == "Thực thi (Excute)")
                    priv = "EXCUTE";

                //sql = "alter session set \"_oracle_script\"=true";
                //Class.DB_Config.RunSqlDel(sql);

                if (((grantee[0] >= 'a') && (grantee[0] <= 'z')) || ((grantee[0] >= 'A') && (grantee[0] <= 'Z')))
                {
                    sql = "GRANT " + priv + " ON " + tab + " TO " + grantee;
                }
                else
                {
                    sql = "GRANT " + priv + " ON " + tab + " TO '" + grantee + "'";
                }

                
                Class.DB_Config.RunSqlDel(sql);

            }
            else
            {
                string role = cbRole.Text;
                string grantee = tbGrantee.Text.ToUpper();
                string sql;


                //sql = "alter session set \"_oracle_script\"=true";
                //Class.DB_Config.RunSqlDel(sql);

                if (((grantee[0] >= 'a') && (grantee[0] <= 'z')) || ((grantee[0] >= 'A') && (grantee[0] <= 'Z')))
                {
                    sql = "GRANT " + role + " TO " + grantee;
                }
                else
                {
                    sql = "GRANT " + role + " TO '" + grantee + "'";
                }
                if (cbAdminOption.IsChecked == true)
                {
                    sql = sql + " WITH ADMIN OPTION";
                }
                MessageBox.Show(sql);
                Class.DB_Config.RunSqlDel(sql);
            }
            MessageBox.Show("Thao tác gán quyền thành công");
        }

        private void btnRevoke_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            if (cbRole.IsEnabled == false)
            {
                string priv = cbPriv.Text;
                string tab = cbTable.Text;
                string grantee = tbGrantee.Text.ToUpper();
                

                if (priv == "Xem (Select)")
                    priv = "SELECT";
                else if (priv == "Xoá (Delete)")
                    priv = "DELETE";
                else if (priv == "Thêm (Insert)")
                    priv = "INSERT";
                else if (priv == "Sửa (Update)")
                    priv = "UPDATE";
                else if (priv == "Thực thi (Excute)")
                    priv = "EXCUTE";

                //sql = "alter session set \"_oracle_script\"=true";
                //Class.DB_Config.RunSqlDel(sql);

                if (((grantee[0] >= 'a') && (grantee[0] <= 'z')) || ((grantee[0] >= 'A') && (grantee[0] <= 'Z')))
                {
                    sql = "REVOKE " + priv + " ON " + tab + " FROM " + grantee;
                }
                else
                {
                    sql = "REVOKE " + priv + " ON " + tab + " FROM '" + grantee + "'";
                }
                
            }
            else
            {
                string role = cbRole.Text;
                string grantee = tbGrantee.Text.ToUpper();

                //sql = "alter session set \"_oracle_script\"=true";
                //Class.DB_Config.RunSQL(sql);

                if (((grantee[0] >= 'a') && (grantee[0] <= 'z')) || ((grantee[0] >= 'A') && (grantee[0] <= 'Z')))
                {
                    sql = "REVOKE " + role + " FROM " + grantee;
                }
                else
                {
                    sql = "REVOKE " + role + " FROM '" + grantee + "'";
                }

            }
            Class.DB_Config.RunSqlDel(sql);
            MessageBox.Show("Thao tác thu hồi quyền thành công");
        }

        private void rdCheck(object sender, RoutedEventArgs e)
        {
            var button = sender as RadioButton;

            string temp = button.Content.ToString();

            if (temp == "Gán quyền")
            {
                cbRole.IsEnabled = false;
                cbPriv.IsEnabled = true;
                cbTable.IsEnabled = true;
                cbAdminOption.IsEnabled = false;
            }
            else
            {
                cbRole.IsEnabled = true;
                cbPriv.IsEnabled = false;
                cbTable.IsEnabled = false;
                cbAdminOption.IsEnabled = true;
            }
        }
    }
}
