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
using GreyGym;

namespace project
{
    public partial class PackageDash : Form
    {
        public PackageDash()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Insert()
        {
            try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-QTAP79E\\SQLEXPRESS;Initial Catalog=GreyGym;Integrated Security=True;Encrypt=False";
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = $"insert into Package values ('{"Starter"}')";
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Fetch()
        {
            try
            {
                var connection = new SqlConnection();
                connection.ConnectionString = ApplicationHelper.cs;
                connection.Open();

                var cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"select * from Package where PackageName = 'STARTER' ";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                DataTable dt = ds.Tables[0];
                connection.Close();

               

                int Pack = Convert.ToInt32(dt.Rows[0]["ID"]);
                Session.PID = Pack;
                int amount = Convert.ToInt32(dt.Rows[0]["Price"]);
                Session.Amount = amount;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStarter_Click(object sender, EventArgs e)
        {
      
            var sure =MessageBox.Show("Are you sure you want to buy 'STARTER PACKAGE'", "Confirmation", MessageBoxButtons.YesNo);


            
            if (sure == DialogResult.Yes)
            {
                this.Fetch();
                Payment pm = new Payment();
                pm.Show();
                this.Hide();
            }
            else if(sure == DialogResult.No) {
            
                return;
            }
   
        }

        private void btnBasic_Click(object sender, EventArgs e)
        {
            var sure = MessageBox.Show("Are you sure you want to buy 'BASIC PACKAGE'", "Confirmation", MessageBoxButtons.YesNo);



            if (sure == DialogResult.Yes)
            {
                try
                {
                    var connection = new SqlConnection();
                    connection.ConnectionString = ApplicationHelper.cs;
                    connection.Open();

                    var cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = $"select * from Package where PackageName = 'BASIC' ";

                    DataSet ds = new DataSet();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    connection.Close();



                    int Pack = Convert.ToInt32(dt.Rows[0]["ID"]);
                    Session.PID = Pack;
                    int amount = Convert.ToInt32(dt.Rows[0]["Price"]);
                    Session.Amount = amount;

                    Payment pm = new Payment();
                    pm.Show();
                    this.Hide();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                

            }
            else if (sure == DialogResult.No)
            {

                return;
            }

        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            var sure = MessageBox.Show("Are you sure you want to buy 'STUDENT PACKAGE'", "Confirmation", MessageBoxButtons.YesNo);



            if (sure == DialogResult.Yes)
            {

               
            }
            else if (sure == DialogResult.No)
            {

                return;
            }
        }

        private void btnStandard_Click(object sender, EventArgs e)
        {
            var sure = MessageBox.Show("Are you sure you want to buy 'STANDARD PACKAGE'", "Confirmation", MessageBoxButtons.YesNo);



            if (sure == DialogResult.Yes)
            {

               
            }
            else if (sure == DialogResult.No)
            {

                return;
            }

        }

      

        private void button4_Click(object sender, EventArgs e)
        {
            var sure = MessageBox.Show("Are you sure you want to buy 'PREMIUM PACKAGE [12 month]'", "Confirmation", MessageBoxButtons.YesNo);



            if (sure == DialogResult.Yes)
            {

          
            }
            else if (sure == DialogResult.No)
            {

                return;
            }

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
}
