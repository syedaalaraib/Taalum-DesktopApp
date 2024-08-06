using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ta_allum
{
    public partial class UploadAssignment : Form
    {
        //private const string connectionString = "Data Source=DESKTOP-KVOMN4G\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
        //Hammad
        private const string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        public string username = string.Empty;
        public string userID = string.Empty;
        public UploadAssignment(string username, string userID)
        {
            InitializeComponent();
            this.username = username;
            openFileDialog1 = new OpenFileDialog();
            PopulateComboBox(username);
            this.userID = userID;
        }


        private string GenerateUniqueFileName(string directory, string fileName)
        {
            string newFileName = fileName;
            int counter = 1;

            // Extract the file extension
            string extension = Path.GetExtension(fileName);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            // Generate a new file name with a suffix
            while (File.Exists(Path.Combine(directory, newFileName)))
            {
                newFileName = $"{fileNameWithoutExtension}_{counter}{extension}";
                counter++;
            }

            return newFileName;
        }

        string selectedCourseID;

        private void PopulateComboBox(string username)
        {
            // Clear existing items in the ComboBox
            comboBox1.Items.Clear();
            //Hammad
         string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");

        // Connection string to connect to the database
        //string connectionString = "Data Source=DESKTOP-KVOMN4G\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";

            // SQL query to select distinct courseIDs for the logged-in user
            string query = "SELECT DISTINCT courseID FROM registered_Courses where Studentid = @username";

            // Create a SqlConnection and a SqlCommand
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    // Add parameter for the userID
                    command.Parameters.AddWithValue("@username", username);

                    // Open the connection
                    connection.Open();

                    // Execute the query
                    SqlDataReader reader = command.ExecuteReader();

                    // Read each row from the result set
                    while (reader.Read())
                    {
                        // Get the value of the courseID column from the current row
                        string courseID = reader["courseID"].ToString();

                        // Add the courseID value to the ComboBox
                        comboBox1.Items.Add(courseID);
                    }

                    // Close the SqlDataReader
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    // Close the connection
                    connection.Close();
                }
            }
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            if (selectedCourseID != null)
            {
                string[] fileName = openFileDialog1.FileName.Split(new string[] { "\\" }, StringSplitOptions.None);
                string courseFolderName = selectedCourseID + "_assignments"; // Append _assignments to course name

                // Specify the destination folder path as the "Assignment" folder on the desktop
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string destinationFolder = Path.Combine(desktopPath, "Assignment", courseFolderName);
                string destinationFilePath = Path.Combine(destinationFolder, fileName[fileName.Length - 1]);

                try
                {
                    // Check if the destination folder exists; if not, create it
                    if (!Directory.Exists(destinationFolder))
                    {
                        Directory.CreateDirectory(destinationFolder);
                    }

                    // Check if the file already exists in the destination folder
                    if (File.Exists(destinationFilePath))
                    {
                        // File with the same name already exists, generate a new file name
                        string newFileName = GenerateUniqueFileName(destinationFolder, fileName[fileName.Length - 1]);
                        destinationFilePath = Path.Combine(destinationFolder, newFileName);
                    }

                    // Check if the deadline for the selected course's assignment has passed
                    if (IsAssignmentDeadlinePassed(selectedCourseID))
                    {
                        MessageBox.Show("Assignment submission deadline has passed. You cannot upload the file.");
                        return; // Exit the method if deadline has passed
                    }

                    else
                    {
                        File.Copy(openFileDialog1.FileName, destinationFilePath);
                        MessageBox.Show("File Uploaded to folder: " + courseFolderName);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error uploading file: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a course before uploading a file.");
            }
        }

        // Function to check if the assignment deadline has passed

        private void assignments_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            StudentDashboard a = new StudentDashboard(userID, username);
            a.Show();
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }
        private void PopulateAssignmentInfo(string courseID)
        {
            // SQL query to select assignment number and deadline for the given course
            string query = "SELECT assignmentID, assignmentDeadline FROM upload_assignment";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameter for the courseID
                command.Parameters.AddWithValue("@courseID", courseID);
                try
                {

                    // Open the connection
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    // Execute the query

                    while (reader.Read())
                    {
                        // Retrieve assignment number and deadline from the database
                        int assignmentID = reader.GetInt32(0);
                        DateTime deadline = reader.GetDateTime(1);

                        // Set label text to display assignment number and deadline
                        label1.Text = "Assignment Number: " + assignmentID + "\nDeadline: " + deadline.ToString("yyyy-MM-dd");
                    }
                    /*  else
                      {
                          // If no assignment information found, display a message
                          label1.Text = "No assignment found for this course.";
                      }*/

                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private bool IsAssignmentDeadlinePassed(string courseID)
        {
            bool isDeadlinePassed = false;

            // SQL query to check if the assignment deadline has passed for the given course
            string query = "SELECT CASE WHEN assignmentDeadline < GETDATE() THEN 1 ELSE 0 END AS IsDeadlinePassed " +
                           "FROM upload_assignment " +
                           "WHERE courseID = @courseID";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@courseID", courseID);
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            isDeadlinePassed = Convert.ToBoolean(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking assignment deadline: " + ex.Message);
            }


            return isDeadlinePassed;
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            selectedCourseID = comboBox1.SelectedItem.ToString();
            PopulateAssignmentInfo(selectedCourseID);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}