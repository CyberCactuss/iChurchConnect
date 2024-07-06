using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iChurch.Dashboard_Forms.Events_Forms
{
    public partial class ViewEvents : Form
    {
        public ViewEvents(string eventName, string eventType, string venue, string startTime, string endTime, DateTime eventDate, Color eventColor)
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

        private void ViewEvents_Load(object sender, EventArgs e)
        {

        }
    }
}
