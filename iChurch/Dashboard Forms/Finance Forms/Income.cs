using iChurch.Dashboard_Forms.Inventory_Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iChurch.Dashboard_Forms.Finance_Forms
{
    public partial class Income : Form
    {
        public Income()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddIncome addincome = new AddIncome();
            addincome.ShowDialog();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            EditIncome editincome = new EditIncome();
            editincome.ShowDialog();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
