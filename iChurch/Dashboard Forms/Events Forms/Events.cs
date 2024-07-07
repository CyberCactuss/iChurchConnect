using ChurchSystem.Dashboard_Forms.Members;
using Guna.UI2.WinForms;
using iChurch.Dashboard_Forms.Events_Forms;
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
            dataGridViewEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEvents.MultiSelect = false; // Ensure only one row can be selected at a time

            refreshbtn.Click += refreshbtn_Click;


        }


        private void Events_Load(object sender, EventArgs e)
        {
            LoadEventsData();


        }


        private void LoadEventsData()
        {
            try
            {
                AccessConnection dbConnection = new AccessConnection();

                dbConnection.OpenConnection();

                string query = "SELECT ID, EventName, EventType, Venue, StartTime, EndTime, Date FROM Events";
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
                    DataPropertyName = "ID", // This should match the column name from your database
                    Name = "ID",
                    Width = 20

                });

                dataGridViewEvents.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Event Name",
                    DataPropertyName = "EventName",
                    Name = "EventName",
                    Width = 100
                });

                dataGridViewEvents.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Event Type",
                    DataPropertyName = "EventType",
                    Name = "EventType",
                    Width = 70

                });

                dataGridViewEvents.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Venue",
                    DataPropertyName = "Venue",
                    Name = "Venue",
                    Width = 100
                });

                dataGridViewEvents.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Start Time",
                    DataPropertyName = "StartTime",
                    Name = "StartTime",
                    Width = 30,
                    ValueType = typeof(DateTime) // Set ValueType to DateTime
                });

                dataGridViewEvents.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "End Time",
                    DataPropertyName = "EndTime",
                    Name = "EndTime",
                    Width = 30,
                    ValueType = typeof(DateTime) // Set ValueType to DateTime
                });

                dataGridViewEvents.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Date",
                    DataPropertyName = "Date",
                    Name = "Date",
                    Width = 70
                });

                dataGridViewEvents.AutoGenerateColumns = false;
                dataGridViewEvents.DataSource = dataTable;

                // Handle CellFormatting event to format time column
                dataGridViewEvents.CellFormatting += DataGridViewEvents_CellFormatting;

                // Set font and color for column headers
                dataGridViewEvents.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 10, FontStyle.Bold);


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
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Get the selected event details from the DataGridView
                DataGridViewRow selectedRow = dataGridViewEvents.Rows[e.RowIndex];
                string eventName = selectedRow.Cells["EventName"].Value.ToString();
                string eventType = selectedRow.Cells["EventType"].Value.ToString();
                string venue = selectedRow.Cells["Venue"].Value.ToString();
                string starttime = FormatTimeValue(selectedRow.Cells["StartTime"]);
                string endtime = FormatTimeValue(selectedRow.Cells["EndTime"]);
                DateTime eventDate = (DateTime)selectedRow.Cells["Date"].Value;


            }
        }
        private string FormatTimeValue(DataGridViewCell cell)
        {
            if (cell.Value != null)
            {
                DateTime dateTimeValue;
                if (DateTime.TryParse(cell.Value.ToString(), out dateTimeValue))
                {
                    return dateTimeValue.ToString("hh:mm tt");
                }
            }
            return string.Empty;
        }


        private void DataGridViewEvents_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewEvents.Columns[e.ColumnIndex].Name == "StartTime" ||
                dataGridViewEvents.Columns[e.ColumnIndex].Name == "EndTime")
            {
                if (e.Value != null)
                {
                    DateTime dateTimeValue;
                    if (DateTime.TryParse(e.Value.ToString(), out dateTimeValue))
                    {
                        e.Value = dateTimeValue.ToString("hh:mm tt");
                    }
                }
            }
        }

        private void OpenEventDetailsForm(string eventName, string eventType, string venue, string startTime, string endTime, DateTime eventDate)
        {
            Color eventColor = GenerateRandomColor(); // Or any method you use to generate the event color
            Panel panel5 = this.panel5; // Assuming panel5 is a member of the class
            EventDetailsForm eventDetailsForm = new EventDetailsForm(eventName, eventType, venue, startTime, endTime, eventDate, eventColor, panel5);


            eventDetailsForm.ShowDialog();
        }


        private void button3_Click(object sender, EventArgs e)
        {

        }



        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            panel5.AutoScroll = true;
        }

        private void refreshbtn_Click(object sender, EventArgs e)
        {
            string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "iChurchConnect.accdb");
            string query = "SELECT ID, EventName, EventType, Venue, StartTime, EndTime, Date FROM Events";

            try
            {
                using (OleDbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};"))
                {
                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                    DataTable eventsTable = new DataTable();
                    adapter.Fill(eventsTable);

                    dataGridViewEvents.DataSource = eventsTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading events: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnview_Click(object sender, EventArgs e)
        {
            if (dataGridViewEvents.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewEvents.SelectedRows[0];
                string eventName = selectedRow.Cells["EventName"].Value.ToString();
                string eventType = selectedRow.Cells["EventType"].Value.ToString();
                string venue = selectedRow.Cells["Venue"].Value.ToString();
                string startTime = FormatTimeValue(selectedRow.Cells["StartTime"]);
                string endTime = FormatTimeValue(selectedRow.Cells["EndTime"]);
                DateTime eventDate = (DateTime)selectedRow.Cells["Date"].Value;

                // Open ViewEvents form with event details
                Color eventColor = GenerateRandomColor(); // Generate a color for the event
                ViewEvents viewEventsForm = new ViewEvents(eventName, eventType, venue, startTime, endTime, eventDate, eventColor);
                viewEventsForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select an event to view.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dataGridViewEvents.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewEvents.SelectedRows[0];
                string eventName = selectedRow.Cells["EventName"].Value.ToString();
                string eventType = selectedRow.Cells["EventType"].Value.ToString();
                string venue = selectedRow.Cells["Venue"].Value.ToString();
                string startTime = FormatTimeValue(selectedRow.Cells["StartTime"]);
                string endTime = FormatTimeValue(selectedRow.Cells["EndTime"]);
                DateTime eventDate = (DateTime)selectedRow.Cells["Date"].Value;


                // Open EditEvent form with event details
                Color eventColor = GenerateRandomColor();
                EditEvent editeventForm = new EditEvent(eventName, eventType, venue, startTime, endTime, eventDate, eventColor);
                editeventForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select an event to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewEvents.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewEvents.SelectedRows[0];
                int eventId = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                DialogResult result = MessageBox.Show($"Are you sure you want to delete the event with ID {eventId}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DeleteEvent(eventId);
                    LoadEventsData();
                }
            }
            else
            {
                MessageBox.Show("Please select an event to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteEvent(int eventId)
        {
            try
            {
                AccessConnection dbConnection = new AccessConnection();

                dbConnection.OpenConnection();

                string deleteQuery = "DELETE FROM Events WHERE ID = @EventID";

                OleDbCommand command = new OleDbCommand(deleteQuery, dbConnection.GetConnection());
                command.Parameters.AddWithValue("@ID", eventId);
                command.ExecuteNonQuery();
                dbConnection.CloseConnection();


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting event data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
