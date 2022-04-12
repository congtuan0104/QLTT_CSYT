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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Oracle.DataAccess.Client;
using QLTT_CSYT;

namespace QLTT_CSYT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {    

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoggin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUsername.Text.ToString();
            string password = tbPass.Password.ToString();

            if (string.IsNullOrEmpty(username))
            {
                txtWarning.Text = "Bạn chưa nhập tên đăng nhập";
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                txtWarning.Text = "Bạn chưa nhập mật khẩu";
                return;
            }


            bool isConnect = Class.Functions.Connect(username, password);
            if (isConnect)
            {
                try
                {
                    DataTable dt = new DataTable();
                    string sql = "SELECT * FROM SESSION_ROLES";
                    dt = Class.Functions.GetDataToTable(sql); //Đọc danh sách role của user hiện tại
                    if (dt.Rows.Count > 1)
                    {
                        Admin_Main admin_Main = new Admin_Main();
                        admin_Main.Show();
                    }
                    else
                    {
                        BenhNhan_Main benhNhan_Main = new BenhNhan_Main();
                        benhNhan_Main.Show();
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
                
                
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
