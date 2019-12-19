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

namespace Client
{
    public partial class formLogin : Form
    {
        public ClientSetting Client { get; set; }
        public formLogin()
        {
            InitializeComponent();
            btnLogin.Enabled = true;
        }

        public String Textb()
        {
            return txbName.Text;
        }

        public void slblU(String v)
        {
            lblName.Text = v;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            btnLogin.Enabled = true; 
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login nd = new Login(txbName.Text, txbPassword.Text);
            try
            {
                string name = txbName.Text.Trim();
                string pw = txbPassword.Text.Trim();
                if (nd.kiemTraDangNhap(name, pw) == 1)
                {
                    MessageBox.Show("Login sucessed", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Client.Connected += Client_Connected;
                    Client.Connect(); // connect port 2014
                    Client.Send("Connect|" + txbName.Text + "|connected");
                }
                else
                {
                    MessageBox.Show("Error", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Client_Connected(object sender, EventArgs e)
        {
            this.Invoke(Close);
        }

        private void Invoke(Action close)
        {
            throw new NotImplementedException();
        }

        public Register formRegister = new Register();
        private void btnRegister_Click(object sender, EventArgs e)
        {
            formRegister.ShowDialog();
        }

    }
}
