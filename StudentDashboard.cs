using System;
using System.Windows.Forms;

namespace Ta_allum
{
    public partial class StudentDashboard : Form
    {
        public string userID = string.Empty;
        public string username = string.Empty; // Add a variable to store the username

        public StudentDashboard(string username, string s)
        {
            this.username = username; // Assign the passed username to the member variable
            InitializeComponent();
            this.userID = s;
        }

        private void StudentDashboard_Load(object sender, EventArgs e)
        {
            // Display the username in the top left corner label
            usernameLabel.Text = "Welcome, " + username;
        }

        private void Attendence_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Attendance_Student_UI attendance = new Attendance_Student_UI(username, userID);
            attendance.ShowDialog(); 

            this.Show();

        }

        private void RegisterCourses_btn_Click(object sender, EventArgs e)
        {
            // Hide the current form (StudentDashboard)
            this.Hide();

            // Show the RegisterCourses form and pass the username
            RegisterCourses registerCoursesForm = new RegisterCourses(username);
            registerCoursesForm.ShowDialog(); // Show the form modally

            // Show the StudentDashboard form again after closing the RegisterCourses form
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            // Show the RegisterCourses form and pass the username
            ViewCourseMaterial registerCoursesForm = new ViewCourseMaterial(username, userID);
            registerCoursesForm.ShowDialog(); // Show the form modally

            // Show the StudentDashboard form again after closing the RegisterCourses form
            this.Show();
        }

        private void attendance_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.Show();
        }

        private void MarksBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Marks attendance = new Marks(username, userID);
            attendance.ShowDialog();

            this.Show();

        }

        private void TranscriptBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Transcript attendance = new Transcript(username, userID);
            attendance.ShowDialog();

            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            view_announcement l = new view_announcement(username, userID);
            l.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            UploadAssignment uploadAssignmentForm = new UploadAssignment(userID, username);

            uploadAssignmentForm.ShowDialog();
            this.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
