using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ta_allum
{
    public partial class FacultyDashboard : Form
    {   
        public string userID = string.Empty;
        public FacultyDashboard()
        {
            string userID = string.Empty;
            InitializeComponent();
        }
        public FacultyDashboard(string userID)
        {
            this.userID = userID;
            InitializeComponent();
        }

        private void FacultyDashboard_Load(object sender, EventArgs e)
        {

        }

        private void Attendence_btn_Click(object sender, EventArgs e)
        {
            Attendence a = new Attendence(userID);
            a.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            coursematerial a = new coursematerial(userID);
            a.Show();
            this.Hide(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            assignments a = new assignments(userID);
            a.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            add_announcement a = new add_announcement(userID);
            a.Show();
            this.Hide();

        }

        private void marks_Click(object sender, EventArgs e)
        {
            // upload marks
            UploadMarks m = new UploadMarks(userID);
            m.Show();
            this.Hide();
        }

        private void grades_Click(object sender, EventArgs e)
        {
            // Create an instance of UploadGrades class with the correct userID parameter
            UploadGrades gradesForm = new UploadGrades(userID);

            // Show the UploadGrades form
            gradesForm.Show();

            // Hide the current form (assuming 'this' refers to the current form)
            this.Hide();
        }

        private void viewAss_Click(object sender, EventArgs e)
        {
            ViewAssignment m = new ViewAssignment(userID);
            m.Show();
            this.Hide();
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.Show();
        }
    }
}
