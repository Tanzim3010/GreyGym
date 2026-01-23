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
using project;

namespace GreyGym
{
    public partial class ManagerialView_Packages_ : Form
    {
        public ManagerialView_Packages_()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.cs;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from Package";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                DataTable dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                dataGridView1.Refresh();



            }
            catch(Exception ex)
            {

            }
        }

        private void ManagerialView_Packages__Load(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text= dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtPackage.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                ttxDuration.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                rDesc.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void RefreshAll()
        {
            txtId.Text = "Auto Generated";
            txtPackage.Text = "";
            ttxDuration.Text = "";
            txtPrice.Text = "";
            rDesc.Text = "";

            dataGridView1.ClearSelection();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.RefreshAll();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void AddData()
        {
            string pack = txtPackage.Text;
            int duration = Convert.ToInt32( ttxDuration.Text);
            int price = Convert.ToInt32(txtPrice.Text);
            string desc = rDesc.Text;


            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.cs;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"insert into Package values ('{pack}',{duration},{price},'{desc}')";
                cmd.ExecuteNonQuery();

                MessageBox.Show("Package Added Successfully");

                this.LoadData();
               



            }
            catch (Exception ex)
            {

            }
        }

        private void Update()
        {
            int id = Convert.ToInt32(txtId.Text);
            string Pack = txtPackage.Text;
            int duration = Convert.ToInt32( ttxDuration.Text);
            int price = Convert.ToInt32 (txtPrice.Text);
            string desc = rDesc.Text;
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = $"Update  Package set PackageName = '{Pack}',Duration = {duration} , Price = {price} , Description = '{desc}' where id = {id}";
                
                MessageBox.Show("Updated Successfully");
                cmd.ExecuteNonQuery();
                this.LoadData();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "Auto Generated")
            {
                this.AddData();
            }
            else
            {
                this.Update();
            }

        }

        private void btnUser_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ManagerialView_User_ mgu = new ManagerialView_User_();
            mgu.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Hide();
        }

        private void btnPaymet_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManegerialView_Amount_ mga = new ManegerialView_Amount_();
            mga.Show();
        }

        private void btnDiet_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManagerialView_DietPlan_ mgd = new ManagerialView_DietPlan_();
            mgd.Show();
        }

        private void btnUserpackage_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManagerialView_UserPackage_ mgu = new ManagerialView_UserPackage_();
            mgu.Show();
        }

        private void btnGym_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManagerialView_TrainerUser_ mgt = new ManagerialView_TrainerUser_();
            mgt.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManagerialView_Incident_ mgi = new ManagerialView_Incident_();
            mgi.Show();
        }

        private void Delete()
        {
            int id = Convert.ToInt32(txtId.Text);
            string Pack = txtPackage.Text;
            int duration = Convert.ToInt32(ttxDuration.Text);
            int price = Convert.ToInt32(txtPrice.Text);
            string desc = rDesc.Text;
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = $"delete from Package where id  = {id}";
                var result = MessageBox.Show("Are you sure to delete this package? ", "Confirmation", MessageBoxButtons.YesNo);
             
                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("Deleted Successfully");
                    cmd.ExecuteNonQuery();
                    this.LoadData();
                    con.Close();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            if(id == "Auto Generated")
            {
                MessageBox.Show("Please select a row first");
                return;
            }
            else
            {
                this.Delete();
            }

           
        }
    }
}
