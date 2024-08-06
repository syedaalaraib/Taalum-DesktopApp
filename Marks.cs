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

namespace Ta_allum
{
    public partial class Marks : Form
    {
        // Declare userID as public to make it accessible from outside
        public string userID;
        public string username;
        //private const string connectionString = "Data Source=DESKTOP-MA76B76\\SQLEXPRESS;Initial Catalog=flex;Integrated Security=True";
        // Hammad's Connection String
        private const string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");

        public Marks()
        {
            InitializeComponent();
        }

        // Constructor with parameters for name and userID
        public Marks(string name, string userID_)
        {
            InitializeComponent();
            // Assign the provided
            // userID to the class variable
            this.userID = userID_;
            this.username = name;
            PopulateComboBox();
            totalMarksLabel.Text = "100";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCourseID = comboBox1.SelectedItem.ToString();
            SubjectLabel.Text = selectedCourseID;
            FetchStudentMarks(selectedCourseID);
            CalculateAverageMarks(selectedCourseID);
        }

        private void PopulateComboBox()
        {
            // Clear existing items in the ComboBox
            comboBox1.Items.Clear();

            // SQL query to fetch course IDs that the student is enrolled in
            string query = "SELECT r.courseID FROM registered_Courses r WHERE r.Studentid = @StudentID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentID", userID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string courseID = reader.GetString(0);
                                comboBox1.Items.Add(courseID);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No courses found for this student.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void FetchStudentMarks(string courseID)
        {
            // SQL query to fetch marks obtained by the student in the selected course
            string query = "SELECT obtained_marks FROM FINALEXAM WHERE courseid = @CourseID AND studentid = @StudentID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseID", courseID);
                    command.Parameters.AddWithValue("@StudentID", username);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            int marks = Convert.ToInt32(result);
                            obtmarkslabel.Text = $"Your Marks: {marks}";
                        }
                        //else
                        //{
                        //    obtmarkslabel.Text = "Marks not available for this course.";
                        //}
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error fetching marks: " + ex.Message);
                    }
                }
            }
        }

        private void CalculateAverageMarks(string courseID)
        {
            // SQL query to calculate the average marks of all students in the selected course
            string query = "SELECT AVG(obtained_marks) FROM FINALEXAM WHERE courseid = @CourseID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseID", courseID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            double averageMarks = Convert.ToDouble(result);
                            averageLabel.Text = $"Average Marks: {averageMarks:F2}";
                        }
                        else
                        {
                            averageLabel.Text = "Average Marks not available for this course.";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error calculating average marks: " + ex.Message);
                    }
                }
            }
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            
        }

        private void obtmarkslabel_Click(object sender, EventArgs e)
        {

        }

        private void backBtn_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
