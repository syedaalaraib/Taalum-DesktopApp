using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Ta_allum
{
    public partial class UploadMarks : Form
    {
        private string userID;

        public UploadMarks(string userID)
        {
            InitializeComponent();
            this.userID = userID;
        }


        private void UploadMarks_Load(object sender, EventArgs e)
        {
            MessageBox.Show("UserID: " + userID);
            LoadSectionsForFaculty();
            LoadSectionsForAssignedCourses();
        }

        private void LoadSectionsForFaculty()
                {

            //string connectionString = "Data Source=DESKTOP-99GHBFU\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
            //string connectionString = "Data Source=DESKTOP-KVOMN4G\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
            //Hammad
         string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        //string query = "SELECT CourseID FROM Section WHERE FacultyID = @FacultyID";
        //    SELECT c.CourseID FROM Course_Allocation c JOIN Faculty f ON c.FacultyID = f.FacultyID WHERE f.UserID = @userID
                 string query = "SELECT c.CourseID FROM section c JOIN Faculty f ON c.FacultyID = f.FacultyID WHERE f.UserID = @userID";

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
            //Hammad
         string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        //string connectionString = "Data Source=DESKTOP-KVOMN4G\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
            //string query = "SELECT SectionName FROM Section WHERE FacultyID = @FacultyID";
            string query = "SELECT c.SectionName FROM section c JOIN Faculty f ON c.FacultyID = f.FacultyID WHERE f.UserID = @userID";

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

            // Prompt the user to enter obtained marks for the selected student
            using (var form = new Form())
            {
                form.Text = "Enter Obtained Marks for Student with Roll No. " + rollNo;
                var obtainedMarksLabel = new Label() { Text = "Obtained Marks:", Location = new Point(10, 20) };
                var obtainedMarksTextBox = new TextBox() { Location = new Point(120, 20) };
                var confirmButton = new Button() { Text = "Confirm", Location = new Point(120, 50) };

                // Add controls to the form
                form.Controls.AddRange(new Control[] { obtainedMarksLabel, obtainedMarksTextBox, confirmButton });

                // Event handler for Confirm button click
                confirmButton.Click += (confirmSender, confirmArgs) =>
                {
                    // Retrieve entered obtained marks
                    int obtainedMarks;
                    if (int.TryParse(obtainedMarksTextBox.Text, out obtainedMarks))
                    {
                        // Get the selected course ID
                        string selectedCourseID = comboBoxFacultySections.SelectedItem.ToString();

                        // Insert marks into the database
                        InsertMarksIntoDatabase(rollNo, obtainedMarks, selectedCourseID);
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid obtained marks (numeric value).");
                    }

                    // Close the form
                    form.Close();
                };

                // Show the form
                form.ShowDialog();
            }
        }

        private void InsertMarksIntoDatabase(string rollNo, int obtainedMarks, string selectedCourseID)
        {
            //string connectionString = "Data Source=DESKTOP-KVOMN4G\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
            //Hammad
             string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");

        string query = "INSERT INTO FINALEXAM (studentid, obtained_marks, courseid) VALUES (@RollNo, @ObtainedMarks, @CourseID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RollNo", rollNo);
                    command.Parameters.AddWithValue("@ObtainedMarks", obtainedMarks);
                    command.Parameters.AddWithValue("@CourseID", selectedCourseID);

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
    }
}