using iChurch.DBAccess.Connection;
using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace iChurch.Dashboard_Forms.Settings_Forms
{
    public partial class UpdateBoth : Form
    {
        public UpdateBoth()
        {
            InitializeComponent();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e) // SAVE BUTTON
        {
            
            string currentUsername = textBox1.Text;
            string newUsername = textBox2.Text;
            string currentPassword = textBox5.Text;
            string newPassword = textBox4.Text;
            string confirmPassword = textBox3.Text;

            
            if (string.IsNullOrEmpty(currentUsername) || string.IsNullOrEmpty(newUsername) ||
                string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("All fields must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
               
                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                
                string query = "SELECT [Username], [Password] FROM Admin WHERE ID = ?";
                OleDbCommand command = new OleDbCommand(query, dbConnection.GetConnection());
                command.Parameters.AddWithValue("@ID", 1); 

                OleDbDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string dbUsername = reader["Username"].ToString();
                    string dbPassword = reader["Password"].ToString();

                    // Verify current username and password.
                    if (dbUsername != currentUsername)
                    {
                        MessageBox.Show("The current username is incorrect.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        reader.Close();
                        dbConnection.CloseConnection();
                        return;
                    }

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

                
                string updateQuery = "UPDATE Admin SET [Username] = ?, [Password] = ? WHERE ID = ?";
                OleDbCommand updateCommand = new OleDbCommand(updateQuery, dbConnection.GetConnection());
                updateCommand.Parameters.AddWithValue("@Username", newUsername);
                updateCommand.Parameters.AddWithValue("@Password", newPassword);
                updateCommand.Parameters.AddWithValue("@ID", 1); 

                updateCommand.ExecuteNonQuery();
                dbConnection.CloseConnection();

                MessageBox.Show("Username and password updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
