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
using Microsoft.SqlServer.Server;
using MviewWorkout;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace GreyGym
{
    public partial class ManagerialView_Incident_ : Form
    {
        public ManagerialView_Incident_()
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

                cmd.CommandText = "select * from IncidentReport";

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
            dtpReport.Text = "";
            rtxtIncident.Text = "";
            cmbStatus.Text = "";

        }

        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                txtId.Text = Convert.ToString(row.Cells["ID"].Value);
                txtName.Text = Convert.ToString(row.Cells["IncidentReport"].Value);
                dtpReport.Value = Convert.ToDateTime(row.Cells["ReportedDate"].Value);
                rtxtIncident.Text = Convert.ToString(row.Cells["Incident"].Value);
                cmbStatus.Text = Convert.ToString(row.Cells["Status"].Value);
            }
        }

        private void btnPaymet_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManegerialView_Amount_ mga = new ManegerialView_Amount_();
            mga.Show();

        }

        private void ManagerialView_Incident__Load(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string name  = txtName.Text;
            DateTime dtp1 = dtpReport.Value;
            string report = rtxtIncident.Text;
            string status = cmbStatus.Text;

            if(id=="Auto Generated")
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ApplicationHelper.cs;
                    con.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText = $"insert into IncidentReport values ('{name}','{dtp1}','{report}','{status}')";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Added Successfully");
                    this.LoadData();
                    con.Close();
                }
                catch(Exception ex)
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

                    cmd.CommandText = $"update IncidentReport set Reporter = '{name}',Date='{dtp1}',Incident='{report}',Status='{status}' where id ={id}";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Updated Successfully");
                    this.LoadData();
                    this.RefreshAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); 
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            if(id=="Auto Generated")
            {
                MessageBox.Show("Please select a row first");
                return;
            }

            var result = MessageBox.Show("Are You Sure?", "Confirm", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
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

                    cmd.CommandText = $"delete from IncidentReport where ID = {id}";
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.RefreshAll();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManagerialView_User_ mgu = new ManagerialView_User_();
            mgu.Show();
        }

        private void btnPaymet_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ManegerialView_Amount_ mga = new ManegerialView_Amount_();
            mga.Show();
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
            this.Hide();
            MviewEquipment eview = new MviewEquipment();
            eview.Show();
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
            ManagerialViewWorkout employee = new ManagerialViewWorkout();
            employee.Show();
        }

        private void button6_Click(object sender, EventArgs e)
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
