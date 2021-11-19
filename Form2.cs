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

namespace ui
{
    public partial class Workers : Form
    {
        public string constring = "Data Source=DESKTOP-30PE2DT;Initial Catalog=Connection;Integrated Security=True";
        public Workers()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btndaily_Click(object sender, EventArgs e)
        {
            home h1 = new home();
            h1.Show();
            this.Hide();
        }

        private void btnworkers_Click(object sender, EventArgs e)
        {

        }

        private void Workers_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlDataAdapter sqda = new SqlDataAdapter("select * from jhalai", con);
                DataTable btbl = new DataTable();
                sqda.Fill(btbl);
                dataGridView1.DataSource = btbl;

            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                if (comboBox1.SelectedIndex == -1)
                {
                    SqlDataAdapter sqda = new SqlDataAdapter("SELECT * FROM jhalai WHERE Date BETWEEN '" + dtpstartdate.Value.ToString() + "' AND '" + dtpenddate.Value.ToString() + "' ", con);
                    DataTable btbl = new DataTable();
                    sqda.Fill(btbl);
                    dataGridView1.DataSource = btbl;

                }

                else
                {
                    SqlDataAdapter sqda = new SqlDataAdapter("SELECT * FROM jhalai WHERE Name='" + comboBox1.SelectedItem.ToString() + "' AND  (Date BETWEEN '" + dtpstartdate.Value.ToString() + "' AND '" + dtpenddate.Value.ToString() + "') ", con);
                    DataTable btbl = new DataTable();
                    sqda.Fill(btbl);
                    dataGridView1.DataSource = btbl;

                }
            }
        }

        private void btnresources_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            control.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            control.Domaximize(this, btn);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            control.Minimize(this);
        }
    }
}
