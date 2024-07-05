﻿using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using iChurch.DBAccess.Connection;
using iChurch.Dashboard_Forms.Members_Forms;

namespace ChurchSystem.Dashboard_Forms.MembersFiles
{
    public partial class MembersMain : Form
    {
        public MembersMain()
        {
            InitializeComponent();
        }

        private void MembersMain_Load(object sender, EventArgs e)
        {
            LoadMembersData();
            AdjustDataGridViewAppearance();
        }

        private void AdjustDataGridViewAppearance()
        {
            
            guna2DataGridView1.Columns["ID"].FillWeight = 20;
            guna2DataGridView1.Columns["Age"].FillWeight = 50;
            guna2DataGridView1.Columns["Sex"].FillWeight = 70;
        }

        private void LoadMembersData()
        {
            try
            {
                AccessConnection dbConnection = new AccessConnection();

                dbConnection.OpenConnection();

                string query = "SELECT ID, FullName, Age, Sex, Contact, Email FROM Members";
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, dbConnection.GetConnection());
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                
                Dictionary<string, float> fillWeights = new Dictionary<string, float>();
                foreach (DataGridViewColumn column in guna2DataGridView1.Columns)
                {
                    if (column is DataGridViewTextBoxColumn textColumn)
                    {
                        fillWeights[column.Name] = textColumn.FillWeight;
                    }
                }

                
                guna2DataGridView1.Columns.Clear();

                
                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "ID",
                    DataPropertyName = "ID",
                    Name = "ID",
                    FillWeight = fillWeights.ContainsKey("ID") ? fillWeights["ID"] : 20
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Full Name",
                    DataPropertyName = "FullName",
                    Name = "FullName",
                    FillWeight = fillWeights.ContainsKey("FullName") ? fillWeights["FullName"] : 100 
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Age",
                    DataPropertyName = "Age",
                    Name = "Age",
                    FillWeight = fillWeights.ContainsKey("Age") ? fillWeights["Age"] : 50
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Sex",
                    DataPropertyName = "Sex",
                    Name = "Sex",
                    FillWeight = fillWeights.ContainsKey("Sex") ? fillWeights["Sex"] : 70
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Contact",
                    DataPropertyName = "Contact",
                    Name = "Contact",
                    FillWeight = fillWeights.ContainsKey("Contact") ? fillWeights["Contact"] : 100
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Email",
                    DataPropertyName = "Email",
                    Name = "Email",
                    FillWeight = fillWeights.ContainsKey("Email") ? fillWeights["Email"] : 100 
                });

                DataGridViewButtonColumn viewButtonColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "Information",
                    Name = "ViewInfo",
                    Text = "View",
                    UseColumnTextForButtonValue = true
                };
                guna2DataGridView1.Columns.Add(viewButtonColumn);

                guna2DataGridView1.AutoGenerateColumns = false;
                guna2DataGridView1.DataSource = dataTable;

                dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading member data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void MembersMain_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void guna2Button1_Click(object sender, EventArgs e) // ADD BUTTON
        {
            AddMember add = new AddMember();
            add.ShowDialog();
            LoadMembersData(); // Refresh data after adding a member
        }

        private void guna2Button2_Click(object sender, EventArgs e) // DELETE BUTTON
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
                int memberId = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                DialogResult result = MessageBox.Show($"Are you sure you want to delete the member with ID {memberId}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DeleteMember(memberId);
                    LoadMembersData(); 
                }
            }
            else
            {
                MessageBox.Show("Please select a member to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteMember(int memberId)
        {
            try
            {
                AccessConnection dbConnection = new AccessConnection();

                dbConnection.OpenConnection();

                string query = "DELETE FROM Members WHERE ID = ?";
                OleDbCommand command = new OleDbCommand(query, dbConnection.GetConnection());
                command.Parameters.AddWithValue("@ID", memberId);
                command.ExecuteNonQuery();

                dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting member data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e) // EDIT BUTTON
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
                int memberId = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                string name = selectedRow.Cells["FullName"].Value.ToString();
                string email = selectedRow.Cells["Email"].Value.ToString();
                int age = Convert.ToInt32(selectedRow.Cells["Age"].Value);
                string sex = selectedRow.Cells["Sex"].Value.ToString();
                string contact = selectedRow.Cells["Contact"].Value.ToString();

                string address = "";
                DateTime birthday = DateTime.MinValue;

                try
                {
                    AccessConnection dbConnection = new AccessConnection();
                    dbConnection.OpenConnection();

                    string query = "SELECT Address, Birthday FROM Members WHERE ID = ?";
                    OleDbCommand command = new OleDbCommand(query, dbConnection.GetConnection());
                    command.Parameters.AddWithValue("@ID", memberId);
                    OleDbDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        address = reader["Address"].ToString();
                        birthday = DateTime.Parse(reader["Birthday"].ToString());
                    }

                    reader.Close();
                    dbConnection.CloseConnection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching member data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                EditMember editMember = new EditMember(memberId, name, email, age, sex, contact, address, birthday);
                editMember.ShowDialog();
                LoadMembersData(); // Refresh data after editing a member
            }
            else
            {
                MessageBox.Show("Please select a member to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
