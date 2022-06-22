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
    /// Interaction logic for DichVu.xaml
    /// </summary>
    public partial class DichVu : Window
    {
        DataTable tbl = new DataTable();
        public DichVu()
        {
            InitializeComponent();
            LayDSDV();
        }

        private void LayDSDV()
        {
            try
            {
                string sql = "SELECT * FROM QLTT.DICHVU";
                tbl = Class.DB_Config.GetDataToTable(sql);
                dgvDSDV.ItemsSource = tbl.DefaultView;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvDSDV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvDSDV.SelectedIndex < 0)
            {
                tbMaDV.Clear();
                tbTenDV.Clear();
                tbGiaDV.Clear();
                btnLuu.IsEnabled = false;
                btnXoa.IsEnabled = false;
                btnHuy.IsEnabled = false;
                return;
            }
            DataRowView DV_Selected = dgvDSDV.SelectedItem as DataRowView;           
            tbMaDV.Text = DV_Selected.Row["MADV"].ToString();
            tbTenDV.Text = DV_Selected.Row["TENDV"].ToString();
            tbGiaDV.Text = DV_Selected.Row["GIADV"].ToString();
            btnLuu.IsEnabled = true;
            btnXoa.IsEnabled = true;
            return;
        }


        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            tbMaDV.Clear();
            tbTenDV.Clear();
            tbGiaDV.Clear();
            btnThem.IsEnabled = false;
            btnLuu.IsEnabled = true;
            btnHuy.IsEnabled = true;
            btnXoa.IsEnabled = false;
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DV_Selected = dgvDSDV.SelectedItem as DataRowView;

            string message = "Bạn có chắc muốn xoá dịch vụ "+ tbTenDV.Text+ " ?";
            // Thực hiện xoá dịch vụ
            if (MessageBox.Show(message, "Cảnh báo", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string sql = "DELETE QLTT.DichVu WHERE MaDV = " + tbMaDV.Text;
                Class.DB_Config.RunSqlDel(sql);
            }
            return;
        }

        private void btnQuayLai_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (tbMaDV.Text == null|| tbMaDV.Text.Length == 0)
            {
                // insert dichvu
                string sql = "INSERT INTO QLTT.DICHVU(TENDV, GIADV) " +
                "VALUES(:tendv, :gia)";
                try
                {
                    OracleCommand command = new OracleCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.Connection = Class.DB_Config.Con;
                    command.Parameters.Add(":tendv", OracleDbType.NVarchar2).Value = tbTenDV.Text;
                    command.Parameters.Add(":gia", OracleDbType.Int32).Value = Int32.Parse(tbGiaDV.Text);
                    
                    command.ExecuteNonQuery();

                    MessageBox.Show("Thêm dịch vụ thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thêm dịch vụ thất bại!\n" + ex.Message);
                }
            }
            else
            {
                // update dichvu
                string sql = "UPDATE QLTT.DICHVU SET " +
                "TENDV = :tendv, " +
                "GIADV = :gia " +
                "WHERE MADV = :madv";
                try
                {
                    OracleCommand command = new OracleCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.Connection = Class.DB_Config.Con;
                    command.Parameters.Add(":tendv", OracleDbType.NVarchar2).Value = tbTenDV.Text;                  
                    command.Parameters.Add(":gia", OracleDbType.Int32).Value = Int32.Parse(tbGiaDV.Text);                    
                    command.Parameters.Add(":mabn", OracleDbType.Int32).Value = Int32.Parse(tbMaDV.Text);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Cập nhật thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cập nhật thất bại!\n" + ex.Message);
                }
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {           
            btnThem.IsEnabled = true;
            if (dgvDSDV.SelectedIndex < 0)
            {
                tbMaDV.Clear();
                tbTenDV.Clear();
                tbGiaDV.Clear();
                btnLuu.IsEnabled = false;
                btnXoa.IsEnabled = false;
                btnHuy.IsEnabled = false;
                return;
            }
            DataRowView DV_Selected = dgvDSDV.SelectedItem as DataRowView;
            tbMaDV.Text = DV_Selected.Row["MADV"].ToString();
            tbTenDV.Text = DV_Selected.Row["TENDV"].ToString();
            tbGiaDV.Text = DV_Selected.Row["GIADV"].ToString();
            btnLuu.IsEnabled = true;
            btnXoa.IsEnabled = true;

        }

    }
}
