using ChurchSystem.Dashboard_Forms;
using iChurch.DBAccess.Connection;
using System;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace iChurch.Dashboard_Forms.Inventory_Forms
{
    public partial class EditItem : Form
    {
        private readonly string itemId;
        private string imagePath;
        private readonly Inventory parentForm;

        public EditItem(string itemId, string itemName, int quantity, string imagePath, Inventory parent)
        {
            InitializeComponent();
            this.itemId = itemId;
            textBox1.Text = itemId;
            textBox2.Text = itemName;
            comboBox1.SelectedItem = quantity.ToString();
            this.imagePath = imagePath;
            if (!string.IsNullOrEmpty(imagePath))
            {
                pictureBox2.Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath));
            }
            parentForm = parent;
        }

        private void guna2Button1_Click(object sender, EventArgs e) // SAVE BUTTON
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string itemName = textBox2.Text;
            if (!int.TryParse(comboBox1.SelectedItem.ToString(), out int quantity))
            {
                MessageBox.Show("Please select a valid quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (AccessConnection dbConnection = new AccessConnection())
                {
                    dbConnection.OpenConnection();

                    string query = "UPDATE Inventory SET [ItemName] = ?, [Qnty] = ?, [Image] = ? WHERE [ItemID] = ?";
                    using (OleDbCommand cmd = new OleDbCommand(query, dbConnection.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("?", itemName);
                        cmd.Parameters.AddWithValue("?", quantity);
                        cmd.Parameters.AddWithValue("?", imagePath);
                        cmd.Parameters.AddWithValue("?", itemId);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Item updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                parentForm.LoadInventoryData();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e) // UPLOAD PICTURE
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string sourcePath = openFileDialog.FileName;
                    pictureBox2.Image = Image.FromFile(sourcePath);

                    string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string imagesFolder = Path.Combine(exeDirectory, "Inventory");

                    if (!Directory.Exists(imagesFolder))
                    {
                        Directory.CreateDirectory(imagesFolder);
                    }

                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(sourcePath)}";
                    string destinationPath = Path.Combine(imagesFolder, fileName);

                    try
                    {
                        File.Copy(sourcePath, destinationPath, true);
                        imagePath = Path.Combine("Inventory", fileName); // Save relative path
                    }
                    catch (IOException ioEx)
                    {
                        MessageBox.Show($"Error copying file: {ioEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e) // CLOSE BUTTON
        {
            this.Close();
        }
    }
}
