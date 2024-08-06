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
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ta_allum
{
    public partial class coursematerial : Form
    {
        public string userID;

        //private const string connectionString = ("Data Source=DESKTOP-MA76B76\\SQLEXPRESS;Initial Catalog=flex;Integrated Security=True");
        // Hammad's Connection String
        private const string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        public coursematerial(string userID)
        {
            this.userID = userID;
            InitializeComponent();
            openFileDialog1 = new OpenFileDialog();
            PopulateComboBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            string[] fileName = openFileDialog1.FileName.Split(new string[] { "\\" }, StringSplitOptions.None);
            string destinationFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, selectedCourseID);
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

                MessageBox.Show("File Uploaded to folder: " + selectedCourseID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error uploading file: " + ex.Message);
            }
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

            // You can continue with your original code to retrieve data based on the selected course
            string query = "SELECT sectionName, facultyID FROM course WHERE course_ID = @courseID";

            // rest of your code remains unchanged...
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FacultyDashboard a = new FacultyDashboard(userID);
            a.Show();
            this.Hide();
           
        }
    }
}

