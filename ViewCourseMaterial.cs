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
    public partial class ViewCourseMaterial : Form
    {
        //private const string connectionString = "Data Source=DESKTOP-MA76B76\\SQLEXPRESS;Initial Catalog=flex;Integrated Security=True";
        // Hammad's Connection String
        private const string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");

        private string userID;
        private string username;

        public ViewCourseMaterial(string name, string userID_)
        {
            InitializeComponent();
            this.userID = userID_;
            PopulateComboBox();
            listBox1.DoubleClick += listBox1_DoubleClick;
            this.username = name;
        }

        string selectedCourseID;

        private void PopulateComboBox()
        {
            string query = @"
        SELECT r.courseID 
        FROM registered_Courses r 
        JOIN Student s ON r.Studentid = s.ROLLNO 
        JOIN User_ u ON s.USERID = u.USERID 
        WHERE u.USERID = @userID";

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
                MessageBox.Show("Error populating ComboBox: " + ex.Message);
            }
        }

        private void PopulateMaterialsList()
        {
            listBox1.Items.Clear();

            string courseFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, selectedCourseID);

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
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, selectedCourseID, selectedFileName);

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

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            //StudentDashboard s = new StudentDashboard(userID, username);
            //s.Show();
        }
    }
}
