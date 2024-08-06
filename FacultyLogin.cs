

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ta_allum
{
    public partial class FacultyLogin : Form
    {
        //private const string connectionString = "Data Source=DESKTOP-KVOMN4G\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
        // Hammad's Connection String
        private const string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
        public FacultyLogin()
        {
            InitializeComponent();
        }

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
                string query = "SELECT USERID, Username FROM User_ WHERE USERNAME = @Username AND PASSWORD = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", usernamestr);
                    command.Parameters.AddWithValue("@Password", passwordstr);

                    connection.Open();

                    // Execute the query to retrieve the UserID
                    object result = command.ExecuteScalar();

                    if (result != null) // If UserID is found
                    {
                        string userID = result.ToString(); // Convert the result to a string
                        MessageBox.Show("Login Successful");

                        // Check if the user is a faculty or a student
                        string roleQuery = "SELECT COUNT(*) FROM Faculty WHERE USERID = @UserID";
                        SqlCommand roleCommand = new SqlCommand(roleQuery, connection);
                        roleCommand.Parameters.AddWithValue("@UserID", userID);
                        int facultyCount = (int)roleCommand.ExecuteScalar();

                        // Determine the appropriate dashboard based on the user's role
                        if (facultyCount > 0)
                        {
                            // User is a faculty
                            FacultyDashboard f = new FacultyDashboard(userID);
                            f.Show();
                        }
                        else
                        {
                            // User is not a faculty (student, admin, etc.)
                            // You can navigate to the appropriate dashboard or page here
                        }

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password");
                    }

                    connection.Close();
                }
            }
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Hide();
        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void FacultyLogin_Load(object sender, EventArgs e)
        {

        }
    }


}



//using System;
//using System.Data.SqlClient;
//using System.Windows.Forms;

//namespace Ta_allum
//{
//    public partial class FacultyLogin : Form
//    {
//        //private const string connectionString = "Data Source=DESKTOP-KVOMN4G\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True";
//        //Hammad
//        private const string connectionString = ("Data Source=DESKTOP-2TLGV5P\\SQLEXPRESS;Initial Catalog=Flex;Integrated Security=True");
//        public FacultyLogin()
//        {
//            InitializeComponent();
//        }

//        private void loginbtn_Click(object sender, EventArgs e)
//        {
//            string usernamestr = username.Text;
//            string passwordstr = password.Text;

//            if (string.IsNullOrEmpty(usernamestr) || string.IsNullOrEmpty(passwordstr))
//            {
//                MessageBox.Show("Enter both username and password");
//                return;
//            }

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                string query = "SELECT COUNT(*) FROM User_ WHERE USERNAME = @Username AND PASSWORD = @Password";
//                string facultyQuery = "SELECT COUNT(*) FROM Faculty f INNER JOIN User_ u ON f.USERID = u.USERID WHERE u.USERNAME = @Username";

//                using (SqlCommand command = new SqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@Username", usernamestr);
//                    command.Parameters.AddWithValue("@Password", passwordstr);

//                    connection.Open();

//                    int count = (int)command.ExecuteScalar();

//                    if (count > 0)
//                    {
//                        // Check if the user is also a faculty member
//                        using (SqlCommand facultyCommand = new SqlCommand(facultyQuery, connection))
//                        {
//                            facultyCommand.Parameters.AddWithValue("@Username", usernamestr);
//                            int facultyCount = (int)facultyCommand.ExecuteScalar();

//                            if (facultyCount > 0)
//                            {
//                                MessageBox.Show("Faculty login successful");
//                                FacultyDashboard facultyDashboard = new FacultyDashboard(usernamestr);
//                                facultyDashboard.Show();
//                                this.Hide();
//                            }
//                            else
//                            {
//                                MessageBox.Show("Login Successful");
//                                FacultyDashboard facultyDashboard = new FacultyDashboard(usernamestr);
//                                facultyDashboard.Show();
//                                this.Hide();
//                                // Proceed with general login logic here
//                                // For example, you might want to navigate to a dashboard for admin or some other role
//                            }
//                        }
//                    }
//                    else
//                    {
//                        MessageBox.Show("Invalid username or password");
//                    }
//                }
//            }
//        }

//        private void backbtn_Click(object sender, EventArgs e)
//        {
//            Login login = new Login();
//            login.Show();
//            this.Hide();
//        }

//        private void FacultyLogin_Load(object sender, EventArgs e)
//        {

//        }
//    }
//}