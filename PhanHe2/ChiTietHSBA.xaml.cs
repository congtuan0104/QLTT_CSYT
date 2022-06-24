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
    /// Interaction logic for ChiTietHSBA.xaml
    /// </summary>
    public partial class ChiTietHSBA : Window
    {
        DataTable tbl = new DataTable();

        public ChiTietHSBA(int mahs)
        {
            InitializeComponent();
            HienThiHS(mahs);
            HienThiDV(mahs);
        }

        private void HienThiHS(int mahs)
        {
            string sql = "SELECT *" +
                    " FROM QLTT.HSBA WHERE MAHSBA = " + mahs;
            tbl = Class.DB_Config.GetDataToTable(sql);
            tbMaHS.Text = tbl.Rows[0]["MAHSBA"].ToString();
            tbMaBN.Text = tbl.Rows[0]["MABN"].ToString();
            tbMaBS.Text = tbl.Rows[0]["MABS"].ToString();
            tbMaKhoa.Text = tbl.Rows[0]["MAKHOA"].ToString();
            tbMaCSYT.Text = tbl.Rows[0]["MACSYT"].ToString();
            tbChanDoan.Text = tbl.Rows[0]["CHANDOAN"].ToString();
            tbKetLuan.Text = tbl.Rows[0]["KETLUAN"].ToString();
            dpNgayLap.Text = tbl.Rows[0]["NGAY"].ToString();
        }

        private void HienThiDV(int mahs)
        {
            try
            {
                string sql = "SELECT D.MADV, TENDV, GIADV, NGAY, MAKTV, KETQUA" +
                    " FROM QLTT.V_CT_HSBA v, QLTT.DICHVU d WHERE v.MADV = d.MADV" +
                    " AND MAHSBA = " + mahs;
                tbl = Class.DB_Config.GetDataToTable(sql);
                dgvDV.ItemsSource = null;
                dgvDV.ItemsSource = tbl.DefaultView;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            string sql = "UPDATE QLTT.HSBA SET " +
                "CHANDOAN = :chandoan, " +
                "KETLUAN = :ketluan " +
                "WHERE MAHSBA = :mahs";
            try
            {
                OracleCommand command = new OracleCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Connection = Class.DB_Config.Con;

                command.Parameters.Add(":chandoan", OracleDbType.NVarchar2).Value = tbChanDoan.Text;               
                command.Parameters.Add(":ketluan", OracleDbType.NVarchar2).Value = tbKetLuan.Text;
                command.Parameters.Add(":mahs", OracleDbType.Int32).Value = Int32.Parse(tbMaHS.Text);

                command.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thất bại!\n" + ex.Message);
            }
        }

        private void btnThemDV_Click(object sender, RoutedEventArgs e)
        {
            ThemDV_HSBA themDV = new ThemDV_HSBA(Int32.Parse(tbMaHS.Text));
            themDV.ShowDialog();
            HienThiDV(Int32.Parse(tbMaHS.Text));
        }

        private void btnXoaDV_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DV_Selected = dgvDV.SelectedItem as DataRowView;
            string MaDV = DV_Selected.Row["MADV"].ToString();

            string message = "Bạn có chắc muốn xoá dịch vụ " + MaDV;

            if (MessageBox.Show(message, "Cảnh báo", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string sql = "DELETE QLTT.HSBA_DV WHERE MAHSBA = " + tbMaHS.Text + " AND MADV + " + MaDV;
                Class.DB_Config.RunSqlDel(sql);
                HienThiDV(Int32.Parse(tbMaHS.Text));
            }
            return;
        }

        private void dgvDV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvDV.SelectedIndex < 0)
            {
                btnXoaDV.IsEnabled = false;
                return;
            }
            btnXoaDV.IsEnabled = true;
        }
    }
}
