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
    /// Interaction logic for ThemHSBA.xaml
    /// </summary>
    public partial class ThemHSBA : Window
    {
        public ThemHSBA()
        {
            InitializeComponent();
            HienThiThongTin();
        }

        private void HienThiThongTin()
        {
            dpNgayLap.SelectedDate = DateTime.Now;
            string sql = "SELECT CSYT FROM QLTT.NHANVIEN " +
                "WHERE 'U'||CMND = (SELECT sys_context('USERENV', 'CURRENT_USER') FROM dual)";
            DataTable dt = new DataTable();
            dt = Class.DB_Config.GetDataToTable(sql);
            if (dt.Rows.Count > 0)
                tbMaCSYT.Text = dt.Rows[0]["CSYT"].ToString();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            string sql = "INSERT INTO QLTT.HSBA(MABN, MABS, NGAY, MAKHOA, MACSYT, " +
                "CHANDOAN, KETLUAN) VALUES(:mabn, :mabs, :ngay, :makhoa, :csyt, " +
                ":chandoan, :ketluan)";
            try
            {
                OracleCommand command = new OracleCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Connection = Class.DB_Config.Con;

                command.Parameters.Add(":mabn", OracleDbType.Int32).Value = Int32.Parse(tbMaBN.Text);
                command.Parameters.Add(":mabs", OracleDbType.Int32).Value = Int32.Parse(tbMaBS.Text);
                command.Parameters.Add(":ngay", OracleDbType.Date).Value = dpNgayLap.SelectedDate;
                command.Parameters.Add(":makhoa", OracleDbType.Int32).Value = Int32.Parse(tbMaKhoa.Text);
                command.Parameters.Add(":csyt", OracleDbType.Int32).Value = Int32.Parse(tbMaCSYT.Text);
                command.Parameters.Add(":chandoan", OracleDbType.NVarchar2).Value = tbChanDoan.Text;
                command.Parameters.Add(":ketluan", OracleDbType.NVarchar2).Value = tbKetLuan.Text;                
                command.ExecuteNonQuery();

                sql = "SELECT MAX(MAHSBA) AS \"MAHS\" FROM QLTT.HSBA";
                DataTable dt = new DataTable();
                dt = Class.DB_Config.GetDataToTable(sql);
                int mahs = Int32.Parse(dt.Rows[0]["MAHS"].ToString());
                MessageBox.Show("Tạo hồ sơ thành công");
                ChiTietHSBA ct = new ChiTietHSBA(mahs);
                ct.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tạo hồ sơ thất bại!\n" + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
