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

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private SQLiteConnection connection;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source=Clients.db");
            connection.Open();

            updateClientList();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
        }

        private void RegistrationButton_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
        }

        private void ClientListButton_Click(object sender, EventArgs e)
        {
            updateClientList();
        }

        private void updateClientList()
        {
            string sqlQuery = "select * from  profile";
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlQuery, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }
    }
}