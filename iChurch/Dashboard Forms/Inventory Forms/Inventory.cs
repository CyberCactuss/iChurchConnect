using iChurch.DBAccess.Connection;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using iChurch.Dashboard_Forms.Inventory_Forms;

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
            guna2DataGridView1.CellPainting += guna2DataGridView1_CellPainting;
        }

        private void LoadInventoryData()
        {
            try
            {
                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                string query = "SELECT ItemID, ItemName, Quantity, Image FROM Inventory";
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
                    DataPropertyName = "Quantity",
                    Name = "Quantity"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Image",
                    DataPropertyName = "Image",
                    Name = "Image"
                });

                guna2DataGridView1.DataSource = dataTable;

                dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView1.Columns["Image"].Index && e.RowIndex >= 0)
            {


                e.Handled = true;
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell content click events if needed
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddItem additem = new AddItem();
            additem.ShowDialog();
        }
    }
}
