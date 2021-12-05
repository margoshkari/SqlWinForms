using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class EditForm : Form
    {
        string value = string.Empty;
        string name = string.Empty;
        SqlConnection sqlConnection = new SqlConnection();
        public EditForm()
        {
            InitializeComponent();
        }

        public EditForm(string tableName, SqlConnection connection)
        {
            InitializeComponent();
            name = tableName;
            this.label.Text = $"Column name: \n{tableName}";
            sqlConnection = connection;
        }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            using (SqlCommand command = new SqlCommand($@"SELECT * FROM [{name}]", sqlConnection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                        }
                    }
                }
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            string columnName = string.Empty;
            this.label.Text = "Enter id of element \nyou want to delete:";
            if (this.valueTB.Text != string.Empty)
            {
                using (SqlCommand sqlCommand = new SqlCommand($@"SELECT * FROM [{name}]", sqlConnection))
                {
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        columnName = reader.GetName(0);
                    }
                }
                using (SqlCommand sqlCommand = new SqlCommand($"DELETE FROM [{name}] WHERE {columnName}={this.valueTB.Text};", sqlConnection))
                {
                    if(sqlCommand.ExecuteNonQuery() > 0)
                        MessageBox.Show("Deleted!");
                    else
                        MessageBox.Show("Not found!");
                }
            }
            else
                MessageBox.Show("Enter value!");
        }
        private void valueTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.valueTB.Text != string.Empty)
            {
                value += $"{this.valueTB.Text}, ";
                this.valueTB.Text = string.Empty;
            }
        }
    }
}
