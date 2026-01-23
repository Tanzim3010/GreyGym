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

namespace MviewWorkout
{
	public partial class ManagerialViewWorkout : Form
	{
		public ManagerialViewWorkout()
		{
			InitializeComponent();
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void ManagerialViewWorkout_Load(object sender, EventArgs e)
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

				cmd.CommandText = "select * from WorkoutPlan";

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

		private void button2_Click(object sender, EventArgs e)
		{
			this.LoadData();
		}

		private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				txtId.Text = dgvData.Rows[e.RowIndex].Cells[0].Value.ToString();
				txtName.Text = dgvData.Rows[e.RowIndex].Cells[1].Value.ToString();
				rtxtDesc.Text = dgvData.Rows[e.RowIndex].Cells[2].Value.ToString();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.RefreshAll();
		}

		private void RefreshAll()
		{
			txtId.Text = "Auto Generated";
			txtName.Text = "";
			rtxtDesc.Text = "";

			dgvData.ClearSelection();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			string id = txtId.Text;
			string name = txtName.Text;
			string desc = rtxtDesc.Text;

			if (txtName.Text == "" || rtxtDesc.Text == "")
			{
				MessageBox.Show("There is no information.\nPlease select the correct one.","Validation Error",MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
				MessageBox.Show($"Name: '{txtName.Text}'\nDesc: '{rtxtDesc.Text}'");
				return;
			}



			if (id == "Auto Generated")
			{


				try
				{
					SqlConnection con = new SqlConnection();
					con.ConnectionString = ApplicationHelper.cs;
					con.Open();

					SqlCommand cmd = new SqlCommand();
					cmd.Connection = con;

					cmd.CommandText = $"insert into WorkoutPlan values('{name}','{desc}')"; ;
					cmd.ExecuteNonQuery();

					con.Close();


					MessageBox.Show("Added Successfully");
					this.LoadData();
					this.RefreshAll();


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

					SqlCommand cmd = new SqlCommand();
					cmd.Connection = con;

					cmd.CommandText = $"update WorkoutPlan set PlanName='{name}',Workout='{desc}' where ID={id}";
					cmd.ExecuteNonQuery();

					con.Close();


					MessageBox.Show("Updated  Successfully");
					this.LoadData();
					this.RefreshAll();


				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);

				}
			}

			
		}

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
			string id = txtId.Text;

			if (id == "Auto Generated")
			{
				MessageBox.Show("Please select a row first");
			}

			var result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo);

			if (result == DialogResult.No)
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

					cmd.CommandText = $"delete from WorkoutPlan where ID = {id}";
					cmd.ExecuteNonQuery();

					MessageBox.Show("Deleted Successfully");
					this.LoadData();
					this.RefreshAll();
					con.Close();

				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}
    }
}
