using iChurch.DBAccess.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChurchSystem.Dashboard_Forms.MembersFiles
{
    public partial class ViewInfo : Form
    {
        private PrintDocument printDocument1 = new PrintDocument();
        private PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
        public ViewInfo()
        {
            InitializeComponent();
            this.Load += new EventHandler(ViewInfo_Load);
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
        }

        public ViewInfo(int memberId)
        {
            InitializeComponent();
            this.Load += new EventHandler(ViewInfo_Load);
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            LoadMemberInfo(memberId);
        }


        private void LoadMemberInfo(int memberId)
        {
            try
            {
                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                string query = "SELECT FullName, Age, Sex, Contact, Email, Address, Birthday, FacebookAccount, ProfilePicturePath FROM Members WHERE ID = ?";
                OleDbCommand command = new OleDbCommand(query, dbConnection.GetConnection());
                command.Parameters.AddWithValue("@ID", memberId);

                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["FullName"].ToString();
                    textBox3.Text = reader["Age"].ToString();
                    textBox2.Text = reader["Sex"].ToString();
                    textBox6.Text = reader["Contact"].ToString();
                    textBox7.Text = reader["Email"].ToString();
                    textBox5.Text = reader["Address"].ToString();
                    textBox4.Text = DateTime.Parse(reader["Birthday"].ToString()).ToString("yyyy-MM-dd");
                    textBox8.Text = reader["FacebookAccount"].ToString();

                    string relativePath = reader["ProfilePicturePath"].ToString();
                    string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string imagePath = Path.Combine(exeDirectory, relativePath);

                    if (!string.IsNullOrEmpty(relativePath) && File.Exists(imagePath))
                    {
                        using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                        {
                            pictureBox2.Image = Image.FromStream(fs);
                        }
                    }
                    else
                    {
                        pictureBox2.Image = null; // or a default image
                    }
                }

                reader.Close();
                dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching member data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Separator2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e) // CONTACT
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e) // SEX
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e) // AGE
        {

        }

        private void ViewInfo_Load(object sender, EventArgs e)
        {
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = 0;
        }

        private void textBox4_TextChanged(object sender, EventArgs e) //BIRTHDAY
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e) // ADDRESS
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e) // EMAIL
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
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
                    string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string imagesFolder = Path.Combine(exeDirectory, @"..\..\..\..\ProfileImages");

                    if (!Directory.Exists(imagesFolder))
                    {
                        Directory.CreateDirectory(imagesFolder);
                    }

                    string fileName = Path.GetFileName(sourcePath);
                    string destinationPath = Path.Combine(imagesFolder, fileName);

                    try
                    {
                        using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
                        using (FileStream destStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                        {
                            sourceStream.CopyTo(destStream);
                        }
                    }
                    catch (IOException ioEx)
                    {
                        MessageBox.Show($"Error copying file: {ioEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string relativePath = Path.Combine(@"..\..\..\..\ProfileImages", fileName);
                    int memberId = (int)this.Tag;
                    SaveImagePathToDatabase(memberId, relativePath);

                    LoadMemberInfo(memberId);
                }
            }
        }

        private void SaveImagePathToDatabase(int memberId, string relativePath)
        {
            try
            {
                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                string query = "UPDATE Members SET ProfilePicturePath = ? WHERE ID = ?";
                OleDbCommand command = new OleDbCommand(query, dbConnection.GetConnection());
                command.Parameters.AddWithValue("@ProfilePicturePath", relativePath);
                command.Parameters.AddWithValue("@ID", memberId);
                command.ExecuteNonQuery();

                dbConnection.CloseConnection();

                MessageBox.Show("Image uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving image path: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e) // PROFILE PICTURE
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e) // PRINT BUTTON
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            
            Font font = new Font("Arial", 16);
            Brush brush = Brushes.Black;
            int x = 100;
            int y = 100;
            int lineHeight = font.Height + 10;

            
            e.Graphics.DrawString("Member Information", new Font("Arial", 22, FontStyle.Bold), brush, x, y);
            y += lineHeight;

            e.Graphics.DrawString($"Full Name: {textBox1.Text}", font, brush, x, y);
            y += lineHeight;

            e.Graphics.DrawString($"Age: {textBox3.Text}", font, brush, x, y);
            y += lineHeight;

            e.Graphics.DrawString($"Sex: {textBox2.Text}", font, brush, x, y);
            y += lineHeight;

            e.Graphics.DrawString($"Contact: {textBox6.Text}", font, brush, x, y);
            y += lineHeight;

            e.Graphics.DrawString($"Email: {textBox7.Text}", font, brush, x, y);
            y += lineHeight;

            e.Graphics.DrawString($"Address: {textBox5.Text}", font, brush, x, y);
            y += lineHeight;

            e.Graphics.DrawString($"Birthday: {textBox4.Text}", font, brush, x, y);
            y += lineHeight;

            e.Graphics.DrawString($"Facebook Account: {textBox8.Text}", font, brush, x, y);
            y += lineHeight;

            
            if (pictureBox2.Image != null)
            {
                int imageX = x + 400;
                int imageY = 100;
                int imageWidth = 220;
                int imageHeight = 220;
                e.Graphics.DrawImage(pictureBox2.Image, imageX, imageY, imageWidth, imageHeight);
            }
        }
    }
}
