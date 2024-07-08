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
    public partial class EditExpenses : Form
    {
        private string _id;

        public EditExpenses(string id, string amount, string category, string paymentMethod, string description, string expenseDate, string enteredBy, string enteredDate)
        {
            InitializeComponent();
            _id = id;
            textBox1.Text = amount;
            comboBox1.SelectedItem = category;
            comboBox2.SelectedItem = paymentMethod;
            textBox2.Text = description;
            guna2DateTimePicker1.Value = DateTime.Parse(expenseDate);
            textBox3.Text = enteredBy;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                int amount = int.Parse(textBox1.Text);
                string category = comboBox1.SelectedItem.ToString();
                string paymentMethod = comboBox2.SelectedItem.ToString();
                string description = textBox2.Text;
                string expenseDate = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd");
                string enteredBy = textBox3.Text;

                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                string query = "UPDATE [Expenses] SET [Amount] = ?, [Category] = ?, [PaymentMethod] = ?, [Description] = ?, [ExpenseDate] = ?, [EnteredBy] = ? WHERE [ID] = ?";
                OleDbCommand cmd = new OleDbCommand(query, dbConnection.GetConnection());
                cmd.Parameters.AddWithValue("?", amount);
                cmd.Parameters.AddWithValue("?", category);
                cmd.Parameters.AddWithValue("?", paymentMethod);
                cmd.Parameters.AddWithValue("?", description);
                cmd.Parameters.AddWithValue("?", expenseDate);
                cmd.Parameters.AddWithValue("?", enteredBy);
                cmd.Parameters.AddWithValue("?", _id);

                cmd.ExecuteNonQuery();

                dbConnection.CloseConnection();

                MessageBox.Show("Expense updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating expense: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

