using Guna.UI2.WinForms;
using iChurch.DBAccess.Connection;
using System;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace iChurch.Dashboard_Forms.Inventory_Forms
{
    public partial class AddItem : Form
    {
        public AddItem()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e) // ITEM NAME
        {
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e) // REGISTER BUTTON
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // ITEM ID
        {
        }

        private void AddItem_Load(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // QUANTITY
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e) // IMAGE
        {
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
                }
            }
        }
    }

}
