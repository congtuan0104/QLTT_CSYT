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

namespace QLTT_CSYT
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        public Login()
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

            //Dùng để đăng nhập nhanh (chỉ dùng khi test)
            if (username == "1")
            {   // admin
                username = "QLTT";
                password = "admin123";
            }

            else if (username == "cs")
            {   // QL_CSYT
                username = "231353133";
                password = "1234";
            }

            else if (username == "bn")
            {   // Bệnh nhân
                username = "946301145";
                password = "4321";
            }

            else if (username == "tt")
            {   // Thanh tra
                username = "232630930";
                password = "1234";
            }

            else if (username == "nc")
            {   // Nghiên cứu
                username = "757592857";
                password = "1234";
            }

            else if (username == "bs")
            {   // Nghiên cứu
                username = "868387298";
                password = "1234";
            }

            bool isConnect = Class.DB_Config.Connect(username, password);
            if (isConnect)  // Kết nối thành công
            {
                try
                {
                    DataTable dt = new DataTable();
                    string sql = "SELECT * FROM SESSION_ROLES";
                    dt = Class.DB_Config.GetDataToTable(sql); //Đọc danh sách role của user hiện tại
                    
                    if (dt.Rows.Count > 1)
                    {
                        // Có nhiều hơn 1 role -> admin 
                        Admin_Main AdminMenu = new Admin_Main();
                        AdminMenu.Show();
                        this.Close();
                        return;
                    }
                    // User chỉ có 1 role
                    // -> [BENH_NHAN, BAC_SI, NGHIEN_CUU, QL_CSYT, THANH_TRA]
                    Menu menu = new Menu();
                    menu.Show();
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
