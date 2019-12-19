using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Client
{
    public class Login
    {
        private string taikhoan, password;
        public SqlConnection conn = new SqlConnection(@"Data Source=(local);Initial Catalog=PrivateChat;User ID=appchat;Password=abc"); // connect server from sql
        public Login()
        {
            taikhoan = password = "";
        }
        public Login(string tk, string pw)
        {
            taikhoan = tk;
            password = pw;
        }
        public int kiemTraDangNhap(string tk, string pw)
        {
            try
            {
                conn.Open();
                string sql = "SELECT *from UserData2 WHERE Name='" + taikhoan + "' and Password= '" + password + "'"; // from sql
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read() == true)
                {
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }
        public void themSql(string tk, string pass)
        {
            try
            {
                conn.Open();
                string sqlINSERT = "INSERT INTO UserData2 VALUES (@Name,@Password)";
                SqlCommand cmd = new SqlCommand(sqlINSERT, conn);
                cmd.Parameters.AddWithValue("Name", tk);
                cmd.Parameters.AddWithValue("Password", pass);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đăng ký thành công");
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Đăng ký không thành công");
            }
        }
    }
}
