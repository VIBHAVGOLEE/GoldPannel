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

    public partial class home : Form
    {   
        public string constring = "Data Source=DESKTOP-30PE2DT;Initial Catalog=Connection;Integrated Security=True";

        public home()
        {
            InitializeComponent();
        }
        public void ShowGridData()
        {
            SqlConnection con = new SqlConnection(constring);
            SqlDataAdapter sqda = new SqlDataAdapter("select * from jhalai where Name = '" + this.txtname.Text + "' AND  Date = '" + dateTimePicker1.Value.ToString("dd MMMM yyyy") + "' ", con);
            DataTable btbl = new DataTable();
            sqda.Fill(btbl);
            dgvsubmit.DataSource = btbl;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btndashboard_MouseHover(object sender, EventArgs e)
        {
            btndaily.BackColor = Color.LightBlue;
        }

        private void btndashboard_MouseLeave(object sender, EventArgs e)
        {
            btndaily.BackColor = Color.Transparent;
        }

        private void home_Load(object sender, EventArgs e)
        {
            
            txtname.Text = "Name";
            txtkachichain.Text = "00";
            txtkachipakki.Text = "0.00";
            txtkherij.Text = "0.00";
            txtname.Text = "";
            txtpakkichain.Text = "00";
            txtpowder.Text = "00";
            txttaar.Text = "0.00";
            txttukda.Text = "0.00";
            SqlConnection con = new SqlConnection(constring);
            SqlDataAdapter sqda = new SqlDataAdapter("select * from jhalai where Date = '" + dateTimePicker1.Value.ToString("dd MMMM yyyy") + "' ", con);
            DataTable btbl = new DataTable();
            sqda.Fill(btbl);
            dgvsubmit.DataSource = btbl;

        }

        private void btnworkers_Click(object sender, EventArgs e)
        {
            Workers w1 = new Workers();
            w1.Show();
            this.Hide();
        }

        private void btndaily_Click(object sender, EventArgs e)
        {

        }

        private void Select_Name_DoubleClick(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("select Name,Kachi_Chain,Powder,Taar,Kherij,Tukda,Kachi_Pakki,Pakki_Chain from jhalai where Name = '" + txtname.Text + "' AND  Date = '" + dateTimePicker1.Value.ToString("dd MMMM yyyy") + "' ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int a = dt.Rows.Count;
            if (a == 0)
            {
                MessageBox.Show("No such Data exist");
            }
            else
            {
                txtname.Text = Convert.ToString(dt.Rows[0]["Name"]);
                txtkachichain.Text = Convert.ToString(dt.Rows[0]["Kachi_Chain"]);
                txtpowder.Text = Convert.ToString(dt.Rows[0]["Powder"]);
                txttaar.Text = Convert.ToString(dt.Rows[0]["Taar"]);
                txtkherij.Text = Convert.ToString(dt.Rows[0]["Kherij"]);
                txttukda.Text = Convert.ToString(dt.Rows[0]["Tukda"]);
                txtkachipakki.Text = Convert.ToString(dt.Rows[0]["Kachi_Pakki"]);
                txtpakkichain.Text = Convert.ToString(dt.Rows[0]["Pakki_Chain"]);
                ShowGridData();
                con.Close();
            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            
           if (txtname.Text == "")
                {
                    MessageBox.Show("you can not leave the name empty", "Empty Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            
            else
            {
                SqlDataAdapter da = new SqlDataAdapter("select Name from jhalai where Name = '" + this.txtname.Text + "' AND  Date = '" + this.dateTimePicker1.Value.ToString("dd MMMM yyyy") + "' ", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int a = dt.Rows.Count;
                if (a > 0)
                {
                    MessageBox.Show("you cannot save same data twice");
                }
                else
                {
                    float kachichain = (float)Convert.ToDouble(txtkachichain.Text);
                    float taar = (float)Convert.ToDouble(txttaar.Text);
                    float kherij = (float)Convert.ToDouble(txtkherij.Text);
                    float tukda = (float)Convert.ToDouble(txttukda.Text);
                    float kachipakki = (float)Convert.ToDouble(txtkachipakki.Text);
                    float pakkichain = (float)Convert.ToDouble(txtpakkichain.Text);
                    float result = ((kachipakki + tukda + kherij + pakkichain) - (kachichain + taar));
                    string q = "insert into jhalai(Name,Kachi_Chain,Powder,Taar,Kherij,Tukda,Kachi_Pakki,Pakki_Chain,Date)values('" + txtname.Text + "','" + txtkachichain.Text + "','" + txtpowder.Text + "','" + txttaar.Text + "','" + txtkherij.Text + "','" + txttukda.Text + "','" + txtkachipakki.Text + "','" + txtpakkichain.Text + "','" + dateTimePicker1.Value.ToString("dd MMMM yyyy") + "')";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        string p = "insert into bhadat(Name,Bhadat,Date)values('" + txtname.Text + "','" + result.ToString() + "','" + dateTimePicker1.Value.ToString("dd MMMM yyyy") + "')";
                        SqlCommand cme = new SqlCommand(p, con);
                        cme.ExecuteNonQuery();
                        txtbhadat.Text = result.ToString();
                        if (con.State == System.Data.ConnectionState.Open)
                        {
                            SqlDataAdapter sqda = new SqlDataAdapter("select id,Bhadat,Date from Bhadat where Name = '" + this.txtname.Text + "' ", con);
                            DataTable btbl = new DataTable();
                            sqda.Fill(btbl);
                            dgvbhadat.DataSource = btbl;
                            if (txtpakkichain.Text.ToString() != "00")
                            {
                                txtbhadat.Text = result.ToString();
                            }
                        }

                    }
                    MessageBox.Show("Your Data save Successfully");
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        ShowGridData();
                    }
                }
                con.Close();
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string q = "update jhalai set Kachi_Chain='" + txtkachichain.Text + "',powder='" + txtpowder.Text + "',Taar='" + txttaar.Text + "',Kherij='" + txtkherij.Text + "',Tukda='" + txttukda.Text + "',Kachi_Pakki='" + txtkachipakki.Text + "',Pakki_Chain='" + txtpakkichain.Text + "' where Name = '" + txtname.Text + "' AND  Date = '" + dateTimePicker1.Value.ToString("dd MMMM yyyy") + "' ";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("SuccessFully Update");
            if (con.State == System.Data.ConnectionState.Open)
            {
                ShowGridData();
            }
            float kachichain = (float)Convert.ToDouble(txtkachichain.Text);
            float taar = (float)Convert.ToDouble(txttaar.Text);
            float kherij = (float)Convert.ToDouble(txtkherij.Text);
            float tukda = (float)Convert.ToDouble(txttukda.Text);
            float kachipakki = (float)Convert.ToDouble(txtkachipakki.Text);
            float pakkichain = (float)Convert.ToDouble(txtpakkichain.Text);
            float result = ((kachipakki+tukda+kherij+pakkichain)-(kachichain+taar));
            if (txtpakkichain.Text != "00")
            {
                string p= "update Bhadat set name='" + txtname.Text + "',Bhadat='" + result.ToString() + "'where Name = '" + this.txtname.Text + "' AND  Date = '" + dateTimePicker1.Value.ToString("dd MMMM yyyy") + "' ";
                SqlCommand cme = new SqlCommand(p, con);
                cme.ExecuteNonQuery();
                txtbhadat.Text = result.ToString();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    SqlDataAdapter sqda = new SqlDataAdapter("select id,Bhadat,Date from Bhadat where Name = '" + this.txtname.Text + "' ", con);
                    DataTable btbl = new DataTable();
                    sqda.Fill(btbl);
                    dgvbhadat.DataSource = btbl;
                }
                con.Close();
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
            {
                SqlConnection con = new SqlConnection(constring);
                con.Open();
                DialogResult result = MessageBox.Show("Are you sure You want delete the row", "Delete Row Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                string q = "delete from jhalai where Name = '" + this.txtname.Text + "' AND  Date = '" + dateTimePicker1.Value.ToString("dd MMMM yyyy") + "' ";
                string p = "delete from bhadat where Name = '"+this.txtname.Text+"' AND Date='" + dateTimePicker1.Value.ToString("dd MMMM yyyy") + "' ";
                SqlCommand cmd = new SqlCommand(q, con);
                SqlCommand amd = new SqlCommand(p, con);
                    cmd.ExecuteNonQuery();
                    amd.ExecuteNonQuery();                
                    MessageBox.Show("Delete SuccessFully");
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        ShowGridData();
                    }
                if (con.State == System.Data.ConnectionState.Open)
                {
                    SqlDataAdapter sqda = new SqlDataAdapter("select id,Bhadat,Date from Bhadat where Name = '" + this.txtname.Text + "' ", con);
                    DataTable btbl = new DataTable();
                    sqda.Fill(btbl);
                    dgvbhadat.DataSource = btbl;

                }
                con.Close();
                }
            

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtname.Text = "Name";
            txtkachichain.Text = "00";
            txtkachipakki.Text = "0.00";
            txtkherij.Text = "0.00";
            txtname.Text = "";
            txtpakkichain.Text = "00";
            txtpowder.Text = "00";
            txttaar.Text = "0.00";
            txttukda.Text = "0.00";
        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Select_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtname.Text = Select_Name.SelectedItem.ToString();
        }

        private void Select_Name_Click(object sender, EventArgs e)
        {
            txtname.Text = Select_Name.SelectedItem.ToString();
        }

        private void btnresources_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void dgvsubmit_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvsubmit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                txtname.Text = dgvsubmit.Rows[e.RowIndex].Cells["Name"].FormattedValue.ToString();
                txtkachichain.Text = dgvsubmit.Rows[e.RowIndex].Cells["Kachi_Chain"].FormattedValue.ToString();
                txttaar.Text = dgvsubmit.Rows[e.RowIndex].Cells["Taar"].FormattedValue.ToString();
                txtpowder.Text = dgvsubmit.Rows[e.RowIndex].Cells["Powder"].FormattedValue.ToString();
                txtkherij.Text = dgvsubmit.Rows[e.RowIndex].Cells["Kherij"].FormattedValue.ToString();
                txttukda.Text = dgvsubmit.Rows[e.RowIndex].Cells["Tukda"].FormattedValue.ToString();
                txtkachipakki.Text = dgvsubmit.Rows[e.RowIndex].Cells["Kachi_Pakki"].FormattedValue.ToString();
                txtpakkichain.Text = dgvsubmit.Rows[e.RowIndex].Cells["Pakki_Chain"].FormattedValue.ToString();

            }
        }

        private void txtname_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtname.Text))
            {
                e.Cancel = true;
                txtname.Focus();
                errorProvider2.SetError(txtname, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider2.SetError(txtname, "");
            }
        }

        private void txtname_Click(object sender, EventArgs e)
        {
            
        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsLetter(e.KeyChar)&& !char.IsControl(e.KeyChar)&& !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            control.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            control.Minimize(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            control.Domaximize(this, btn);
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            login lg = new login();
            lg.Show();
            
        }
    }
}
