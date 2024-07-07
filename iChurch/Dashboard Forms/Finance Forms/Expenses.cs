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
    public partial class Expenses : Form
    {
        public Expenses()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddExpenses expenses = new AddExpenses();
            expenses.ShowDialog();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            EditExpenses expenses = new EditExpenses();
            expenses.ShowDialog();
        }
    }
}
