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


namespace Ta_allum
{
    public partial class Transcript : Form
    {
        private const string connectionString = "Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";

        private string username;
        private string userID;

        public Transcript()
        {
            InitializeComponent();
            PopulateTranscript(); // Populate the DataGridView when the form is initialized
        }

        // Constructor with parameters for name and userID
        public Transcript(string name, string userID_)
        {
            InitializeComponent();
            this.userID = userID_;
            this.username = name;
            PopulateTranscript(); // Populate the DataGridView when the form is initialized
        }

        private void PopulateTranscript()
        {
            // SQL query to fetch course names and grades obtained by the 
            string query = "SELECT courseid, coursename, grade FROM Transcript WHERE studentid = @username";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Calculate and set the CGPA label
                        double cgpa1 = CalculateCGPA(dataTable);
                        cgpa.Text = $"CGPA: {cgpa1:F2}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error fetching transcript: " + ex.Message);
                    }
                }
            }
        }

        private double CalculateCGPA(DataTable dataTable)
        {
            double totalGradePoints = 0;
            double totalCreditHours = 0;

            foreach (DataRow row in dataTable.Rows)
            {
                string grade = row["grade"].ToString();
                double gradePoints = CalculateGradePoints(grade);
                int credits = GetCourseCredits(row["courseid"].ToString());
                //int credits = GetCourseCredits(row["Cname"].ToString());

                if (!string.IsNullOrEmpty(grade))
                {
                    totalGradePoints += gradePoints * credits;
                    totalCreditHours += credits;
                }
            }

            return totalCreditHours > 0 ? totalGradePoints / totalCreditHours : 0;
        }

        private double CalculateGradePoints(string grade)
        {
            // Assign grade points based on the grade obtained
            switch (grade)
            {
                case "A+":
                case "A":
                    return 4.0;
                case "A-":
                    return 3.67;
                case "B+":
                    return 3.33;
                case "B":
                    return 3.0;
                case "B-":
                    return 2.66677;
                case "C+":
                    return 2.333;
                case "C":
                    return 2.0;
                case "C-":
                    return 1.66677;
                case "D+":
                    return 1.333;
                case "D":
                    return 1;
                case "F":
                    return 0.0;
                default:
                    return 0.0; // For grades that are not announced or other non-standard grades
            }
        }

        private int GetCourseCredits(string courseId)
        {
            // Fetch the credit hours of the course from the database based on the course ID
            string query = "SELECT credits FROM Course WHERE course_ID = @CourseId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseId", courseId);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error fetching course credits: " + ex.Message);
                    }
                }
            }

            return 0;
        }



        private void Backbtn_Click(object sender, EventArgs e)
        { 
            StudentDashboard a = new StudentDashboard(userID, username);
            a.Show();
            this.Close();

        }
    }
}
