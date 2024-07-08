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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ChurchSystem.Dashboard_Forms
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            LoadIncomeData();
            LoadEventsData(); 
        }

        public void LoadEventsData()
        {
            try
            {
                // Establishing the connection
                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                // Query to fetch event details
                string query = "SELECT EventName, EventType, Venue, Date FROM Events ORDER BY Date";
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, dbConnection.GetConnection());
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Ensure that the DataGridView doesn't auto-generate columns
                guna2DataGridView2.AutoGenerateColumns = false;

                // Clear existing columns
                guna2DataGridView2.Columns.Clear();

                // Add columns to the DataGridView
                guna2DataGridView2.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Event",
                    DataPropertyName = "EventName",
                    Name = "EventName"
                });

                guna2DataGridView2.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Type",
                    DataPropertyName = "EventType",
                    Name = "EventType"
                });

                guna2DataGridView2.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Venue",
                    DataPropertyName = "Venue",
                    Name = "Venue"
                });

                guna2DataGridView2.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Date",
                    DataPropertyName = "Date",
                    Name = "Date"
                });

                // Bind the data to the DataGridView
                guna2DataGridView2.DataSource = dataTable;

                // Close the database connection
                dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading events data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void LoadIncomeData()
        {
            try
            {
                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                string query = "SELECT Amount, GivenDate FROM Income";
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, dbConnection.GetConnection());
                DataTable incomeDataTable = new DataTable();
                dataAdapter.Fill(incomeDataTable);
                dbConnection.CloseConnection();

                DateTime now = DateTime.Now;
                var thisWeekStart = now.AddDays(-(int)now.DayOfWeek);
                var thisMonthStart = new DateTime(now.Year, now.Month, 1);
                var thisYearStart = new DateTime(now.Year, 1, 1);

                decimal totalIncomeMonth = 0;

                foreach (DataRow row in incomeDataTable.Rows)
                {
                    DateTime givenDate = Convert.ToDateTime(row["GivenDate"]);
                    decimal amount = Convert.ToDecimal(row["Amount"]);


                    if (givenDate >= thisMonthStart) totalIncomeMonth += amount;

                }

                textBox2.Text = totalIncomeMonth.ToString("C2");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading income data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
