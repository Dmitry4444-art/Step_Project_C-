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
        private const string FileName = "guess_leaderboard.json"; // ფაილის სახელი, სადაც ინახება შედეგები
        public List<LeaderboardEntry> Entries { get; set; } = new(); // ლიდერბორდის ჩანაწერების სია

        public void Add(LeaderboardEntry entry) // ახალი ჩანაწერის დამატება
        {
            Entries.Add(entry);
            Entries = Entries
                .OrderByDescending(e => e.Score) // სორტირება ქულების კლებადობით
                .ThenBy(e => e.Attempts) // თანაბარი ქულის შემთხვევაში — მცდელობების რაოდენობით
                .ThenBy(e => e.TimeSeconds) // შემდეგ — დროით
                .Take(100) // მხოლოდ ტოპ 100 შენარჩუნება
                .ToList();
        }

        public void Save() // შედეგების ფაილში შენახვა
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

        public static Leaderboard Load() // შედეგების ჩატვირთვა
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

        public void PrintTop(int n) // ტოპ N მოთამაშის ჩვენება
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

