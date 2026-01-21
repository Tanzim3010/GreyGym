using GreyGym;
using Microsoft.SqlServer.Server;
using project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace GreyGym
{
    public partial class EmployeeView_TrainerUser_ : Form
    {
        public EmployeeView_TrainerUser_()
        {
            InitializeComponent();
        }

        private void LoadTrainers()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select ID,Name from UserInfo where UserType = 'Trainer'";

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                cmbTrainerID.Items.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    cmbTrainerID.Items.Add(row["ID"].ToString());
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadWorkouts()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select PlanName from WorkoutPlan";

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                cmbWorkout.Items.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    cmbWorkout.Items.Add(row["PlanName"].ToString());
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

                cmd.CommandText =
                                 "select tu.ID, tu.CustomerID, ui.Name AS CustomerName, " +
                                 "tu.TrainerID, ti.Name AS TrainerName, " +
                                 "tu.PackID, dp.ID AS DietID, tu.WorkOutID, wp.PlanName AS WorkoutName, " +
                                 "tu.AssignDate " +
                                 "from TrainerUser tu " +
                                 "inner join UserInfo ui ON ui.ID = tu.CustomerID " +
                                 "inner join DietPlan dp ON dp.ID = tu.DietID " +
                                 "left join UserInfo ti ON ti.ID = tu.TrainerID " +
                                 "left join WorkoutPlan wp ON wp.ID = tu.WorkOutID";





                DataSet ds = new DataSet();

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                DataTable dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Refresh();
                dataGridView1.ClearSelection();

                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void RefreshAll()
        {
            txtId.Text = "Auto Generated";
            txtCustomerID.Text = "";
            txtName.Text = "";
            cmbTrainerID.Text = "";
            txtTrainerName.Text = "";
            txtPackId.Text = "";
            txtDietID.Text = "";
            cmbWorkout.Text = "";
      
            dataGridView1.ClearSelection();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                return;
            }
            var row = dataGridView1.Rows[e.RowIndex];


            txtId.Text = Convert.ToString(row.Cells["ID"].Value);
            txtCustomerID.Text = Convert.ToString(row.Cells["UserID"].Value);
            txtName.Text = Convert.ToString(row.Cells["CustomerName"].Value);
            cmbTrainerID.Text = Convert.ToString(row.Cells["TrainerID"].Value);
            txtTrainerName.Text = Convert.ToString(row.Cells["TrainerName"].Value);
            txtPackId.Text = Convert.ToString(row.Cells["PackID"].Value);
            txtDietID.Text = Convert.ToString(row.Cells["DietID"].Value);    
            cmbWorkout.Text = Convert.ToString(row.Cells["WorkoutName"].Value);  

        }


        private void btnPaymet_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManegerialView_Amount_ mga = new ManegerialView_Amount_();
            mga.Show();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string customerID = txtCustomerID.Text;
            string customerName = txtName.Text;
            string trainerID = cmbTrainerID.Text;
            string trainerName = txtTrainerName.Text;
            string PackId = txtPackId.Text;
            string dietId = txtDietID.Text;
            string workoutName = cmbWorkout.Text;

            if (id == "Auto Generated")
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ApplicationHelper.cs;
                    con.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText =
                        "insert into TrainerUser (CustomerID, TrainerID, PackId, WorkOutID) values (" +
                        $"{customerID}, " +
                        $"{trainerID}, " +
                        $"{PackId}, " +
                        $"(select ID from WorkoutPlan where PlanName = '{workoutName}'))";

                    cmd.ExecuteNonQuery();

                    SqlCommand cmdUpdateDiet = new SqlCommand();
                    cmdUpdateDiet.Connection = con;
                    cmdUpdateDiet.CommandText =
                        $"update DietPlan set TrainerID = {trainerID}, " +
                        $"Goal ='{workoutName}'" +
                        $"where UserID = {customerID}";
                    cmdUpdateDiet.ExecuteNonQuery();

                    MessageBox.Show("Added Successfully");
                    this.LoadData();
                    con.Close();
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

                    cmd.CommandText =
                        "update TrainerUser set " +
                        $"TrainerID = {trainerID}, " +
                        $"PackId = {PackId}, " +
                        $"WorkOutID = (select ID from WorkoutPlan where PlanName = '{workoutName}') " +
                        $"where ID = {id}";

                    cmd.ExecuteNonQuery();

                    SqlCommand cmdUpdateDiet = new SqlCommand();
                    cmdUpdateDiet.Connection = con;
                    cmdUpdateDiet.CommandText =
                        $"update DietPlan set TrainerID = {trainerID}, " +
                        $"Goal = '{workoutName}'" +
                        $"where UserID = {customerID}";
                    cmdUpdateDiet.ExecuteNonQuery();

                    MessageBox.Show("Updated Successfully");
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

        private void ManagerialView_TrainerUser__Load(object sender, EventArgs e)
        {
            this.LoadData();
            this.LoadTrainers();
            this.LoadWorkouts();
        }

        private void cmbTrainerID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbTrainerID.SelectedItem == null)
                {
                    txtTrainerName.Text = "";
                    return;
                }

                string trainerId = cmbTrainerID.SelectedItem.ToString();

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select Name from UserInfo where ID = " + trainerId;

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    txtTrainerName.Text = dt.Rows[0]["Name"].ToString();
                }
                else
                {
                    txtTrainerName.Text = "";
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeView_User_ egu = new EmployeeView_User_();
            egu.Show();
        }

        private void btnPaymet_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeView_Amount_ mga = new EmployeeView_Amount_();
            mga.Show();
        }

        private void btnPackage_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeView_Packages_ mgp = new EmployeeView_Packages_();
            mgp.Show();
        }

        private void btnUserpackage_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeView_UserPackage_ mgu = new EmployeeView_UserPackage_();
            mgu.Show();
        }

        private void btnGym_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeView_Incident_ mgi = new EmployeeView_Incident_();
            mgi.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login lg = new Login();
            lg.Show();
        }
    }
}
