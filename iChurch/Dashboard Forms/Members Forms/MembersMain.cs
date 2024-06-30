using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using iChurch.DBAccess.Connection;

namespace ChurchSystem.Dashboard_Forms.MembersFiles
{
    public partial class MembersMain : Form
    {
        public MembersMain()
        {
            InitializeComponent();
        }

        private void MembersMain_Load(object sender, EventArgs e)
        {
            LoadMembersData();
        }

        private void LoadMembersData()
        {
            try
            {
                string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "iChurchConnect.accdb");
                AccessConnection dbConnection = new AccessConnection(dbPath);

                dbConnection.OpenConnection();

                string query = "SELECT FullName, Age, Sex, Contact, Email FROM Members";
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, dbConnection.GetConnection());
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                guna2DataGridView1.AutoGenerateColumns = false;
                guna2DataGridView1.DataSource = dataTable;

                dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading member data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MembersMain_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void guna2Button1_Click(object sender, EventArgs e) // ADD BUTTON
        {
            AddMember add = new AddMember();
            add.ShowDialog();
            LoadMembersData(); // Refresh data after adding a member
        }

        private void guna2Button2_Click(object sender, EventArgs e) // DELETE BUTTON
        {
        }

        private void guna2Button3_Click(object sender, EventArgs e) // UPDATE BUTTON
        {
            LoadMembersData();
        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
