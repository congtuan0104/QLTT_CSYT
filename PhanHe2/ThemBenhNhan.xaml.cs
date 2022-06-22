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
    /// Interaction logic for ThemBenhNhan.xaml
    /// </summary>
    public partial class ThemBenhNhan : Window
    {
        public ThemBenhNhan()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            string sql = "INSERT INTO QLTT.BENHNHAN(MACSYT, TENBN, CMND, NGAYSINH, " +
                "SONHA,TENDUONG, QUANHUYEN, TINHTP, TIENSUBENH, TIENSUBENHGD, DIUNGTHUOC) " +
                "VALUES(:macsyt, :tenbn, :cmnd, :ngaysinh, :sonha, :tenduong, :quanhuyen, " +
                ":tinhtp, :tiensubenh, :tiensubenhgd, :diungthuoc)";
            try
            {
                OracleCommand command = new OracleCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Connection = Class.DB_Config.Con;

                command.Parameters.Add(":macsyt", OracleDbType.Int32).Value = Int32.Parse(tbMaCSYT.Text);
                command.Parameters.Add(":tenbn", OracleDbType.NVarchar2).Value = tbTenBN.Text;
                command.Parameters.Add(":cmnd", OracleDbType.Char).Value = tbCMND.Text;
                command.Parameters.Add(":ngaysinh", OracleDbType.Date).Value = dpNgaySinh.SelectedDate;
                command.Parameters.Add(":sonha", OracleDbType.Int32).Value = Int32.Parse(tbSoNha.Text);
                command.Parameters.Add(":tenduong", OracleDbType.NVarchar2).Value = tbTenDuong.Text;
                command.Parameters.Add(":quanhuyen", OracleDbType.NVarchar2).Value = tbQuanHuyen.Text;
                command.Parameters.Add(":tinhtp", OracleDbType.NVarchar2).Value = tbThanhPho.Text;
                command.Parameters.Add(":tiensubenh", OracleDbType.NVarchar2).Value = tbTSBenh.Text;
                command.Parameters.Add(":tiensubenhgd", OracleDbType.NVarchar2).Value = tbTSBenhGD.Text;
                command.Parameters.Add(":diungthuoc", OracleDbType.NVarchar2).Value = tbDiUng.Text;
                

                command.ExecuteNonQuery();
                MessageBox.Show("Thêm bệnh nhân thành công");
                BenhNhan bn = new BenhNhan();
                bn.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm bệnh nhân thất bại!\n" + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            BenhNhan bn = new BenhNhan();
            bn.Show();
            this.Close();
        }
    }
}
