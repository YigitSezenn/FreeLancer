namespace FreeLancer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbl_brand = new Label();
            lbl_subtitle = new Label();
            panel1 = new Panel();
            label4 = new Label();
            label1 = new Label();
            txt_Email = new TextBox();
            label2 = new Label();
            txt_Password = new TextBox();
            btn_sign = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lbl_brand
            // 
            lbl_brand.AutoSize = true;
            lbl_brand.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lbl_brand.ForeColor = Color.FromArgb(20, 168, 0);
            lbl_brand.Location = new Point(168, 22);
            lbl_brand.Name = "lbl_brand";
            lbl_brand.Size = new Size(127, 32);
            lbl_brand.TabIndex = 0;
            lbl_brand.Text = "Freelancer";
            // 
            // lbl_subtitle
            // 
            lbl_subtitle.AutoSize = true;
            lbl_subtitle.Font = new Font("Segoe UI", 9.5F);
            lbl_subtitle.ForeColor = Color.FromArgb(190, 210, 198);
            lbl_subtitle.Location = new Point(168, 56);
            lbl_subtitle.Name = "lbl_subtitle";
            lbl_subtitle.Size = new Size(137, 17);
            lbl_subtitle.TabIndex = 1;
            lbl_subtitle.Text = "İş Arama Otomasyonu";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(16, 48, 38);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txt_Email);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txt_Password);
            panel1.Controls.Add(btn_sign);
            panel1.Location = new Point(58, 96);
            panel1.Name = "panel1";
            panel1.Size = new Size(384, 268);
            panel1.TabIndex = 2;
            panel1.Paint += panel1_Paint;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label4.ForeColor = Color.WhiteSmoke;
            label4.Location = new Point(24, 22);
            label4.Name = "label4";
            label4.Size = new Size(101, 21);
            label4.TabIndex = 0;
            label4.Text = "Giriş Bilgileri";
            label4.Click += label4_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.WhiteSmoke;
            label1.Location = new Point(24, 72);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 1;
            label1.Text = "Eposta";
            // 
            // txt_Email
            // 
            txt_Email.BackColor = Color.White;
            txt_Email.BorderStyle = BorderStyle.FixedSingle;
            txt_Email.Location = new Point(24, 92);
            txt_Email.Name = "txt_Email";
            txt_Email.Size = new Size(336, 23);
            txt_Email.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.WhiteSmoke;
            label2.Location = new Point(24, 128);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 3;
            label2.Text = "Şifre";
            label2.Click += label2_Click;
            // 
            // txt_Password
            // 
            txt_Password.BackColor = Color.White;
            txt_Password.BorderStyle = BorderStyle.FixedSingle;
            txt_Password.Location = new Point(24, 148);
            txt_Password.Name = "txt_Password";
            txt_Password.Size = new Size(336, 23);
            txt_Password.TabIndex = 4;
            txt_Password.UseSystemPasswordChar = true;
            txt_Password.TextChanged += txt_Password_TextChanged;
            // 
            // btn_sign
            // 
            btn_sign.BackColor = Color.FromArgb(16, 48, 38);
            btn_sign.Cursor = Cursors.Hand;
            btn_sign.FlatAppearance.BorderColor = Color.WhiteSmoke;
            btn_sign.FlatAppearance.MouseOverBackColor = Color.FromArgb(16, 138, 0);
            btn_sign.FlatStyle = FlatStyle.Flat;
            btn_sign.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btn_sign.ForeColor = Color.White;
            btn_sign.Location = new Point(24, 198);
            btn_sign.Name = "btn_sign";
            btn_sign.Size = new Size(336, 38);
            btn_sign.TabIndex = 5;
            btn_sign.Text = "Giriş Yap";
            btn_sign.UseVisualStyleBackColor = false;
            btn_sign.Click += btn_sign_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(12, 36, 28);
            ClientSize = new Size(500, 400);
            Controls.Add(lbl_brand);
            Controls.Add(lbl_subtitle);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Freelancer - Giriş";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_brand;
        private Label lbl_subtitle;
        private Panel panel1;
        private Label label4;
        private Label label1;
        private TextBox txt_Email;
        private Label label2;
        private TextBox txt_Password;
        private Button btn_sign;
    }
}
