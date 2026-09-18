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

        private async void button1_Click(object sender, EventArgs e)
        {
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
                Ara.Enabled = true;
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
                Work = Aranacak_İs.Text.ToString() ?? ""
            };
            AppSession.Save();
            MessageBox.Show("Bilgiler Başarıyla Kayıt Edildi");
        }

        private void Aranacak_İs_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
