using iChurch.DBAccess.Connection;
using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace iChurch.Dashboard_Forms.Settings_Forms
{
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
        }

        private void Password_Load(object sender, EventArgs e)
        {
            // Initialize form if needed.
            // Ensure password fields are masked by default
            textBox1.PasswordChar = '•';
            textBox2.PasswordChar = '•';
            textBox3.PasswordChar = '•';
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // CURRENT PASSWORD
        {
            // Handle text changes if needed.
        }

        private void textBox2_TextChanged(object sender, EventArgs e) // NEW PASSWORD
        {
            // Handle text changes if needed.
        }

        private void textBox3_TextChanged(object sender, EventArgs e) // CONFIRM PASSWORD
        {
            // Handle text changes if needed.
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e) // UPDATE BUTTON
        {
            // Fetch the entered passwords.
            string currentPassword = textBox1.Text;
            string newPassword = textBox2.Text;
            string confirmPassword = textBox3.Text;

            // Basic validation to ensure no fields are empty.
            if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("All password fields must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ensure new password and confirm password match.
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Create a connection to the database.
                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                // Check if the current password is correct.
                string query = "SELECT [Password] FROM Admin WHERE ID = ?";
                OleDbCommand command = new OleDbCommand(query, dbConnection.GetConnection());
                command.Parameters.AddWithValue("@ID", 1); // Use the actual Admin ID

                OleDbDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string dbPassword = reader["Password"].ToString();

                    // Verify current password.
                    if (dbPassword != currentPassword)
                    {
                        MessageBox.Show("The current password is incorrect.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        reader.Close();
                        dbConnection.CloseConnection();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Admin record not found.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                    dbConnection.CloseConnection();
                    return;
                }

                reader.Close();

                // Update the password in the database.
                string updateQuery = "UPDATE Admin SET [Password] = ? WHERE ID = ?";
                OleDbCommand updateCommand = new OleDbCommand(updateQuery, dbConnection.GetConnection());
                updateCommand.Parameters.AddWithValue("@Password", newPassword);
                updateCommand.Parameters.AddWithValue("@ID", 1); // Use the actual Admin ID

                updateCommand.ExecuteNonQuery();
                dbConnection.CloseConnection();

                MessageBox.Show("Password updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Close the form after successful update.
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e) // CHECKBOX FOR SHOW/HIDE PASSWORD
        {
            // Check if the CheckBox is checked or not.
            bool isChecked = guna2CheckBox1.Checked;

            // If checked, remove the PasswordChar to show the password, otherwise mask the password.
            if (isChecked)
            {
                textBox1.PasswordChar = '\0'; // Show password
                textBox2.PasswordChar = '\0'; // Show password
                textBox3.PasswordChar = '\0'; // Show password
            }
            else
            {
                textBox1.PasswordChar = '•'; // Mask password
                textBox2.PasswordChar = '•'; // Mask password
                textBox3.PasswordChar = '•'; // Mask password
            }
        }
    }
}
