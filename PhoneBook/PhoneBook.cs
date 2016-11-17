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
    public partial class phoneBook : Form
    {

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["PhoneBook.Properties.Settings.PhoneConnectionString"].ConnectionString);
        
        public phoneBook()
        {
            InitializeComponent();
        }
        private void phoneBook_Load(object sender, EventArgs e)
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
                       VALUES('" + nameBox.Text + "','" + mobileBox.Text + "','" + emailBox.Text + "')"
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
                    nameBox.Clear();
                    mobileBox.Clear();
                    emailBox.Clear();
                }
            }
        }
        private void editBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        @"UPDATE CONTACTS
                          SET name = '" + nameBox.Text + "', mobile = '" + mobileBox.Text + "', email  = '" + emailBox.Text + "' WHERE (name = '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "' and mobile = '" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + "' and email = '" + dataGridView1.SelectedRows[0].Cells[2].Value.ToString() + "') "
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
            else
            {
                MessageBox.Show("Please insert or pick some data from table", "Message", MessageBoxButtons.OK);
            }
        }
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        @"DELETE FROM CONTACTS
                      WHERE name = '" + nameBox.Text + "' and mobile = '" + mobileBox.Text + "' and email = '" + emailBox.Text + "'"
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
                    nameBox.Clear();
                    mobileBox.Clear();
                    emailBox.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please insert or pick some data from table", "Message", MessageBoxButtons.OK);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                nameBox.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                mobileBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                emailBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            }
        }
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(
                    @"SELECT *
                  FROM CONTACTS
                  WHERE name like '" + searchBox.Text + "%' or mobile like '" + searchBox.Text + "%' or email like '" + searchBox.Text + "%'"
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
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
        private void dataGrid_Display()
        {
            try
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
        private int checkTextBoxes()
        {
            if (String.IsNullOrEmpty(nameBox.Text) && String.IsNullOrEmpty(mobileBox.Text) && String.IsNullOrEmpty(emailBox.Text))
            {
                MessageBox.Show("Please insert or pick some data from table", "Message", MessageBoxButtons.OK);
                return -1;
            }
            else
            {
                if (String.IsNullOrEmpty(nameBox.Text))
                {
                    if (String.IsNullOrEmpty(mobileBox.Text))
                    {
                        nameBox.Text = emailBox.Text;
                    }
                    else
                    {
                        nameBox.Text = mobileBox.Text;
                    }
                }
                return 0;
            }
        }
    }
}
