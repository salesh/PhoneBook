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

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["PhoneBook.Properties.Settings.PhoneConnectionString"].ConnectionString);
        
        public PhoneBook()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) && String.IsNullOrEmpty(textBox2.Text) && String.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please insert some data", "Message", MessageBoxButtons.OK);
            }
            else
            {
                if (String.IsNullOrEmpty(textBox1.Text) && String.IsNullOrEmpty(textBox2.Text))
                {
                    textBox1.Text = textBox3.Text;
                }
                else
                {
                    textBox1.Text = textBox2.Text;
                }
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                         @"INSERT into CONTACTS(name,mobile,email)
                       VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')"
                            , connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Added new contact", "Message", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                }
                finally
                {
                    connection.Close();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
            }

        }

        private void editBtn_Click(object sender, EventArgs e)
        {

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {

        }

        private void PhoneBook_Load(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
