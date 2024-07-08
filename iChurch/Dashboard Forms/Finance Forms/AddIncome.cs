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

namespace iChurch.Dashboard_Forms.Finance_Forms
{
    public partial class AddIncome : Form
    {
        private Income parentForm;

        public AddIncome(Income parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e) // SAVE BUTTON
        {
            try
            {
                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                string query = "INSERT INTO Income ([Amount], [Category], [PaymentMethod], [Person/Organization], [GivenDate]) VALUES (?, ?, ?, ?, ?)";
                OleDbCommand cmd = new OleDbCommand(query, dbConnection.GetConnection());
                cmd.Parameters.AddWithValue("?", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("?", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("?", comboBox2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("?", textBox2.Text);
                cmd.Parameters.AddWithValue("?", guna2DateTimePicker1.Value.ToString("yyyy-MM-dd"));
                cmd.ExecuteNonQuery();

                dbConnection.CloseConnection();
                parentForm.LoadIncomeData();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding income item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

