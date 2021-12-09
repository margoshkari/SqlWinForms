using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp1
{
    public class Connection
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public Connection()
        {
                
        }
        public Connection(string server, string database, string password, string username)
        {
            Server = server;
            Database = database;
            Password = password;
            Username = username;
        }
    }
}
