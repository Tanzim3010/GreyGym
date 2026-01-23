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

namespace GreyGym
{
    public partial class ProfileUpdate2 : Form
    {
        public ProfileUpdate2()
        {
            InitializeComponent();
        }

        private void ProfileUpdate2_Load(object sender, EventArgs e)
        {
            txtName.Text = Session.Name;
            txtGmail.Text = Session.GMAIL;
            txtPhone.Text = Session.PHONE;
            txtPass.Text = Session.PASSWORD;
            cmbGender.Text = Session.GENDER;
           


        }
        private void LoadData()
        {
            int id = Session.ID;
            string name = txtName.Text;
            string gmail = txtGmail.Text;
            string phone = txtPhone.Text;
            string pass = txtPass.Text;
            string gender = cmbGender.Text;
            try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.cs;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"select * from UserInfo where ID = {id}";
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                DataTable dt = new DataTable();
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtGmail.Text = dt.Rows[0]["Gmail"].ToString();
                txtPhone.Text = dt.Rows[0]["Phone"].ToString();
                txtPass.Text = dt.Rows[0]["Password"].ToString();
                cmbGender.Text = dt.Rows[0]["Gender"].ToString();
                con.Close();

            }
            catch (Exception ex)
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id =  Session.ID;
            string name = txtName.Text;
            string gmail = txtGmail.Text;
            string phone = txtPhone.Text;
            string pass = txtPass.Text;
            string gender = cmbGender.Text;
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ApplicationHelper.cs;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"update UserInfo set Name = '{name}',Gmail='{gmail}',Phone ='{phone}',Password='{pass}',Gender = '{gender}' where ID ={id}";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated Successfully");
                this.LoadData();
                Session.Name = name;
                Session.GMAIL = gmail;
                Session.PHONE = phone;
                Session.PASSWORD = pass;
                Session.GENDER = gender;
                ProfileUpdate pf = new ProfileUpdate();
                pf.Show();
                this.Hide();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProfileUpdate pf = new ProfileUpdate();
            pf.Show();
            this.Hide();
        }
    }
}
