using FreeLancer.Settings;

namespace FreeLancer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            AppSession.Load();
            Application.Run(new Form1());
        }
    }
}
