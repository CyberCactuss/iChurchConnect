using ChurchSystem.Dashboard_Forms.Members;
using Guna.UI2.WinForms;
using iChurch.DBAccess.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.OleDb;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChurchSystem.Dashboard_Forms
{
    public partial class Events : Form
    {
        private int month;
        private int year;
        private DataTable eventsDataTable;

        public Events()
        {
            InitializeComponent();
            month = DateTime.Now.Month;
            year = DateTime.Now.Year;
            SetMonthYearLabel();
            btnnext.Click += btnNext_Click;
            btnprevious.Click += btnPrevious_Click;
            LoadEventsData();
            dataGridViewEvents.CellClick += dataGridViewEvents_CellClick;

        }

        private void Events_Load(object sender, EventArgs e)
        {
            AdjustDataGridViewAppearance();


        }
        private void AdjustDataGridViewAppearance()
        {
            // Adjust column widths or fill weights if needed
            dataGridViewEvents.Columns["ID"].FillWeight = 20;
            dataGridViewEvents.Columns["EventName"].FillWeight = 100;
            dataGridViewEvents.Columns["EventType"].FillWeight = 70;
            dataGridViewEvents.Columns["Venue"].FillWeight = 100;
            dataGridViewEvents.Columns["Time"].FillWeight = 50;
            dataGridViewEvents.Columns["Date"].FillWeight = 70;

            dataGridViewEvents.Columns["ID"].Width = 50; // Adjust the width as needed
            dataGridViewEvents.Columns["EventName"].Width = 200;
            dataGridViewEvents.Columns["EventType"].Width = 100;
            dataGridViewEvents.Columns["Venue"].Width = 150;
            dataGridViewEvents.Columns["Time"].Width = 80;
            dataGridViewEvents.Columns["Date"].Width = 100;

            // Configure Time column to display time in "h:mm tt" format
            dataGridViewEvents.Columns["Time"].ValueType = typeof(DateTime);
        }

        private void LoadEventsData()
        {
            try
            {
                string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "iChurchConnect.accdb");

                AccessConnection dbConnection = new AccessConnection(dbPath);

                dbConnection.OpenConnection();

                string query = "SELECT ID, EventName, EventType, Venue, Time, Date FROM Events";
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, dbConnection.GetConnection());
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Display a message if no data is loaded
                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("No events found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Clear existing columns if any
                dataGridViewEvents.Columns.Clear();

                // Add columns to dataGridViewEvents
                dataGridViewEvents.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "ID",
                    DataPropertyName = "ID",
                    Name = "ID",
                    FillWeight = 20
                });

                dataGridViewEvents.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Event Name",
                    DataPropertyName = "EventName",
                    Name = "EventName",
                    FillWeight = 100
                });

                dataGridViewEvents.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Event Type",
                    DataPropertyName = "EventType",
                    Name = "EventType",
                    FillWeight = 70
                });

                dataGridViewEvents.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Venue",
                    DataPropertyName = "Venue",
                    Name = "Venue",
                    FillWeight = 100
                });

                dataGridViewEvents.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Time",
                    DataPropertyName = "Time",
                    Name = "Time",
                    FillWeight = 50,
                    ValueType = typeof(DateTime) // Set ValueType to DateTime
                });

                dataGridViewEvents.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Date",
                    DataPropertyName = "Date",
                    Name = "Date",
                    FillWeight = 70
                });

                dataGridViewEvents.AutoGenerateColumns = false;
                dataGridViewEvents.DataSource = dataTable;

                // Handle CellFormatting event to format time column
                dataGridViewEvents.CellFormatting += DataGridViewEvents_CellFormatting;

                // Enable interactive resizing
                dataGridViewEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewEvents.AllowUserToResizeColumns = true;

                dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading events data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SetMonthYearLabel()
        {
            textBox1.Text = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {year}";
            PopulateDays();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            month++;
            if (month > 12)
            {
                month = 1;
                year++;
            }
            SetMonthYearLabel();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            month--;
            if (month < 1)
            {
                month = 12;
                year--;
            }
            SetMonthYearLabel();
        }

        private void PopulateDays()
        {
            flowLayoutPanel1.Controls.Clear();

            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int startingDayOffset = (int)firstDayOfMonth.DayOfWeek;

            int daysInMonth = DateTime.DaysInMonth(year, month);
            int buttonWidth = flowLayoutPanel1.ClientSize.Width / 8;
            int buttonHeight = flowLayoutPanel1.ClientSize.Height / 7;

            for (int day = 1; day <= 42; day++)
            {
                Button button1 = new Button();
                button1.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
                button1.Margin = new Padding(1);

                if (day <= startingDayOffset || day > daysInMonth + startingDayOffset)
                {
                    button1.Enabled = false;
                    button1.BackColor = System.Drawing.Color.LightGray;
                    button1.Font = new System.Drawing.Font("Helvetica", 12, System.Drawing.FontStyle.Regular);
                    button1.ForeColor = System.Drawing.Color.Gray;

                    if (day <= startingDayOffset)
                        button1.Text = (daysInMonth - startingDayOffset + day).ToString();
                    else
                        button1.Text = (day - (daysInMonth + startingDayOffset)).ToString();
                }
                else
                {
                    int currentDay = day - startingDayOffset;
                    button1.Text = currentDay.ToString();
                    button1.BackColor = System.Drawing.Color.White;
                    button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                    button1.FlatAppearance.BorderSize = 1;
                    button1.Font = new System.Drawing.Font("Helvetica", 12, System.Drawing.FontStyle.Regular);

                    if (currentDay == DateTime.Now.Day && month == DateTime.Now.Month && year == DateTime.Now.Year)
                    {
                        button1.BackColor = System.Drawing.Color.LightBlue;
                        button1.ForeColor = System.Drawing.Color.Black;
                    }

                    // Add click event handler for each day button
                    button1.Click += (sender, e) =>
                    {
                        int selectedDay = int.Parse(button1.Text);
                        DateTime selectedDate = new DateTime(year, month, selectedDay);
                        OpenEventDetailsForm(selectedDate);
                    };
                }

                button1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                flowLayoutPanel1.Controls.Add(button1);
            }
        }

        private void OpenEventDetailsForm(DateTime selectedDate)
        {
            Color eventColor = GenerateRandomColor(); // Or any method you use to generate the event color
            Panel panel5 = this.panel5; // Assuming panel5 is a member of the class
            EventDetailsForm eventDetailsForm = new EventDetailsForm(selectedDate, eventColor, panel5);
            eventDetailsForm.EventAdded += (s, e) => LoadEventsData();
            eventDetailsForm.ShowDialog();
        }


        private Color GenerateRandomColor()
        {
            Random random = new Random();
            int red = random.Next(128, 256);
            int blue = random.Next(128, 256);
            int green = random.Next(128, 256);

            return Color.FromArgb(red, green, blue);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = DateTime.Today;
            Color eventColor = GenerateRandomColor();
            EventDetailsForm eventDetailsForm = new EventDetailsForm(selectedDate, eventColor, panel5);
            eventDetailsForm.ShowDialog();
        }

        private void dataGridViewEvents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if a cell is clicked and it's not the header row
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Get the selected event details from the DataGridView
                DataGridViewRow selectedRow = dataGridViewEvents.Rows[e.RowIndex];
                int eventId = (int)selectedRow.Cells["ID"].Value;
                string eventName = selectedRow.Cells["EventName"].Value.ToString();
                string eventType = selectedRow.Cells["EventType"].Value.ToString();
                string venue = selectedRow.Cells["Venue"].Value.ToString();
                string time = selectedRow.Cells["Time"].Value.ToString();
                DateTime eventDate = (DateTime)selectedRow.Cells["Date"].Value;

                // Open EventDetailsForm with selected event details
                OpenEventDetailsForm(eventId, eventName, eventType, venue, time, eventDate);
            }
        }

        private void DataGridViewEvents_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if it's the column you want to format (e.g., "Time" column)
            if (dataGridViewEvents.Columns[e.ColumnIndex].Name == "Time" && e.Value != null && e.Value != DBNull.Value)
            {
                // Format the DateTime value to display only time
                if (DateTime.TryParse(e.Value.ToString(), out DateTime timeValue))
                {
                    e.Value = timeValue.ToString("h:mm tt");
                    e.FormattingApplied = true; // Set to true to indicate that the formatting was applied
                }
            }
        }

        private void OpenEventDetailsForm(int eventId, string eventName, string eventType, string venue, string time, DateTime eventDate)
        {
            Color eventColor = GenerateRandomColor();
            EventDetailsForm eventDetailsForm = new EventDetailsForm(eventId, eventName, eventType, venue, time, eventDate, eventColor, panel5);
            eventDetailsForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }


        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            panel5.AutoScroll = true;
        }
    }
}