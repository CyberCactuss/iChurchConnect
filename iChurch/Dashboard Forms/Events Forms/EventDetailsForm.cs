using iChurch.DBAccess.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChurchSystem.Dashboard_Forms.Members
{
    public partial class EventDetailsForm : Form
    {
        private string eventName;
        private string eventType;
        private string eventVenue;
        private string eventTime;
        private DateTime selectedDate;
        private Color eventColor;
        private Panel panel5;
        private ContextMenuStrip optionsMenu;
        private Button newButton;
        private AccessConnection dbConnection;
        private int eventId;

        public EventDetailsForm(DateTime selectedDate, Color eventColor, Panel panel5)
        {
            InitializeComponent();

            this.selectedDate = selectedDate;
            this.eventColor = eventColor;
            this.panel5 = panel5;

            txtdate.Text = selectedDate.ToString("yyyy-MM-dd");
            SetSelectedDate(selectedDate);

            optionsMenu = new ContextMenuStrip();
            ToolStripMenuItem menuItem1 = new ToolStripMenuItem("Edit");
            ToolStripMenuItem menuItem2 = new ToolStripMenuItem("Delete");
            ToolStripMenuItem menuItem3 = new ToolStripMenuItem("Save");

            optionsMenu.Items.Add(menuItem1);
            optionsMenu.Items.Add(menuItem2);
            optionsMenu.Items.Add(menuItem3);

            button2.ContextMenuStrip = optionsMenu;
            button2.Click += button2_Click;
        }


        public EventDetailsForm(int eventId, string eventName, string eventType, string eventVenue, string eventTime, DateTime eventDate, Color eventColor, Panel panel5)
        {
            InitializeComponent();

            this.eventId = eventId;
            this.eventName = eventName;
            this.eventType = eventType;
            this.eventVenue = eventVenue;
            this.eventTime = eventTime;
            this.selectedDate = eventDate;
            this.eventColor = eventColor;
            this.panel5 = panel5;

            txtdate.Text = selectedDate.ToString("yyyy-MM-dd");
            txteventname.Text = eventName;
            txttype.Text = eventType;
            txtvenue.Text = eventVenue;
            cmbtime.Text = eventTime;

            SetSelectedDate(selectedDate);

            optionsMenu = new ContextMenuStrip();
            ToolStripMenuItem menuItem1 = new ToolStripMenuItem("Edit");
            ToolStripMenuItem menuItem2 = new ToolStripMenuItem("Delete");
            ToolStripMenuItem menuItem3 = new ToolStripMenuItem("Save");

            optionsMenu.Items.Add(menuItem1);
            optionsMenu.Items.Add(menuItem2);
            optionsMenu.Items.Add(menuItem3);

            button2.ContextMenuStrip = optionsMenu;
            button2.Click += button2_Click;
        }


        private void DeleteEvent_Click(object sender, EventArgs e)
        {

            Button eventButton = FindEventButton(panel5);
            if (eventButton != null)
            {
                panel5.Controls.Remove(eventButton);
            }
            this.Close();
        }


        private Button FindEventButton(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is Button button && button.Tag == this)
                {
                    return button;
                }
            }
            return null;
        }

        public void SetSelectedDate(DateTime date)
        {
            selectedDate = date;
            txtdate.Text = selectedDate.ToString("MMMM dd, yyyy");
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
        }

        private void EventDetailsForm_Load(object sender, EventArgs e)
        {
            panel4.BackColor = eventColor;
            SetSelectedDate(selectedDate);
        }

        private void btnadd_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to create this event?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                string eventName = txteventname.Text;
                string eventType = txttype.Text;
                string eventVenue = txtvenue.Text;
                string eventTimeString = cmbtime.SelectedItem?.ToString(); // Ensure eventTimeString is not null
                DateTime eventDate;

                bool isValidDate = DateTime.TryParse(txtdate.Text, out eventDate);
                DateTime eventTime;

                if (!isValidDate || string.IsNullOrEmpty(eventTimeString) || !DateTime.TryParseExact(eventTimeString, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out eventTime))
                {
                    MessageBox.Show("Invalid date or time. Please check the values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Combine the date and time
                eventDate = eventDate.Date.Add(eventTime.TimeOfDay);

                string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "iChurchConnect.accdb");
                AccessConnection dbConnection = new AccessConnection(dbPath);

                string query = "INSERT INTO Events (EventName, EventType, Venue, [StartTime], [Date]) " +
                               "VALUES (@eventName, @eventType, @eventVenue, @eventTime, @eventDate)";

                try
                {
                    dbConnection.OpenConnection();
                    OleDbCommand cmd = new OleDbCommand(query, dbConnection.GetConnection());
                    cmd.Parameters.AddWithValue("@eventName", eventName);
                    cmd.Parameters.AddWithValue("@eventType", eventType);
                    cmd.Parameters.AddWithValue("@eventVenue", eventVenue);
                    cmd.Parameters.AddWithValue("@eventTime", eventTime.ToString("h:mm tt")); // Store time as string
                    cmd.Parameters.AddWithValue("@eventDate", eventDate.ToString("yyyy-MM-dd")); // Store date as string

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Event successfully created and saved to database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        OnEventAdded(); // Raise the event
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


        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            optionsMenu.Show(button2, new Point(0, button2.Height));
        }

        public event EventHandler EventAdded;

        protected virtual void OnEventAdded()
        {
            EventAdded?.Invoke(this, EventArgs.Empty);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
        }


    }
}