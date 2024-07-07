using Guna.UI2.WinForms;
using iChurch.DBAccess.Connection;
using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace iChurch.Dashboard_Forms.Members_Forms
{
    public partial class EditMember2 : Form
    {
        private int MemberId { get; set; }
        private string MemberName { get; set; }
        private string MemberEmail { get; set; }
        private int MemberAge { get; set; }
        private string MemberSex { get; set; }
        private string MemberContact { get; set; }
        private string MemberAddress { get; set; }
        private DateTime MemberBirthday { get; set; }

        public EditMember2()
        {
            InitializeComponent();
        }

        public EditMember2(int memberId, string name, string email, int age, string sex, string contact, string address, DateTime birthday, string facebook)
        {
            InitializeComponent();

            MemberId = memberId;
            MemberName = name;
            MemberEmail = email;
            MemberAge = age;
            MemberSex = sex;
            MemberContact = contact;
            MemberAddress = address;
            MemberBirthday = birthday;

            textBox3.Text = contact;
            textBox2.Text = facebook;
            textBox1.Text = address;
            guna2DateTimePicker1.Value = birthday;
        }

        private void textBox3_TextChanged(object sender, EventArgs e) // CONTACT
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) // ADDRESS
        {

        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e) // BIRTHDAY
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e) // SAVE BUTTON
        {
            try
            {

                AccessConnection dbConnection = new AccessConnection();
                dbConnection.OpenConnection();

                string query = "UPDATE Members SET FullName = ?, Email = ?, Age = ?, Sex = ?, Contact = ?, Address = ?, Birthday = ?, FacebookAccount = ? WHERE ID = ?";
                OleDbCommand command = new OleDbCommand(query, dbConnection.GetConnection());
                command.Parameters.AddWithValue("@FullName", MemberName);
                command.Parameters.AddWithValue("@Email", MemberEmail);
                command.Parameters.AddWithValue("@Age", MemberAge);
                command.Parameters.AddWithValue("@Sex", MemberSex);
                command.Parameters.AddWithValue("@Contact", textBox3.Text);
                command.Parameters.AddWithValue("@Address", textBox1.Text);
                command.Parameters.AddWithValue("@Birthday", guna2DateTimePicker1.Value);
                command.Parameters.AddWithValue("@FacebookAccount", textBox2.Text);
                command.Parameters.AddWithValue("@ID", MemberId);

                command.ExecuteNonQuery();

                dbConnection.CloseConnection();
                MessageBox.Show("Member data updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating member data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
