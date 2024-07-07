using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace iChurch.Dashboard_Forms.Members_Forms
{
    public partial class EditMember : Form
    {

        public int MemberId { get; set; }
        public string num;
        public string add;
        public DateTime bday;
        public string fb;
        public EditMember()
        {
            InitializeComponent();
        }

        public EditMember(int memberId, string name, string email, int age, string sex, string contact, string address, DateTime birthday, string facebook)
        {
            InitializeComponent();

            MemberId = memberId;
            textBox1.Text = name;
            textBox2.Text = email;
            comboBox1.SelectedItem = age.ToString();
            comboBox2.SelectedItem = sex;
            num = contact;
            add = address;
            bday = birthday;
            fb = facebook;
        }

        public string MemberName => textBox1.Text;
        public string MemberEmail => textBox2.Text;
        public int MemberAge => int.Parse(comboBox1.SelectedItem.ToString());
        public string MemberSex => comboBox2.SelectedItem.ToString();

        private void textBox1_TextChanged(object sender, EventArgs e) // NAME
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e) // EMAIL
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // AGE
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) // SEX
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidEmail(textBox2.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Please make a selection for all dropdowns.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            EditMember2 editMember2 = new EditMember2(MemberId, MemberName, MemberEmail, MemberAge, MemberSex, num, add, bday, fb);
            editMember2.Owner = this;
            editMember2.FormClosed += (s, args) => this.Close();
            this.Hide();
            editMember2.ShowDialog();
        }

        private bool IsValidEmail(string text)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(text);
                return addr.Address == text;
            }
            catch
            {
                return false;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
