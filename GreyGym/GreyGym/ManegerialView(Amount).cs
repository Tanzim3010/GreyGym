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
using System.Xml.Linq;

namespace project
{
    public partial class ManegerialView_Amount_ : Form
    {
        public ManegerialView_Amount_()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-QTAP79E\\SQLEXPRESS;Initial Catalog=GreyGym;Integrated Security=True;Encrypt=False";
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select Amount.* , UserInfo.Name as 'User Name',Package.PackageName as 'Package Name' from Amount inner join UserInfo on UserInfo.ID = Amount.UserId inner join Package on Package.ID = Amount.PackageId ;select * from UserInfo ;select * from Package";

                DataSet ds = new DataSet();

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                DataTable dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Refresh();
                dataGridView1.ClearSelection();

                DataTable dt1 = ds.Tables[1];
                cmbUser.DataSource = dt1;
                cmbUser.ValueMember = "ID";
                cmbUser.DisplayMember = "Name";
                

                DataTable dt2 = ds.Tables[2];
                cmbPackage.DataSource = dt2;
                cmbPackage.ValueMember = "ID";
                cmbPackage.DisplayMember = "PackageName";
                
                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ManegerialView_Amount__Load(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                cmbUser.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                cmbPackage.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                cmbMethod.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                cmbPaymetStatus.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                
              
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.RefreshAll();
        }

        private void RefreshAll()
        {
            txtId.Text = "Auto Generated";
            cmbMethod.Text = "";
            cmbPackage.Text = "";
            cmbPaymetStatus.Text = "";
            cmbUser.Text = "";
            txtAmount.Text = "";

            dataGridView1.ClearSelection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string userId = cmbUser.SelectedValue.ToString();
            string PackageId = cmbPackage.SelectedValue.ToString();
            int amount = Convert.ToInt32( txtAmount.Text);
            string Method = cmbMethod.Text;
            string Status = cmbPaymetStatus.Text;


            if(id == "Auto Generated")
            {
                try
                {

                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "Data Source=DESKTOP-QTAP79E\\SQLEXPRESS;Initial Catalog=GreyGym;Integrated Security=True;Encrypt=False";
                    con.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText = $"insert into Amount values ({userId},{PackageId},{amount},'{Method}','{Status}')";

                    cmd.ExecuteNonQuery();



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                try
                {

                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "Data Source=DESKTOP-QTAP79E\\SQLEXPRESS;Initial Catalog=GreyGym;Integrated Security=True;Encrypt=False";
                    con.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText = $"update Amount set UserId ={userId} , PackageId = {PackageId} , Amount = {amount} , Method = '{Method}',PaymentStatus = '{Status}' where ID = {id}";

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Updated Successfully");
                    this.LoadData();




                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            if (id == "Auto Generated")
            {
                MessageBox.Show("Please Select a row first.");
                return;
            }

            var result = MessageBox.Show("Are You Sure?", "Confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }
            else
            {
                try
                {

                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "Data Source=DESKTOP-QTAP79E\\SQLEXPRESS;Initial Catalog=GreyGym;Integrated Security=True;Encrypt=False";
                    con.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText = $"delete from UserInfo where ID = {id}";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Deleted Successfully");
                    this.LoadData();
                    this.RefreshAll();



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
