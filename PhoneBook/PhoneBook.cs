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
        //SqlConnection connection = new SqlConnection("");
        public PhoneBook()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(
                @"INSERT into Phone (first,last,mobile) 
                  values('"+textBox1.Text+ "','" + textBox2.Text + "','" + textBox3.Text + "')",connection);
            command.ExecuteNonQuery();

            connection.Close();
            //Clear all box after add
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

            
        }

        private void edit_Click(object sender, EventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {

        }
    }
}
