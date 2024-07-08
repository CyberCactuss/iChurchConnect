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
    public partial class EditIncome : Form
    {
        private Income parentForm;
        private string incomeId;

        public EditIncome(string id, string amount, string category, string paymentMethod, string personOrganization, string givenDate, Income parent)
        {
            InitializeComponent();
            parentForm = parent;
            incomeId = id;
            textBox1.Text = amount;
            comboBox1.SelectedItem = category;
            comboBox2.SelectedItem = paymentMethod;
            textBox2.Text = personOrganization;
            guna2DateTimePicker1.Value = DateTime.Parse(givenDate);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                string query = "UPDATE Income SET [Amount] = ?, [Category] = ?, [PaymentMethod] = ?, [Person/Organization] = ?, [GivenDate] = ? WHERE [ID] = ?";
                OleDbCommand cmd = new OleDbCommand(query, dbConnection.GetConnection());
                cmd.Parameters.AddWithValue("?", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("?", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("?", comboBox2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("?", textBox2.Text);
                cmd.Parameters.AddWithValue("?", guna2DateTimePicker1.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("?", incomeId);
                cmd.ExecuteNonQuery();

                dbConnection.CloseConnection();
                parentForm.LoadIncomeData();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating income item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

