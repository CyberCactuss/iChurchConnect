using iChurch.Dashboard_Forms.Settings_Forms;
using iChurch.DBAccess.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChurchSystem.Dashboard_Forms
{
    public partial class Settings : Form
    {

        private AccessConnection dbConnection;
        public Settings()
        {
            InitializeComponent();
            dbConnection = new AccessConnection();
            DisplayWarningImageForEmptyGmail();
        }

        private void DisplayWarningImageForEmptyGmail()
        {
            try
            {
                dbConnection.OpenConnection();

                // Query to check if Gmail column is empty for Admin with ID 1
                string query = "SELECT Gmail FROM Admin WHERE ID = ?";
                OleDbCommand command = new OleDbCommand(query, dbConnection.GetConnection());
                command.Parameters.AddWithValue("@ID", 1); // Replace with actual admin ID

                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string gmailValue = reader["Gmail"].ToString();
                    Console.WriteLine(gmailValue);

                    if (string.IsNullOrEmpty(gmailValue))
                    {
                        // Gmail is empty, display warning image
                        string imagePath = Path.Combine(Application.StartupPath, @"..\..\..\..\RelatedImages\warning.png");
                        if (File.Exists(imagePath))
                        {
                            pictureBox2.Image = Image.FromFile(imagePath);
                        }
                        else
                        {
                            MessageBox.Show("Warning image not found!");
                        }
                    }
                    // If Gmail is not empty, do nothing (or handle differently)
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

            public void CheckAndPromptForEmptyGmail()
            {
                try
                {
                    dbConnection.OpenConnection();

                    // Query to check if Gmail column is empty for Admin with ID 1
                    string query = "SELECT Gmail FROM Admin WHERE ID = ?";
                    OleDbCommand command = new OleDbCommand(query, dbConnection.GetConnection());
                    command.Parameters.AddWithValue("@ID", 1); // Replace with actual admin ID

                    OleDbDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string gmailValue = reader["Gmail"].ToString();

                        if (string.IsNullOrEmpty(gmailValue))
                        {
                            // Gmail is empty, prompt the user
                            MessageBox.Show("Please enter your Gmail account for verification purposes.", "Empty Gmail Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                finally
                {
                    dbConnection.CloseConnection();
                }
            }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) // USERNMAE
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e) // PASSWORD
        {

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

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            Gmail gmail = new Gmail();
            gmail.ShowDialog();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e) // PICTURE
        {

        }
    }
}
