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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection($@"Data Source={this.serverTB.Text};Initial Catalog={this.loginTB.Text};UID=student;Password={this.passwordTB.Text}"))
                {
                    connection.Open();
                    MessageBox.Show("Connected");
                    using (SqlCommand command = new SqlCommand(@"SELECT name FROM sys.databases;", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                object name = reader.GetValue(0);
                                this.treeView1.Nodes.Add(name.ToString());
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
        }
        private List<string> Select(string dbname)
        {
            List<string> list = new List<string>();
            using (SqlConnection connection = new SqlConnection($@"Data Source={this.serverTB.Text};Initial Catalog={this.loginTB.Text};UID=student;Password={this.passwordTB.Text}"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($@"SELECT name FROM {dbname}.sys.tables;", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            object name = reader.GetValue(0);
                            list.Add(name.ToString());
                        }
                    }
                }
            }
            return list;
        }
        private void serverTB_Click(object sender, EventArgs e)
        {
            this.serverTB.Text = string.Empty;
        }

        private void loginTB_Click(object sender, EventArgs e)
        {
            this.loginTB.Text = string.Empty;
        }

        private void passwordTB_Click(object sender, EventArgs e)
        {
            this.passwordTB.Text = string.Empty;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            foreach (var item in Select((sender as TreeView).SelectedNode.Text))
            {
                (sender as TreeView).SelectedNode.Nodes.Add(item);
            }
        }
    }
}
