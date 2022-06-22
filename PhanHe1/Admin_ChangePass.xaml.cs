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
    /// Interaction logic for Admin_ChangePass.xaml
    /// </summary>
    public partial class Admin_ChangePass : Window
    {
        public Admin_ChangePass(string username)
        {
            InitializeComponent();
            tbUsername.Text = username;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string user = tbUsername.Text;
            string pass = tbPass.Password;
            string cpass = tbCPass.Password;
            if (pass == null)
            {
                txtWarning.Text = "The password field is required";
                txtWarning.Foreground=Brushes.Red;
                return;
            }
            if (cpass == null)
            {
                txtWarning.Text = "The confirm password field is required";
                txtWarning.Foreground = Brushes.Red;
                return;
            }
            if (pass != cpass)
            {
                txtWarning.Text = "Confirm password must same value with new password";
                txtWarning.Foreground = Brushes.Red;
                return;
            }

            string sql;
            sql = "alter session set \"_oracle_script\"=true";
            Class.DB_Config.RunSQL(sql);


            if (((user[0] >= 'a') && (user[0] <= 'z')) || ((user[0] >= 'A') && (user[0] <= 'Z')))
            {
                sql = "ALTER USER " + user + " IDENTIFIED BY " + pass;
            }
            else
            {
                sql = "ALTER USER '" + user + " IDENTIFIED BY " + pass;
            }
            Class.DB_Config.RunSQL(sql);
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
