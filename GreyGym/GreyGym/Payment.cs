using GreyGym;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }

        

       

        


        private void button2_Click(object sender, EventArgs e)
        {
            int number = Convert.ToInt32(txtNagad.Text);
            if (txtNagad.Text == "")
            {
                MessageBox.Show("Enter Number ");
                return;
            }

            if (btnNagad.Enabled)
            {
                try
                {
                    MessageBox.Show("Purchase Successful by Nagad");
                   
                    PackageDash ds = new PackageDash();
                    ds.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

       
        private void Payment_Load(object sender, EventArgs e)
        {
            btnBkash.Visible = false;
            textBkash.Visible = false;
            label2.Visible= false;  
            label3.Visible= false;
            txtNagad.Visible = false;
            btnNagad.Visible = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            

        }

       

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBkash.Visible = true;
                label2.Visible = true;
                btnBkash.Visible = true;
            }
                
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                txtNagad.Visible= true;
                label3.Visible = true;
                btnNagad.Visible = true;
            }
        }

        private void btnBkash_Click_1(object sender, EventArgs e)
        {
            int userId = Session.ID;
            int PackageId = Session.PID;
            int amount = Session.Amount;

            int number = Convert.ToInt32(textBkash.Text);
            if (textBkash.Text == "")
            {
                MessageBox.Show("Enter Number ");
                return;
            }

            if (btnBkash.Enabled)
            {
                try
                {
                    MessageBox.Show("Thank you for your Response");

                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ApplicationHelper.cs;
                    con.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText = $"insert into Amount values ({userId},{PackageId},{amount},'Bkash','Pending')";
                    cmd.ExecuteNonQuery();
                    con.Close();



                    PackageDash ds = new PackageDash();
                    ds.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnNagad_Click(object sender, EventArgs e)
        {
            int userId = Session.ID;
            int PackageId = Session.PID;
            int amount = Session.Amount;

            int number = Convert.ToInt32(txtNagad.Text);
            if (txtNagad.Text == "")
            {
                MessageBox.Show("Enter Number ");
                return;
            }

            if (btnNagad.Enabled)
            {
                try
                {
                    MessageBox.Show("Thank you for your Response");

                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ApplicationHelper.cs;
                    con.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText = $"insert into Amount values ({userId},{PackageId},{amount},'Nagad','Pending')";
                    cmd.ExecuteNonQuery();
                    con.Close();



                    PackageDash ds = new PackageDash();
                    ds.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
