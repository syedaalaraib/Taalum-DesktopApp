namespace Ta_allum
{
    partial class UploadMarks
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxFacultySections = new System.Windows.Forms.ComboBox();
            this.comboBoxAssignedCourses = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listBoxStudents = new System.Windows.Forms.ListBox();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.Back = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxFacultySections
            // 
            this.comboBoxFacultySections.FormattingEnabled = true;
            this.comboBoxFacultySections.Location = new System.Drawing.Point(411, 90);
            this.comboBoxFacultySections.Name = "comboBoxFacultySections";
            this.comboBoxFacultySections.Size = new System.Drawing.Size(110, 21);
            this.comboBoxFacultySections.TabIndex = 0;
            this.comboBoxFacultySections.SelectedIndexChanged += new System.EventHandler(this.comboBoxFacultySections_SelectedIndexChanged);
            // 
            // comboBoxAssignedCourses
            // 
            this.comboBoxAssignedCourses.FormattingEnabled = true;
            this.comboBoxAssignedCourses.Location = new System.Drawing.Point(411, 146);
            this.comboBoxAssignedCourses.Name = "comboBoxAssignedCourses";
            this.comboBoxAssignedCourses.Size = new System.Drawing.Size(110, 21);
            this.comboBoxAssignedCourses.TabIndex = 1;
            this.comboBoxAssignedCourses.SelectedIndexChanged += new System.EventHandler(this.comboBoxAssignedCourses_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Script", 20.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(264, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 44);
            this.label1.TabIndex = 2;
            this.label1.Text = "Upload Marks";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(234, 147);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(130, 20);
            this.textBox2.TabIndex = 10;
            this.textBox2.Text = "Select course section ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(234, 90);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(130, 20);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "Select a course ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(352, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Confirm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBoxStudents
            // 
            this.listBoxStudents.FormattingEnabled = true;
            this.listBoxStudents.Location = new System.Drawing.Point(243, 227);
            this.listBoxStudents.Name = "listBoxStudents";
            this.listBoxStudents.Size = new System.Drawing.Size(287, 147);
            this.listBoxStudents.TabIndex = 13;
            // 
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(424, 241);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(97, 25);
            this.flowLayoutPanelButtons.TabIndex = 15;
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(352, 401);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(75, 23);
            this.Back.TabIndex = 16;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // UploadMarks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.flowLayoutPanelButtons);
            this.Controls.Add(this.listBoxStudents);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxAssignedCourses);
            this.Controls.Add(this.comboBoxFacultySections);
            this.Name = "UploadMarks";
            this.Text = "UploadMarks";
            this.Load += new System.EventHandler(this.UploadMarks_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxFacultySections;
        private System.Windows.Forms.ComboBox comboBoxAssignedCourses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBoxStudents; // Declare ListBox control
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        private System.Windows.Forms.Button Back;
    }
}