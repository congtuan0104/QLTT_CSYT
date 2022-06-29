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
    /// Interaction logic for ThongBao.xaml
    /// </summary>
    public partial class ThongBao : Window
    {
        public ThongBao()
        {
            InitializeComponent();
            HienThiThongBao();
        }

        private void HienThiThongBao()
        {
            DataTable tbl = new DataTable();
            try
            {
                string sql = "SELECT * FROM QLTT.THONGBAO";
                tbl = Class.DB_Config.GetDataToTable(sql);
                dgvThongBao.ItemsSource = null;
                dgvThongBao.ItemsSource = tbl.DefaultView;
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
    }
}
