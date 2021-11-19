using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ui
{
    public partial class login : Form
    {
        public string constring = "Data Source=DESKTOP-30PE2DT;Initial Catalog=Connection;Integrated Security=True";

        public login()
        {
            InitializeComponent();
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            string q = "insert into Login(username,password)values('" + txtuser.Text + "','" + txtpassword.Text +"')";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.ExecuteNonQuery();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
