
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections.Specialized;

namespace Ta_allum
{
    public partial class Attendance_Student_UI : Form
    {
        //Hammad
        private const string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        private string studentID;
        public string username = string.Empty;



        public Attendance_Student_UI(string user, string userId)
        {
            InitializeComponent();
            this.username = user;
            this.studentID = userId;
            PopulateCoursesComboBox();
            UpdateProgressBar("");
            label1.Text = user + "'s Attendance";
        }

        private void PopulateCoursesComboBox()
        {
            // Clear existing items in the combo box
            coursesComboBox.Items.Clear();

            // SQL query to fetch course names from the registered courses table for the current student
            string query = "SELECT c.course_ID FROM registered_Courses r, Course c WHERE r.Studentid = @StudentID AND r.courseID = c.course_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentID", studentID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string courseID = reader.GetString(0);
                                coursesComboBox.Items.Add(courseID);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No courses found for the current student.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void coursesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCourseID = coursesComboBox.SelectedItem.ToString();
            PopulateAttendanceData(selectedCourseID);
        }


        private void PopulateAttendanceData(string courseID)
        {
            try
            {
                // Clear existing rows in the 
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                // Define columns for DataGridView
                dataGridView1.Columns.Add("DateColumn", "Date");
                dataGridView1.Columns.Add("StatusColumn", "Status");

                // Get the current user's ID
                string uID = username;

                // SQL query to fetch attendance data for the selected course and student
                string query = "SELECT date_, status FROM attendance WHERE studentid = @uID AND courseid = @CourseID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Set parameter values
                        command.Parameters.AddWithValue("@uID", uID);
                        command.Parameters.AddWithValue("@CourseID", courseID);

                        connection.Open(); // Open the connection

                        // Execute the query
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Retrieve data from the reader
                                DateTime date = reader.GetDateTime(0);
                                string status = reader.GetString(1);

                                // Add data to the DataGridView
                                dataGridView1.Rows.Add(date.ToShortDateString(), status);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No attendance data found for the selected course.");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("SQL Error: " + sqlEx.Message); // Print SQL exceptions
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Print other exceptions
            }
        }



        private void back_btn_Click(object sender, EventArgs e)
        {
            // Close the current form
            this.Close();
        }

        private float CalculateAttendancePercentage(string courseID)
        {
            try
            {
                // SQL query to count the number of rows where the student is present
                string presentQuery = "SELECT COUNT(*) FROM attendance WHERE studentid = @uID AND courseid = @CourseID AND status = 'Present'";

                // SQL query to count the total number of rows in attendance for the student in the given course
                string totalQuery = "SELECT COUNT(*) FROM attendance WHERE studentid = @uID AND courseid = @CourseID";

                int presentCount = 0;
                int totalCount = 0;
                

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Count the number of rows where the student is present
                    using (SqlCommand command = new SqlCommand(presentQuery, connection))
                    {
                        command.Parameters.AddWithValue("@uID", username);
                        command.Parameters.AddWithValue("@CourseID", courseID);
                        presentCount = (int)command.ExecuteScalar();
                    }

                    // Count the total number of rows in attendance for the student in the given course
                    using (SqlCommand command = new SqlCommand(totalQuery, connection))
                    {
                        command.Parameters.AddWithValue("@uID", username);
                        command.Parameters.AddWithValue("@CourseID", courseID);
                        totalCount = (int)command.ExecuteScalar();
                    }
                }
                if((float)totalCount == 0.0) { return 1; }

                // Calculate the attendance percentage
                float attendancePercentage = (presentCount / (float)totalCount) * 100;
                return attendancePercentage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating attendance percentage: " + ex.Message);
                return -1; // Return a placeholder value or handle the error as needed
            }
        }


        private void UpdateProgressBar(string courseid)
        {
            float attendancePercentage = CalculateAttendancePercentage(courseid);
            progressBar.Value = (int)attendancePercentage;
            attendanceLabel.Text = $"Attendance: {attendancePercentage}%";
        }

        private void coursesComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedCourseID = coursesComboBox.SelectedItem.ToString();
            PopulateAttendanceData(selectedCourseID);
            UpdateProgressBar(selectedCourseID);
        }
    }
}

