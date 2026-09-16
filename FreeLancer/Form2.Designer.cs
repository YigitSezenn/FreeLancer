namespace FreeLancer
{
    partial class Form2
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
            panel_header = new Panel();
            lbl_brand = new Label();
            lbl_title = new Label();
            panel_content = new Panel();
            lbl_jobType = new Label();
            cmb_JobType = new ComboBox();
            panel_header.SuspendLayout();
            panel_content.SuspendLayout();
            SuspendLayout();
            // 
            // panel_header
            // 
            panel_header.BackColor = Color.FromArgb(16, 48, 38);
            panel_header.Controls.Add(lbl_brand);
            panel_header.Controls.Add(lbl_title);
            panel_header.Dock = DockStyle.Top;
            panel_header.Location = new Point(0, 0);
            panel_header.Name = "panel_header";
            panel_header.Size = new Size(800, 72);
            panel_header.TabIndex = 0;
            // 
            // lbl_brand
            // 
            lbl_brand.AutoSize = true;
            lbl_brand.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lbl_brand.ForeColor = Color.FromArgb(20, 168, 0);
            lbl_brand.Location = new Point(24, 18);
            lbl_brand.Name = "lbl_brand";
            lbl_brand.Size = new Size(116, 30);
            lbl_brand.TabIndex = 0;
            lbl_brand.Text = "Freelancer";
            // 
            // lbl_title
            // 
            lbl_title.AutoSize = true;
            lbl_title.Font = new Font("Segoe UI", 10F);
            lbl_title.ForeColor = Color.FromArgb(190, 210, 198);
            lbl_title.Location = new Point(162, 26);
            lbl_title.Name = "lbl_title";
            lbl_title.Size = new Size(147, 19);
            lbl_title.TabIndex = 1;
            lbl_title.Text = "İş Arama Otomasyonu";
            // 
            // panel_content
            // 
            panel_content.BackColor = Color.FromArgb(16, 48, 38);
            panel_content.Controls.Add(lbl_jobType);
            panel_content.Controls.Add(cmb_JobType);
            panel_content.Location = new Point(24, 96);
            panel_content.Name = "panel_content";
            panel_content.Size = new Size(752, 328);
            panel_content.TabIndex = 1;
            panel_content.Paint += panel_content_Paint;
            // 
            // lbl_jobType
            // 
            lbl_jobType.AutoSize = true;
            lbl_jobType.BackColor = Color.FromArgb(16, 48, 38);
            lbl_jobType.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lbl_jobType.ForeColor = Color.White;
            lbl_jobType.Location = new Point(24, 28);
            lbl_jobType.Name = "lbl_jobType";
            lbl_jobType.Size = new Size(51, 19);
            lbl_jobType.TabIndex = 0;
            lbl_jobType.Text = "İş Türü";
            lbl_jobType.Click += lbl_jobType_Click;
            // 
            // cmb_JobType
            // 
            cmb_JobType.BackColor = Color.FromArgb(16, 48, 38);
            cmb_JobType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_JobType.FlatStyle = FlatStyle.Flat;
            cmb_JobType.Font = new Font("Segoe UI", 10F);
            cmb_JobType.ForeColor = Color.White;
            cmb_JobType.FormattingEnabled = true;
            cmb_JobType.Items.AddRange(new object[] { "Hourly Rate", "Fixed Price" });
            cmb_JobType.Location = new Point(24, 54);
            cmb_JobType.Name = "cmb_JobType";
            cmb_JobType.Size = new Size(280, 25);
            cmb_JobType.TabIndex = 1;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(12, 36, 28);
            ClientSize = new Size(800, 450);
            Controls.Add(panel_content);
            Controls.Add(panel_header);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Freelancer - İş Arama";
            panel_header.ResumeLayout(false);
            panel_header.PerformLayout();
            panel_content.ResumeLayout(false);
            panel_content.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel_header;
        private Label lbl_brand;
        private Label lbl_title;
        private Panel panel_content;
        private Label lbl_jobType;
        private ComboBox cmb_JobType;
    }
}
