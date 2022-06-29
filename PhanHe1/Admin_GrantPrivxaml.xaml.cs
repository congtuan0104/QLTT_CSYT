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

namespace QLTT_CSYT
{
    /// <summary>
    /// Interaction logic for Admin_GrantPrivxaml.xaml
    /// </summary>
    public partial class Admin_GrantPrivxaml : Window
    {
        public Admin_GrantPrivxaml()
        {
            InitializeComponent();
        }

        private void btn_GrantPriv_Click(object sender, RoutedEventArgs e)
        {
            if(cbRole.IsEnabled == false)
            {
                string priv = cbPriv.Text;
                string tab = cbTable.Text;
                string grantee = tbGrantee.Text.ToUpper();
                string sql;

                if (priv == "Xem")
                    priv = "SELECT";
                else if (priv == "Xoá")
                    priv = "DELETE";
                else
                    priv = "UPDATE";

                //sql = "alter session set \"_oracle_script\"=true";
                //Class.DB_Config.RunSqlDel(sql);

                if (((grantee[0] >= 'a') && (grantee[0] <= 'z')) || ((grantee[0] >= 'A') && (grantee[0] <= 'Z')))
                {
                    sql = "GRANT " + priv + " ON " + tab + " TO " + grantee;
                }
                else
                {
                    sql = "GRANT " + priv + " ON " + tab + " TO '" + grantee + "'";
                }

                Class.DB_Config.RunSqlDel(sql);
                
            }
            else
            {
                string role = cbRole.Text;
                string grantee = tbGrantee.Text.ToUpper();
                string sql;
               

                //sql = "alter session set \"_oracle_script\"=true";
                //Class.DB_Config.RunSqlDel(sql);

                if (((grantee[0] >= 'a') && (grantee[0] <= 'z')) || ((grantee[0] >= 'A') && (grantee[0] <= 'Z')))
                {
                    sql = "GRANT " + role + " TO " + grantee;
                }
                else
                {
                    sql = "GRANT " + role + " TO '" + grantee + "'";
                }

                Class.DB_Config.RunSqlDel(sql);
            }
            MessageBox.Show("Thao tác gán quyền thành công");
        }

        private void btnRevoke_Click(object sender, RoutedEventArgs e)
        {
            if(cbRole.IsEnabled == false)
            {
                string priv = cbPriv.Text;
                string tab = cbTable.Text;
                string grantee = tbGrantee.Text.ToUpper();
                string sql;

                if (priv == "Xem")
                    priv = "SELECT";
                else if (priv == "Xoá")
                    priv = "DELETE";
                else
                    priv = "UPDATE";

                //sql = "alter session set \"_oracle_script\"=true";
                //Class.DB_Config.RunSqlDel(sql);

                if (((grantee[0] >= 'a') && (grantee[0] <= 'z')) || ((grantee[0] >= 'A') && (grantee[0] <= 'Z')))
                {
                    sql = "REVOKE " + priv + " ON " + tab + " FROM " + grantee;
                }
                else
                {
                    sql = "REVOKE " + priv + " ON " + tab + " FROM '" + grantee + "'";
                }
   

                Class.DB_Config.RunSqlDel(sql);
            }
            else
            {
                string role = cbRole.Text;
                string grantee = tbGrantee.Text.ToUpper();
                string sql;

                //sql = "alter session set \"_oracle_script\"=true";
                //Class.DB_Config.RunSQL(sql);

                if (((grantee[0] >= 'a') && (grantee[0] <= 'z')) || ((grantee[0] >= 'A') && (grantee[0] <= 'Z')))
                {
                    sql = "REVOKE " + role + " FROM " + grantee;
                }
                else
                {
                    sql = "REVOKE " + role + " FROM '" + grantee + "'";
                }
    
                Class.DB_Config.RunSqlDel(sql);
            }
            MessageBox.Show("Thao tác thu hồi quyền thành công");
        }

        private void rdCheck(object sender, RoutedEventArgs e)
        {
            var button = sender as RadioButton;

            string temp = button.Content.ToString();
            
            if (temp == "Grant/Revoke Privileges To User/Role")
            {
                cbRole.IsEnabled = false;
                cbPriv.IsEnabled = true;
                cbTable.IsEnabled = true;

            }
            else
            {
                cbRole.IsEnabled = true;
                cbPriv.IsEnabled = false;
                cbTable.IsEnabled = false;

            }
        }

        
    }
}
