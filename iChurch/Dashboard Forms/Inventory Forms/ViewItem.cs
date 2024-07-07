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

            // Load image if exists
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                pictureBox1.Image = Image.FromFile(imagePath);
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
                        pictureBox1.Image = Image.FromFile(destinationPath);
                    }
                    catch (IOException ioEx)
                    {
                        MessageBox.Show($"Error copying file: {ioEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e) // ITEM ID
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) // ITEM NAME
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e) // ITEM QUANTITY
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e) // PICTURE BOX
        {

        }

        private void ViewItem_Load(object sender, EventArgs e)
        {

        }
    }
}