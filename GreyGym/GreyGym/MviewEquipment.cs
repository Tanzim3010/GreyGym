using GreyGym;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MviewWorkout
{
    public partial class MviewEquipment : Form
    {
        public MviewEquipment()
        {
            InitializeComponent();
        }

        private void MviewEquipment_Load(object sender, EventArgs e)
        {
			this.LoadData();
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

				cmd.CommandText = "select * from GymEquipment";

				DataSet ds = new DataSet();

				SqlDataAdapter adp = new SqlDataAdapter(cmd);
				adp.Fill(ds);

				DataTable dt = ds.Tables[0];
				dgvData.DataSource = dt;
				dgvData.AutoGenerateColumns = false;
				dgvData.Refresh();
				dgvData.ClearSelection();

				con.Close();


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
			this.LoadData();
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
			if(e.RowIndex>=0)
			{
				txtId.Text = dgvData.Rows[e.RowIndex].Cells[0].Value.ToString();
				txtEname.Text = dgvData.Rows[e.RowIndex].Cells[1].Value.ToString();
				txtCategory.Text = dgvData.Rows[e.RowIndex].Cells[2].Value.ToString();
				txtQuantity.Text = dgvData.Rows[e.RowIndex].Cells[3].Value.ToString();
				dtpPd.Value = Convert.ToDateTime(dgvData.Rows[e.RowIndex].Cells[4].Value);
				cmbStatus.Text = dgvData.Rows[e.RowIndex].Cells[5].Value.ToString();
			}
        }

        private void button1_Click(object sender, EventArgs e)
        {
			this.Refreshall();
			this.LoadData();
		}

		private void Refreshall()
		{
			txtId.Text = "Auto Generated";
			txtEname.Text = "";
			txtCategory.Text = "";
			txtQuantity.Text = "";
			dtpPd.Text = "";
			cmbStatus.Text = "";

			dgvData.ClearSelection();
		}

        private void button2_Click(object sender, EventArgs e)
        {
			string id = txtId.Text;

			if(id =="Auto Generated")
			{
				MessageBox.Show("Please select a row first");
			}

			var result=MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo);

			if(result == DialogResult.No)
			{
				return;
			}

			else
			{
				try
				{
					SqlConnection con = new SqlConnection();
					con.ConnectionString = ApplicationHelper.cs;
					con.Open();

					SqlCommand cmd = new SqlCommand();
					cmd.Connection = con;

					cmd.CommandText = $"delete from GymEquipment where ID = {id}";
					cmd.ExecuteNonQuery();

					MessageBox.Show("Deleted Successfully");
					this.LoadData();
					this.Refreshall();
					con.Close();

				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}

        }

        private void button4_Click(object sender, EventArgs e)
        {
			string id = txtId.Text;
			string name = txtEname.Text;
			string category = txtCategory.Text;
            int quantity = int.Parse(txtQuantity.Text);
			string status = cmbStatus.Text;

			DateTime purchaseDate = dtpPd.Value;
			string pDate = purchaseDate.ToString("yyyy-MM-dd");

			if (txtEname.Text == "" || txtCategory.Text == "")
			{
				MessageBox.Show(
					"There is no information.\nPlease select the correct one.", "Validation Error", MessageBoxButtons.OK,
					MessageBoxIcon.Warning
				);
				return;
			}



			if (id == "Auto Generated")
			{


				try
				{
					SqlConnection con = new SqlConnection();
					con.ConnectionString = ApplicationHelper.cs;
					con.Open();


					SqlCommand checkCmd = new SqlCommand();
					checkCmd.Connection = con;

					checkCmd.CommandText =$"select count(*) from GymEquipment where Ename='{name}' and Category='{category}'";

					SqlCommand cmd = new SqlCommand();
					cmd.Connection = con;

					cmd.CommandText = $"insert into GymEquipment values ('{name}','{category}','{quantity}','{pDate}','{status}')"; 
					cmd.ExecuteNonQuery();
					con.Close();


					MessageBox.Show("Added Successfully");
					this.LoadData();
					this.Refreshall();


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
					con.ConnectionString = ApplicationHelper.cs;
					con.Open();


					SqlCommand checkCmd = new SqlCommand();
					checkCmd.Connection = con;

					checkCmd.CommandText =$"select count(*) from GymEquipment where Ename='{name}' and Category='{category}' and ID={id}";

					SqlCommand cmd = new SqlCommand();
					cmd.Connection = con;

					cmd.CommandText = $"update GymEquipment set Ename='{name}',Category='{category}',Quantity='{quantity}',PurchaseDate='{pDate}',Status='{status}' where ID={id}";
					cmd.ExecuteNonQuery();

					con.Close();


					MessageBox.Show("Updated  Successfully");
					this.LoadData();
					this.Refreshall();


				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);

				}
			}
			}

        private void btnPaymet_Click_1(object sender, EventArgs e)
        {
			this.Hide();
			ManegerialView_Amount_ mga = new ManegerialView_Amount_();
			mga.Show();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
			this.Hide();
			ManagerialView_User_ mgu = new ManagerialView_User_();
			mgu.Show();
        }

        private void btnPackage_Click(object sender, EventArgs e)
        {
			this.Hide();
			ManagerialView_Packages_ mgp = new ManagerialView_Packages_();
			mgp.Show();
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
			this.Hide();
			ManagerialViewWorkout mgw = new ManagerialViewWorkout();
			mgw.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
			this.Hide();
			ManagerialView_Incident_ mgi = new ManagerialView_Incident_();
			mgi.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
			this.Hide();
			Login lg  = new Login();
			lg.Show();
        }
    }
}
