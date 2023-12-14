using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace F1Hangman
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Connection object
            SQLiteConnection con = new SQLiteConnection(@"data source=C:\Users\lucas\Thomas more\2APPAI02\Devops\CaseStudy\database\F1woorden.db");
            // commond object
            string query = "SELECT * FROM Formula1";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            // adapter
            // datatable
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
        }
    }
}