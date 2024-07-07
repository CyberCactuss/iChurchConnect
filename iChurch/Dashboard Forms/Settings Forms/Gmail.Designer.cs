namespace iChurch.Dashboard_Forms.Settings_Forms
{
    partial class Gmail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Gmail));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            guna2GradientButton1 = new Guna.UI2.WinForms.Guna2GradientButton();
            textBox1 = new TextBox();
            label3 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(109, 50);
            label1.Name = "label1";
            label1.Size = new Size(408, 44);
            label1.TabIndex = 29;
            label1.Text = "Set up Gmail account";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(30, 41);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(72, 62);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 28;
            pictureBox1.TabStop = false;
            // 
            // guna2GradientButton1
            // 
            guna2GradientButton1.Animated = true;
            guna2GradientButton1.AnimatedGIF = true;
            guna2GradientButton1.AutoRoundedCorners = true;
            guna2GradientButton1.BorderColor = Color.White;
            guna2GradientButton1.BorderRadius = 28;
            guna2GradientButton1.BorderThickness = 3;
            guna2GradientButton1.CustomizableEdges = customizableEdges1;
            guna2GradientButton1.DisabledState.BorderColor = Color.DarkGray;
            guna2GradientButton1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2GradientButton1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2GradientButton1.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            guna2GradientButton1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2GradientButton1.FillColor = Color.DeepSkyBlue;
            guna2GradientButton1.FillColor2 = Color.MediumBlue;
            guna2GradientButton1.FocusedColor = Color.DodgerBlue;
            guna2GradientButton1.Font = new Font("Century Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2GradientButton1.ForeColor = Color.White;
            guna2GradientButton1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            guna2GradientButton1.Location = new Point(30, 352);
            guna2GradientButton1.Name = "guna2GradientButton1";
            guna2GradientButton1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2GradientButton1.Size = new Size(151, 58);
            guna2GradientButton1.TabIndex = 27;
            guna2GradientButton1.Text = "Next";
            guna2GradientButton1.Click += guna2GradientButton1_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(23, 260);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(316, 33);
            textBox1.TabIndex = 26;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(23, 208);
            label3.Name = "label3";
            label3.Size = new Size(195, 28);
            label3.TabIndex = 25;
            label3.Text = "Gmail Account:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(23, 140);
            label2.Name = "label2";
            label2.Size = new Size(407, 19);
            label2.TabIndex = 30;
            label2.Text = "Please enter a valid and working gmail account.";
            // 
            // Gmail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 475);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(guna2GradientButton1);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Gmail";
            StartPosition = FormStartPosition.CenterScreen;
            Text = " Verify";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2GradientButton guna2GradientButton1;
        private TextBox textBox1;
        private Label label3;
        private Label label2;
    }
}