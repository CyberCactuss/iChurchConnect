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
    public partial class Expenses : Form
    {
        public Expenses()
        {
            InitializeComponent();
            LoadExpensesData();
        }

        private void LoadExpensesData()
        {
            try
            {
                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                string query = "SELECT [ID], [Amount], [Category], [PaymentMethod], [Description], [ExpenseDate], [EnteredBy], [EnteredDate] FROM Expenses";
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, dbConnection.GetConnection());
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                guna2DataGridView1.AutoGenerateColumns = false;

                // Clear existing columns
                guna2DataGridView1.Columns.Clear();

                // Add columns
                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "ID",
                    DataPropertyName = "ID",
                    Name = "ID"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Amount",
                    DataPropertyName = "Amount",
                    Name = "Amount"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Category",
                    DataPropertyName = "Category",
                    Name = "Category"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Payment Method",
                    DataPropertyName = "PaymentMethod",
                    Name = "PaymentMethod"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Description",
                    DataPropertyName = "Description",
                    Name = "Description"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Expense Date",
                    DataPropertyName = "ExpenseDate",
                    Name = "ExpenseDate"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Entered By",
                    DataPropertyName = "EnteredBy",
                    Name = "EnteredBy"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Entered Date",
                    DataPropertyName = "EnteredDate",
                    Name = "EnteredDate"
                });

                guna2DataGridView1.DataSource = dataTable;

                dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading expenses data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e) // EDIT BUTTON
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
                string id = selectedRow.Cells["ID"].Value.ToString();
                string amount = selectedRow.Cells["Amount"].Value.ToString();
                string category = selectedRow.Cells["Category"].Value.ToString();
                string paymentMethod = selectedRow.Cells["PaymentMethod"].Value.ToString();
                string description = selectedRow.Cells["Description"].Value.ToString();
                string expenseDate = selectedRow.Cells["ExpenseDate"].Value.ToString();
                string enteredBy = selectedRow.Cells["EnteredBy"].Value.ToString();
                string enteredDate = selectedRow.Cells["EnteredDate"].Value.ToString();

                EditExpenses editExpenses = new EditExpenses(id, amount, category, paymentMethod, description, expenseDate, enteredBy, enteredDate);
                editExpenses.ShowDialog();
                LoadExpensesData();
            }
            else
            {
                MessageBox.Show("Please select an expense to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) // TABLE
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e) // DELETE BUTTON
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Are you sure you want to delete this expense?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
                        string id = selectedRow.Cells["ID"].Value.ToString();

                        AccessConnection dbConnection = new AccessConnection();
                        dbConnection.OpenConnection();

                        string query = "DELETE FROM Expenses WHERE [ID] = ?";
                        OleDbCommand cmd = new OleDbCommand(query, dbConnection.GetConnection());
                        cmd.Parameters.AddWithValue("?", id);
                        cmd.ExecuteNonQuery();

                        dbConnection.CloseConnection();

                        LoadExpensesData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting expense item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an expense to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e) // ADD BUTTON
        {
            AddExpenses addExpenses = new AddExpenses();
            addExpenses.ShowDialog();
            LoadExpensesData();
        }
    }
}
