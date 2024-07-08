using ChurchSystem.Dashboard_Forms;
using iChurch.DBAccess.Connection;
using System;
using System.Data.OleDb;
using System.Net.Mail;
using System.Windows.Forms;

namespace iChurch.Dashboard_Forms.Settings_Forms
{
    public partial class Gmail : Form
    {
        private Settings parentSettingsForm;

        public Gmail(Settings parentSettingsForm)
        {
            InitializeComponent();
            this.parentSettingsForm = parentSettingsForm;
        }

        public string EmailText
        {
            get { return textBox1.Text; }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e) // UPDATE BUTTON
        {
            string email = textBox1.Text;
            if (IsValidEmail(email))
            {
                GmailService gmailService = new GmailService();
                string generatedOTP = gmailService.SendOTP(email);
                OTP otpForm = new OTP(generatedOTP, email, this, parentSettingsForm);
                otpForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please enter a valid email address.");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // GMAIL ACC
        {
        }

        private void RemoveGmailFromDatabase()
        {
            using (AccessConnection connection = new AccessConnection())
            {
                connection.OpenConnection();
                string query = "UPDATE Admin SET Gmail = NULL WHERE ID = 1"; // Assuming you want to update the Gmail for a specific admin (ID=1)
                using (OleDbCommand command = new OleDbCommand(query, connection.GetConnection()))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void guna2GradientButton2_Click_1(object sender, EventArgs e)
        {
            RemoveGmailFromDatabase();
            MessageBox.Show("Gmail account removed successfully.");
            parentSettingsForm.LoadAdminData(); // Refresh the Settings form data
        }
    }
}
