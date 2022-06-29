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
    /// Interaction logic for ChinhSuaBenhNhan.xaml
    /// </summary>
    public partial class ChinhSuaBenhNhan : Window
    {
        public ChinhSuaBenhNhan(int mabn)
        {
            InitializeComponent();
            HienThiThongTinBN(mabn);
        }

        private void HienThiThongTinBN(int mabn)
        {
            DataTable dt = new DataTable();
            string sql = "Select * from QLTT.BENHNHAN WHERE MaBN = " + mabn;
            dt = Class.DB_Config.GetDataToTable(sql);
            tbMaBN.Text = dt.Rows[0]["MABN"].ToString();
            tbMaCSYT.Text = dt.Rows[0]["MACSYT"].ToString();
            tbTenBN.Text = dt.Rows[0]["TENBN"].ToString();
            tbCMND.Text = dt.Rows[0]["CMND"].ToString();
            dpNgaySinh.Text = dt.Rows[0]["NGAYSINH"].ToString();
            tbSoNha.Text = dt.Rows[0]["SONHA"].ToString();
            tbTenDuong.Text = dt.Rows[0]["TENDUONG"].ToString();
            tbQuanHuyen.Text = dt.Rows[0]["QUANHUYEN"].ToString();
            tbThanhPho.Text = dt.Rows[0]["TINHTP"].ToString();
            tbTSBenh.Text = dt.Rows[0]["TIENSUBENH"].ToString();
            tbTSBenhGD.Text = dt.Rows[0]["TIENSUBENHGD"].ToString();
            tbDiUng.Text = dt.Rows[0]["DIUNGTHUOC"].ToString();

        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {       
            string sql = "UPDATE QLTT.BENHNHAN SET " +
                "TENBN = :tenbn, " +
                "NGAYSINH = :ngaysinh, " +
                "SONHA = :sonha, " +
                "TENDUONG = :tenduong, " +
                "QUANHUYEN = :quanhuyen, " +
                "TINHTP = :tinhtp, " +
                "TIENSUBENH = :tiensubenh, " +
                "TIENSUBENHGD = :tiensubenhgd, " +
                "DIUNGTHUOC = :diungthuoc " +
                "WHERE MABN = :mabn";
            try
            {
                OracleCommand command = new OracleCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Connection = Class.DB_Config.Con;

                command.Parameters.Add(":tenbn", OracleDbType.NVarchar2).Value = tbTenBN.Text;
                command.Parameters.Add(":ngaysinh", OracleDbType.Date).Value = dpNgaySinh.SelectedDate;
                command.Parameters.Add(":sonha", OracleDbType.Int32).Value = Int32.Parse(tbSoNha.Text);
                command.Parameters.Add(":tenduong", OracleDbType.NVarchar2).Value = tbTenDuong.Text;
                command.Parameters.Add(":quanhuyen", OracleDbType.NVarchar2).Value = tbQuanHuyen.Text;
                command.Parameters.Add(":tinhtp", OracleDbType.NVarchar2).Value = tbThanhPho.Text;
                command.Parameters.Add(":tiensubenh", OracleDbType.NVarchar2).Value = tbTSBenh.Text;
                command.Parameters.Add(":tiensubenhgd", OracleDbType.NVarchar2).Value = tbTSBenhGD.Text;
                command.Parameters.Add(":diungthuoc", OracleDbType.NVarchar2).Value = tbDiUng.Text;
                command.Parameters.Add(":mabn", OracleDbType.Int32).Value = Int32.Parse(tbMaBN.Text);
                
                command.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thất bại!\n" + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbMaHoa_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbMaBN.Text == null || tbMaBN.Text.Length == 0)
                    return;
                DataTable dt = new DataTable();
                string sql = "Select DIUNGTHUOC from QLTT.BENHNHAN WHERE MaBN = " + Int32.Parse(tbMaBN.Text);
                dt = Class.DB_Config.GetDataToTable(sql);
                tbDiUng.Text = dt.Rows[0]["DIUNGTHUOC"].ToString();
            }
            catch (Exception ex)
            {
                cbMaHoa.IsChecked = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void cbMaHoa_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbMaBN.Text == null || tbMaBN.Text.Length == 0)
                    return;
                DataTable dt = new DataTable();
                string sql = "Select QLTT.ENCRYPT_DECRYPT.DECRYPT_BENHNHAN(DIUNGTHUOC, CMND)" +
                    "AS \"DIUNGTHUOC\" from QLTT.BENHNHAN WHERE MaBN = " + Int32.Parse(tbMaBN.Text);
                dt = Class.DB_Config.GetDataToTable(sql);
                tbDiUng.Text = dt.Rows[0]["DIUNGTHUOC"].ToString();
            }
            catch (Exception ex)
            {
                cbMaHoa.IsChecked = true;
                MessageBox.Show("Không thể thực hiện giải mã\n" + ex.Message);
            }
        }
    }
}
