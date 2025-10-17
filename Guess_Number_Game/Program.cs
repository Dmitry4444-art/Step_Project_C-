using GuessNumberGame.Models; // იმპორტირდება Models ნეიმსფეისი, სადაც მოთავსებულია კლასები Game, Leaderboard და LeaderboardEntry

Console.OutputEncoding = System.Text.Encoding.UTF8; // კონსოლის სიმბოლური კოდირება UTF-8-ზე, რომ ემოჯები და ქართული ტექსტი სწორად გამოჩნდეს
Console.WriteLine("  ===  Gamoicanit ricxvi  ===  "); // სათაურის გამოტანა

var leaderboard = Leaderboard.Load(); // ვტვირთავთ ლიდერბორდის მონაცემებს JSON ფაილიდან

while (true) // მთავარი თამაშის ციკლი, სანამ მოთამაშე არ გადაწყვეტს გასვლას
{
    var game = new Game(); // ახალი თამაშის ობიექტის შექმნა
    game.Play(); // თამაშის დაწყება და გაშვება

    Console.WriteLine();
    Console.Write("Gsurt chawerot tqveni shedegi liderbordshi? (Y/N): "); // მოთამაშეს ეკითხება, სურს თუ არა შედეგის შენახვა
    var input = Console.ReadLine()?.Trim().ToLower(); // წაკითხვა და ფორმატირება
    if (input == "y" || input == "yes") // თუ მოთამაშე თანახმაა
    {
        Console.Write("Sheiyvanet tqveni saxeli: "); // სახელის შეყვანა
        var name = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(name)) // თუ სახელი ცარიელია
            name = "Player" + new Random().Next(1000, 9999); // გენერირდება შემთხვევითი სახელის ფორმატით

        leaderboard.Add(new LeaderboardEntry // ლიდერბორდში ახალი ჩანაწერის დამატება
        {
            Name = name,
            Score = game.CalculateScore(), // ქულის გამოთვლა
            Attempts = game.AttemptsUsed, // რამდენი ცდა გააკეთა
            Difficulty = game.DifficultyName, // რომელი სირთულე იყო
            TimeSeconds = game.ElapsedSeconds, // რამდენი წამი დასჭირდა
            DateOnly = DateTime.UtcNow // თარიღი
        });
        leaderboard.Save(); // შედეგების შენახვა ფაილში
        Console.WriteLine("Shedegi shenaxulia lideordshi."); // შეტყობინება წარმატებით შენახვისას
    }

    Console.WriteLine(" Liderbordi - Sauketeso 10 shedegi: "); // ლიდერბორდის გამოტანა
    leaderboard.PrintTop(10); // ტოპ 10 მოთამაშის ჩვენება

    Console.WriteLine();
    Console.Write("Gsurt tamashi isev? (Y/N): "); // კითხვა – უნდა გაგრძელდეს თუ არა თამაში
    var again = Console.ReadLine()?.Trim().ToLower();
    if (!(again == "y" || again == "yes")) break; // თუ არაა თანხმობა, ვწყვეტთ ციკლს

    Console.Clear(); // ეკრანის გასუფთავება, ახალი თამაშის დასაწყებად
}

Console.WriteLine("Sasiamovno tamashi iyo! Gauziare megobrebs :)"); // დამამშვიდობებელი ტექსტი
