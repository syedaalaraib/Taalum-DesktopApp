// RegisterCourses.Designer.cs
namespace Ta_allum
{
    partial class RegisterCourses
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

        private void InitializeComponent()
        {
            this.usernameLabel = new System.Windows.Forms.Label();
            this.offeredCoursesDataGridView = new System.Windows.Forms.DataGridView();
            this.backButton = new System.Windows.Forms.Button();
            this.Heading = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.offeredCoursesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.Location = new System.Drawing.Point(735, -2);
            this.usernameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(102, 35);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Welcome";
            // 
            // offeredCoursesDataGridView
            // 
            this.offeredCoursesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.offeredCoursesDataGridView.Location = new System.Drawing.Point(67, 112);
            this.offeredCoursesDataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.offeredCoursesDataGridView.Name = "offeredCoursesDataGridView";
            this.offeredCoursesDataGridView.RowHeadersWidth = 51;
            this.offeredCoursesDataGridView.Size = new System.Drawing.Size(800, 308);
            this.offeredCoursesDataGridView.TabIndex = 1;
            this.offeredCoursesDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.offeredCoursesDataGridView_CellContentClick);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(67, 455);
            this.backButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(133, 37);
            this.backButton.TabIndex = 2;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // Heading
            // 
            this.Heading.AutoSize = true;
            this.Heading.Font = new System.Drawing.Font("Segoe Print", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Heading.Location = new System.Drawing.Point(248, 25);
            this.Heading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Heading.Name = "Heading";
            this.Heading.Size = new System.Drawing.Size(357, 71);
            this.Heading.TabIndex = 3;
            this.Heading.Text = "Register Courses";
            // 
            // RegisterCourses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 554);
            this.Controls.Add(this.Heading);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.offeredCoursesDataGridView);
            this.Controls.Add(this.usernameLabel);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "RegisterCourses";
            this.Text = "Register Courses";
            ((System.ComponentModel.ISupportInitialize)(this.offeredCoursesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.DataGridView offeredCoursesDataGridView;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Label Heading;
    }
}
