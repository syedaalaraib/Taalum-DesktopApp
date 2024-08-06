using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ta_allum
{
    
    public partial class Attendence : Form
    {
        public string userID;

        DateTime selectedDate;
        //private const string connectionString = ("Data Source=DESKTOP-99GHBFU\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");

        //private const string connectionString = ("Data Source=DESKTOP-MA76B76\\SQLEXPRESS;Initial Catalog=flex;Integrated Security=True");
        // Hammad's Connection String
         private const string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        public Attendence(string userID)
        {
            InitializeComponent();
            PopulateCoursesComboBox();
            this.userID = userID;
            //PopulateDataGridView();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void PopulateCoursesComboBox()
        {
            // Clear existing items in the combo box
            courses.Items.Clear();

            // SQL query to fetch course names from the Section table
            string query = "Select c.CourseID from Course_Allocation c, Faculty f , User_ u WHERE  u.USERID= f.USERID ";
            //u.USERID = 101 AND

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Add course names to the combo box
                                string courseID = reader.GetString(0);
                                courses.Items.Add(courseID);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No courses found.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        string selectedCourseID;
        private void courses_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Assuming 'courses' is the name of your ComboBox control
            selectedCourseID = courses.SelectedItem.ToString();

            // SQL query to fetch specific data based on the selected course
            string query = "SELECT s.SectionName, f.FacultyID " +
                           "FROM Section s " +
                           "JOIN Faculty f ON s.FacultyID = f.FacultyID " +
                           "WHERE s.CourseID = @CourseID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the query to prevent SQL injection
                    command.Parameters.AddWithValue("@CourseID", selectedCourseID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        // Process the results or perform any other action
                        // For example, display data in message box
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string sectionName = reader.GetString(0);
                                string facultyID = reader.GetString(1);
                                MessageBox.Show($"Section: {sectionName}, Faculty ID: {facultyID}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No data found for the selected course.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void PopulateDataGridView()
        {
            // Clear existing rows in the DataGridView
            dataGridView1.Rows.Clear();

            // Add columns to the DataGridView
            dataGridView1.Columns.Add("StudentID", "Student ID");
            dataGridView1.Columns.Add("Username", "Username");
            dataGridView1.Columns.Add("Date", "Date");
            dataGridView1.Columns.Add("Attendance", "Attendance");

            // SQL query to fetch student usernames and IDs
            string query = @"
        SELECT s.ROLLNO, u.USERNAME 
        FROM User_ u, Student s, registered_Courses r 
        WHERE r.Studentid = s.ROLLNO 
        AND u.USERID = s.USERID 
        AND r.courseID = @CourseID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseID", selectedCourseID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string studentID = reader.GetString(reader.GetOrdinal("ROLLNO")); // Get the index of the column "ROLLNO"
                                string username = reader.GetString(reader.GetOrdinal("USERNAME")); // Get the index of the column "USERNAME"

                                // Add a row for each student
                                dataGridView1.Rows.Add(studentID, username, selectedDate.ToShortDateString(), "Absent");

                                // InsertAttendanceData(selectedDate, studentID, selectedCourseID, "Absent");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No students found for the selected course.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }





        private void InsertAttendanceData(DateTime date, string studentID, string courseID, string status)
        {
            // SQL query to insert attendance data into the 'attendance' table
            string query = "INSERT INTO attendance (date_, studentid, courseid, status) VALUES (@Date, @StudentID, @CourseID, @Status)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the query to prevent SQL injection
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@StudentID", studentID);
                    command.Parameters.AddWithValue("@CourseID", courseID);
                    command.Parameters.AddWithValue("@Status", status);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Attendance data inserted successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error inserting attendance data: " + ex.Message);
                    }
                }
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Attendance"].Index && e.RowIndex >= 0)
            {
                // Toggle between "Absent" and "Present" when the cell is clicked
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //cell.Value = (cell.Value.ToString() == "Absent") ? "Present" : "Absent":"Late";
                cell.Value = (cell.Value.ToString() == "Absent") ? "Present" : ((cell.Value.ToString() == "Present") ? "Late" : "Absent");

            }

        }
        

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            selectedDate = monthCalendar1.SelectionStart;

            // Do something with the selected date, such as displaying it in a MessageBox
            MessageBox.Show("Selected date: " + selectedDate.ToShortDateString());

            PopulateDataGridView();

        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            FacultyDashboard a = new FacultyDashboard(userID);
            a.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Iterate through DataGridView rows to get attendance data
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow) // Skip the new row if any
                {
                    string studentID = row.Cells["StudentID"].Value.ToString();
                    string status = row.Cells["Attendance"].Value.ToString();

                    // Insert attendance data for each student
                    InsertAttendanceData(selectedDate, studentID, selectedCourseID, status);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
