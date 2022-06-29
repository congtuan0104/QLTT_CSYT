using Oracle.DataAccess.Client;
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
    /// Interaction logic for ChinhSuaNhanVien.xaml
    /// </summary>
    public partial class ChinhSuaNhanVien : Window
    {
        public ChinhSuaNhanVien(int manv)
        {
            InitializeComponent();
            HienThiThongTinNV(manv);
        }

        private void HienThiThongTinNV(int manv)
        {
            DataTable dt = new DataTable();
            string sql = "Select * from QLTT.NHANVIEN WHERE MaNV = " + manv;
            dt = Class.DB_Config.GetDataToTable(sql);
            tbMaNV.Text = dt.Rows[0]["MANV"].ToString();
            tbMaCSYT.Text = dt.Rows[0]["CSYT"].ToString();
            tbTenNV.Text = dt.Rows[0]["HOTEN"].ToString();
            tbCMND.Text = dt.Rows[0]["CMND"].ToString();
            tbSDT.Text = dt.Rows[0]["SODT"].ToString();
            tbMaNV.Text = dt.Rows[0]["MANV"].ToString();
            tbQueQuan.Text = dt.Rows[0]["QUEQUAN"].ToString();
            tbChuyenKhoa.Text = dt.Rows[0]["CHUYENKHOA"].ToString();
            dpNgaySinh.Text = dt.Rows[0]["NGAYSINH"].ToString();
            if(dt.Rows[0]["PHAI"].ToString() == "Nam")
                cbGioiTinh.SelectedIndex = 0;
            else
                cbGioiTinh.SelectedIndex = 1;

            if (dt.Rows[0]["VAITRO"].ToString() == "Bac si")
                cbVaiTro.SelectedIndex = 0;
            else if(dt.Rows[0]["VAITRO"].ToString() == "Thanh tra")
                cbVaiTro.SelectedIndex = 1;
            else if (dt.Rows[0]["VAITRO"].ToString() == "CSYT")
                cbVaiTro.SelectedIndex = 2;
            else
                cbVaiTro.SelectedIndex = 3;
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            string sql = "UPDATE QLTT.NHANVIEN SET " +
                "HOTEN = :tenbn, " +
                "NGAYSINH = :ngaysinh, " +  
                "SODT = :sdt, " +
                "QUEQUAN = :quequan, " +     
                "PHAI = :phai " +
                "WHERE MANV = :manv";
            try
            {
                OracleCommand command = new OracleCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Connection = Class.DB_Config.Con;

                command.Parameters.Add(":tenbn", OracleDbType.NVarchar2).Value = tbTenNV.Text;
                command.Parameters.Add(":ngaysinh", OracleDbType.Date).Value = dpNgaySinh.SelectedDate;
                command.Parameters.Add(":sdt", OracleDbType.Char).Value = tbSDT.Text;
                command.Parameters.Add(":quequan", OracleDbType.NVarchar2).Value = tbQueQuan.Text;
                command.Parameters.Add(":phai", OracleDbType.NVarchar2).Value = cbGioiTinh.Text;  
                command.Parameters.Add(":manv", OracleDbType.Int32).Value = Int32.Parse(tbMaNV.Text);

                command.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thất bại!\n" + ex.Message);
            }
        }
    }
}
