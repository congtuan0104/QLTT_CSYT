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
using Oracle.DataAccess.Client;
using System.Data;
namespace QLTT_CSYT
{
    /// <summary>
    /// Interaction logic for Admin_AddUser.xaml
    /// </summary>
    public partial class Admin_AddUser : Window
    {
        public Admin_AddUser()
        {
            InitializeComponent();


        }

        private void btnName_Click(object sender, RoutedEventArgs e)
        {
            if (user.IsChecked == true)
            {
                string username = txbUser.Text.ToString();
                string password = txbPass.Text.ToString();
                string role = cbRole.Text.ToString();
                string sql;

                sql = "ALTER SESSION SET \"_ORACLE_SCRIPT\" = true";
                Class.Functions.RunSQL(sql);

                if (((username[0] >= 'a') && (username[0] <= 'z')) || ((username[0] >= 'A') && (username[0] <= 'Z')))
                {
                    sql = "CREATE USER " + username + " IDENTIFIED BY " + password;
                }
                else
                {
                    sql = "CREATE USER " + "\"" + username + "\"" + " IDENTIFIED BY " + password;
                }



                Class.Functions.RunSQL(sql);

                if (role != "KHONG")
                {
                    if (((username[0] >= 'a') && (username[0] <= 'z')) || ((username[0] >= 'A') && (username[0] <= 'Z')))
                    {
                        sql = "GRANT " + role + " TO " + username;
                    }
                    else
                    {
                        sql = "GRANT " + role + " TO '" + username + "'";
                    }
                    MessageBox.Show(sql);
                    Class.Functions.RunSQL(sql);
                }

            }

            if (role.IsChecked == true)
            {
                
                string rolename = txbrole.Text.ToString();
                string sql;
                sql = "ALTER SESSION SET \"_ORACLE_SCRIPT\" = true";
                Class.Functions.RunSQL(sql);
                sql = "CREATE ROLE " + rolename;
                
                Class.Functions.RunSQL(sql);
                
            }
            MessageBox.Show("Thao tác thành công");
            this.Close();
        }

        private void rdChecked(object sender, RoutedEventArgs e)
        {
            var button = sender as RadioButton;
            string temp = button.Content.ToString();

            if(temp == "User")
            {
                txbUser.IsEnabled = true;
                txbPass.IsEnabled = true;
                cbRole.IsEnabled = true;
                txbrole.IsEnabled = false;
            }
            else
            {
                txbUser.IsEnabled = false;
                txbPass.IsEnabled = false;
                cbRole.IsEnabled = false;
                txbrole.IsEnabled = true;
            }
            
        }
    }
}