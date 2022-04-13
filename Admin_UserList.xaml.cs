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

namespace QLTT_CSYT
{
    /// <summary>
    /// Interaction logic for Admin_UserList.xaml
    /// </summary>
    public partial class Admin_UserList : Window
    {
        DataTable tblUser;
        public Admin_UserList()
        {
            InitializeComponent();
        }

        private void Admin_User_Loading(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT USER_ID, USERNAME, ACCOUNT_STATUS, CREATED, EXPIRY_DATE, LAST_LOGIN " +
                "FROM DBA_USERS WHERE DEFAULT_TABLESPACE = 'USERS'";

            tblUser = Class.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvUser.ItemsSource = tblUser.DefaultView; //Nguồn dữ liệu            
            //dgvUser.Columns[0].Header = "Username";
            //dgvUser.Columns[1].Header = "Status";
            //dgvUser.Columns[2].Header = "Created date";
            //dgvUser.Columns[3].Header = "Expiry Date";
            //dgvUser.Columns[4].Header = "Last Login";
            dgvUser.AutoGenerateColumns = true;
            

        }
    }
}
