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
    public class Category
    {
        public string ID { get; }
        public string Name { get; }
        public Category(string id, string name)
        {
            ID = id;
            Name = name;
        }
    }

    public partial class Registration : Form
    {
        private List<Category> categories;
        private SQLiteConnection connection;

        public Registration()
        {
            InitializeComponent();
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source=Clients.db");
            connection.Open();

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "select * from category";
            SQLiteDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                categories = new List<Category>();
                while (dataReader.Read())
                {
                    Category category = new Category(dataReader["ID"].ToString(), dataReader["name"].ToString());
                    categories.Add(category);

                }
                comboBox1.DataSource = categories;
                comboBox1.DisplayMember = "name";
            }
        }

        private void Registration_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
        }

        private void registrationButton_Click(object sender, EventArgs e)
        {
            if (validateFields())
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "insert into profile(name, surname, middlename, phone, email, categoryID) values(@name, @surname, @middlename, @phone, @email, @categoryID)";
                command.Parameters.Add("@name", System.Data.DbType.String).Value = nameTextBox.Text;
                command.Parameters.Add("@surname", System.Data.DbType.String).Value = surnameTextBox.Text;
                command.Parameters.Add("@middlename", System.Data.DbType.String).Value = middleNameTextBox.Text;
                command.Parameters.Add("@phone", System.Data.DbType.String).Value = phoneTextBox.Text;
                command.Parameters.Add("@email", System.Data.DbType.String).Value = emailTextBox.Text;
                command.Parameters.Add("@categoryID", System.Data.DbType.String).Value = categories[comboBox1.SelectedIndex].ID;
                command.ExecuteNonQuery();

                MessageBox.Show("Пользователь добавлен");
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверно введены данные");
            }
        }

        private bool validateFields()
        {
            if (nameTextBox.Text == "" || surnameTextBox.Text == "" || middleNameTextBox.Text == "" || phoneTextBox.Text == "" || emailTextBox.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
