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
        }

        private void Admin_User_Loading(object sender, EventArgs e)
        {
            LoadDataGridView();
            //dgvUser.Columns[0].Header = "Username";
            //dgvUser.Columns[1].Header = "Status";
            //dgvUser.Columns[2].Header = "Created date";
            //dgvUser.Columns[3].Header = "Expiry Date";
            //dgvUser.Columns[4].Header = "Last Login";
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT USERNAME, ACCOUNT_STATUS, CREATED, EXPIRY_DATE, LAST_LOGIN " +
                "FROM DBA_USERS WHERE DEFAULT_TABLESPACE = 'USERS'";

            tblUser = Class.DB_Config.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            
            dgvUser.ItemsSource = tblUser.DefaultView; //Nguồn dữ liệu

            dgvUser.Items.SortDescriptions.Clear();
            dgvUser.Items.SortDescriptions.Add(new SortDescription("CREATED", ListSortDirection.Descending));
            dgvUser.Items.Refresh();

            btnDeleteUser.IsEnabled = false;
            btnDetail.IsEnabled = false;
            btnChangePass.IsEnabled = false;
            btnLock.IsEnabled = false;
            btnUnlock.IsEnabled = false;

        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            Admin_AddUser admin_AddUser = new Admin_AddUser();
            admin_AddUser.Show();
        }


        private void dgvUser_RowContentClick(object sender, SelectionChangedEventArgs e)
        {
            if (dgvUser.SelectedIndex < 0)
                return;
            btnDeleteUser.IsEnabled = true;
            btnDetail.IsEnabled = true;
            btnChangePass.IsEnabled = true;

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
            if (MessageBox.Show("Xác nhận xoá tài khoản này!!!", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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
                LoadDataGridView();
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
            LoadDataGridView();
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
            LoadDataGridView();
        }

        private void btnChangePass_Click(object sender, RoutedEventArgs e)
        {
            Admin_ChangePass admin_ChangePass = new Admin_ChangePass(Username);
            admin_ChangePass.ShowDialog();
        }
    }
}
