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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
            LayThongTinUser();
        }

        private void LayThongTinUser()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT sys_context('USERENV', 'CURRENT_USER') AS \"USERNAME\" FROM dual";
            dt = Class.DB_Config.GetDataToTable(sql);
            txtUser.Text = dt.Rows[0]["USERNAME"].ToString();

            sql = "SELECT * FROM SESSION_ROLES";
            dt = Class.DB_Config.GetDataToTable(sql);
            if(dt.Rows.Count > 1)
            {
                txtRole.Text = "Admin";
                return;
            }
            txtRole.Text = dt.Rows[0]["ROLE"].ToString();
        }

        private void btnBenhNhan_Click(object sender, RoutedEventArgs e)
        {
            BenhNhan BN = new BenhNhan();
            BN.Show();
            this.Close();
        }

        private void btnHSBA_Click(object sender, RoutedEventArgs e)
        {
            HSBA hs = new HSBA();
            hs.Show();
            this.Close();
        }

        private void btnNhanVien_Click(object sender, RoutedEventArgs e)
        {
            DSNhanVien dSNhanVien = new DSNhanVien();
            dSNhanVien.Show();
            this.Close();
        }

        private void btnDangXuat_Click(object sender, RoutedEventArgs e)
        {
            Class.DB_Config.Disconnect();
            Login mainWindow = new Login();
            mainWindow.Show();
            this.Close();
        }

        private void btnThongBao_Click(object sender, RoutedEventArgs e)
        {
            ThongBao tb = new ThongBao();
            tb.Show();
            this.Close();
        }

        private void btnDichVu_Click(object sender, RoutedEventArgs e)
        {
            DichVu dv = new DichVu();
            dv.Show();
            this.Close();
        }

        private void btnCSYT_Click(object sender, RoutedEventArgs e)
        {
            CSYT cs = new CSYT();
            cs.Show();
            this.Close();
        }

        private void btnDoiMK_Click(object sender, RoutedEventArgs e)
        {
            Admin_ChangePass changePass = new Admin_ChangePass(txtUser.Text);
            changePass.ShowDialog();
        }
    }
}
