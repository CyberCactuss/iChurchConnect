using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace iChurch.Dashboard_Forms.Inventory_Forms
{
    public partial class ViewItem : Form
    {
        private string itemId;
        private string itemName;
        private int quantity;
        private string imagePath;

        public ViewItem(string itemId, string itemName, int quantity, string imagePath)
        {
            InitializeComponent();
            this.itemId = itemId;
            this.itemName = itemName;
            this.quantity = quantity;
            this.imagePath = imagePath;
            LoadItemDetails();
        }

        private void LoadItemDetails()
        {
            textBox2.Text = itemId.ToString();
            textBox1.Text = itemName;
            textBox3.Text = quantity.ToString();

            // Convert relative path to absolute path before loading the image
            if (!string.IsNullOrEmpty(imagePath))
            {
                string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string fullPath = Path.Combine(exeDirectory, imagePath);

                if (File.Exists(fullPath))
                {
                    pictureBox1.Image = Image.FromFile(fullPath);
                }
                else
                {
                    MessageBox.Show($"Image file not found: {fullPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pictureBox1.Image = null; // or set to a default image
                }
            }
            else
            {
                pictureBox1.Image = null; // or set to a default image
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
                    string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string imagesFolder = Path.Combine(exeDirectory, @"..\..\..\..\Inventory");

                    if (!Directory.Exists(imagesFolder))
                    {
                        Directory.CreateDirectory(imagesFolder);
                    }

                    string destinationPath = Path.Combine(imagesFolder, $"{itemId}.jpg");

                    try
                    {
                        File.Copy(sourcePath, destinationPath, true);
                        // Update picture box with new image
                        pictureBox1.Image = Image.FromFile(destinationPath);

                        // Save the relative path
                        imagePath = Path.Combine(@"..\..\..\..\Inventory", $"{itemId}.jpg");
                    }
                    catch (IOException ioEx)
                    {
                        MessageBox.Show($"Error copying file: {ioEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Other methods remain unchanged
    }
}
