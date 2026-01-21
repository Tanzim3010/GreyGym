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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace GreyGym
{
    public partial class TrainerView_DietPlan_ : Form
    {
        public TrainerView_DietPlan_()
        {
            InitializeComponent();
        }

        private void ManagerialView_User__Load(object sender, EventArgs e)
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

                cmd.CommandText =
                                 "select dp.ID, dp.UserID, ui.Name AS CustomerName, " +
                                 "dp.TrainerID, ti.Name AS TrainerName, " +
                                 "dp.CurrentWeight, dp.TargetWeight, dp.Goal, " +
                                 "dp.FoodPlan, dp.StartDate " +
                                 "from DietPlan dp " +
                                 "inner join UserInfo ui ON ui.ID = dp.UserID " +
                                 "inner join UserInfo ti ON ti.ID = dp.TrainerID " +
                                 $"where dp.TrainerID = {Session.ID}";


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
            txtName.Text = "";
            txtTrainerName.Text = "";
            txtCurrWt.Text = "";
            txtTarWt.Text = "";
            cmbGoal.Text = "";
            rtxtFoodPlan.Text = "";
            dtpStart.Text = "";
      
            dataGridView1.ClearSelection();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                txtId.Text = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells["CustomerName"].Value.ToString();
                txtTrainerName.Text = dataGridView1.Rows[e.RowIndex].Cells["TrainerName"].Value.ToString();
                txtCurrWt.Text = dataGridView1.Rows[e.RowIndex].Cells["CurrentWeight"].Value.ToString();
                txtTarWt.Text = dataGridView1.Rows[e.RowIndex].Cells["TargetWeight"].Value.ToString();
                cmbGoal.Text = dataGridView1.Rows[e.RowIndex].Cells["Goal"].Value.ToString();
                rtxtFoodPlan.Text = dataGridView1.Rows[e.RowIndex].Cells["FoodPlan"].Value.ToString();
                dtpStart.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["StartDate"].Value);
            }

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
            string customerName = txtName.Text;
            string trainerName = txtTrainerName.Text;
            string currWt = txtCurrWt.Text;
            string tarWt = txtTarWt.Text;
            string goal = cmbGoal.Text;
            string diet = rtxtFoodPlan.Text;

            DateTime startDate = dtpStart.Value;
            string startDateStr = startDate.ToString("yyyy-MM-dd");

            if (id == "Auto Generated")
            {
                var row = dataGridView1.CurrentRow;
                int userId = Convert.ToInt32(row.Cells["UserID"].Value);
                int trainerId = Convert.ToInt32(row.Cells["TrainerID"].Value);

                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ApplicationHelper.cs;
                    con.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText =
                                        "insert into DietPlan " +
                                        "(UserID, TrainerID, CurrentWeight, TargetWeight, Goal, FoodPlan, StartDate) " +
                                        "values (" +
                                        $"{userId}, " +          
                                        $"{trainerId}, " +       
                                        $"{currWt}, " +
                                        $"{tarWt}, " +
                                        $"'{goal}', " +
                                        $"'{diet}', " +
                                        $"'{startDateStr}')";

                    cmd.ExecuteNonQuery();
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
                var row = dataGridView1.CurrentRow;
                int userId = Convert.ToInt32(row.Cells["UserID"].Value);

                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ApplicationHelper.cs;
                    con.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText = $"update DietPlan set " +
                                      $"CurrentWeight = {currWt}, " +
                                      $"TargetWeight = {tarWt}, " +
                                      $"Goal = '{goal}', " +
                                      $"FoodPlan = '{diet}', " +
                                      $"StartDate = '{startDateStr}' " +
                                      $"where ID = {id}";
                    cmd.ExecuteNonQuery();

                    SqlCommand cmdGetWorkoutId = new SqlCommand();
                    cmdGetWorkoutId.Connection = con;
                    cmdGetWorkoutId.CommandText =
                        $"select ID from WorkoutPlan where PlanName = '{goal}'";

                    DataTable dtWorkout = new DataTable();
                    SqlDataAdapter adpWorkout = new SqlDataAdapter(cmdGetWorkoutId);
                    adpWorkout.Fill(dtWorkout);

                    if (dtWorkout.Rows.Count > 0)
                    {
                        int workoutId = Convert.ToInt32(dtWorkout.Rows[0]["ID"]);

                        SqlCommand cmdUpdateTrainerUser = new SqlCommand();
                        cmdUpdateTrainerUser.Connection = con;
                        cmdUpdateTrainerUser.CommandText =
                            $"update TrainerUser set WorkOutID = {workoutId} " +
                            $"where CustomerID = {userId}";
                        cmdUpdateTrainerUser.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("Workout plan not found for Goal: " + goal);
                    }

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

        private void btnDiet_Click(object sender, EventArgs e)
        {

        }

        private void btnGym_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            TrainerView_TrainerUser_ mgt = new TrainerView_TrainerUser_();
            mgt.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }


        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login lg = new Login();
            lg.Show();
        }
    }
}
