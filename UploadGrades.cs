using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Ta_allum
{
    public partial class UploadGrades : Form
    {
        private string userID;

        public UploadGrades(string userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void UploadMarks_Load(object sender, EventArgs e)
        {
            LoadSectionsForFaculty();
            LoadSectionsForAssignedCourses();
        }

        private void LoadSectionsForFaculty()
        {
            //string connectionString = "Data Source=DESKTOP-99GHBFU\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
            //string connectionString = "Data Source=DESKTOP-KVOMN4G\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
            //Hammad
            string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        string query = "SELECT c.CourseID, c.SectionName FROM section c JOIN Faculty f ON c.FacultyID = f.FacultyID WHERE f.UserID = @userID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {

                            string courseID = reader["CourseID"].ToString();
                            comboBoxFacultySections.Items.Add($"{courseID}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void LoadSectionsForAssignedCourses()
        {
            //string connectionString = "Data Source=DESKTOP-KVOMN4G\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
            //Hammad
            string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
            string query = "SELECT c.CourseID, c.SectionName FROM section c JOIN Faculty f ON c.FacultyID = f.FacultyID WHERE f.UserID = @userID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string sectionName = reader["SectionName"].ToString();

                            comboBoxAssignedCourses.Items.Add($"{sectionName}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void comboBoxFacultySections_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle selection change in the dropdown for faculty sections
            // You can implement the logic here to process the selected section
        }

        private void comboBoxAssignedCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle selection change in the dropdown for assigned courses
            // You can implement the logic here to process the selected section
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedCourseID = comboBoxFacultySections.SelectedItem.ToString();
            string selectedSectionName = comboBoxAssignedCourses.SelectedItem.ToString();

            //string connectionString = "Data Source=DESKTOP-KVOMN4G\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
            //Hammad
             string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
            string query = "SELECT u.NAME, u.USERNAME, s.ROLLNO FROM registered_Courses rc " +
                           "INNER JOIN Student s ON rc.Studentid = s.ROLLNO " +
                           "INNER JOIN User_ u ON s.USERID = u.USERID " +
                           "WHERE rc.courseID = @CourseID AND rc.sectionname = @SectionName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseID", selectedCourseID);
                    command.Parameters.AddWithValue("@SectionName", selectedSectionName);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {

                            string rollNo = reader["ROLLNO"].ToString();

                            // Display the student information
                            listBoxStudents.Items.Add($"{rollNo}");

                            // Dynamically create a button for each student
                            Button button = new Button();
                            button.Text = "Select";
                            button.Tag = rollNo; // Store the roll number as the button's tag
                            button.Click += Button_Click; // Assign click event handler
                            flowLayoutPanelButtons.Controls.Add(button); // Add button to flow layout panel
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            // Get the roll number of the selected student
            string rollNo = (sender as Button).Tag.ToString();

            // Fetch obtained marks from the FINALEXAM table for the selected student
            int obtainedMarks = 0; // Default value
            string selectedCourseID = comboBoxFacultySections.SelectedItem.ToString(); // Get selected course ID

            //Hammad
             string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
            //string connectionString = "Data Source=DESKTOP-KVOMN4G\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
            string query = "SELECT obtained_marks FROM FINALEXAM WHERE studentid = @RollNo AND courseid = @CourseID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RollNo", rollNo);
                    command.Parameters.AddWithValue("@CourseID", selectedCourseID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            obtainedMarks = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

            // Prompt the user to select a grading scale
            using (var form = new Form())
            {
                form.Text = "Select Grading Scale for Student with Roll No. " + rollNo;
                var obtainedMarksLabel = new Label() { Text = "Obtained Marks: " + obtainedMarks.ToString(), Location = new Point(10, 20) };
                var gradingScaleLabel = new Label() { Text = "Select Grading Scale:", Location = new Point(10, 50) };
                var gradingScaleComboBox = new ComboBox() { Location = new Point(150, 50) };
                gradingScaleComboBox.Items.AddRange(new object[] { "A: 90-100, B: 80-89, C: 70-79, D: 60-69, F: <60",
                                               "A: 90-100, B: 80-89, C: 70-79, D: 55-69, F: <55",
                                               "A: 90-100, B: 80-89, C: 70-79, D: 50-69, F: <50" });
                var confirmButton = new Button() { Text = "Confirm", Location = new Point(150, 80) };

                // Add controls to the form
                form.Controls.AddRange(new Control[] { obtainedMarksLabel, gradingScaleLabel, gradingScaleComboBox, confirmButton });

                // Event handler for Confirm button click
                confirmButton.Click += (confirmSender, confirmArgs) =>
                {
                    // Get the selected grading scale
                    string selectedGradingScale = gradingScaleComboBox.SelectedItem.ToString();

                    // Determine the grading scale number
                    int gradingScaleNumber = GetGradingScaleNumber(selectedGradingScale);

                    // Calculate grade based on the selected grading scale number
                    string grade = CalculateGrade(obtainedMarks, gradingScaleNumber);

                    // Insert grade into the database
                    InsertMarksIntoDatabase(rollNo, obtainedMarks, grade, selectedCourseID);

                    // Close the form
                    form.Close();
                };
                // Show the form
                form.ShowDialog();
            }
        }


        private string CalculateGrade(int obtainedMarks, int gradingScaleNumber)
        {
            // Determine the grade based on obtained marks and grading scale number
            switch (gradingScaleNumber)
            {
                case 1: // Grading scale 1
                    if (obtainedMarks >= 90)
                    {
                        return "A";
                    }
                    else if (obtainedMarks >= 80)
                    {
                        return "B";
                    }
                    else if (obtainedMarks >= 70)
                    {
                        return "C";
                    }
                    else if (obtainedMarks >= 60)
                    {
                        return "D";
                    }
                    else
                    {
                        return "F";
                    }
                case 2: // Grading scale 2
                    if (obtainedMarks >= 90)
                    {
                        return "A";
                    }
                    else if (obtainedMarks >= 80)
                    {
                        return "B";
                    }
                    else if (obtainedMarks >= 70)
                    {
                        return "C";
                    }
                    else if (obtainedMarks >= 55)
                    {
                        return "D";
                    }
                    else
                    {
                        return "F";
                    }
                case 3: // Grading scale 3
                    if (obtainedMarks >= 90)
                    {
                        return "A";
                    }
                    else if (obtainedMarks >= 80)
                    {
                        return "B";
                    }
                    else if (obtainedMarks >= 70)
                    {
                        return "C";
                    }
                    else if (obtainedMarks >= 50)
                    {
                        return "D";
                    }
                    else
                    {
                        return "F";
                    }
                default:
                    return "F"; // Default case
            }
        }

        // Function to determine the grading scale number
        private int GetGradingScaleNumber(string gradingScale)
        {
            if (gradingScale.StartsWith("A: 90-100, B: 80-89, C: 70-79, D: 60-69, F: <60"))
            {
                return 1;
            }
            else if (gradingScale.StartsWith("A: 90-100, B: 80-89, C: 70-79, D: 55-69, F: <55"))
            {
                return 2;
            }
            else if (gradingScale.StartsWith("A: 90-100, B: 80-89, C: 70-79, D: 50-69, F: <50"))
            {
                return 3;
            }
            else
            {
                return 0; // Default to grading scale 1 if not matched
            }
        }


        private void InsertMarksIntoDatabase(string rollNo, int obtainedMarks, string grade, string selectedCourseID)
        {
            //string connectionString = "Data Source=DESKTOP-KVOMN4G\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
            //Hammad
            string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
            string query = "INSERT INTO Grade (grade, courseid, studentid) VALUES (@Grade, @CourseID, @RollNo)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Grade", grade);
                    command.Parameters.AddWithValue("@CourseID", selectedCourseID);
                    command.Parameters.AddWithValue("@RollNo", rollNo);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show(rowsAffected + " row(s) inserted.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {

            FacultyDashboard a = new FacultyDashboard(userID);
            a.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

}