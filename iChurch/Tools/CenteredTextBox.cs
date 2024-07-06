using System.Drawing;
using System.Windows.Forms;

namespace iChurch.Tools
{
    public class CenteredTextBox : TextBox
    {
        public CenteredTextBox()
        {
            this.TextAlign = HorizontalAlignment.Center;
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (SolidBrush brush = new SolidBrush(this.ForeColor))
            {
                e.Graphics.DrawString(this.Text, this.Font, brush, ClientRectangle, new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
            }
        }

        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate(); // Forces the control to be redrawn
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate(); // Forces the control to be redrawn
        }
    }
}
