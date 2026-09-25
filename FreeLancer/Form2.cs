using FreeLancer.Playwright;
using FreeLancer.Settings;

namespace FreeLancer
{
    public partial class Form2 : Form
    {
        private Login? _pw;



        public Form2()
        {
            InitializeComponent();
            HourlyRate.Text = AppSession.Current.HourlyRate.ToString();
            FixedPrice.Text = AppSession.Current.FixedPrice.ToString();
            var workTypeIndex = cmb_JobType.Items.IndexOf(AppSession.Current.WorkType);
            cmb_JobType.SelectedIndex = workTypeIndex >= 0 ? workTypeIndex : 0;
            var Work = Aranacak_İs.Text = AppSession.Current.Work;
            if (!string.IsNullOrEmpty(AppSession.Current.BasvuruMetni))
                txt_basvuru_metni.Text = AppSession.Current.BasvuruMetni;

            AraDurumunuGuncelle();

        }

        private void panel_content_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbl_jobType_Click(object sender, EventArgs e)
        {

        }

        private void cmb_JobType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FixedPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void HourlyRate_TextChanged(object sender, EventArgs e)
        {

        }

        private void Hourly_Click(object sender, EventArgs e)
        {

        }

        private bool BasvuruMetniGecerli()
        {
            int uzunluk = txt_basvuru_metni.Text.Length;

            if (uzunluk < 100)
            {
                MessageBox.Show("Başvuru metni en az 100 karakter olmalıdır.");
                return false;
            }

            if (uzunluk > 2000)
            {
                MessageBox.Show("Başvuru metni en fazla 2000 karakter olabilir.");
                return false;
            }

            return true;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!BasvuruMetniGecerli())
                return;

           
            Ara.Enabled = false;
            try
            {
                AppSession.Current.Work = Aranacak_İs.Text;
                AppSession.Current.WorkType = cmb_JobType.SelectedItem?.ToString() ?? "";
                int.TryParse(HourlyRate.Text, out int hourly);
                int.TryParse(FixedPrice.Text, out int fixedPrice);
                AppSession.Current.HourlyRate = hourly;
                AppSession.Current.FixedPrice = fixedPrice;
                _pw ??= new Login();
                var page = await _pw.Browser();
                await _pw.FreeLancerLogin(page);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                AraDurumunuGuncelle();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AppSession.Current.IsLoggedIn = false;
            AppSession.Save();
            Close();
        }

        private void Price_Info_Click(object sender, EventArgs e)
        {
            if (!BasvuruMetniGecerli())
                return;

            if (string.IsNullOrWhiteSpace(Aranacak_İs.Text))
            {
                MessageBox.Show("Aranacak iş giriniz.");
                return;
            }

            int.TryParse(HourlyRate.Text, out int hourly);
            int.TryParse(FixedPrice.Text, out int fixedPrice);

            AppSession.Current = new AppSession
            {
                Email = AppSession.Current.Email,
                Password = AppSession.Current.Password,
                IsLoggedIn = AppSession.Current.IsLoggedIn,
                HourlyRate = hourly,
                FixedPrice = fixedPrice,
                WorkType = cmb_JobType.SelectedItem?.ToString() ?? "",
                Work = Aranacak_İs.Text.ToString() ?? "",
                BasvuruMetni = txt_basvuru_metni.Text
            };
            AppSession.Save();
            MessageBox.Show("Bilgiler Başarıyla Kayıt Edildi");
        }

        private void Aranacak_İs_TextChanged(object sender, EventArgs e)
        {
            AraDurumunuGuncelle();
        }

        private void AraDurumunuGuncelle()
        {
            bool length = !string.IsNullOrWhiteSpace(Aranacak_İs.Text);
            Ara.Enabled = length;
            Ara.Text = length ? "Ara" : "Aranacak iş giriniz";
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void txt_basvuru_metni_TextChanged(object sender, EventArgs e)
        {
            int uzunluk = txt_basvuru_metni.Text.Length;

            if (uzunluk >= 2000)
            {
                MessageBox.Show("2000 karakterden fazla yazılamaz");
            }
        }
    }
}
