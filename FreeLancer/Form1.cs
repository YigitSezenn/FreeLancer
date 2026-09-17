using FreeLancer.Settings;

namespace FreeLancer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txt_Password_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btn_sign_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txt_Password.Text) || string.IsNullOrWhiteSpace(txt_Email.Text)) {
                MessageBox.Show("Eposta ve şifrenizi giriniz");
                return;
            }
            AppSession.Current = new AppSession
            {
                Email = txt_Email.Text.Trim(),
                Password = txt_Password.Text,
                IsLoggedIn = true,
                FixedPrice = AppSession.Current.FixedPrice,
                HourlyRate = AppSession.Current.HourlyRate,
                WorkType = AppSession.Current.WorkType
            };
            AppSession.Save();
            OpenForm_2();


           

        }
        public  void OpenForm_2()
        {
            Hide();
            var form2 = new Form2();
            form2.FormClosed += (s, args) =>
            {
                if (AppSession.Current.IsLoggedIn)
                    Close();
                else
                    Show();
            };
            form2.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            if (AppSession.Current.IsLoggedIn)
              OpenForm_2();
            

        }
    }
}
