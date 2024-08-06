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

namespace Ta_allum
{
    public partial class assignments : Form
    {
        // Hammad's Connection String
        //Hammad
        private const string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        //private const string connectionString = ("Data Source=DESKTOP-KVOMN4G\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
       // private const string connectionString = ("Data Source=DESKTOP-MA76B76\\SQLEXPRESS;Initial Catalog=flex;Integrated Security=True");
        public string userID = string.Empty;
        public assignments(string userID)
        {
            InitializeComponent();
            this.userID = userID;
            openFileDialog1 = new OpenFileDialog();
            PopulateComboBox();

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

        string selectedCourseID ;


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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Assuming 'courses' is the name of your ComboBox control
            selectedCourseID = comboBox1.SelectedItem.ToString();

            // rest of your code remains unchanged...
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (selectedCourseID != null)
            {
                string[] fileName = openFileDialog1.FileName.Split(new string[] { "\\" }, StringSplitOptions.None);
                string courseFolderName = selectedCourseID + "_assignments"; // Append _assignments to course name
                string destinationFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, courseFolderName);
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

                    // Copy the file to the destination folder
                    File.Copy(openFileDialog1.FileName, destinationFilePath);

                    MessageBox.Show("File Uploaded to folder: " + courseFolderName);
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







        private void assignments_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FacultyDashboard a = new FacultyDashboard(userID);
            a.Show();
            this.Hide();
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            selectedCourseID = comboBox1.SelectedItem.ToString();
        }
    }
}
