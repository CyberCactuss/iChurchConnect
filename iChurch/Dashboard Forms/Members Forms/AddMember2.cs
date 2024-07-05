using iChurch.DBAccess.Connection;
using System.Data.OleDb;
using iChurch.Dashboard_Forms.Members_Forms;
namespace ChurchSystem.Dashboard_Forms.Members
{
    public partial class AddMember2 : Form
    {
        private string? name;
        private string? email;
        private string? age;
        private string? sex;

        public AddMember2()
        {
            InitializeComponent();
        }

        public AddMember2(string name, string email, string age, string sex)
        {
            InitializeComponent();
            this.name = name;
            this.email = email;
            this.age = age;
            this.sex = sex;
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e) // REGISTER BUTTON
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (guna2DateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Please enter a valid date of birth.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string birthday = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd");
            string contact = textBox3.Text;
            string address = textBox1.Text;

            MemberDetails memberDetails = new MemberDetails
            {
                Name = name,
                Email = email,
                Age = age,
                Sex = sex,
                Birthday = birthday, // Now a string
                Contact = contact,
                Address = address
            };

            SaveMemberDetails(memberDetails);

            MessageBox.Show("Member details saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void SaveMemberDetails(MemberDetails memberDetails)
        {
            try
            {
                AccessConnection dbConnection = new AccessConnection();

                dbConnection.OpenConnection();

                string query = "INSERT INTO Members (FullName, Email, Age, Sex, Birthday, Contact, Address) VALUES (?, ?, ?, ?, ?, ?, ?)";
                using (OleDbCommand cmd = new OleDbCommand(query, dbConnection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("FullName", memberDetails.Name);
                    cmd.Parameters.AddWithValue("Email", memberDetails.Email);
                    cmd.Parameters.AddWithValue("Age", memberDetails.Age);
                    cmd.Parameters.AddWithValue("Sex", memberDetails.Sex);
                    cmd.Parameters.AddWithValue("Birthday", memberDetails.Birthday); // Treated as string
                    cmd.Parameters.AddWithValue("Contact", memberDetails.Contact);
                    cmd.Parameters.AddWithValue("Address", memberDetails.Address);
                    cmd.ExecuteNonQuery();
                }

                dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving member details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
