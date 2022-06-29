using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace QLTT_CSYT
{
    /// <summary>
    /// Interaction logic for Admin_UserList.xaml
    /// </summary>
    public partial class Admin_UserList : Window
    {
        DataTable tblUser;

        string Username;
        string Status;
        public Admin_UserList()
        {
            InitializeComponent();
            DSUser();
            DSRole();
        }

        private void DSRole()
        {
            string sql;
            sql = "SELECT GRANTED_ROLE AS ROLE FROM USER_ROLE_PRIVS WHERE GRANTED_ROLE != 'RESOURCE'";
            DataTable dt = Class.DB_Config.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvRole.ItemsSource = null;
            dgvRole.ItemsSource = dt.DefaultView; //Nguồn dữ liệu
        }

        

        private void DSUser()
        {
            string sql;
            sql = "SELECT USERNAME, ACCOUNT_STATUS AS STATUS, CREATED, EXPIRY_DATE, LAST_LOGIN " +
                "FROM DBA_USERS WHERE DEFAULT_TABLESPACE = 'USERS'";
            if(cbOnly.IsChecked == false)
            {
                sql = sql + " AND USERNAME LIKE 'U%'";
            }
            tblUser = Class.DB_Config.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvUser.ItemsSource = null;
            dgvUser.ItemsSource = tblUser.DefaultView; //Nguồn dữ liệu

            dgvUser.Items.SortDescriptions.Clear();
            dgvUser.Items.SortDescriptions.Add(new SortDescription("CREATED", ListSortDirection.Descending));
            dgvUser.Items.Refresh();

            btnDeleteUser.IsEnabled = false;
            btnGrant.IsEnabled = false;
            btnChangePass.IsEnabled = false;
            btnLock.IsEnabled = false;
            btnUnlock.IsEnabled = false;

        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            Admin_AddUser admin_AddUser = new Admin_AddUser();
            admin_AddUser.ShowDialog();
            DSUser();
            DSRole();
            btnGrant.IsEnabled = false;
            btnDeleteUser.IsEnabled=false;
        }


        private void dgvUser_RowContentClick(object sender, SelectionChangedEventArgs e)
        {
            if (dgvUser.SelectedIndex < 0)
                return;
            btnDeleteUser.IsEnabled = true;
            btnGrant.IsEnabled = true;
            btnChangePass.IsEnabled = true;
            dgvRole.SelectedIndex = -1;

            DataRowView curRow = dgvUser.SelectedItem as DataRowView;

            Username = curRow.Row.ItemArray[0].ToString();
            Status = curRow.Row.ItemArray[1].ToString();

            if(Status == "OPEN")
            {
                btnLock.IsEnabled = true;
                btnUnlock.IsEnabled = false;
            }
            else
            {
                btnLock.IsEnabled = false;
                btnUnlock.IsEnabled = true;
            }
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            if(dgvRole.SelectedIndex >= 0)
            {
                DataRowView curRow = dgvRole.SelectedItem as DataRowView;
                string Role = curRow.Row.ItemArray[0].ToString();
                if (MessageBox.Show("Xác nhận xoá role "+ Role, "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (((Role[0] >= 'a') && (Role[0] <= 'z')) || ((Role[0] >= 'A') && (Role[0] <= 'Z')))
                    {
                        sql = "DROP ROLE " + Role;
                    }
                    else
                    {
                        sql = "DROP ROLE \"" + Role + "\"";
                    }

                    Class.DB_Config.RunSqlDel(sql);
                    DSRole();
                    btnGrant.IsEnabled = false;
                    btnDeleteUser.IsEnabled = false;
                }
                return;
            }

            if (MessageBox.Show("Xác nhận xoá user "+ Username, "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //sql = "alter session set \"_oracle_script\"=true";              
                //Class.DB_Config.RunSqlDel(sql);

                if (((Username[0] >= 'a') && (Username[0] <= 'z')) || ((Username[0] >= 'A') && (Username[0] <= 'Z'))){
                    sql = "DROP USER " + Username + " CASCADE";
                }
                else
                {
                    sql = "DROP USER \"" + Username + "\" CASCADE";
                }

                Class.DB_Config.RunSqlDel(sql);
                DSUser();
                btnGrant.IsEnabled = false;
                btnDeleteUser.IsEnabled = false;
            }
        }

        private void btnLock_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            //sql = "alter session set \"_oracle_script\"=true";
            //Class.DB_Config.RunSqlDel(sql);

            if (((Username[0] >= 'a') && (Username[0] <= 'z')) || ((Username[0] >= 'A') && (Username[0] <= 'Z')))
            {
                sql = "ALTER USER " + Username + " ACCOUNT LOCK";
            }
            else
            {
                sql = "ALTER USER \"" + Username + "\" ACCOUNT LOCK";
            }
            Class.DB_Config.RunSqlDel(sql);
            DSUser();
            btnGrant.IsEnabled = false;
            btnDeleteUser.IsEnabled = false;
        }

        private void btnUnlock_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            //sql = "alter session set \"_oracle_script\"=true";
            //Class.DB_Config.RunSqlDel(sql);
            if (((Username[0] >= 'a') && (Username[0] <= 'z')) || ((Username[0] >= 'A') && (Username[0] <= 'Z')))
            {
                sql = "ALTER USER " + Username + " ACCOUNT UNLOCK";
            }
            else
            {
                sql = "ALTER USER \"" + Username + "\" ACCOUNT UNLOCK";
            }

            Class.DB_Config.RunSqlDel(sql);
            DSUser();
            btnGrant.IsEnabled = false;
            btnDeleteUser.IsEnabled = false;
        }

        private void btnChangePass_Click(object sender, RoutedEventArgs e)
        {
            Admin_ChangePass admin_ChangePass = new Admin_ChangePass(Username);
            admin_ChangePass.ShowDialog();
        }

        private void dgvRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvRole.SelectedIndex < 0)
                return;
            btnDeleteUser.IsEnabled = true;
            btnGrant.IsEnabled = true;
            btnChangePass.IsEnabled = false;
            btnUnlock.IsEnabled = false;
            btnLock.IsEnabled = false;
            dgvUser.SelectedIndex = -1;

            DataRowView curRow = dgvRole.SelectedItem as DataRowView;
            string Role = curRow.Row.ItemArray[0].ToString();
        }

        private void cbChange(object sender, RoutedEventArgs e)
        {
            DSUser();
            btnGrant.IsEnabled = false;
            btnDeleteUser.IsEnabled = false;
        }
    }
}
