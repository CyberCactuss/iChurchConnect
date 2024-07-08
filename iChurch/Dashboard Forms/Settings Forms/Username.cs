using iChurch.DBAccess.Connection;
using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace iChurch.Dashboard_Forms.Settings_Forms
{
    public partial class Username : Form
    {
        public Username()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // Current Username
        {
            // Handle text changes if needed.
        }

        private void textBox2_TextChanged(object sender, EventArgs e) // New Username
        {
            // Handle text changes if needed.
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e) // Save Button
        {
            // Fetch the entered usernames.
            string currentUsername = textBox1.Text;
            string newUsername = textBox2.Text;

            // Basic validation to ensure no fields are empty.
            if (string.IsNullOrEmpty(currentUsername) || string.IsNullOrEmpty(newUsername))
            {
                MessageBox.Show("Both username fields must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Create a connection to the database.
                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                // Check if the current username is correct.
                string query = "SELECT [Username] FROM Admin WHERE ID = ?";
                OleDbCommand command = new OleDbCommand(query, dbConnection.GetConnection());
                command.Parameters.AddWithValue("@ID", 1); // Use the actual Admin ID

                OleDbDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string dbUsername = reader["Username"].ToString();

                    // Verify current username.
                    if (dbUsername != currentUsername)
                    {
                        MessageBox.Show("The current username is incorrect.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            
                string updateQuery = "UPDATE Admin SET [Username] = ? WHERE ID = ?";
                OleDbCommand updateCommand = new OleDbCommand(updateQuery, dbConnection.GetConnection());
                updateCommand.Parameters.AddWithValue("@Username", newUsername);
                updateCommand.Parameters.AddWithValue("@ID", 1); 

                updateCommand.ExecuteNonQuery();
                dbConnection.CloseConnection();

                MessageBox.Show("Username updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
