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
        private string dbName = string.Empty;
        private string tableName = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            try
            {
                this.treeView1.Nodes.Clear();
                using (SqlConnection connection = new SqlConnection($@"Data Source={this.serverTB.Text};Initial Catalog={this.loginTB.Text};UID={this.userTB.Text};Password={this.passwordTB.Text}"))
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
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if ((sender as TreeView).SelectedNode.Parent != null &&
     (sender as TreeView).SelectedNode.GetType() == typeof(TreeNode))
            {
                dbName = (sender as TreeView).SelectedNode.Parent.Text;
                tableName = (sender as TreeView).SelectedNode.Text;
            }
            else
            {
                dbName = (sender as TreeView).SelectedNode.Text;
            }
            if (isDatabaseExists(dbName, $@"Data Source={this.serverTB.Text};Initial Catalog={dbName};UID={this.userTB.Text};Password={this.passwordTB.Text}"))
            {
                foreach (var item in Select(dbName, tableName))
                {
                    (sender as TreeView).SelectedNode.Nodes.Add(item);
                }
            }
        }
        private List<string> Select(string dbname, string tableName)
        {
            List<string> list = new List<string>();
            string commandstring = string.Empty;
            string connectionstring = $@"Data Source={this.serverTB.Text};Initial Catalog={dbname};UID={this.userTB.Text};Password={this.passwordTB.Text}";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    if (tableName != string.Empty)
                        commandstring = Command(tableName, connectionstring);
                    else
                        commandstring = Command(dbname, connectionstring);

                    if (commandstring != string.Empty)
                    {
                        using (SqlCommand command = new SqlCommand(commandstring, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    object names = reader.GetValue(0);
                                    list.Add(names.ToString());
                                }
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return list;
        }
        private string Command(string name, string str)
        {
            if (isDatabaseExists(name, str))
            {
                return $@"SELECT name FROM [{name}].sys.tables;";
            }
            else
            {
                return $@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{name}';";
            }
        }
        private bool isDatabaseExists(string name, string str)
        {
            SqlConnection connection = new SqlConnection(str);
            connection.Open();
            using (SqlCommand command = new SqlCommand($@"if Exists(select 1 from master.dbo.sysdatabases where name='{name}') 
                       select 1 else select 0", connection))
            {
                int exists = Convert.ToInt32(command.ExecuteScalar());

                if (exists > 0)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }
        }

        private void showBtn_Click(object sender, EventArgs e)
        {
            this.dataTB.Text = string.Empty;
            string connectionstring = $@"Data Source={this.serverTB.Text};Initial Catalog={dbName};UID={this.userTB.Text};Password={this.passwordTB.Text}";
            SqlConnection connection = new SqlConnection(connectionstring);
            int counter = 0;
            connection.Open();
            if (tableName != string.Empty)
            {
                if (isDatabaseExists(dbName, connectionstring))
                {
                    using (SqlCommand command = new SqlCommand($@"SELECT * FROM [{tableName}]", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            counter = reader.FieldCount;
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    this.dataTB.Text += $"{reader.GetValue(i)} ";
                                }
                                this.dataTB.Text += "\r\n";
                            }
                        }
                    }
                }
            }
            connection.Close();
        }
    }
}
