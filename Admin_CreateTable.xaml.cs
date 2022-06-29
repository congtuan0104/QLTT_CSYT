using System;
using System.Collections.Generic;
using System.Data.OracleClient;
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
    /// Interaction logic for Admin_CreateTable.xaml
    /// </summary>
    public partial class Admin_CreateTable : Window
    {
        public Admin_CreateTable()
        {
            InitializeComponent();
            List<Table> tbl = new List<Table>();
            dgvTable.ItemsSource = null;
            dgvTable.ItemsSource = tbl;
        }

        private void change(object sender, TextChangedEventArgs e)
        {
            //string sql = "CREATE TABLE " + tbSchema.Text + "." + tbTable.Text + "(" +
            //    "   ";

            //sql = sql + ")";
            //tbScript.Text = sql;
        }

        private void create_script()
        {
            string sql = "CREATE TABLE " + tbSchema.Text + "." + tbTable.Text + "(\n";
            List<Table> cols = dgvTable.ItemsSource as List<Table>;
            for (int i = 0; i < cols.Count; i++)
            {
                Table col = cols[i];
                if (col.ColumnName != null && col.Type != null
                    && col.ColumnName.Length > 0 && col.Type.Length > 0)
                {
                    sql = sql + "   " + col.ColumnName.ToUpper() + " " + col.Type.ToUpper();
                    if (col.PrimaryKey)
                    {
                        sql = sql + " PRIMARY KEY";
                    }
                    if (col.NotNULL)
                    {
                        sql = sql + " NOT NULL";
                    }
                    if (col.Default != null && col.Default.Length > 0)
                    {
                        sql = sql + " DEFULT " + col.Default.ToUpper();
                    }
                    if (i < cols.Count - 1)
                        sql = sql + ",";
                    sql = sql + "\n";
                }
            }
            sql = sql + "\n)";
            tbScript.Text = sql;
        }

        private void change(object sender, RoutedEventArgs e)
        {

            create_script();
        }

        private void change2(object sender, DataGridRowEditEndingEventArgs e)
        {
            create_script();
        }


        private void change4(object sender, EventArgs e)
        {
            create_script();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            Class.DB_Config.RunSQL(tbScript.Text);
            MessageBox.Show("Tạo bảng thành công");
            this.Close();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class Table
    {
        public bool PrimaryKey { get; set; }
        public string ColumnName { get; set; }
        public string Type { get; set; }
        public bool NotNULL { get; set; }
        public string Default { get; set; }
    }
}
