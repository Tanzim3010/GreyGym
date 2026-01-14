using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreyGym
{
    public partial class CustomerHome : Form
    {
        int userId = Session.ID;
        public CustomerHome()
        {
            InitializeComponent();
        }

        private void CustomerHome_Load(object sender, EventArgs e)
        {
            txtId.Text = userId.ToString();
        }
    }
}
