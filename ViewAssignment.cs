using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ta_allum
{
    public partial class ViewAssignment : Form
    {
        //  private const string connectionString = "Data Source=DESKTOP-MA76B76\\SQLEXPRESS;Initial Catalog=flex;Integrated Security=True";
        //private const string connectionString = @"Data Source=DESKTOP-99GHBFU\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
        //private const string connectionString = "Data Source=DESKTOP-KVOMN4G\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
        //Hammad
        private const string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        private string userID;

        public ViewAssignment(string userID)
        {
            InitializeComponent();
            this.userID = userID;

            PopulateComboBox(userID);
            listBox1.DoubleClick += listBox1_DoubleClick;
        }

        string selectedCourseID;

        /* private void PopulateComboBox()
         {
            string query = @"SELECT DISTINCT CourseID  FROM Section";

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
                 // Log the exception details for debugging purposes

                 // Display a user-friendly error message
                 MessageBox.Show("An error occurred while populating ComboBox.");
             }

         }*/
        private void PopulateComboBox(string username)
        {
            // Clear existing items in the ComboBox
            comboBox1.Items.Clear();

            // Connection string to connect to the database
            //string connectionString = "Data Source=DESKTOP-99GHBFU\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
            //string connectionString = "Data Source=DESKTOP-KVOMN4G\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
            //Hammad
            string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        // SQL query to select distinct courseIDs for the logged-in user
        //string query = "SELECT DISTINCT courseID FROM Section where FacultyID = @username";
        //string query = "SELECT DISTINCT c.CourseID FROM section c JOIN Faculty f ON c.FacultyID = f.FacultyID WHERE f.UserID = @userID";
        string query = "SELECT c.CourseID FROM Course_Allocation c JOIN Faculty f ON c.FacultyID = f.FacultyID WHERE f.UserID = @userID";

            // Create a SqlConnection and a SqlCommand
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    // Add parameter for the userID
                    command.Parameters.AddWithValue("@UserID", username);

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


        private void PopulateMaterialsList()
        {
            listBox1.Items.Clear();

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string courseFolderName = $"{selectedCourseID}_assignments";
            string courseFolderPath = Path.Combine(desktopPath, "Assignment", courseFolderName);

            try
            {
                if (Directory.Exists(courseFolderPath))
                {
                    string[] files = Directory.GetFiles(courseFolderPath);
                    foreach (string file in files)
                    {
                        listBox1.Items.Add(Path.GetFileName(file));
                    }
                }
                else
                {
                    MessageBox.Show("No materials found for the selected course.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedFileName = listBox1.SelectedItem.ToString();
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, selectedCourseID, selectedFileName);
                textBoxFilePath.Text = filePath;
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedFileName = listBox1.SelectedItem.ToString();
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string courseFolderName = $"{selectedCourseID}_assignments";
                string filePath = Path.Combine(desktopPath, "Assignment", courseFolderName, selectedFileName);

                if (File.Exists(filePath))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error opening file: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("The selected file does not exist.");
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Placeholder method
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCourseID = comboBox1.SelectedItem.ToString();
            PopulateMaterialsList();
        }

        private void ViewCourseMaterial_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Close the current form
            FacultyDashboard a = new FacultyDashboard(userID);
            a.Show();
            this.Hide();
        }
    }
}