using System;
using System.Drawing;
using System.Windows.Forms;

namespace iChurch.Dashboard_Forms.Events_Forms
{
    public partial class EditEvent : Form
    {
        public EditEvent(string eventName, string eventType, string venue, string startTime, string endTime, DateTime eventDate, Color eventColor)
        {
            InitializeComponent();

            // Populate controls with event details
            txtname.Text = eventName;
            txttype.Text = eventType;
            txtvenue.Text = venue;
            cmbtime.Text = startTime;
            comboBox1.Text = endTime;
            txtdate.Text = eventDate.ToString("yyyy-MM-dd");

            // Customize form appearance or do any additional setup
            panel4.BackColor = eventColor;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Add functionality for your button here
        }
    }
}
