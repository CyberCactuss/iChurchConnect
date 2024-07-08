using iChurch.Dashboard_Forms.Inventory_Forms;
using iChurch.DBAccess.Connection;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ChurchSystem.Dashboard_Forms
{
    public partial class Inventory : Form
    {
        public Inventory()
        {
            InitializeComponent();
            this.Load += Inventory_Load;
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            LoadInventoryData();
        }

        public void LoadInventoryData()
        {
            try
            {
                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                string query = "SELECT ItemID, ItemName, Qnty FROM Inventory";
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, dbConnection.GetConnection());
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                guna2DataGridView1.AutoGenerateColumns = false;

                // Clear existing columns
                guna2DataGridView1.Columns.Clear();

                // Add columns
                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Item ID",
                    DataPropertyName = "ItemID",
                    Name = "ItemID"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Item Name",
                    DataPropertyName = "ItemName",
                    Name = "ItemName"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Quantity",
                    DataPropertyName = "Qnty",
                    Name = "Qnty"
                });

                guna2DataGridView1.DataSource = dataTable;

                dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddItem addItem = new AddItem(this); // Pass reference to the current form
            addItem.ShowDialog();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
                string itemID = selectedRow.Cells["ItemID"].Value.ToString();
                string itemName = selectedRow.Cells["ItemName"].Value.ToString();
                int quantity = Convert.ToInt32(selectedRow.Cells["Qnty"].Value);

                // Get the image path from the database
                string imagePath = GetImagePath(itemID);

                EditItem editItem = new EditItem(itemID, itemName, quantity, imagePath, this);
                editItem.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select an item to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetImagePath(string itemId)
        {
            try
            {
                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                string query = "SELECT Image FROM Inventory WHERE ItemID = ?";
                OleDbCommand cmd = new OleDbCommand(query, dbConnection.GetConnection());
                cmd.Parameters.AddWithValue("?", itemId);
                object result = cmd.ExecuteScalar();

                dbConnection.CloseConnection();

                return result?.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving image path: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e) // VIEW BUTTON
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
                string itemId = selectedRow.Cells["ItemID"].Value.ToString();
                string itemName = selectedRow.Cells["ItemName"].Value.ToString();
                int quantity = Convert.ToInt32(selectedRow.Cells["Qnty"].Value);

                // Get the image path from the database
                string imagePath = GetImagePath(itemId);

                ViewItem viewItem = new ViewItem(itemId, itemName, quantity, imagePath);
                viewItem.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select an item to view.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        string itemId = selectedRow.Cells["ItemID"].Value.ToString();

                        AccessConnection dbConnection = new AccessConnection();
                        dbConnection.OpenConnection();

                        string query = "DELETE FROM Inventory WHERE ItemID = ?";
                        OleDbCommand cmd = new OleDbCommand(query, dbConnection.GetConnection());
                        cmd.Parameters.AddWithValue("?", itemId);
                        cmd.ExecuteNonQuery();

                        dbConnection.CloseConnection();

                        LoadInventoryData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}