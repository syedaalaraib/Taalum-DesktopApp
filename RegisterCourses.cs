using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ta_allum
{
    public partial class RegisterCourses : Form
    {
        // Data Source=DESKTOP-99GHBFU\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True
        private string username;
        //private string connectionString = "Data Source=DESKTOP-99GHBFU\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True"; // Update with your SQL connection string
        // Hammad's Connection String
        private const string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        public RegisterCourses(string username)
        {
            InitializeComponent();
            this.username = username;
            DisplayUsername();
            LoadOfferedCourses();
        }

        private void DisplayUsername()
        {
            usernameLabel.Text = "Welcome, " + username;
        }

        private void LoadOfferedCourses()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT course_id, course_name, department, credits, prerequisites, registration_deadline, drop_deadline FROM offered_courses";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        offeredCoursesDataGridView.DataSource = dataTable;
                        AddButtonsToGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading offered courses: " + ex.Message);
            }
        }

        private void AddButtonsToGrid()
        {
            // Add register button
            DataGridViewButtonColumn registerButton = new DataGridViewButtonColumn();
            registerButton.HeaderText = "Register Course";
            registerButton.Text = "Register";
            registerButton.UseColumnTextForButtonValue = true;
            offeredCoursesDataGridView.Columns.Add(registerButton);

            // Add drop button
            DataGridViewButtonColumn dropButton = new DataGridViewButtonColumn();
            dropButton.HeaderText = "Drop Course";
            dropButton.Text = "Drop";
            dropButton.UseColumnTextForButtonValue = true;
            offeredCoursesDataGridView.Columns.Add(dropButton);

            // Hook up the CellClick event to handle button clicks
            offeredCoursesDataGridView.CellClick += offeredCoursesDataGridView_CellClick;
        }

        private void offeredCoursesDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string courseId = offeredCoursesDataGridView.Rows[e.RowIndex].Cells["course_id"].Value.ToString();
                DateTime registrationDeadline = Convert.ToDateTime(offeredCoursesDataGridView.Rows[e.RowIndex].Cells["registration_deadline"].Value);
                DateTime dropDeadline = Convert.ToDateTime(offeredCoursesDataGridView.Rows[e.RowIndex].Cells["drop_deadline"].Value);

                if (DateTime.Now <= registrationDeadline)
                {
                    // Registration deadline has not passed
                    if (offeredCoursesDataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                        offeredCoursesDataGridView.Columns[e.ColumnIndex].HeaderText == "Drop Course")
                    {
                        DropCourse(courseId);
                    }
                    else if (offeredCoursesDataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                             offeredCoursesDataGridView.Columns[e.ColumnIndex].HeaderText == "Register Course")
                    {
                        RegisterCourse(courseId);
                    }
                }
                else
                {
                    MessageBox.Show("Cannot register/drop course. Registration deadline has passed.");
                }
            }
        }

        private void RegisterCourse(string courseId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Check if the user is already registered for the course
                    string checkQuery = "SELECT COUNT(*) FROM registered_Courses WHERE courseID = @courseId AND Studentid = @studentId";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@courseId", courseId);
                        checkCommand.Parameters.AddWithValue("@studentId", username);
                        int existingRecords = (int)checkCommand.ExecuteScalar();
                        if (existingRecords > 0)
                        {
                            MessageBox.Show("You are already registered for this course.");
                            return;
                        }
                    }

                    // Insert a new record into the registered_Courses table
                    string query = "INSERT INTO registered_Courses (courseID, Studentid, RegisterCourse, sectionname) VALUES (@courseId, @studentId, 'Yes', 'A')";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@courseId", courseId);
                        command.Parameters.AddWithValue("@studentId", username);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Course registered successfully.");
                            LoadOfferedCourses(); // Refresh the grid
                        }
                        else
                        {
                            MessageBox.Show("Failed to register the course.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error registering the course: " + ex.Message);
            }
        }

        private void DropCourse(string courseId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Delete the record from the registered_Courses table
                    string query = "DELETE FROM registered_Courses WHERE courseID = @courseId AND Studentid = @studentId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@courseId", courseId);
                        command.Parameters.AddWithValue("@studentId", username);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Course dropped successfully.");
                            LoadOfferedCourses(); // Refresh the grid
                        }
                        else
                        {
                            MessageBox.Show("Failed to drop the course.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error dropping the course: " + ex.Message);
            }
        }


        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void offeredCoursesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
