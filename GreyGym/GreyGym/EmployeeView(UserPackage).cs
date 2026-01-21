using GreyGym;
using Microsoft.SqlServer.Server;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace GreyGym
{
    public partial class EmployeeView_UserPackage_ : Form
    {
        public EmployeeView_UserPackage_()
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

                cmd.CommandText = "select * from UserPackage";

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

        }

        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                txtId.Text = Convert.ToString(row.Cells["ID"].Value);
                txtUser.Text = Convert.ToString(row.Cells["UserId"].Value);
                txtPack.Text = Convert.ToString(row.Cells["PackId"].Value);
                dtpStart.Value = Convert.ToDateTime(row.Cells["StartDate"].Value);
                dtpEnd.Value = Convert.ToDateTime(row.Cells["EndDate"].Value);
                cmbStatus.Text = Convert.ToString(row.Cells["IsActive"].Value);
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
            string UserId = txtUser.Text;
            string PackId = txtPack.Text;
            DateTime start = dtpStart.Value;
            DateTime end = dtpEnd.Value;
            string status = cmbStatus.Text;

            try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = $"update UserPackage set UserId = '{UserId}',PackId='{PackId}',StartDate='{start}',EndDate='{end}',IsActive='{status}' where id ={id}";
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
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

        }

        private void btnGym_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeView_TrainerUser_ mgt = new EmployeeView_TrainerUser_();
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

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login lg = new Login();
            lg.Show();
        }
    }
}
