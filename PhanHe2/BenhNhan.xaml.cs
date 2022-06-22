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
    /// Interaction logic for BenhNhan.xaml
    /// </summary>
    public partial class BenhNhan : Window
    {
        DataTable tblBenhNhan = new DataTable();
        public BenhNhan()
        {
            InitializeComponent();
            LayDSBenhNhan();
        }

        private void LayDSBenhNhan()
        {
            try
            {              
                string sql = "SELECT * FROM QLTT.BENHNHAN";
                tblBenhNhan = Class.DB_Config.GetDataToTable(sql);
                dgvDSBN.ItemsSource = tblBenhNhan.DefaultView;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            if(tbThongTin.Text == "" || tbThongTin == null)
            {
                MessageBox.Show("Bạn chưa nhập thông tin tìm kiếm");
                return;
            }

            if(cbTimKiemTheo.SelectedIndex < 0)
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
                    sql = "Select * from QLTT.BENHNHAN WHERE MaBN = " + MaBN;
                }
                else
                {
                    sql = "Select * from QLTT.BENHNHAN " +
                        "WHERE CMND LIKE '" + tbThongTin.Text + "%'";
                }
                tblBenhNhan = Class.DB_Config.GetDataToTable(sql);
                dgvDSBN.ItemsSource = null;
                dgvDSBN.ItemsSource = tblBenhNhan.DefaultView;
            }
            catch(Exception ex)
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
            ThemBenhNhan themBenhNhan = new ThemBenhNhan();
            themBenhNhan.Show();
            this.Close();
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            DataRowView BN_Selected = dgvDSBN.SelectedItem as DataRowView;
            int MaBN = Int32.Parse(BN_Selected.Row["MABN"].ToString());
            ChinhSuaBenhNhan chinhSuaBenhNhan = new ChinhSuaBenhNhan(MaBN);
            chinhSuaBenhNhan.Show();
            this.Close();
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            DataRowView BN_Selected = dgvDSBN.SelectedItem as DataRowView;
            string MaBN = BN_Selected.Row["MABN"].ToString();            
            
            string message = "Bạn có chắc muốn xoá bệnh nhân ?";
            // Thực hiện xoá khách hàng
            if (MessageBox.Show(message, "Cảnh báo", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string sql = "DELETE QLTT.BenhNhan WHERE MaBN = " + MaBN;
                
                Class.DB_Config.RunSqlDel(sql);
            }
            return;
        }

        private void dgvDSBN_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (dgvDSBN.SelectedIndex < 0)
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
