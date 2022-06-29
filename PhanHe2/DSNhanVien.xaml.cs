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
    /// Interaction logic for DSNhanVien.xaml
    /// </summary>
    public partial class DSNhanVien : Window
    {
        DataTable tblNhanVien = new DataTable();
        public DSNhanVien()
        {           
            InitializeComponent();
            LayDSNhanVien();
        }

        private void LayDSNhanVien()
        {
            try
            {
                string sql = "SELECT * FROM QLTT.NHANVIEN";
                tblNhanVien = Class.DB_Config.GetDataToTable(sql);
                dgvDSNV.ItemsSource = null;
                dgvDSNV.ItemsSource = tblNhanVien.DefaultView;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

            string sql;

            try
            {
                if (cbTimKiemTheo.SelectedIndex == 0)
                {
                    int MaBN = Int32.Parse(tbThongTin.Text);
                    sql = "Select * from QLTT.NHANVIEN WHERE MaNV = " + MaBN;
                }
                else
                {
                    sql = "Select * from QLTT.NHANVIEN " +
                        "WHERE CMND LIKE '" + tbThongTin.Text + "%'";
                }
                tblNhanVien = Class.DB_Config.GetDataToTable(sql);
                dgvDSNV.ItemsSource = null;
                dgvDSNV.ItemsSource = tblNhanVien.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQuayLai_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            ThemNhanVien themNV = new ThemNhanVien();
            themNV.ShowDialog();
            LayDSNhanVien();
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            DataRowView NV_Selected = dgvDSNV.SelectedItem as DataRowView;
            int MaNV = Int32.Parse(NV_Selected.Row["MANV"].ToString());
            ChinhSuaNhanVien chinhSuaNV = new ChinhSuaNhanVien(MaNV);
            chinhSuaNV.ShowDialog();
            LayDSNhanVien();
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            DataRowView NV_Selected = dgvDSNV.SelectedItem as DataRowView;
            string MaNV = NV_Selected.Row["MANV"].ToString();
            string CMND = NV_Selected.Row["CMND"].ToString();

            string message = "Bạn có chắc muốn xoá nhân viên ?";
            // Thực hiện xoá nhân viên
            if (MessageBox.Show(message, "Cảnh báo", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string sql = "DELETE QLTT.NhanVien WHERE MaNV = " + MaNV;

                if (Class.DB_Config.RunSQL(sql))
                {
                    sql = "DROP USER U" + CMND + " CASCADE";
                    Class.DB_Config.RunSQL(sql);
                }
                LayDSNhanVien();
            }
            return;
        }

        private void dgvDSNV_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (dgvDSNV.SelectedIndex < 0)
            {
                btnSua.IsEnabled = false;
                btnXoa.IsEnabled = false;
                return;
            }
            btnSua.IsEnabled = true;
            btnXoa.IsEnabled = true;
            return;
        }
    }
}
