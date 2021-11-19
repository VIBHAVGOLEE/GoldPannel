using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ui
{
    public partial class Form3 : Form
    {
        public string constring = "Data Source=DESKTOP-30PE2DT;Initial Catalog=Connection;Integrated Security=True";
        public string n = "mukesh";
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand("SELECT Image FROM Images WHERE Name = '"+n+"' ", con));
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count == 1)
            {
                Byte[] data = new Byte[0];
                data = (Byte[])(dataSet.Tables[0].Rows[0]["Image"]);
                MemoryStream mem = new MemoryStream(data);
                pbuser.Image = Image.FromStream(mem);
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            double totalkc = 0;
            double totalpc = 0;
            double totalk = 0;
            double totalt = 0;
            double totalkp = 0;
            double wastage = 0;
            double totalb = 0;
            int totalwd = 0;
            int totaldd = 0;
            SqlConnection con = new SqlConnection(constring);
            SqlDataAdapter sqdakc = new SqlDataAdapter("SELECT SUM(Kachi_Chain) As Totalkc FROM jhalai WHERE Name='" + txtname.Text + "' AND  (Date BETWEEN '" + dtpstartdate.Value.ToString() + "' AND '" + dtpenddate.Value.ToString() + "') ", con);
            SqlDataAdapter sqdapc = new SqlDataAdapter("SELECT SUM(Pakki_Chain) As Totalpc FROM jhalai WHERE Name='" + txtname.Text + "' AND  (Date BETWEEN '" + dtpstartdate.Value.ToString() + "' AND '" + dtpenddate.Value.ToString() + "') ", con);
            SqlDataAdapter sqdak = new SqlDataAdapter("SELECT SUM(Kherij) As Totalk FROM jhalai WHERE Name='" + txtname.Text + "' AND  (Date BETWEEN '" + dtpstartdate.Value.ToString() + "' AND '" + dtpenddate.Value.ToString() + "') ", con);
            SqlDataAdapter sqdat = new SqlDataAdapter("SELECT SUM(Tukda) As Totalt FROM jhalai WHERE Name='" + txtname.Text + "' AND  (Date BETWEEN '" + dtpstartdate.Value.ToString() + "' AND '" + dtpenddate.Value.ToString() + "') ", con);
            SqlDataAdapter sqdakp = new SqlDataAdapter("SELECT SUM(Kachi_Pakki) As Totalkp FROM jhalai WHERE Name='" + txtname.Text + "' AND  (Date BETWEEN '" + dtpstartdate.Value.ToString() + "' AND '" + dtpenddate.Value.ToString() + "') ", con);
            SqlDataAdapter sqdab = new SqlDataAdapter("SELECT SUM(Bhadat) As Totalb FROM bhadat WHERE Name='" + txtname.Text + "' AND  (Date BETWEEN '" + dtpstartdate.Value.ToString() + "' AND '" + dtpenddate.Value.ToString() + "') ", con);
            SqlDataAdapter sqdawd = new SqlDataAdapter("SELECT Count(Kachi_Chain) As Totalwd FROM jhalai WHERE Name='" + txtname.Text + "' AND  (Date BETWEEN '" + dtpstartdate.Value.ToString() + "' AND '" + dtpenddate.Value.ToString() + "') ", con);
            SqlDataAdapter sqdadd = new SqlDataAdapter("SELECT DATEDIFF (day, '" + dtpstartdate.Value.ToString() + "','" + dtpenddate.Value.ToString() + "') As Totaldd ", con);

            DataTable btblkc = new DataTable();
            DataTable btblpc = new DataTable();
            DataTable btblk = new DataTable();
            DataTable btblt = new DataTable();
            DataTable btblkp = new DataTable();
            DataTable btblb = new DataTable();
            DataTable btblwd = new DataTable();
            DataTable btbldd = new DataTable();

            sqdakc.Fill(btblkc);
            sqdapc.Fill(btblpc);
            sqdak.Fill(btblk);
            sqdat.Fill(btblt);
            sqdakp.Fill(btblkp);
            sqdab.Fill(btblb);
            sqdawd.Fill(btblwd);
            sqdadd.Fill(btbldd);

            totalkc = Convert.ToDouble(btblkc.Rows[0]["Totalkc"]);
            totalpc = Convert.ToDouble(btblpc.Rows[0]["Totalpc"]);
            totalk = Convert.ToDouble(btblk.Rows[0]["Totalk"]);
            totalt = Convert.ToDouble(btblt.Rows[0]["Totalt"]);
            totalkp = Convert.ToDouble(btblkp.Rows[0]["Totalkp"]);
            totalb = Convert.ToDouble(btblb.Rows[0]["Totalb"]);
            totalwd = Convert.ToInt32(btblwd.Rows[0]["Totalwd"]);
            totaldd = Convert.ToInt32(btbldd.Rows[0]["Totaldd"]);
            MessageBox.Show(totaldd.ToString());

            txttkachichain.Text = totalkc.ToString();
            totalkc = totalkc*25;
            txtsallery.Text = totalkc.ToString();
            txttpakkichain.Text = totalpc.ToString();
            wastage = totalk + totalt + totalkp;
            txttwastage.Text = wastage.ToString();
            txttbhadat.Text = totalb.ToString();
            txtworkingdays.Text = totalwd.ToString();
            totaldd = (totaldd+1) - totalwd;
            txttleave.Text = totaldd.ToString();


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnworkers_Click(object sender, EventArgs e)
        {
            Workers w = new Workers();
            w.Show();
            this.Hide();
        }

        private void btndaily_Click(object sender, EventArgs e)
        {
            home h = new home();
            h.Show();
            this.Hide();
        }

        private void btnresources_Click(object sender, EventArgs e)
        {

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
