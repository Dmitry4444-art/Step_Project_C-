using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GuessNumberGame.Models
{
    public class Leaderboard
    {
        private const string FileName = "guess_leaderboard.json";
        public List<LeaderboardEntry> Entries { get; set; } = new();

        public void Add(LeaderboardEntry entry)
        {
            Entries.Add(entry);
            Entries = Entries
                .OrderByDescending(e => e.Score)
                .ThenBy(e => e.Attempts)
                .ThenBy(e => e.TimeSeconds)
                .Take(100)
                .ToList();
        }

        public void Save()
        {
            try
            {
                var json = JsonSerializer.Serialize(Entries, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FileName, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Shenaxva ver moxerxda: " + ex.Message);
            }
        }

        public static Leaderboard Load()
        {
            try
            {
                if (!File.Exists(FileName)) return new Leaderboard();
                var json = File.ReadAllText(FileName);
                var entries = JsonSerializer.Deserialize<List<LeaderboardEntry>>(json);
                return new Leaderboard { Entries = entries ?? new List<LeaderboardEntry>() };
            }
            catch
            {
                return new Leaderboard();
            }
        }

        public void PrintTop(int n)
        {
            var top = Entries.Take(n).ToList();
            if (!top.Any())
            {
                Console.WriteLine("lIDERBORDI CARIELIA. TQVEN PIRVELI IYAVIT!");
                return;
            }

            int i = 1;
            foreach (var e in top)
            {
                Console.WriteLine($"{i}. {e.Name} — {e.Score}  Qula — Mcdelobebi: {e.Attempts} — Time: {e.TimeSeconds:F1} Wm — {e.Difficulty}");
                i++;
            }
        }
    }
}
