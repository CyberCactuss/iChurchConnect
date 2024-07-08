using iChurch.DBAccess.Connection;
using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace iChurch.Dashboard_Forms.Finance_Forms
{
    public partial class Expenses : Form
    {
        private DataTable expensesDataTable;

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
                expensesDataTable = new DataTable();
                dataAdapter.Fill(expensesDataTable);

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

                guna2DataGridView1.DataSource = expensesDataTable;

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

        private void guna2Button4_Click(object sender, EventArgs e) // PRINT BUTTON
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog
            {
                Document = printDocument
            };
            printPreviewDialog.ShowDialog();
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (expensesDataTable == null || expensesDataTable.Rows.Count == 0)
            {
                MessageBox.Show("No data to print.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Calculate summary
            decimal totalExpenses = 0;
            int recordCount = expensesDataTable.Rows.Count;

            foreach (DataRow row in expensesDataTable.Rows)
            {
                if (row["Amount"] != DBNull.Value)
                {
                    totalExpenses += Convert.ToDecimal(row["Amount"]);
                }
            }

            // Define the font and the initial print position
            Font font = new Font("Arial", 10);
            float lineHeight = font.GetHeight(e.Graphics);
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;

            // Print header
            e.Graphics.DrawString("Expenses Records", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, x, y);
            y += lineHeight * 2;

            // Print column headers
            e.Graphics.DrawString("ID", font, Brushes.Black, x, y);
            e.Graphics.DrawString("Amount", font, Brushes.Black, x + 50, y);
            e.Graphics.DrawString("Category", font, Brushes.Black, x + 150, y);
            e.Graphics.DrawString("Payment Method", font, Brushes.Black, x + 300, y);
            e.Graphics.DrawString("Description", font, Brushes.Black, x + 450, y);
            e.Graphics.DrawString("Expense Date", font, Brushes.Black, x + 600, y);
            e.Graphics.DrawString("Entered By", font, Brushes.Black, x + 750, y);
            e.Graphics.DrawString("Entered Date", font, Brushes.Black, x + 900, y);
            y += lineHeight;

            // Print each record
            foreach (DataRow row in expensesDataTable.Rows)
            {
                e.Graphics.DrawString(row["ID"].ToString(), font, Brushes.Black, x, y);
                e.Graphics.DrawString(row["Amount"].ToString(), font, Brushes.Black, x + 50, y);
                e.Graphics.DrawString(row["Category"].ToString(), font, Brushes.Black, x + 150, y);
                e.Graphics.DrawString(row["PaymentMethod"].ToString(), font, Brushes.Black, x + 300, y);
                e.Graphics.DrawString(row["Description"].ToString(), font, Brushes.Black, x + 450, y);
                e.Graphics.DrawString(row["ExpenseDate"].ToString(), font, Brushes.Black, x + 600, y);
                e.Graphics.DrawString(row["EnteredBy"].ToString(), font, Brushes.Black, x + 750, y);
                e.Graphics.DrawString(row["EnteredDate"].ToString(), font, Brushes.Black, x + 900, y);
                y += lineHeight;
            }

            // Print summary
            y += lineHeight * 2;
            e.Graphics.DrawString($"Total Records: {recordCount}", font, Brushes.Black, x, y);
            y += lineHeight;
            e.Graphics.DrawString($"Total Expenses: {totalExpenses:C2}", font, Brushes.Black, x, y);
        }
    }
}
