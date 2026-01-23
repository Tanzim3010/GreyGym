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

					int count = Convert.ToInt32(checkCmd.ExecuteScalar());

					if (count > 0)
					{
						MessageBox.Show("This equipment already exists!","Validation Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
						con.Close();
						return;
					}


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

					int count = Convert.ToInt32(checkCmd.ExecuteScalar());

					if (count > 0)
					{
						MessageBox.Show("Another equipment with the same name and category already exists!","Validation Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
						con.Close();
						return;
					}


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
    }
}
