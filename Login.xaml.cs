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
                username = "U231353133";
                password = "01042001";
            }

            else if (username == "bn")
            {   // Bệnh nhân
                username = "U946301145";
                password = "13031953";
            }

            else if (username == "tt")
            {   // Thanh tra
                username = "U232630930";
                password = "15111952";
            }

            else if (username == "nc")
            {   // Nghiên cứu
                username = "U757592857";
                password = "08071994";
            }

            else if (username == "bs")
            {   // Bác sĩ
                username = "U868387298";
                password = "05101980";
            }

            else if (username == "gds")
            {   // Giám đốc sở y tế
                username = "U234567890";
                password = "24031990";
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

                    if (dt.Rows[0]["ROLE"].ToString() == "BENH_NHAN")
                    {
                        sql = "SELECT TO_CHAR(NGAYSINH, 'ddMMyyyy') AS NS FROM QLTT.BENHNHAN " +
                        "WHERE CMND = SUBSTR('" + username + "',2)";
                    }
                    else
                    {
                        sql = "SELECT TO_CHAR(NGAYSINH, 'ddMMyyyy') AS NS FROM QLTT.NHANVIEN " +
                        "WHERE CMND = SUBSTR('" + username + "',2)";
                    }

                    dt = Class.DB_Config.GetDataToTable(sql);
                    if (dt.Rows[0]["NS"].ToString() == password)
                    {
                        MessageBox.Show("Lần đầu đăng nhập. Vui lòng đổi mật khẩu");
                        Admin_ChangePass admin_ChangePass = new Admin_ChangePass(username);
                        admin_ChangePass.Show();
                        this.Close();
                        return;
                    }
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
            Environment.Exit(0);
        }
    }
}
