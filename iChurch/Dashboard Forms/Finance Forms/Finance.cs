using iChurch.DBAccess.Connection;
using Guna.Charts.WinForms;
using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using iChurch.Dashboard_Forms.Finance_Forms;

namespace ChurchSystem.Dashboard_Forms
{
    public partial class Finance : Form
    {
        public Finance()
        {
            InitializeComponent();
            LoadIncomeData();
        }

        private void Finance_Load(object sender, EventArgs e)
        {
            LoadIncomeData();
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

                decimal totalIncomeWeek = 0;
                decimal totalIncomeMonth = 0;
                decimal totalIncomeYear = 0;

                foreach (DataRow row in incomeDataTable.Rows)
                {
                    DateTime givenDate = Convert.ToDateTime(row["GivenDate"]);
                    decimal amount = Convert.ToDecimal(row["Amount"]);

                    if (givenDate >= thisWeekStart) totalIncomeWeek += amount;
                    if (givenDate >= thisMonthStart) totalIncomeMonth += amount;
                    if (givenDate >= thisYearStart) totalIncomeYear += amount;
                }

                textBox1.Text = totalIncomeWeek.ToString("C2");
                textBox2.Text = totalIncomeMonth.ToString("C2");
                textBox3.Text = totalIncomeYear.ToString("C2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading income data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Income income = new Income();
            income.ShowDialog();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Expenses expenses = new Expenses();
            expenses.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
