using iChurch.DBAccess.Connection;
using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace ChurchSystem.Dashboard_Forms.Members
{
    public partial class EventDetailsForm : Form
    {
        private string eventName;
        private string eventType;
        private string eventVenue;
        private string startTime;
        private string endTime;
        private DateTime selectedDate;
        private Color eventColor;
        private Panel panel5;
        private ContextMenuStrip optionsMenu;
        private Button newButton;
        private string eventId;
        private AccessConnection dbConnection;



        public EventDetailsForm(DateTime selectedDate, Color eventColor, Panel panel5)
        {
            InitializeComponent();

            this.selectedDate = selectedDate;
            this.eventColor = eventColor;
            this.panel5 = panel5;

            txtdate.Text = selectedDate.ToString("yyyy-MM-dd");
            SetSelectedDate(selectedDate);

        }



        private void LoadEventDetails()
        {
            dbConnection = new AccessConnection();

            string query = "SELECT EventName, EventType, Venue, [StartTime], [EndTime], [Date] " +
                           "FROM Events " +
                           "WHERE EventID = @eventId";

            try
            {
                dbConnection.OpenConnection();
                OleDbCommand cmd = new OleDbCommand(query, dbConnection.GetConnection());
                cmd.Parameters.AddWithValue("@eventId", eventId);

                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    eventName = reader["EventName"].ToString();
                    eventType = reader["EventType"].ToString();
                    eventVenue = reader["Venue"].ToString();
                    startTime = reader["StartTime"].ToString();
                    endTime = reader["EndTime"].ToString();
                    selectedDate = Convert.ToDateTime(reader["Date"]);

                    // Update UI with loaded event details
                    txtdate.Text = selectedDate.ToString("yyyy-MM-dd");
                    txteventname.Text = eventName;
                    txttype.Text = eventType;
                    txtvenue.Text = eventVenue;
                    cmbtime.Text = startTime;
                    comboBox1.Text = endTime;

                    DateTime startTimeValue;
                    if (DateTime.TryParseExact(startTime, "H:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out startTimeValue))
                    {
                        cmbtime.SelectedItem = startTimeValue.ToString("h:mm tt");
                    }

                    DateTime endTimeValue;
                    if (DateTime.TryParseExact(endTime, "H:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out endTimeValue))
                    {
                        comboBox1.SelectedItem = endTimeValue.ToString("h:mm tt");
                    }

                    SetSelectedDate(selectedDate);
                }
                else
                {
                    MessageBox.Show("Event not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }



        public EventDetailsForm(string eventName, string eventType, string eventVenue, string startTime, string endTime, DateTime eventDate, Color eventColor, Panel panel5)
        {
            InitializeComponent();


            this.eventName = eventName;
            this.eventType = eventType;
            this.eventVenue = eventVenue;
            this.startTime = startTime;
            this.endTime = endTime;
            this.selectedDate = eventDate;
            this.eventColor = eventColor;
            this.panel5 = panel5;

            txtdate.Text = selectedDate.ToString("yyyy-MM-dd");
            txteventname.Text = eventName;
            txttype.Text = eventType;
            txtvenue.Text = eventVenue;
            cmbtime.Text = startTime;
            comboBox1.Text = endTime;

            DateTime startTimeValue;
            if (DateTime.TryParseExact(startTime, "H:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out startTimeValue))
            {
                cmbtime.SelectedItem = startTimeValue.ToString("h:mm tt");
            }

            // Format and display end time
            DateTime endTimeValue;
            if (DateTime.TryParseExact(endTime, "H:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out endTimeValue))
            {
                comboBox1.SelectedItem = endTimeValue.ToString("h:mm tt");
            }

            SetSelectedDate(selectedDate);

        }


        public event EventHandler EventUpdated;

        protected virtual void OnEventUpdated()
        {
            EventUpdated?.Invoke(this, EventArgs.Empty);
        }


        public void SetSelectedDate(DateTime date)
        {
            selectedDate = date;
            txtdate.Text = selectedDate.ToString("MMMM dd, yyyy");
        }

        private void EventDetailsForm_Load(object sender, EventArgs e)
        {
            panel4.BackColor = eventColor;
            SetSelectedDate(selectedDate);
        }

        public event EventHandler EventAdded;

        protected virtual void OnEventAdded()
        {
            EventAdded?.Invoke(this, EventArgs.Empty);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to create this event?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                string eventName = txteventname.Text;
                string eventType = txttype.Text;
                string eventVenue = txtvenue.Text;
                string startTimeString = cmbtime.SelectedItem?.ToString();
                string endTimeString = comboBox1.SelectedItem?.ToString();
                DateTime eventDate;
                DateTime startTime;
                DateTime endTime;

                // Check if date is valid
                bool isValidDate = DateTime.TryParse(txtdate.Text, out eventDate);
                if (!isValidDate)
                {
                    MessageBox.Show("Invalid date. Please check the value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if start time is valid
                if (string.IsNullOrEmpty(startTimeString) || !DateTime.TryParseExact(startTimeString, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out startTime))
                {
                    MessageBox.Show("Invalid start time. Please check the value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if end time is valid
                if (string.IsNullOrEmpty(endTimeString) || !DateTime.TryParseExact(endTimeString, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out endTime))
                {
                    MessageBox.Show("Invalid end time. Please check the value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                dbConnection = new AccessConnection();

                string query = "INSERT INTO Events (EventName, EventType, Venue, [StartTime], [EndTime], [Date]) " +
                               "VALUES (@eventName, @eventType, @eventVenue, @startTime, @endTime, @eventDate)";

                try
                {
                    dbConnection.OpenConnection();
                    OleDbCommand cmd = new OleDbCommand(query, dbConnection.GetConnection());
                    cmd.Parameters.AddWithValue("@eventName", eventName);
                    cmd.Parameters.AddWithValue("@eventType", eventType);
                    cmd.Parameters.AddWithValue("@eventVenue", eventVenue);
                    cmd.Parameters.AddWithValue("@startTime", startTime.ToString("h:mm tt")); // Store only time
                    cmd.Parameters.AddWithValue("@endTime", endTime.ToString("h:mm tt")); // Store only time
                    cmd.Parameters.AddWithValue("@eventDate", eventDate);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Event successfully created and saved to database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        OnEventAdded();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to save event to database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    dbConnection.CloseConnection();
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
            foreach (Form form in Application.OpenForms)
            {
                if (form is Events)
                {
                    form.Visible = true;
                    break;
                }
            }
        }
    }
}
