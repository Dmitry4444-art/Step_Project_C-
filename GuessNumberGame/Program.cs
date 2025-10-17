using GuessNumberGame.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("  ===  Gamoicanit ricxvi  ===  ");

var leaderboard = Leaderboard.Load();

while (true)
{
    var game = new Game();
    game.Play();

    Console.WriteLine();
    Console.Write("Gsurt chawerot tqveni shedegi liderbordshi? (Y/N): ");
    var input = Console.ReadLine()?.Trim().ToLower();
    if(input == "y" || input == "yes")
    {
        Console.Write("Sheiyvanet tqveni saxeli: ");
        var name = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(name))
            name = "Player" + new Random().Next(1000, 9999);

        leaderboard.Add(new LeaderboardEntry
        {
            Name = name,
            Score = game.CalculateScore(),
            Attempts = game.AttemptsUsed,
            Difficulty = game.DifficultyName,
            TimeSeconds = game.ElapsedSeconds,
            DateOnly = DateTime.UtcNow
        });
        leaderboard.Save();
        Console.WriteLine("Shedegi shenaxulia lideordshi.");
    }

    Console.WriteLine(" Liderbordi - Sauketeso 10 shedegi: ");
    leaderboard.PrintTop(10);

    Console.WriteLine();
    Console.Write("Gsurt tamashi isev? (Y/N): ");
    var again = Console.ReadLine()?.Trim().ToLower();
    if (!(again == "y" || again == "yes")) break;

    Console.Clear();
    
}

Console.WriteLine("Sasiamovno tamashi iyo! Gauziare megobrebs :)");