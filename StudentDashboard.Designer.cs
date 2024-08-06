namespace Ta_allum
{
    partial class StudentDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.assignment = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.TranscriptBtn = new System.Windows.Forms.Button();
            this.MarksBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.RegisterCourses_btn = new System.Windows.Forms.Button();
            this.Attendence_btn = new System.Windows.Forms.Button();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.logout_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.assignment);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.TranscriptBtn);
            this.panel1.Controls.Add(this.MarksBtn);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.RegisterCourses_btn);
            this.panel1.Controls.Add(this.Attendence_btn);
            this.panel1.Controls.Add(this.usernameLabel);
            this.panel1.Location = new System.Drawing.Point(-1, -5);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(230, 522);
            this.panel1.TabIndex = 0;
            // 
            // assignment
            // 
            this.assignment.BackColor = System.Drawing.Color.Transparent;
            this.assignment.Location = new System.Drawing.Point(17, 440);
            this.assignment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.assignment.Name = "assignment";
            this.assignment.Size = new System.Drawing.Size(164, 45);
            this.assignment.TabIndex = 7;
            this.assignment.Text = "Assignments";
            this.assignment.UseVisualStyleBackColor = false;
            this.assignment.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.Location = new System.Drawing.Point(17, 239);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(164, 45);
            this.button2.TabIndex = 6;
            this.button2.Text = "View announcements ";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TranscriptBtn
            // 
            this.TranscriptBtn.BackColor = System.Drawing.Color.Transparent;
            this.TranscriptBtn.Location = new System.Drawing.Point(17, 371);
            this.TranscriptBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TranscriptBtn.Name = "TranscriptBtn";
            this.TranscriptBtn.Size = new System.Drawing.Size(164, 45);
            this.TranscriptBtn.TabIndex = 5;
            this.TranscriptBtn.Text = "Transcript";
            this.TranscriptBtn.UseVisualStyleBackColor = false;
            this.TranscriptBtn.Click += new System.EventHandler(this.TranscriptBtn_Click);
            // 
            // MarksBtn
            // 
            this.MarksBtn.BackColor = System.Drawing.Color.Transparent;
            this.MarksBtn.Location = new System.Drawing.Point(17, 303);
            this.MarksBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MarksBtn.Name = "MarksBtn";
            this.MarksBtn.Size = new System.Drawing.Size(164, 45);
            this.MarksBtn.TabIndex = 4;
            this.MarksBtn.Text = "Marks";
            this.MarksBtn.UseVisualStyleBackColor = false;
            this.MarksBtn.Click += new System.EventHandler(this.MarksBtn_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(17, 171);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 45);
            this.button1.TabIndex = 3;
            this.button1.Text = "View Course Material";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RegisterCourses_btn
            // 
            this.RegisterCourses_btn.BackColor = System.Drawing.Color.Transparent;
            this.RegisterCourses_btn.Location = new System.Drawing.Point(17, 99);
            this.RegisterCourses_btn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RegisterCourses_btn.Name = "RegisterCourses_btn";
            this.RegisterCourses_btn.Size = new System.Drawing.Size(164, 45);
            this.RegisterCourses_btn.TabIndex = 1;
            this.RegisterCourses_btn.Text = "Register Courses";
            this.RegisterCourses_btn.UseVisualStyleBackColor = false;
            this.RegisterCourses_btn.Click += new System.EventHandler(this.RegisterCourses_btn_Click);
            // 
            // Attendence_btn
            // 
            this.Attendence_btn.BackColor = System.Drawing.Color.Transparent;
            this.Attendence_btn.Location = new System.Drawing.Point(17, 31);
            this.Attendence_btn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Attendence_btn.Name = "Attendence_btn";
            this.Attendence_btn.Size = new System.Drawing.Size(164, 45);
            this.Attendence_btn.TabIndex = 0;
            this.Attendence_btn.Text = "Attendance";
            this.Attendence_btn.UseVisualStyleBackColor = false;
            this.Attendence_btn.Click += new System.EventHandler(this.Attendence_btn_Click);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.ForeColor = System.Drawing.Color.White;
            this.usernameLabel.Location = new System.Drawing.Point(13, 12);
            this.usernameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(44, 16);
            this.usernameLabel.TabIndex = 2;
            this.usernameLabel.Text = "label1";
            // 
            // logout_btn
            // 
            this.logout_btn.BackColor = System.Drawing.Color.DarkRed;
            this.logout_btn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.logout_btn.Location = new System.Drawing.Point(721, 26);
            this.logout_btn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.logout_btn.Name = "logout_btn";
            this.logout_btn.Size = new System.Drawing.Size(113, 31);
            this.logout_btn.TabIndex = 4;
            this.logout_btn.Text = "Logout";
            this.logout_btn.UseVisualStyleBackColor = false;
            this.logout_btn.Click += new System.EventHandler(this.attendance_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Handwriting", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(288, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(401, 43);
            this.label1.TabIndex = 9;
            this.label1.Text = "Student Dashboard";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // StudentDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(847, 499);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logout_btn);
            this.Controls.Add(this.panel1);
            this.Name = "StudentDashboard";
            this.Text = "StudentDashboard";
            this.Load += new System.EventHandler(this.StudentDashboard_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Attendence_btn;
        private System.Windows.Forms.Button RegisterCourses_btn;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button logout_btn;
        private System.Windows.Forms.Button MarksBtn;
        private System.Windows.Forms.Button TranscriptBtn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button assignment;
        private System.Windows.Forms.Label label1;
    }
}
