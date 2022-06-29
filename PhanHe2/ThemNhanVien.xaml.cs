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
    /// Interaction logic for ThemNhanVien.xaml
    /// </summary>
    public partial class ThemNhanVien : Window
    {
        public ThemNhanVien()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            OracleCommand command = new OracleCommand();
            command.CommandType = CommandType.Text;
            
            if (tbMaCSYT.Text.Length > 0)
            {
                command.Parameters.Add(":macsyt", OracleDbType.Int32).Value = Int32.Parse(tbMaCSYT.Text);
                if (tbChuyenKhoa.Text.Length > 0)
                {
                    sql = "INSERT INTO QLTT.NHANVIEN(CSYT, HOTEN, CMND, NGAYSINH, PHAI, " +
                            "SODT, QUEQUAN, VAITRO, CHUYENKHOA) " +
                            "VALUES(:macsyt, :tennv, :cmnd, :ngaysinh, :phai, :sdt, :quequan, " +
                            ":vaitro, :chuyenkhoa)";
                    command.Parameters.Add(":chuyenkhoa", OracleDbType.Int32).Value = Int32.Parse(tbChuyenKhoa.Text);
                }
                else
                {
                    sql = "INSERT INTO QLTT.NHANVIEN(CSYT, HOTEN, CMND, NGAYSINH, PHAI, " +
                            "SODT, QUEQUAN, VAITRO) " +
                            "VALUES(:macsyt, :tennv, :cmnd, :ngaysinh, :phai, :sdt, :quequan, " +
                            ":vaitro)";
                }
            }
            else
            {
                sql = "INSERT INTO QLTT.NHANVIEN(HOTEN, CMND, NGAYSINH, PHAI, " +
                            "SODT, QUEQUAN, VAITRO) " +
                            "VALUES(:tennv, :cmnd, :ngaysinh, :phai, :sdt, :quequan, " +
                            ":vaitro)";
            }

            try
            {
                command.CommandText = sql;
                command.Connection = Class.DB_Config.Con;
                
                command.Parameters.Add(":tennv", OracleDbType.NVarchar2).Value = tbTenNV.Text;
                command.Parameters.Add(":cmnd", OracleDbType.Char).Value = tbCMND.Text;
                command.Parameters.Add(":ngaysinh", OracleDbType.Date).Value = dpNgaySinh.SelectedDate;
                command.Parameters.Add(":phai", OracleDbType.NVarchar2).Value = cbGioiTinh.Text;
                command.Parameters.Add(":sdt", OracleDbType.Char).Value = tbSDT.Text;
                command.Parameters.Add(":quequan", OracleDbType.NVarchar2).Value = tbQueQuan.Text;
                command.Parameters.Add(":vaitro", OracleDbType.NVarchar2).Value = cbVaiTro.Text;
                
                MessageBox.Show(cbVaiTro.Text);
                int kq = command.ExecuteNonQuery();

                if (kq < 0)
                    return;
                
                sql = "CREATE USER U" + tbCMND.Text + " IDENTIFIED BY \""
                    + dpNgaySinh.SelectedDate.Value.ToString("ddMMyyyy") +"\"";
                if (Class.DB_Config.RunSQL(sql))
                    return;

                if (cbVaiTro.Text == "Bac si")
                    sql = "GRANT BAC_SI TO U" + tbCMND.Text;
                else if (cbVaiTro.Text == "Nghien cuu")
                    sql = "GRANT NGHIEN_CUU TO U" + tbCMND.Text;
                else if (cbVaiTro.Text == "Thanh tra")
                    sql = "GRANT THANH_TRA TO U" + tbCMND.Text;
                else if (cbVaiTro.Text == "CSYT")
                    sql = "GRANT QL_CSYT TO U" + tbCMND.Text;
                Class.DB_Config.RunSQL(sql);
                MessageBox.Show("Thêm nhân viên thành công");
                //DSNhanVien nv = new DSNhanVien();
                //nv.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm nhân viên thất bại!\n" + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
