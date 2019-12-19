using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Server
{
    public partial class quanlytk : Form
    {
        SqlConnection connection1;
        SqlCommand cmd1;
        string str = @"Data Source=(local);Initial Catalog=PrivateChat;Persist Security Info=True;User ID=appchat;Password=abc";

        SqlDataAdapter adapter1 = new SqlDataAdapter();
        DataTable table1 = new DataTable();
        void loaddata()
        {
            cmd1 = connection1.CreateCommand();
            cmd1.CommandText = "select *from UserData2 ";

            adapter1.SelectCommand = cmd1;
            table1.Clear();
            adapter1.Fill(table1);

            //Fake
            //DataColumn dc = new DataColumn("Name", typeof(String));
            //table1.Columns.Add(dc);
            //dc = new DataColumn("Password", typeof(String));
            //table1.Columns.Add(dc);

            //DataRow workRow = table1.NewRow();
            //workRow[0] = "Chau";
            //workRow[1] = "abc";
            //table1.Rows.Add(workRow);

            dgv.DataSource = table1;
        }

        public quanlytk()
        {
            InitializeComponent();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txbName.ReadOnly = true;
            int i;
            i = dgv.CurrentRow.Index;

            txbName.Text = dgv.Rows[i].Cells[0].Value.ToString();
            txbPass.Text = dgv.Rows[i].Cells[1].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            cmd1 = connection1.CreateCommand();
            cmd1.CommandText = "insert into UserData2 values (' " + txbName.Text + "','" + txbPass.Text + "')";
            cmd1.ExecuteNonQuery();
            loaddata();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            cmd1 = connection1.CreateCommand();
            cmd1.CommandText = "delete from UserData2 where Name= '" + txbName.Text + "'";
            cmd1.ExecuteNonQuery();
            loaddata();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            cmd1 = connection1.CreateCommand();
            cmd1.CommandText = "update UserData2 set Password= '" + txbPass.Text + "' where Name= '" + txbName.Text+"'";
            cmd1.ExecuteNonQuery();
            loaddata();
        }

        private void quanlytk_Load_1(object sender, EventArgs e)
        {
            connection1 = new SqlConnection(str);
            connection1.Open();
            loaddata();
            connection1.Close();
        }

        private void btnCreat_Click(object sender, EventArgs e)
        {
            txbName.ReadOnly = false;
            txbName.Text = "";
            txbPass.Text = "";
        }
    }
}
