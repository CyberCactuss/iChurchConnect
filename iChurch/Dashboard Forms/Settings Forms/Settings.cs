using iChurch.Dashboard_Forms.Settings_Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChurchSystem.Dashboard_Forms
{
    public partial class Settings : Form
    {


        public Settings()
        {
            InitializeComponent();
            textBox1.Text = "admin";
            textBox2.Text = "admin";
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) // USERNMAE
        {
            textBox1.Text = "admin";
        }

        private void textBox2_TextChanged(object sender, EventArgs e) // PASSWORD
        {
            textBox2.Text = "admin";
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e) // UPDATE BOTH
        {
            UpdateBoth updateBoth = new UpdateBoth();
            updateBoth.ShowDialog();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e) // UPDATE USERNAME ONLY
        {
            Username username = new Username();
            username.ShowDialog();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e) // UPDATE PASSWORD ONLY
        {
            Password password = new Password();
            password.ShowDialog();
        }
    }
}
