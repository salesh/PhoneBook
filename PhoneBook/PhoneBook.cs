using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace PhoneBook
{
    public partial class PhoneBook : Form
    {

        SqlConnection connection;
        string connectionString;

        public PhoneBook()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["PhoneBook.Properties.Settings.Database1ConnectionString"].ConnectionString;
        }

        private void add_Click(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(
                 "INSERT into PHONES " +
                 "values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')", connection);
            command.ExecuteNonQuery();

            connection.Close();
            //Clear all box after add
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
         


        }

        private void edit_Click(object sender, EventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {

        }

        private void PhoneBook_Load(object sender, EventArgs e)
        {

        }
    }
}
