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
    public partial class add_announcement : Form
    {
        public string userID;
        //private const string connectionString = ("Data Source=DESKTOP-MA76B76\\SQLEXPRESS;Initial Catalog=flex;Integrated Security=True");
        //Hammad
        private const string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        public add_announcement(string userID)
        {
            this.userID = userID;
            InitializeComponent();
            
            PopulateComboBox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FacultyDashboard a = new FacultyDashboard(userID);
            a.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        string selectedCourseID;
        private void PopulateComboBox()
        {
            string query = "SELECT c.CourseID FROM Course_Allocation c JOIN Faculty f ON c.FacultyID = f.FacultyID WHERE f.UserID = @userID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameter for userID
                        command.Parameters.AddWithValue("@userID", userID);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        // Clear existing items in the comboBox1
                        comboBox1.Items.Clear();

                        // Add items to comboBox1
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get the text from textBox1
            string statement = textBox1.Text;

            // Get the selected course ID from comboBox1
            string selectedCourseID = comboBox1.SelectedItem.ToString(); // Assuming comboBox1 is populated with course IDs

            // Get the date and time from dateTimePicker1
            DateTime selectedDateTime = dateTimePicker1.Value;
            string announcementDate = selectedDateTime.ToString("yyyy-MM-dd");
            string announcementTime = selectedDateTime.ToString("HH:mm:ss");

            // SQL query to insert into the announcement table
            string insertQuery = "INSERT INTO announcement (course_ID, statement, announcement_date, announcement_time) VALUES (@courseID, @statement, @announcementDate, @announcementTime)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Add parameters for course ID, statement, date, and time
                        command.Parameters.AddWithValue("@courseID", selectedCourseID);
                        command.Parameters.AddWithValue("@statement", statement);
                        command.Parameters.AddWithValue("@announcementDate", announcementDate);
                        command.Parameters.AddWithValue("@announcementTime", announcementTime);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Announcement added successfully!");

                            // Clear textBox1 after adding the announcement
                            textBox1.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add announcement.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding announcement: " + ex.Message);
            }
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
