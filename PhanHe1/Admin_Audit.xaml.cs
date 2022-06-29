using System;
using System.Collections.Generic;
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
using System.Data;

namespace QLTT_CSYT
{
    /// <summary>
    /// Interaction logic for Admin_Audit.xaml
    /// </summary>
    public partial class Admin_Audit : Window
    {
        public Admin_Audit()
        {
            InitializeComponent();
        }

        private void En_Audit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Dis_Audit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rd_Standard_Checked(object sender, RoutedEventArgs e)
        {
            DataTable tbl = new DataTable();
            try
            {
                //string sql = "SELECT AUDIT_TYPE, EXTENDED_TIMESTAMP, DB_USER, OBJECT_SCHEMA, OBJECT_NAME, STATEMENT_TYPE FROM DBA_COMMON_AUDIT_TRAIL";
                string sql = "select DBUSERNAME, ACTION_NAME," +
                    " OBJECT_NAME, EVENT_TIMESTAMP" +
                    " from unified_audit_trail WHERE AUDIT_TYPE = 'Standard'";
                tbl = Class.DB_Config.GetDataToTable(sql);
                dgvAudit.ItemsSource = null;
                dgvAudit.ItemsSource = tbl.DefaultView;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rd_FGA_Checked(object sender, RoutedEventArgs e)
        {
            DataTable tbl = new DataTable();
            try
            {
                string sql = "select DBUSERNAME, ACTION_NAME, OBJECT_SCHEMA," +
                    " OBJECT_NAME , FGA_POLICY_NAME, EVENT_TIMESTAMP" +
                    " from unified_audit_trail WHERE AUDIT_TYPE = 'FineGrainedAudit'";
                tbl = Class.DB_Config.GetDataToTable(sql);
                dgvAudit.ItemsSource = null;
                dgvAudit.ItemsSource = tbl.DefaultView;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

