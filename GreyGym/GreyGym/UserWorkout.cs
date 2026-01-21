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
    public partial class UserWorkout : Form
    {
        public UserWorkout()
        {
            InitializeComponent();
        }

        private void CustomerHome_Load(object sender, EventArgs e)
        {
            try
            {
                labWelcome.Text = Session.Name;

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.cs;
                con.Open();

                SqlCommand cmdWorkout = new SqlCommand();
                cmdWorkout.Connection = con;
                cmdWorkout.CommandText = $"select WorkOutID from TrainerUser where CustomerID = {Session.ID}";

                DataSet ds = new DataSet();
                SqlDataAdapter adp1 = new SqlDataAdapter(cmdWorkout);
                adp1.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    rtxtWorkout.Text = "No workout assigned";
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["WorkOutID"] == DBNull.Value)
                    {
                        rtxtWorkout.Text = "No workout assigned";
                    }
                    else
                    {
                        int workoutId = Convert.ToInt32(ds.Tables[0].Rows[0]["WorkOutID"]);

                        SqlCommand cmdWorkoutName = new SqlCommand();
                        cmdWorkoutName.Connection = con;
                        cmdWorkoutName.CommandText =
                            $"select Workout from WorkoutPlan where ID = {workoutId}";

                        DataSet dsWorkout = new DataSet();
                        new SqlDataAdapter(cmdWorkoutName).Fill(dsWorkout);

                        if (dsWorkout.Tables[0].Rows.Count > 0)
                            rtxtWorkout.Text = dsWorkout.Tables[0].Rows[0]["Workout"].ToString();
                        else
                            rtxtWorkout.Text = "No workout assigned";
                    }
                }

                SqlCommand cmdTrainer = new SqlCommand();
                cmdTrainer.Connection = con;
                cmdTrainer.CommandText =
                    "SELECT u.Name " +
                    "FROM TrainerUser tu " +
                    "JOIN UserInfo u ON tu.TrainerID = u.ID " +
                    $"WHERE tu.CustomerID = {Session.ID}";

                DataSet dsTrainer = new DataSet();
                SqlDataAdapter adp2 = new SqlDataAdapter(cmdTrainer);
                adp2.Fill(dsTrainer);

                if (dsTrainer.Tables[0].Rows.Count > 0)
                    labTrainer.Text = dsTrainer.Tables[0].Rows[0]["Name"].ToString();
                else
                    labTrainer.Text = "Not Assigned";

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnMember_Click(object sender, EventArgs e)
        {
            this.Hide();
            PackageDash pd = new PackageDash();
            pd.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateInfo U = new UpdateInfo();
            U.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login lg = new Login();
            lg.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerHome ch = new CustomerHome();
            ch.Show();
        }
    }
}
