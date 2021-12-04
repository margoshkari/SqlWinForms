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
            sqlConnection = connection;
        }

        private void insertBtn_Click(object sender, EventArgs e)
        {

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {

        }

        private void valueTB_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && this.valueTB.Text != string.Empty)
            {
                value += $"{this.valueTB.Text}, ";
                this.valueTB.Text = string.Empty;
            }
        }
    }
}
