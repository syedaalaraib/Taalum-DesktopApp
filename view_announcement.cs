using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ta_allum
{
    public partial class view_announcement : Form
    {
        //private const string connectionString = "Data Source=DESKTOP-MA76B76\\SQLEXPRESS;Initial Catalog=flex;Integrated Security=True";

        //Hammad
        private const string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        private string userID;
        private string username;
        public view_announcement(string userID_, string username)
        {
            InitializeComponent();
            this.userID = username;
            PopulateComboBox();
            this.username = userID_;
        }

        string selectedCourseID;

        private void PopulateComboBox()
        {
            string query = @"
        SELECT r.courseID 
        FROM registered_Courses r 
        JOIN Student s ON r.Studentid = s.ROLLNO 
        JOIN User_ u ON s.USERID = u.USERID 
        WHERE u.USERID = @userID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        comboBox1.Items.Clear();

                        while (reader.Read())
                        {
                            string courseID = reader.GetString(0);
                            comboBox1.Items.Add(courseID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating ComboBox: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When a course is selected, populate the DataGridView with announcements for that course
            string selectedCourseID = comboBox1.SelectedItem.ToString();

            string query = "SELECT statement, announcement_date, announcement_time FROM announcement WHERE course_ID = @courseID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@courseID", selectedCourseID);
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving announcements: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentDashboard s =  new StudentDashboard(username, userID);
            s.Show();
        }
    }
}
