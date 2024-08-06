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
    public partial class Login : Form


    {
        // Zainab's Connecton String
        //private const string connectionString = ("Data Source=DESKTOP-99GHBFU\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");

        // Laraib's Connection String
        //private const string connectionString = ("Data Source=DESKTOP-KVOMN4G\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        //Hammad
         private const string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        public Login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }


        // Inside the loginbtn_Click method in Login form
        private void loginbtn_Click(object sender, EventArgs e)
        {
            string usernamestr = username.Text;
            string passwordstr = password.Text;


            if (string.IsNullOrEmpty(usernamestr) || string.IsNullOrEmpty(passwordstr))
            {
                MessageBox.Show("Enter both username and password");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM User_ WHERE USERNAME = @Username AND PASSWORD = @Password";
                string studentQuery = "SELECT COUNT(*) FROM Student s INNER JOIN User_ u ON s.USERID = u.USERID WHERE u.USERNAME = @Username";
                string userIDQuery = "SELECT u.USERID FROM User_ u WHERE u.USERNAME = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", usernamestr);
                    command.Parameters.AddWithValue("@Password", passwordstr);

                    connection.Open();

                    int count = (int)command.ExecuteScalar(); // ExecuteScalar returns the first column of the first row

                    if (count > 0)
                    {
                        // Check if the user is also a student
                        using (SqlCommand studentCommand = new SqlCommand(studentQuery, connection))
                        {
                            studentCommand.Parameters.AddWithValue("@Username", usernamestr);
                            int studentCount = (int)studentCommand.ExecuteScalar();

                            if (studentCount > 0)
                            {
                                // Retrieve the USERID
                                using (SqlCommand userIDCommand = new SqlCommand(userIDQuery, connection))
                                {
                                    userIDCommand.Parameters.AddWithValue("@Username", usernamestr);
                                    string userID = userIDCommand.ExecuteScalar()?.ToString();

                                    if (!string.IsNullOrEmpty(userID))
                                    {
                                        MessageBox.Show("Student login successful");
                                        // Navigate to StudentDashboard with username and userID
                                        StudentDashboard studentDashboard = new StudentDashboard(usernamestr, userID);
                                        studentDashboard.Show();
                                        this.Hide();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error: User ID not found.");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Login Successful");
                                // Proceed with general login logic here
                                // For example, you might want to navigate to a dashboard for faculty or admin
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password");
                    }

                    connection.Close();
                }
            }

        }


        private void Facultybtn_Click(object sender, EventArgs e)
        {
            FacultyLogin f = new FacultyLogin();
            f.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void adminbtn_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        //private void loginbtn_Click(object sender, EventArgs e)
        //{
        //    string usernamestr = username.Text;
        //    string passwordstr = password.Text;
        //    if(usernamestr == null || passwordstr == null )
        //    {
        //        MessageBox.Show("Enter both username and password");
        //    }

        //    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";

        //}
    }
}
