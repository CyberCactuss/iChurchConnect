using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using iChurch.DBAccess.Connection;

namespace iChurch.Dashboard_Forms.Events_Forms
{
    public partial class EditEvent : Form
    {
        private AccessConnection dbConnection;
        private string eventID;

        public EditEvent(string eventID, string eventName, string eventType, string venue, string startTime, string endTime, DateTime eventDate, string about)
        {
            InitializeComponent();

            this.eventID = eventID;

            txteventname.Text = eventName;
            txttype.Text = eventType;
            txtvenue.Text = venue;
            cmbtime.Text = startTime;
            comboBox1.Text = endTime;
            txtdate.Text = eventDate.ToString("yyyy-MM-dd");
            txtdescription.Text = about;


        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                string query = "UPDATE Events SET EventName = @eventName, EventType = @eventType, Venue = @venue, StartTime = @startTime, EndTime = @endTime, [Date] = @eventDate, About = @about WHERE ID = @eventID";

                OleDbCommand command = new OleDbCommand(query, dbConnection.GetConnection());
                command.Parameters.AddWithValue("@eventName", txteventname.Text);
                command.Parameters.AddWithValue("@eventType", txttype.Text);
                command.Parameters.AddWithValue("@venue", txtvenue.Text);
                command.Parameters.AddWithValue("@startTime", cmbtime.Text);
                command.Parameters.AddWithValue("@endTime", comboBox1.Text);
                command.Parameters.AddWithValue("@eventDate", DateTime.Parse(txtdate.Text));
                command.Parameters.AddWithValue("@about", txtdescription.Text);
                command.Parameters.AddWithValue("@eventID", eventID);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Event updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No rows updated. Event might not exist or no changes made.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbConnection != null)
                {
                    dbConnection.CloseConnection();
                    dbConnection = null;
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
