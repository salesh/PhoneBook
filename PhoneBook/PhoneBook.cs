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
        private void PhoneBook_Load(object sender, EventArgs e)
        {
            dataGrid_Display();
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            if (checkTextBoxes() == 0)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                         @"INSERT into CONTACTS(name,mobile,email)
                       VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')"
                            , connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Record added successfully", "Message", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                }
                finally
                {
                    connection.Close();
                    dataGrid_Display();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
            }
        }
        private void editBtn_Click(object sender, EventArgs e)
        {
            if (checkTextBoxes() == 0)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        @"UPDATE CONTACTS
                      SET   name = '" + textBox1.Text + "', mobile = '" + textBox2.Text + "', email  = '" + textBox3.Text + "' WHERE (name = '" + textBox1.Text + "') "
                        , connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Record updated successfully!", "Message", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK);
                }
                finally
                {
                    connection.Close();
                    dataGrid_Display();
                }
            }
        }
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (checkTextBoxes() == 0)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        @"DELETE FROM CONTACTS
                      WHERE name = '" + textBox1.Text + "'"
                        , connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Record deleted successfully!", "Message", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                }
                finally
                {
                    connection.Close();
                    dataGrid_Display();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            }
        }
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(
                @"SELECT *
                  FROM CONTACTS
                  WHERE name like '"+searchBox.Text+ "%' or mobile like '" + searchBox.Text + "%' or email like '" + searchBox.Text + "%'"
                  , connection);
            DataTable datatab = new DataTable();
            adapter.Fill(datatab);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in datatab.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
            }

        }
        private void dataGrid_Display()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select * from contacts order by name asc", connection);
            DataTable datatab = new DataTable();
            adapter.Fill(datatab);
            dataGridView1.Rows.Clear();             //n => 0
            foreach (DataRow item in datatab.Rows)
            {
                int n = dataGridView1.Rows.Add();   //n++
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
            }
            dataGridView1.ClearSelection();
        }
        private int checkTextBoxes()
        {
            if (String.IsNullOrEmpty(textBox1.Text) && String.IsNullOrEmpty(textBox2.Text) && String.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please insert or pick some data from table", "Message", MessageBoxButtons.OK);
                return -1;
            }
            else
            {
                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    if (String.IsNullOrEmpty(textBox2.Text))
                    {
                        textBox1.Text = textBox3.Text;
                    }
                    else
                    {
                        textBox1.Text = textBox2.Text;
                    }
                }
                return 0;
            }
        }
    }
}
