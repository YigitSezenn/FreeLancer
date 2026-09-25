using System.Text.Json;

namespace FreeLancer.Sessions
{
    public class Applied
    {
        private static readonly string JobPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "FreeLancerWork", "jobsappliedfor.json");

        public List<string> AppliedJobs { get; set; } = [];
        public static Applied Current { get; set; } = new();

        public static void Load()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(JobPath)!);
            if (!File.Exists(JobPath))
                return;

            Current = JsonSerializer.Deserialize<Applied>(File.ReadAllText(JobPath)) ?? new Applied();
        }

        public static void SaveWork()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(JobPath)!);
            File.WriteAllText(JobPath, JsonSerializer.Serialize(Current));
        }

        public static bool Apply(string jobUrl)
        {
            if (Current.AppliedJobs.Contains(jobUrl, StringComparer.OrdinalIgnoreCase))
                return false;

            Current.AppliedJobs.Add(jobUrl);
            SaveWork();
            return true;
        }
    }
}
