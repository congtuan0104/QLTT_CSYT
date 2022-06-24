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
    /// Interaction logic for HSBA.xaml
    /// </summary>
    public partial class HSBA : Window
    {
        DataTable tbl = new DataTable();
        public HSBA()
        {
            InitializeComponent();
            LayDSHSBA();
        }

        private void LayDSHSBA()
        {
            try
            {
                string sql = "SELECT MAHSBA, MABN, NGAY, MABS, MAKHOA, MACSYT" +
                    " FROM QLTT.HSBA";
                tbl = Class.DB_Config.GetDataToTable(sql);
                dgvDSHS.ItemsSource = null;
                dgvDSHS.ItemsSource = tbl.DefaultView;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvDSHS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvDSHS.SelectedIndex < 0)
            {
                btnSua.IsEnabled = false;
                btnXoa.IsEnabled = false;
                return;
            }
            btnSua.IsEnabled = true;
            btnXoa.IsEnabled = true;
            return;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            if (tbThongTin.Text == "" || tbThongTin == null)
            {
                MessageBox.Show("Bạn chưa nhập thông tin tìm kiếm");
                return;
            }

            if (cbTimKiemTheo.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn chưa chọn tìm kiếm theo");
                return;
            }

            try
            {
                string sql;
                int Ma = Int32.Parse(tbThongTin.Text);
                if (cbTimKiemTheo.SelectedIndex == 0)
                {                  
                    sql = "SELECT MAHSBA, MABN, NGAY, MABS, MAKHOA, MACSYT" +
                    " FROM QLTT.HSBA WHERE MaBN = " + Ma;
                }
                else if(cbTimKiemTheo.SelectedIndex == 1)
                {
                    sql = "SELECT MAHSBA, MABN, NGAY, MABS, MAKHOA, MACSYT" +
                    " FROM QLTT.HSBA WHERE MaBS = " + Ma;
                }
                else
                {
                    sql = "SELECT MAHSBA, MABN, NGAY, MABS, MAKHOA, MACSYT" +
                    " FROM QLTT.HSBA WHERE MaCSYT = " + Ma;
                }
                tbl = Class.DB_Config.GetDataToTable(sql);
                dgvDSHS.ItemsSource = null;
                dgvDSHS.ItemsSource = tbl.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            ThemHSBA themHSBA = new ThemHSBA();
            themHSBA.ShowDialog();
            LayDSHSBA();
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            DataRowView HS_Selected = dgvDSHS.SelectedItem as DataRowView;
            int MaHS = Int32.Parse(HS_Selected.Row["MAHSBA"].ToString());
            ChiTietHSBA ct = new ChiTietHSBA(MaHS);
            ct.ShowDialog();
            LayDSHSBA();
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            DataRowView HS_Selected = dgvDSHS.SelectedItem as DataRowView;
            string MaHS = HS_Selected.Row["MAHSBA"].ToString();

            string message = "Bạn có chắc muốn xoá hồ sơ ?";

            if (MessageBox.Show(message, "Cảnh báo", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string sql = "DELETE QLTT.HSBA WHERE MaHSBA = " + MaHS;
                string sql1 = "DELETE QLTT.HSBA_DV WHERE MaHSBA = " + MaHS;
                Class.DB_Config.RunSqlDel(sql);
                Class.DB_Config.RunSqlDel(sql1);
                sql = "SELECT * FROM QLTT.HSBA WHERE MaHSBA = " + MaHS;
                if (Class.DB_Config.CheckValue(sql))
                {
                    MessageBox.Show("Bạn không có quyền xoá hồ sơ này");                   
                    return;
                }
                MessageBox.Show("Xoá hồ sơ thành công");
                LayDSHSBA();
            }           
            return;
        }

        private void btnQuayLai_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
