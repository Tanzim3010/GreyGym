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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string name = textBox1.Text;
            if (name == "")
            {
               

                MessageBox.Show("Please Fill Your Name");
                textBox1.Focus();
                textBox1.BackColor = Color.DarkGray;
                textBox1.ForeColor = Color.Black;

                return;
            }else {
                 textBox1.BackColor= Color.White;
            }

            string Surname = txtSurname.Text;
            if (Surname == "")
            {
                MessageBox.Show("Please Fill Surname");
                txtSurname.Focus();
                txtSurname.BackColor = Color.DarkGray;
                return;

            }
            else
            {
                txtSurname.BackColor= Color.White;
            }


                string email = "";
            email = textEmail.Text;
            if (email == "")
            {
                MessageBox.Show("Please Fill Email.");
                textEmail.BackColor= Color.DarkGray;
                return;

            }
           
            if (email.Contains('@')==false)
            {
                MessageBox.Show("Uffs You May forgot to add '@' or '.' ");
                return;
            }
            else
            {
                textEmail.BackColor = Color.White;
            }

            int phone=0;
           
            if(txtPhone.Text == "")
            {
                MessageBox.Show("Phone number required");
                return;
            }
            if (txtPhone.TextLength!=11)
            {
                MessageBox.Show("11 numeric number required");
                return;
            }
            try
            {
                phone = Convert.ToInt32(txtPhone.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Numeric number required");
                return;
            }
          
           
            

            string password = textBox2.Text;
            string confirmPass = textBox3.Text;
            if (password=="" )
            {
                textBox2.BackColor = Color.DarkGray;
                textBox3.BackColor = Color.DarkGray;
                textBox3.ForeColor = Color.Blue;
                MessageBox.Show("Password required");
                textBox2.Focus();
                
                return;
            }
            else if (password.Length<=5) {
                MessageBox.Show("More than 5 char required.");
                return;
            }else if (confirmPass == "")
            {
                MessageBox.Show("Fill up your confirm password.");
                return;
            }else if (password != confirmPass)
            {
                MessageBox.Show("Password match required");
                return;
            }
            else
            {
                textBox2.BackColor = Color.White;
                textBox3.BackColor = Color.White;
            }


            string gender = "";
            if (radioButton1.Checked)
            {
                gender = "Male";


            }
            else if (radioButton2.Checked) {
                gender = "Female";
            }else
            {
                MessageBox.Show("Please select Gender");
                return;
            }

           
            
                try
                {
                    SqlConnection con = new SqlConnection(); // Connection dewa
                    con.ConnectionString =
               "Data Source=DESKTOP-QTAP79E\\SQLEXPRESS;Initial Catalog=oop2;Integrated Security=True;Encrypt=False";

                    con.Open();

                    SqlCommand cmd = new SqlCommand(); // panel banano
                    cmd.Connection = con; // database select hoye gese
                    cmd.CommandText = $"insert into Register values ('{name}','{Surname}','{email}','{phone}','{password}','{confirmPass}','{gender}')";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    



                    MessageBox.Show("Dear "+Surname+" Your Registration is Successful.");



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

             
           

            
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = false ;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar=true;
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            textBox3.UseSystemPasswordChar = false ;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            textBox3.UseSystemPasswordChar = true;
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.Black;
            button1.ForeColor = Color.White;
        }

       

        private void button1_MouseHover_1(object sender, EventArgs e)
        {
            button1.BackColor = Color.Black;
            button1.ForeColor = Color.White;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(51,51,76);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void textEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            textBox3.UseSystemPasswordChar = false ;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            textBox3 .UseSystemPasswordChar = true ;
        }

        private void Registration_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Login lg = new Login();
            lg.Show();
        }
    }
}
