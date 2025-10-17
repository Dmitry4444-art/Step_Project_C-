using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GuessNumberGame.Models
{
    public class Game
    {
        private readonly Random _rng = new Random(); // შემთხვევითი რიცხვების გენერატორი
        public int Min { get; private set; } // მინიმალური მნიშვნელობა
        public int Max { get; private set; } // მაქსიმალური მნიშვნელობა
        public int Secret { get; private set; } // დასამალავი "საიდუმლო" რიცხვი
        public int MaxAttempts { get; private set; } // მაქსიმალური ცდების რაოდენობა
        public int AttemptsUsed { get; private set; } // რამდენჯერ სცადა
        public string DifficultyName { get; private set; } = "Custom"; // სირთულის დონე

        private bool _hotColdHints; // "ცხელი/ცივი" მინიშნებების გამოყენება
        private int _previousDistance = -1; // წინა ცდიდან მანძილი საიდუმლო რიცხვამდე
        private Stopwatch _stopwatch = new Stopwatch(); // ტაიმერი დროის გასაზომად
        public double ElapsedSeconds => _stopwatch.Elapsed.TotalSeconds; // გასული დრო წამებში

        public Game()
        {
            Configure(); // კონფიგურაციის გამოძახება თამაშის დასაწყისში
        }

        private void Configure()
        {
            Console.WriteLine("Choose Level: 1) Easy; 2) Medium, 3) Hard, 4) Custom"); // სირთულის დონეების ჩვენება
            Console.Write("Airchiet (Standartuli : 2): ");
            var level = ReadOption(new[] { "1", "2", "3", "4" }, "2"); // არჩევანის წაკითხვა

            switch (level) // დონის მიხედვით პარამეტრების მინიჭება
            {
                case "1":
                    Min = 1; Max = 50; MaxAttempts = 12; DifficultyName = "Easy"; break;
                case "2":
                    Min = 1; Max = 100; MaxAttempts = 10; DifficultyName = "Medium"; break;
                case "3":
                    Min = 1; Max = 1000; MaxAttempts = 12; DifficultyName = "Hard"; break;
                default:
                    DifficultyName = "Custum"; // მომხმარებლის პირადი პარამეტრები
                    Min = ReadInt("Min. ricxvi (1): ", 1);
                    Max = ReadInt("Max. ricxvi (100): ", 100);
                    if (Max <= Min) { Console.WriteLine("Max. unda iyos meti."); Min = 1; Max = 100; }
                    MaxAttempts = ReadInt("Max. mcdelobebi (10): ", 10);
                    break;
            }

            Console.Write("Gsurt Hot/Cold minishnebebi? (Y/N): "); // ეკითხება მოთამაშეს, უნდა თუ არა მინიშნებები
            var hotcold = Console.ReadLine()?.Trim().ToLower();
            _hotColdHints = (hotcold == "y" || hotcold == "yes"); // პასუხის შემოწმება

            Secret = _rng.Next(Min, Max + 1); // საიდუმლო რიცხვის გენერაცია
            Console.WriteLine($"Me avirchie ricxvi diapazonidan {Min} - {Max}. Gaqvs {MaxAttempts} mcdeloba. ");
        }

        public void Play()
        {
            AttemptsUsed = 0; // ცდების რაოდენობის განულება
            _stopwatch.Restart(); // ტაიმერის ჩართვა
            int low = Min, high = Max; // დასაშვები დიაპაზონის საზღვრები

            while (AttemptsUsed < MaxAttempts)
            {
                AttemptsUsed++;
                Console.Write($"[{AttemptsUsed}/{MaxAttempts}] sheiyvanet ricxvi ({low}-{high}): ");
                if (!int.TryParse(Console.ReadLine(), out int guess)) // შემოწმება, სწორად შეიყვანა თუ არა რიცხვი
                {
                    Console.WriteLine("Sheiyvanet swori ricxvi!");
                    AttemptsUsed--; // არასწორი შეყვანა არ ითვლება ცდად
                    continue;
                }

                if (guess < low || guess > high) // თუ დიაპაზონს გარეთაა
                {
                    Console.WriteLine($"Gtxovt sheiyvanot ricxvi {low}-{high} diapazonshi.");
                    AttemptsUsed--;
                    continue;
                }

                if (guess == Secret) // თუ სწორად გამოიცნო
                {
                    _stopwatch.Stop(); // ტაიმერის გაჩერება
                    Console.WriteLine($"  Gilocaaavt! Tqven gamoicaniit {Secret}!");
                    Console.WriteLine($"Mcdelobebi: {AttemptsUsed}, Dro: {ElapsedSeconds:F2} Wm, Qula: {CalculateScore()}.");
                    return;
                }

                // არასწორი ვარიანტის შემთხვევაში მინიშნებები
                if (guess < Secret)
                {
                    Console.WriteLine("🔼 Scade ufro didi ricxvi!");
                    low = guess + 1;
                }
                else
                {
                    Console.WriteLine("🔽 Scade ufro patara ricxvi!");
                    high = guess - 1;
                }

                int distance = Math.Abs(Secret - guess); // მანძილი საიდუმლო რიცხვამდე
                if (_hotColdHints && _previousDistance != -1)
                {
                    if (distance < _previousDistance) Console.WriteLine("🔥 Ufro axlos xar!");
                    else if (distance > _previousDistance) Console.WriteLine("❄️ Ufro shors xar!");
                }
                _previousDistance = distance;

                if (AttemptsUsed == MaxAttempts - 1)
                    Console.WriteLine($"📘 Bolo minishenba: ricxvi {low}-{high} shorisaa.");
                Console.WriteLine();
            }

            _stopwatch.Stop();
            Console.WriteLine($"  Samwuxarod, cdebis raodenoba amoiwura. Swori ricxvi iyo: {Secret}.");
        }

        public int CalculateScore() // ქულის გამოთვლა
        {
            double baseScore = 1000.0 / (Max - Min + 1) * (MaxAttempts + 1);
            double attemptFactor = Math.Max(0.1, (MaxAttempts - AttemptsUsed + 1) / (double)MaxAttempts);
            double timeFactor = Math.Max(0.1, 60.0 / (_stopwatch.Elapsed.TotalSeconds + 1));
            int score = (int)Math.Round(baseScore * attemptFactor * timeFactor * (_hotColdHints ? 1.05 : 1.0));
            return Math.Max(0, score);
        }

        private static string ReadOption(string[] allowed, string def)
        {
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) return def;
            return allowed.Contains(line.Trim()) ? line.Trim() : def;
        }

        private static int ReadInt(string msg, int def)
        {
            Console.Write(msg);
            var input = Console.ReadLine();
            return int.TryParse(input, out int value) ? value : def;
        }
    }
}