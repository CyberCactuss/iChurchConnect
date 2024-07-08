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
    public partial class AddExpenses : Form
    {
        public AddExpenses()
        {
            InitializeComponent();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e) // SAVE BUTTON
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    comboBox1.SelectedItem == null ||
                    comboBox2.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Please fill all fields and select valid options.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int amount;
                if (!int.TryParse(textBox1.Text, out amount))
                {
                    MessageBox.Show("Please enter a valid amount.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string category = comboBox1.SelectedItem.ToString();
                string paymentMethod = comboBox2.SelectedItem.ToString();
                string description = textBox2.Text;
                string expenseDate = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd");
                string enteredBy = textBox3.Text;
                string enteredDate = DateTime.Now.ToString("yyyy-MM-dd");

                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                string query = "INSERT INTO [Expenses] ([Amount], [Category], [PaymentMethod], [Description], [ExpenseDate], [EnteredBy], [EnteredDate]) VALUES (?, ?, ?, ?, ?, ?, ?)";
                OleDbCommand cmd = new OleDbCommand(query, dbConnection.GetConnection());
                cmd.Parameters.AddWithValue("?", amount);
                cmd.Parameters.AddWithValue("?", category);
                cmd.Parameters.AddWithValue("?", paymentMethod);
                cmd.Parameters.AddWithValue("?", description);
                cmd.Parameters.AddWithValue("?", expenseDate);
                cmd.Parameters.AddWithValue("?", enteredBy);
                cmd.Parameters.AddWithValue("?", enteredDate);

                cmd.ExecuteNonQuery();

                dbConnection.CloseConnection();

                MessageBox.Show("Expense added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding expense: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

