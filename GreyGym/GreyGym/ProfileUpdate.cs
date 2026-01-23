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
using project;

namespace GreyGym
{
    public partial class ProfileUpdate : Form
    {
        public ProfileUpdate()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
       

        private void ProfileUpdate_Load(object sender, EventArgs e)
        {
            label2.Text = Session.Name;
            lblName.Text = Session.Name;
            lblGmail.Text = Session.GMAIL;
            lblPhone.Text = Session.PHONE;
            lblPassword.Text = Session.PASSWORD;
            lblGender.Text = Session.GENDER;

          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CustomerHome ch = new CustomerHome();
            ch.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProfileUpdate2 pf = new ProfileUpdate2();
            pf.Show();
            this.Hide();
        }
    }
}
