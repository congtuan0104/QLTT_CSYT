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
    /// Interaction logic for Admin_MaHoa.xaml
    /// </summary>
    public partial class Admin_MaHoa : Window
    {
        public Admin_MaHoa()
        {
            InitializeComponent();
            HienThi();
        }

        private void HienThi()
        {
            DataTable tbl = new DataTable();
            try
            {
                //string sql = "SELECT AUDIT_TYPE, EXTENDED_TIMESTAMP, DB_USER, OBJECT_SCHEMA, OBJECT_NAME, STATEMENT_TYPE FROM DBA_COMMON_AUDIT_TRAIL";
                string sql = "SELECT MABN, CMND AS KEY, DIUNGTHUOC AS DIUNGTHUOC_MAHOA," +
                    "QLTT.ENCRYPT_DECRYPT.DECRYPT_BENHNHAN(DIUNGTHUOC, CMND)" +
                    " AS DIUNGTHUOC_GIAIMA FROM BENHNHAN";
                tbl = Class.DB_Config.GetDataToTable(sql);
                dgvMaHoa.ItemsSource = null;
                dgvMaHoa.ItemsSource = tbl.DefaultView;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
