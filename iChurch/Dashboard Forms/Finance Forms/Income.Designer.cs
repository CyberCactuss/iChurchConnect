﻿namespace iChurch.Dashboard_Forms.Finance_Forms
{
    partial class Income
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Income));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            guna2Button3 = new Guna.UI2.WinForms.Guna2Button();
            guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            guna2DataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
            ID = new DataGridViewTextBoxColumn();
            Amount = new DataGridViewTextBoxColumn();
            Category = new DataGridViewTextBoxColumn();
            PaymentMethod = new DataGridViewTextBoxColumn();
            Person = new DataGridViewTextBoxColumn();
            GivenDate = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)guna2DataGridView1).BeginInit();
            SuspendLayout();
            // 
            // guna2Button3
            // 
            guna2Button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2Button3.BackColor = SystemColors.Control;
            guna2Button3.BorderRadius = 14;
            guna2Button3.CustomizableEdges = customizableEdges1;
            guna2Button3.DisabledState.BorderColor = Color.DarkGray;
            guna2Button3.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button3.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button3.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button3.FillColor = Color.FromArgb(57, 205, 73);
            guna2Button3.Font = new Font("Century Gothic", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2Button3.ForeColor = Color.White;
            guna2Button3.Image = (Image)resources.GetObject("guna2Button3.Image");
            guna2Button3.ImageOffset = new Point(-36, 0);
            guna2Button3.ImageSize = new Size(22, 22);
            guna2Button3.Location = new Point(693, 697);
            guna2Button3.Name = "guna2Button3";
            guna2Button3.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Button3.Size = new Size(142, 52);
            guna2Button3.TabIndex = 8;
            guna2Button3.Text = "Edit";
            guna2Button3.TextAlign = HorizontalAlignment.Left;
            guna2Button3.TextOffset = new Point(47, 0);
            guna2Button3.Click += guna2Button3_Click;
            // 
            // guna2Button2
            // 
            guna2Button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2Button2.BorderRadius = 14;
            guna2Button2.CustomizableEdges = customizableEdges3;
            guna2Button2.DisabledState.BorderColor = Color.DarkGray;
            guna2Button2.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button2.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button2.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button2.FillColor = Color.FromArgb(235, 68, 56);
            guna2Button2.Font = new Font("Century Gothic", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2Button2.ForeColor = Color.White;
            guna2Button2.Image = (Image)resources.GetObject("guna2Button2.Image");
            guna2Button2.ImageOffset = new Point(-38, 0);
            guna2Button2.ImageSize = new Size(22, 22);
            guna2Button2.Location = new Point(867, 697);
            guna2Button2.Name = "guna2Button2";
            guna2Button2.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Button2.Size = new Size(142, 52);
            guna2Button2.TabIndex = 7;
            guna2Button2.Text = "Delete";
            guna2Button2.TextAlign = HorizontalAlignment.Left;
            guna2Button2.TextOffset = new Point(42, 0);
            guna2Button2.Click += guna2Button2_Click;
            // 
            // guna2Button1
            // 
            guna2Button1.BorderRadius = 14;
            guna2Button1.CustomizableEdges = customizableEdges5;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.FillColor = Color.FromArgb(2, 145, 247);
            guna2Button1.Font = new Font("Century Gothic", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.Image = (Image)resources.GetObject("guna2Button1.Image");
            guna2Button1.ImageOffset = new Point(-36, 0);
            guna2Button1.ImageSize = new Size(22, 22);
            guna2Button1.Location = new Point(90, 697);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2Button1.Size = new Size(142, 52);
            guna2Button1.TabIndex = 6;
            guna2Button1.Text = "Add";
            guna2Button1.TextAlign = HorizontalAlignment.Left;
            guna2Button1.TextOffset = new Point(47, 0);
            guna2Button1.Click += guna2Button1_Click;
            // 
            // guna2DataGridView1
            // 
            guna2DataGridView1.AllowUserToAddRows = false;
            guna2DataGridView1.AllowUserToDeleteRows = false;
            guna2DataGridView1.AllowUserToResizeColumns = false;
            guna2DataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.SeaGreen;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            guna2DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            guna2DataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            guna2DataGridView1.BackgroundColor = Color.WhiteSmoke;
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
            guna2DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.Green;
            dataGridViewCellStyle2.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(0, 64, 0);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            guna2DataGridView1.ColumnHeadersHeight = 52;
            guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            guna2DataGridView1.Columns.AddRange(new DataGridViewColumn[] { ID, Amount, Category, PaymentMethod, Person, GivenDate });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.LimeGreen;
            dataGridViewCellStyle3.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.SeaGreen;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            guna2DataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            guna2DataGridView1.GridColor = Color.White;
            guna2DataGridView1.Location = new Point(12, 12);
            guna2DataGridView1.Name = "guna2DataGridView1";
            guna2DataGridView1.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.Padding = new Padding(5);
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            guna2DataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            guna2DataGridView1.RowHeadersVisible = false;
            guna2DataGridView1.RowTemplate.DividerHeight = 5;
            guna2DataGridView1.RowTemplate.Height = 47;
            guna2DataGridView1.Size = new Size(1055, 663);
            guna2DataGridView1.TabIndex = 9;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.Font = null;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            guna2DataGridView1.ThemeStyle.BackColor = Color.WhiteSmoke;
            guna2DataGridView1.ThemeStyle.GridColor = Color.White;
            guna2DataGridView1.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            guna2DataGridView1.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            guna2DataGridView1.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            guna2DataGridView1.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            guna2DataGridView1.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            guna2DataGridView1.ThemeStyle.HeaderStyle.Height = 52;
            guna2DataGridView1.ThemeStyle.ReadOnly = true;
            guna2DataGridView1.ThemeStyle.RowsStyle.BackColor = Color.White;
            guna2DataGridView1.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            guna2DataGridView1.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            guna2DataGridView1.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            guna2DataGridView1.ThemeStyle.RowsStyle.Height = 47;
            guna2DataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            guna2DataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            guna2DataGridView1.CellContentClick += guna2DataGridView1_CellContentClick;
            // 
            // ID
            // 
            ID.FillWeight = 20F;
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.ReadOnly = true;
            // 
            // Amount
            // 
            Amount.DividerWidth = 1;
            Amount.FillWeight = 50F;
            Amount.HeaderText = "Amount";
            Amount.Name = "Amount";
            Amount.ReadOnly = true;
            Amount.Resizable = DataGridViewTriState.False;
            // 
            // Category
            // 
            Category.FillWeight = 70F;
            Category.HeaderText = "Category";
            Category.Name = "Category";
            Category.ReadOnly = true;
            Category.Resizable = DataGridViewTriState.False;
            // 
            // PaymentMethod
            // 
            PaymentMethod.FillWeight = 70F;
            PaymentMethod.HeaderText = "Payment Method";
            PaymentMethod.Name = "PaymentMethod";
            PaymentMethod.ReadOnly = true;
            PaymentMethod.Resizable = DataGridViewTriState.False;
            // 
            // Person
            // 
            Person.FillWeight = 80F;
            Person.HeaderText = "Person/Organization";
            Person.Name = "Person";
            Person.ReadOnly = true;
            Person.Resizable = DataGridViewTriState.False;
            // 
            // GivenDate
            // 
            GivenDate.FillWeight = 40F;
            GivenDate.HeaderText = "Given Date";
            GivenDate.Name = "GivenDate";
            GivenDate.ReadOnly = true;
            GivenDate.Resizable = DataGridViewTriState.False;
            // 
            // Income
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1072, 770);
            Controls.Add(guna2DataGridView1);
            Controls.Add(guna2Button3);
            Controls.Add(guna2Button2);
            Controls.Add(guna2Button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Income";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Income";
            ((System.ComponentModel.ISupportInitialize)guna2DataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button guna2Button3;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        public Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView1;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Amount;
        private DataGridViewTextBoxColumn Category;
        private DataGridViewTextBoxColumn PaymentMethod;
        private DataGridViewTextBoxColumn Person;
        private DataGridViewTextBoxColumn GivenDate;
    }
}