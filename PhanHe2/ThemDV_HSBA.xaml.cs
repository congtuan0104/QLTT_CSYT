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
    /// Interaction logic for ThemDV_HSBA.xaml
    /// </summary>
    public partial class ThemDV_HSBA : Window
    {
        public int Ma;
        public ThemDV_HSBA(int mahs)
        {
            InitializeComponent();
            LayDSDV(mahs);
            Ma = mahs;
        }

        private void LayDSDV(int mahs)
        {
            try
            {
                List<DichVu> dsdv = new List<DichVu>();
                OracleCommand command = new OracleCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM QLTT.DICHVU " +
                    "WHERE MADV NOT IN (SELECT MADV " +
                    "FROM QLTT.V_CT_HSBA WHERE MAHSBA = " + mahs + ")";
                command.Connection = Class.DB_Config.Con;
                OracleDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DichVu dv = new DichVu();
                    dv.CHON = false;
                    dv.MADV = reader.GetInt32(0);
                    dv.TENDV = reader.GetString(1);
                    dv.GIADV = reader.GetDecimal(2);
                    dv.NgayTH = DateTime.Now;
                    dsdv.Add(dv);
                }
                reader.Close();

                dgvDSDV.ItemsSource = null;
                dgvDSDV.ItemsSource = dsdv;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            List<DichVu> dsdv = dgvDSDV.ItemsSource as List<DichVu>;
            foreach (DichVu dv in dsdv)
            {
                if (dv.CHON)
                {
                    string sql = "INSERT INTO QLTT.HSBA_DV(MAHSBA, MADV, NGAY, MAKTV, KETQUA) " +
                                 "VALUES(:mahsba, :madv, :ngay, :maktv, :ketqua)";
                    try
                    {
                        OracleCommand command = new OracleCommand();
                        command.CommandType = CommandType.Text;
                        command.CommandText = sql;
                        command.Connection = Class.DB_Config.Con;
                        command.Parameters.Add(":mahsba", OracleDbType.Int32).Value = Ma;
                        command.Parameters.Add(":madv", OracleDbType.Int32).Value = dv.MADV;
                        command.Parameters.Add(":ngay", OracleDbType.Date).Value = dv.NgayTH;
                        command.Parameters.Add(":maktv", OracleDbType.Int32).Value = dv.MAKTV;
                        command.Parameters.Add(":ketqua", OracleDbType.NVarchar2).Value = dv.KETLUAN;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Thêm dịch vụ "+ dv.TENDV +" thất bại!\n" + ex.Message);
                    }
                }
            }
            this.Close();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        public class DichVu
        {
            public bool CHON { get; set; }
            public int MADV { get; set; }
            public string TENDV { get; set; }
            public DateTime NgayTH { get; set; }
            public decimal GIADV { get; set; }
            public int MAKTV { get; set; }
            public string KETLUAN { get; set; }
        }
    }
}
