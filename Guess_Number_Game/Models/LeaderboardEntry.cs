using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessNumberGame.Models
{
    public class LeaderboardEntry
    {
        public string Name { get; set; } = "Player"; // მოთამაშის სახელი
        public int Score { get; set; } // მიღებული ქულა
        public int Attempts { get; set; } // მცდელობების რაოდენობა
        public double TimeSeconds { get; set; } // დრო წამებში
        public string Difficulty { get; set; } = "Unknown"; // სირთულის დონე
        public DateTime DateOnly { get; set; } = DateTime.UtcNow; // თარიღი
    }
}
