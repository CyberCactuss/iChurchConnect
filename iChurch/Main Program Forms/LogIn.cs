using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iChurch.DBAccess.Authentication;
using iChurch.DBAccess.Connection;

namespace ChurchSystem
{
    public partial class LogIn : Form
    {
        private AccessConnection dbConnection;
        private AccessAuthentication authenticationService;

        public LogIn()
        {
            InitializeComponent();
            string exeDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            string dbFilePath = Path.Combine(exeDirectory, "iChurchConnect.accdb");
            dbConnection = new AccessConnection(dbFilePath);
            authenticationService = new AccessAuthentication(dbConnection.GetConnection());
        }

        private void LogIn_Load(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {

            string username = guna2TextBox3.Text.Trim();
            string password = guna2TextBox1.Text;

            bool isValidCredentials = authenticationService.CheckCredentials(username, password);

            if (isValidCredentials)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                MainDashboard dashboardForm = new MainDashboard();
                dashboardForm.FormClosed += (s, args) => this.Close();
                this.Hide(); 
                dashboardForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                guna2TextBox3.Clear();
                guna2TextBox1.Clear();
                guna2TextBox3.Focus();
            }
        }


        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
