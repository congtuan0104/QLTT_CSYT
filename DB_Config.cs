using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using Oracle.DataAccess.Client;
using System.Configuration;

namespace QLTT_CSYT.Class
{
    class DB_Config
    {
        public static OracleConnection Con;  //Khai báo đối tượng kết nối
                                   

        public static bool Connect(string username, string password)
        {
            string host = "localhost";
            string port = "1521";
            string sid = "xe";

            string strConn = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = "
            + host + ")(PORT = " + port + "))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = "
            + sid + ")));Password=" + password + ";User ID=" + username;


            Con = new OracleConnection(strConn);

            try
            {
                if (Con == null)
                {
                    Con = new OracleConnection(strConn);
                    return true;
                }
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                    //MessageBox.Show("Đăng nhập thành công");     
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static void Disconnect()
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();   	//Đóng kết nối
                Con.Dispose(); 	//Giải phóng tài nguyên
                Con = null;
                MessageBox.Show("Đã ngắt kết nối");
            }
        }

        //Lấy dữ liệu vào bảng
        public static DataTable GetDataToTable(string sql)
        {
            sql = "((" + sql + "))";
            OracleDataAdapter DataAdapter = new OracleDataAdapter(); //Định nghĩa đối tượng thuộc lớp SqlDataAdapter
            //Tạo đối tượng thuộc lớp OracleCommand
            DataAdapter.SelectCommand = new OracleCommand();
            DataAdapter.SelectCommand.Connection = DB_Config.Con; //Kết nối cơ sở dữ liệu
            DataAdapter.SelectCommand.CommandText = sql; //Lệnh SQL
            //Khai báo đối tượng table thuộc lớp DataTable
            DataTable table = new DataTable();
            DataAdapter.Fill(table);
            return table;
        }

        public static bool CheckValue(string sql)
        {
            sql = "((" + sql + "))";
            OracleDataAdapter dap = new OracleDataAdapter(sql, Con);
            DataTable table = new DataTable();
            dap.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else return false;
        }

        public static void RunSQL(string sql)
        {
            sql = "((" + sql + "))";
            OracleCommand cmd;
            cmd = new OracleCommand();
            cmd.Connection = Con;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }

        public static void RunSqlDel(string sql)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = DB_Config.Con;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }

        public static OracleDataReader GetDataToReader(string sql)
        {
            sql = "((" + sql + "))";
            OracleCommand command = new OracleCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = sql;
            command.Connection = DB_Config.Con;
            OracleDataReader reader = command.ExecuteReader();
            return reader;
        }
    }

}
