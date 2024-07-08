using iChurch.Dashboard_Forms.Inventory_Forms;
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
    public partial class Income : Form
    {
        public Income()
        {
            InitializeComponent();
            LoadIncomeData();
        } 

        public void LoadIncomeData()
        {
            try
            {
                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                string query = "SELECT [ID], [Amount], [Category], [PaymentMethod], [Person/Organization], [GivenDate] FROM Income";
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
                    HeaderText = "Person/Organization",
                    DataPropertyName = "Person/Organization",
                    Name = "Person/Organization"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Given Date",
                    DataPropertyName = "GivenDate",
                    Name = "GivenDate"
                });

                guna2DataGridView1.Columns["ID"].FillWeight = 20;
                guna2DataGridView1.Columns["Amount"].FillWeight = 50;
                guna2DataGridView1.Columns["Category"].FillWeight = 70;
                guna2DataGridView1.Columns["PaymentMethod"].FillWeight = 70;
                guna2DataGridView1.Columns["Person/Organization"].FillWeight = 80;
                guna2DataGridView1.Columns["GivenDate"].FillWeight = 40;

                guna2DataGridView1.DataSource = dataTable;

                dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading income data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e) // ADD BUTTON
        {
            AddIncome addincome = new AddIncome(this);
            addincome.ShowDialog();
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
                string personOrganization = selectedRow.Cells["Person/Organization"].Value.ToString();
                string givenDate = selectedRow.Cells["GivenDate"].Value.ToString();

                EditIncome editincome = new EditIncome(id, amount, category, paymentMethod, personOrganization, givenDate, this);
                editincome.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select an item to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e) // DELETE BUTTON
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
                        string id = selectedRow.Cells["ID"].Value.ToString();

                        AccessConnection dbConnection = new AccessConnection();
                        dbConnection.OpenConnection();

                        string query = "DELETE FROM Income WHERE [ID] = ?";
                        OleDbCommand cmd = new OleDbCommand(query, dbConnection.GetConnection());
                        cmd.Parameters.AddWithValue("?", id);
                        cmd.ExecuteNonQuery();

                        dbConnection.CloseConnection();

                        LoadIncomeData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting income item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) // TABLE
        {

        }
    }
}
