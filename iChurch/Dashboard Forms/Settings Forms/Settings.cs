using iChurch.Dashboard_Forms.Settings_Forms;
using iChurch.DBAccess.Connection;
using System;
using System.Data.OleDb;
using System.IO;
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
            LoadAdminData(); 
        }

        private void DisplayWarningImageForEmptyGmail()
        {
            try
            {
                dbConnection.OpenConnection();

                string query = "SELECT Gmail FROM Admin WHERE ID = ?";
                OleDbCommand command = new OleDbCommand(query, dbConnection.GetConnection());
                command.Parameters.AddWithValue("?", 1);

                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string gmailValue = reader["Gmail"].ToString();
                    Console.WriteLine(gmailValue);

                    if (string.IsNullOrEmpty(gmailValue))
                    {
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

                string query = "SELECT Gmail FROM Admin WHERE ID = ?";
                OleDbCommand command = new OleDbCommand(query, dbConnection.GetConnection());
                command.Parameters.AddWithValue("?", 1);

                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string gmailValue = reader["Gmail"].ToString();

                    if (string.IsNullOrEmpty(gmailValue))
                    {
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

        public void LoadAdminData()
        {
            try
            {
                dbConnection.OpenConnection();

                string query = "SELECT Username, Password, Gmail FROM Admin WHERE ID = ?";
                OleDbCommand command = new OleDbCommand(query, dbConnection.GetConnection());
                command.Parameters.AddWithValue("?", 1);

                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["Username"].ToString();
                    textBox2.Text = reader["Password"].ToString();
                    textBox3.Text = reader["Gmail"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading admin data: {ex.Message}");
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        private bool IsGmailSet()
        {
            try
            {
                dbConnection.OpenConnection();

                string query = "SELECT Gmail FROM Admin WHERE ID = ?";
                OleDbCommand command = new OleDbCommand(query, dbConnection.GetConnection());
                command.Parameters.AddWithValue("?", 1);

                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string gmailValue = reader["Gmail"].ToString();
                    return !string.IsNullOrEmpty(gmailValue);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking Gmail: {ex.Message}");
            }
            finally
            {
                dbConnection.CloseConnection();
            }
            return false;
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e) // UPDATE BOTH
        {
            if (IsGmailSet())
            {
                UpdateBoth updateBoth = new UpdateBoth();
                updateBoth.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must set your Gmail for verification purposes before updating both Username and Password.", "Gmail Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e) // UPDATE USERNAME ONLY
        {
            if (IsGmailSet())
            {
                Username username = new Username();
                username.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must set your Gmail for verification purposes before updating the Username.", "Gmail Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e) // UPDATE PASSWORD ONLY
        {
            if (IsGmailSet())
            {
                Password password = new Password();
                password.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must set your Gmail for verification purposes before updating the Password.", "Gmail Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            Gmail gmail = new Gmail(this); // Pass the current Settings form instance
            gmail.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e) // USERNAME
        { }

        private void textBox2_TextChanged(object sender, EventArgs e) // PASSWORD
        { }

        private void textBox3_TextChanged(object sender, EventArgs e) { }

        private void pictureBox2_Click(object sender, EventArgs e) // PICTURE
        { }

        private void Settings_Load(object sender, EventArgs e) { }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            DisplayWarningImageForEmptyGmail();
            LoadAdminData();
        }
    }
}
