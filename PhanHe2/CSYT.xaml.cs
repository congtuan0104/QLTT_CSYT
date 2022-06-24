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
    /// Interaction logic for CSYT.xaml
    /// </summary>
    public partial class CSYT : Window
    {
        DataTable tbl = new DataTable();
        public CSYT()
        {
            InitializeComponent();
            LayDSCSYT();
        }

        private void LayDSCSYT()
        {
            try
            {
                string sql = "SELECT * FROM QLTT.CSYT";
                tbl = Class.DB_Config.GetDataToTable(sql);
                dgvDSCS.ItemsSource = null;
                dgvDSCS.ItemsSource = tbl.DefaultView;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvDSCS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvDSCS.SelectedIndex < 0)
            {
                tbMaCS.Clear();
                tbTenCS.Clear();
                tbDiaChi.Clear();
                tbSDT.Clear();
                btnLuu.IsEnabled = false;
                btnXoa.IsEnabled = false;
                btnHuy.IsEnabled = false;
                return;
            }
            DataRowView CS_Selected = dgvDSCS.SelectedItem as DataRowView;
            tbMaCS.Text = CS_Selected.Row["MACSYT"].ToString();
            tbTenCS.Text = CS_Selected.Row["TENCSYT"].ToString();
            tbDiaChi.Text = CS_Selected.Row["DCCSYT"].ToString();
            tbSDT.Text = CS_Selected.Row["SDTCSYT"].ToString();
            btnLuu.IsEnabled = true;
            btnXoa.IsEnabled = true;
            return;
        }


        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            tbMaCS.Clear();
            tbTenCS.Clear();
            tbDiaChi.Clear();
            tbSDT.Clear();
            btnThem.IsEnabled = false;
            btnLuu.IsEnabled = true;
            btnHuy.IsEnabled = true;
            btnXoa.IsEnabled = false;
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            DataRowView CS_Selected = dgvDSCS.SelectedItem as DataRowView;

            string message = "Bạn có chắc muốn xoá cơ sở " + tbTenCS.Text + " ?";

            if (MessageBox.Show(message, "Cảnh báo", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string sql = "DELETE QLTT.CSYT WHERE MaCSYT = " + tbMaCS.Text;
                Class.DB_Config.RunSqlDel(sql);
                LayDSCSYT();
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
            if (tbMaCS.Text == null || tbMaCS.Text.Length == 0)
            {
                // insert dichvu
                string sql = "INSERT INTO QLTT.CSYT(TENCSYT, DCCSYT, SDTCSYT) " +
                "VALUES(:tencs, :diachi, :sdt)";
                try
                {
                    OracleCommand command = new OracleCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.Connection = Class.DB_Config.Con;
                    command.Parameters.Add(":tencs", OracleDbType.NVarchar2).Value = tbTenCS.Text;
                    command.Parameters.Add(":diachi", OracleDbType.NVarchar2).Value = tbDiaChi.Text;
                    command.Parameters.Add(":sdt", OracleDbType.Char).Value = tbSDT.Text;
                    command.ExecuteNonQuery();

                    MessageBox.Show("Thêm CSYT thành công");
                    LayDSCSYT();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thêm CSYT thất bại!\n" + ex.Message);
                }
            }
            else
            {
                // update dichvu
                string sql = "UPDATE QLTT.CSYT SET " +
                "TENCSYT = :tendv, " +
                "DCCSYT = :diachi, " +
                "SDTCSYT = :sdt " +
                "WHERE MACSYT = :macs";
                try
                {
                    OracleCommand command = new OracleCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.Connection = Class.DB_Config.Con;
                    command.Parameters.Add(":tencs", OracleDbType.NVarchar2).Value = tbTenCS.Text;
                    command.Parameters.Add(":diachi", OracleDbType.NVarchar2).Value = tbDiaChi.Text;
                    command.Parameters.Add(":sdt", OracleDbType.Char).Value = tbSDT.Text;
                    command.Parameters.Add(":macs", OracleDbType.Int32).Value = Int32.Parse(tbMaCS.Text);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Cập nhật thành công");
                    LayDSCSYT();
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
            if (dgvDSCS.SelectedIndex < 0)
            {
                tbMaCS.Clear();
                tbTenCS.Clear();
                tbDiaChi.Clear();
                tbSDT.Clear();
                btnLuu.IsEnabled = false;
                btnXoa.IsEnabled = false;
                btnHuy.IsEnabled = false;
                return;
            }
            DataRowView CS_Selected = dgvDSCS.SelectedItem as DataRowView;
            tbMaCS.Text = CS_Selected.Row["MACSYT"].ToString();
            tbTenCS.Text = CS_Selected.Row["TENCSYT"].ToString();
            tbDiaChi.Text = CS_Selected.Row["DCCSYT"].ToString();
            tbSDT.Text = CS_Selected.Row["SDTCSYT"].ToString();
            btnLuu.IsEnabled = true;
            btnXoa.IsEnabled = true;

        }
    }
}
