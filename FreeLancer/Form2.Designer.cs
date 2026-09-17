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
            label1 = new Label();
            Aranacak_İs = new TextBox();
            Price_Info = new Button();
            button1 = new Button();
            Ara = new Button();
            HourlyRate = new TextBox();
            Hourly = new Label();
            FixedPrice = new TextBox();
            Fixed = new Label();
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
            panel_content.Controls.Add(label1);
            panel_content.Controls.Add(Aranacak_İs);
            panel_content.Controls.Add(Price_Info);
            panel_content.Controls.Add(button1);
            panel_content.Controls.Add(Ara);
            panel_content.Controls.Add(HourlyRate);
            panel_content.Controls.Add(Hourly);
            panel_content.Controls.Add(FixedPrice);
            panel_content.Controls.Add(Fixed);
            panel_content.Controls.Add(lbl_jobType);
            panel_content.Controls.Add(cmb_JobType);
            panel_content.Location = new Point(25, 96);
            panel_content.Name = "panel_content";
            panel_content.Size = new Size(752, 328);
            panel_content.TabIndex = 1;
            panel_content.Paint += panel_content_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.WhiteSmoke;
            label1.Location = new Point(20, 193);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 12;
            label1.Text = "Aranacak İş";
            // 
            // Aranacak_İs
            // 
            Aranacak_İs.BackColor = Color.FromArgb(16, 48, 38);
            Aranacak_İs.ForeColor = Color.WhiteSmoke;
            Aranacak_İs.Location = new Point(20, 216);
            Aranacak_İs.Name = "Aranacak_İs";
            Aranacak_İs.Size = new Size(280, 23);
            Aranacak_İs.TabIndex = 11;
            Aranacak_İs.Text = "Ornk : Developer";
            Aranacak_İs.TextChanged += Aranacak_İs_TextChanged;
            // 
            // Price_Info
            // 
            Price_Info.BackColor = Color.FromArgb(16, 48, 38);
            Price_Info.ForeColor = Color.WhiteSmoke;
            Price_Info.Location = new Point(20, 245);
            Price_Info.Name = "Price_Info";
            Price_Info.Size = new Size(158, 23);
            Price_Info.TabIndex = 10;
            Price_Info.Text = "Bilgileri Kaydet";
            Price_Info.UseVisualStyleBackColor = false;
            Price_Info.Click += Price_Info_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(16, 48, 38);
            button1.ForeColor = Color.WhiteSmoke;
            button1.Location = new Point(103, 293);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 9;
            button1.Text = "ÇıkışYap";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // Ara
            // 
            Ara.BackColor = Color.FromArgb(16, 48, 38);
            Ara.ForeColor = Color.WhiteSmoke;
            Ara.Location = new Point(20, 293);
            Ara.Name = "Ara";
            Ara.Size = new Size(75, 23);
            Ara.TabIndex = 7;
            Ara.Text = "Ara";
            Ara.UseVisualStyleBackColor = false;
            Ara.Click += button1_Click;
            // 
            // HourlyRate
            // 
            HourlyRate.BackColor = Color.FromArgb(16, 48, 38);
            HourlyRate.ForeColor = Color.WhiteSmoke;
            HourlyRate.Location = new Point(24, 167);
            HourlyRate.Name = "HourlyRate";
            HourlyRate.Size = new Size(280, 23);
            HourlyRate.TabIndex = 6;
            HourlyRate.TextChanged += HourlyRate_TextChanged;
            // 
            // Hourly
            // 
            Hourly.AutoSize = true;
            Hourly.ForeColor = Color.WhiteSmoke;
            Hourly.Location = new Point(24, 149);
            Hourly.Name = "Hourly";
            Hourly.Size = new Size(71, 15);
            Hourly.TabIndex = 5;
            Hourly.Text = "Saatlik ücret";
            Hourly.Click += Hourly_Click;
            // 
            // FixedPrice
            // 
            FixedPrice.BackColor = Color.FromArgb(16, 48, 38);
            FixedPrice.ForeColor = Color.WhiteSmoke;
            FixedPrice.Location = new Point(24, 110);
            FixedPrice.Name = "FixedPrice";
            FixedPrice.Size = new Size(280, 23);
            FixedPrice.TabIndex = 4;
            FixedPrice.TextChanged += FixedPrice_TextChanged;
            // 
            // Fixed
            // 
            Fixed.AutoSize = true;
            Fixed.ForeColor = Color.White;
            Fixed.Location = new Point(24, 92);
            Fixed.Name = "Fixed";
            Fixed.Size = new Size(58, 15);
            Fixed.TabIndex = 3;
            Fixed.Text = "SabitFiyat";
            Fixed.Click += label1_Click;
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
            cmb_JobType.SelectedIndexChanged += cmb_JobType_SelectedIndexChanged;
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
        private TextBox FixedPrice;
        private TextBox HourlyRate;
        private Label Hourly;
        private Label Fixed;
        private Button Ara;
        private Button button1;
        private Button Price_Info;
        private Label label1;
        private TextBox Aranacak_İs;
    }
}
