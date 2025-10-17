using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessNumberGame.Models
{
    public class LeaderboardEntry
    {
        public string Name { get; set; } = "Player";
        public int Score { get; set; }
        public int Attempts { get; set; }
        public double TimeSeconds { get; set; }
        public string Difficulty { get; set; } = "Unknown";
        public DateTime DateOnly { get; set; } = DateTime.UtcNow;
    }
}
