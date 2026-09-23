using System.Text.Json;

namespace FreeLancer.Settings
{
    public class AppSession
    {
        private static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "FreeLancer", "session.json");


        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public bool IsLoggedIn { get; set; }
        public int FixedPrice { get; set; }
        public int HourlyRate { get; set; }
        public string WorkType { get; set; } = "";
        public string Work { get; set; } = "";
        public string BasvuruMetni { get; set; } = "";
        public static AppSession Current { get; set; } = new();

        public static void Load()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            if (!File.Exists(FilePath))
                return;

            string jsonString = File.ReadAllText(FilePath);
            Current = JsonSerializer.Deserialize<AppSession>(jsonString)!;
        }

   

        public static void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            string jsonString = JsonSerializer.Serialize(Current);
            File.WriteAllText(FilePath, jsonString);
        }
    }
}
