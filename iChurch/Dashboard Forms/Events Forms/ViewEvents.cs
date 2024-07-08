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
        public ViewEvents(string eventName, string eventType, string venue, string startTime, string endTime, DateTime eventDate, string about)
        {
            InitializeComponent();

            // Populate controls with event details
            txteventname.Text = eventName;
            txttype.Text = eventType;
            txtvenue.Text = venue;
            cmbtime.Text = startTime;
            comboBox1.Text = endTime;
            txtdate.Text = eventDate.ToString("yyyy-MM-dd");
            txtdescription.Text = about;


            // Customize form appearance or do any additional setup


        }

        private void ViewEvents_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
