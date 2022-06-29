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
    /// Interaction logic for Admin_Object.xaml
    /// </summary>
    public partial class Admin_Object : Window
    {
        public Admin_Object()
        {
            InitializeComponent();
            HienThi("Tất cả");
        }

        private void HienThi(string ob)
        {
            DataTable tbl = new DataTable();
            string sql = "SELECT OBJECT_NAME, OBJECT_TYPE, CREATED, STATUS" +
                " FROM USER_OBJECTS WHERE OBJECT_TYPE != 'SEQUENCE'";
            if (ob != "Tất cả")
            {
                sql = sql + " AND OBJECT_TYPE = '" + ob.ToUpper() + "'";
            }
            tbl = Class.DB_Config.GetDataToTable(sql);
            dgvObject.ItemsSource = null;
            dgvObject.ItemsSource = tbl.DefaultView;
        }

        private void Object_Checked(object sender, RoutedEventArgs e)
        {
            var button = sender as RadioButton;
            HienThi(button.Content.ToString());
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Admin_CreateTable admin_CreateTable = new Admin_CreateTable();
            admin_CreateTable.ShowDialog();
            HienThi("Tất cả");
        }
    }
}
