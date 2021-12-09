using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class ConnectionForm : Form
    {
        public ConnectionForm()
        {
            InitializeComponent();
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.serverTB.Text != string.Empty && this.databaseTB.Text != string.Empty && this.usernameTB.Text != string.Empty && this.passwordTB.Text != string.Empty)
                {
                    using(SqlConnection sqlConnection = new SqlConnection($@"Data Source={this.serverTB.Text};Initial Catalog={this.databaseTB.Text};UID={this.usernameTB.Text};Password={this.passwordTB.Text}"))
                    {
                        sqlConnection.Open();
                        MessageBox.Show("Connected!");
                        Connection connection = new Connection(this.serverTB.Text, this.databaseTB.Text, this.passwordTB.Text, this.usernameTB.Text);
                        string json = JsonSerializer.Serialize<Connection>(connection);
                        File.WriteAllText("last connection.json", json);
                        this.Hide();
                        Form1 form = new Form1(sqlConnection, this.serverTB.Text, this.usernameTB.Text, this.passwordTB.Text);
                        form.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Fill all fields!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connection");
            }
        }

        private void lastConBtn_Click(object sender, EventArgs e)
        {
            if(File.Exists("last connection.json"))
            {
                Connection connection = JsonSerializer.Deserialize<Connection>(File.ReadAllText("last connection.json"));
                this.serverTB.Text = connection.Server;
                this.databaseTB.Text = connection.Database;
                this.usernameTB.Text = connection.Username;
                this.passwordTB.Text = connection.Password;
                using (SqlConnection sqlConnection = new SqlConnection($@"Data Source={this.serverTB.Text};Initial Catalog={this.databaseTB.Text};UID={this.usernameTB.Text};Password={this.passwordTB.Text}"))
                {
                    sqlConnection.Open();
                    MessageBox.Show("Connected!");
                    this.Hide();
                    Form1 form = new Form1(sqlConnection, connection.Server, connection.Username, connection.Password);
                    form.ShowDialog();
                    this.Close();
                }
            }
        }
    }
}
