using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChurchSystem.Dashboard_Forms.MembersFiles
{
    public partial class ViewInfo : Form
    {
        public ViewInfo()
        {
            InitializeComponent();
        }

        public ViewInfo(String memberName)
        {
            InitializeComponent();
            textBox1.Text = memberName;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
