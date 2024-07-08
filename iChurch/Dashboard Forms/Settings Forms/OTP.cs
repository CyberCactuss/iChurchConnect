using System;
using System.Data.OleDb;
using System.Windows.Forms;
using iChurch.DBAccess.Connection;

namespace iChurch.Dashboard_Forms.Settings_Forms
{
    public partial class OTP : Form
    {
        private string generatedOTP;
        private string email;

        public OTP(string otp, string email)
        {
            InitializeComponent();
            generatedOTP = otp;
            this.email = email;
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e) // CONFIRM BUTTON
        {
            string enteredOTP = guna2TextBox2.Text;
            if (enteredOTP == generatedOTP)
            {
                UpdateDatabaseGmail(email); 
                MessageBox.Show("OTP verified and Gmail updated successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid OTP. Please try again.");
            }
        }

        private void UpdateDatabaseGmail(string email)
        {
            using (AccessConnection connection = new AccessConnection())
            {
                connection.OpenConnection();
                string query = "UPDATE Admin SET Gmail = ? WHERE ID = 1"; 
                using (OleDbCommand command = new OleDbCommand(query, connection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@Gmail", email);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e) // OTP CODE
        {
        }
    }
}
