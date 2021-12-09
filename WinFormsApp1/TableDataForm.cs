using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class TableDataForm : Form
    {
        public TableDataForm()
        {
            InitializeComponent();
        }
        public TableDataForm(string tablename, string data)
        {
            InitializeComponent();
            this.label1.Text = tablename;
            this.textBox1.Text = data;
        }
    }
}
