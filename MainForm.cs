using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRUD
{
    public partial class MainForm : Form
    {
        private SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CRUD;Integrated Security=True");
        
        public MainForm()
        {
            InitializeComponent();
        }

        //@@@@ load event for MainForm @@@//
        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowAllButton_Click(null, null);
        }

        //@@@@ click event for InsertButton @@@//
        private void InsertButton_Click(object sender, EventArgs e)
        {
            var query = "INSERT INTO student VALUES (@name, @gender, @dob, @contact, @address, @city)";
            var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", nameTextBox.Text);
            cmd.Parameters.AddWithValue("@gender", maleRadioBtn.Checked ? "Male" : femaleRadioBtn.Checked ? "Female" : "");
            cmd.Parameters.AddWithValue("@dob", dobDateTimePicker.Value);
            cmd.Parameters.AddWithValue("@contact", contactTextBox.Text);
            cmd.Parameters.AddWithValue("@address", addressTextBox.Text);
            cmd.Parameters.AddWithValue("@city", cityComboBox.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record added successfully.");
            ShowAllButton_Click(null, null);
        }


        //@@@@ click event for UpdateButton @@@//
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            var query = "UPDATE student SET name=@name, gender=@gender, dob=@dob, contact=@contact, address=@address, city=@city WHERE id=@id";
            var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", int.Parse(idTextBox.Text));
            cmd.Parameters.AddWithValue("@name", nameTextBox.Text);
            cmd.Parameters.AddWithValue("@gender", maleRadioBtn.Checked ? "Male" : femaleRadioBtn.Checked ? "Female" : "");
            cmd.Parameters.AddWithValue("@dob", dobDateTimePicker.Value);
            cmd.Parameters.AddWithValue("@contact", contactTextBox.Text);
            cmd.Parameters.AddWithValue("@address", addressTextBox.Text);
            cmd.Parameters.AddWithValue("@city", cityComboBox.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record updated successfully.");
            ShowAllButton_Click(null, null);
        }

        //@@@@ click event for DeleteButton @@@//
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var cmd = new SqlCommand("DELETE student WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", int.Parse(idTextBox.Text));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record deleted successfully.");
            ShowAllButton_Click(null, null);
        }

        //@@@@ click event for ShowAllButton @@@//
        private void ShowAllButton_Click(object sender, EventArgs e)
        {
            var cmd = new SqlCommand("SELECT * FROM student", con);
            var sda = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            sda.Fill(dt);
            dataGridView.DataSource = dt;
            ResetButton_Click(null, null);
        }


        //@@@@ click event for ResetButton @@@//
        private void ResetButton_Click(object sender, EventArgs e)
        {
            idTextBox.Clear();
            nameTextBox.Clear();
            contactTextBox.Clear();
            addressTextBox.Clear();
            cityComboBox.ResetText();
            dobDateTimePicker.ResetText();
            maleRadioBtn.Checked = femaleRadioBtn.Checked = false;
        }

        //@@@@ click event for SearchButton @@@//

        private void SearchButton_Click(object sender, EventArgs e)
        {
            /****** 1st method ******/
            var query = "SELECT * FROM student WHERE "
                            + "id LIKE '%'+@SEARCH+'%' "
                            + "OR name LIKE '%'+@SEARCH+'%' "
                            + "OR gender LIKE '%'+@SEARCH+'%' "
                            + "OR dob LIKE '%'+@SEARCH+'%' "
                            + "OR contact LIKE '%'+@SEARCH+'%' "
                            + "OR address LIKE '%'+@SEARCH+'%' "
                            + "OR city LIKE '%'+@SEARCH+'%'";

            /****** 2nd method ******/
            // uncomment this line if want to use
            //var query = "SELECT * FROM student WHERE CONVERT(VARCHAR, id) + name + CONVERT(VARCHAR, dob) + gender +  contact + address + city LIKE '%'+@SEARCH+'%'";

            /****** 3rd method ******/
            // uncomment this line if want to use
            //var query = "SELECT * FROM student WHERE name LIKE '%'+@SEARCH+'%'";


            var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@SEARCH", searchTextBox.Text);
            var sda = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            sda.Fill(dt);
            dataGridView.DataSource = dt;
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // if any row is selected 
            if (e.RowIndex >= 0)
            {
                // current selected row
                var row = dataGridView.Rows[e.RowIndex];
                
                // set data in form controls
                idTextBox.Text = row.Cells["id"].Value.ToString();
                nameTextBox.Text = row.Cells["name"].Value.ToString();
                dobDateTimePicker.Text = row.Cells["dob"].Value.ToString();
                contactTextBox.Text = row.Cells["contact"].Value.ToString();
                addressTextBox.Text = row.Cells["address"].Value.ToString();
                cityComboBox.Text = row.Cells["city"].Value.ToString();
                maleRadioBtn.Checked = row.Cells["gender"].Value.ToString() == "Male";
                femaleRadioBtn.Checked = row.Cells["gender"].Value.ToString() == "Female";
            }
        }

    }
}
