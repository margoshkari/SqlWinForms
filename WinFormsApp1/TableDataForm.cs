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
        public TableDataForm(string tableName, string text)
        {
            InitializeComponent();
            this.label1.Text = tableName;
            this.textBox1.Text = text;
        }
    }
}
