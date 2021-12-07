using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class EditForm : Form
    {
        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
        string value = string.Empty;
        string name = string.Empty;
        string actionName = string.Empty;
        SqlConnection sqlConnection = new SqlConnection();
        public EditForm()
        {
            InitializeComponent();
        }

        public EditForm(string action, string tableName, SqlConnection connection)
        {
            InitializeComponent();
            name = tableName;
            actionName = action;
            this.label.Text = $"Column name: \n{tableName}";
            sqlConnection = connection;
            this.valueTB.Enabled = false;
        }
        private void valueTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(this.valueTB.Text != string.Empty)
                    tcs?.TrySetResult(true);
                else
                    MessageBox.Show("Enter value!");
            } 
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            switch (actionName)
            {
                case "Insert":
                    Insert();
                    break;
                case "Delete":
                    Delete();
                    break;
                default:
                    break;
            }
        }
        private async void Insert()
        {
            this.submitBtn.Enabled = false;
            using (SqlCommand command = new SqlCommand($@"SELECT * FROM [{name}]", sqlConnection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    for (int i = 1; i < reader.FieldCount; i++)
                    {
                        this.label.Text = $"Enter: \n{reader.GetName(i)}";
                        tcs = new TaskCompletionSource<bool>();
                        if (await tcs.Task == true)
                        {
                            value += $"{this.valueTB.Text},";
                            this.valueTB.Text = string.Empty;
                        }
                    }
                }
            }
            value = value.Remove(value.Length - 1, 1);
            try
            {
                using (SqlCommand command = new SqlCommand($@"INSERT INTO [{name}] VALUES ({value});", sqlConnection))
                {
                    if (command.ExecuteNonQuery() > 0)
                        MessageBox.Show("Added!");
                    else
                        MessageBox.Show("Not added!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding value!");
            }
            this.submitBtn.Enabled = true;
        }
        private async void Delete()
        {
            this.submitBtn.Enabled = false;
            this.valueTB.Enabled = true;
            string columnName = string.Empty;
            this.label.Text = "Enter id of element \nyou want to delete:";

            using (SqlCommand sqlCommand = new SqlCommand($@"SELECT * FROM [{name}]", sqlConnection))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    columnName = reader.GetName(0);
                }
            }
            if (await tcs.Task == true)
            {
                using (SqlCommand sqlCommand = new SqlCommand($"DELETE FROM [{name}] WHERE {columnName}={this.valueTB.Text};", sqlConnection))
                {
                    if (sqlCommand.ExecuteNonQuery() > 0)
                        MessageBox.Show("Deleted!");
                    else
                        MessageBox.Show("Not found!");
                }
                this.valueTB.Text = string.Empty;
                this.valueTB.Enabled = false;
                this.submitBtn.Enabled = true;
            }
        }
    }
}
