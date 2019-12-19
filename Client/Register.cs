using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace Client
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập vào tên đăng nhập");
                txtName.Focus();
            }
            else if (txtPass.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập Password");
            }
            else if (txtConfirm.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập xác nhận mật khẩu");
            }
            else if (txtPass.Text != txtConfirm.Text)
            {
                MessageBox.Show("Mật khẩu và xác nhận mật khẩu chưa đúng");
                txtConfirm.Focus();
                txtConfirm.SelectAll();
            }
            Login nd1 = new Login(txtName.Text, txtPass.Text);
            nd1.themSql(txtName.Text, txtPass.Text);
            this.Close();
        }
    }
}
