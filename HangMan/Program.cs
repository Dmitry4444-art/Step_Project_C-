

Dictionary<string, List<string>> categories = new Dictionary<string, List<string>>()
{
    { "Animals", new List<string> { "elephant", "giraffe", "kangaroo", "dolphin", "crocodile" } },
    { "Fruits", new List<string> { "pineapple", "strawberry", "blueberry", "watermelon", "pomegranate" } },
    { "Countries", new List<string> { "argentina", "finland", "mozambique", "singapore", "uzbekistan" } },
    { "Colors", new List<string> { "turquoise", "magenta", "crimson", "lavender", "chartreuse" } },
    { "Sports", new List<string> { "basketball", "cricket", "badminton", "volleyball", "snowboarding" } }

};

Random random = new Random();
string chosenCategory = categories.Keys.ToList()[random.Next(categories.Count)];
string word = categories[chosenCategory][random.Next(categories[chosenCategory].Count)].ToLower();

char[] guessedWord = new string('_', word.Length).ToCharArray();
List<char> guessedLetters = new List<char>();
int attemptsLeft = 7;

Console.WriteLine(" Welcome to Hangman! ");
Console.WriteLine($" Category: {chosenCategory} ");

while (attemptsLeft > 0 && new string(guessedWord) != word)
{
    DrawHangman(attemptsLeft);
    Console.WriteLine(" Word: " + string.Join(" ", guessedWord));
    Console.WriteLine($" Attempts left: {attemptsLeft} ");
    Console.WriteLine($" Guessed letters: {string.Join(", ", guessedLetters)} ");
    Console.Write(" Enter a letter: ");
    string input = Console.ReadLine().ToLower().Trim();

    if (string.IsNullOrWhiteSpace(input) || input.Length != 1 || !char.IsLetter(input[0]))
    {
        Console.WriteLine(" Invalid input! Please enter a single letter. ");
        continue;
    }

    char guess = input[0];

    if (guessedLetters.Contains(guess))
    {
        Console.WriteLine(" You already guessed that letter! ");
        continue;
    }

    guessedLetters.Add(guess);

    if (word.Contains(guess))
    {
        for (int i = 0; i < word.Length; i++)
        if (word[i] == guess) guessedWord[i] = guess;
        Console.WriteLine(" Good guess! ");
    }
    else
    {
        attemptsLeft--;
        Console.WriteLine(" Wrong guess! ");
    }
}

if (new string(guessedWord) == word)
{
    Console.WriteLine($" Congratulations! You guessed the word: {word} ");
}
else
{
    DrawHangman(attemptsLeft);
    Console.WriteLine($" Game over! The word was: {word} ");
}


static void DrawHangman(int attemptsLeft)
{
    string[] hangman = new string[7];
    hangman[0] = "  ____";
    hangman[1] = " |    |";
    hangman[2] = attemptsLeft <= 6 ? " O    |" : "      |";
    hangman[3] = attemptsLeft <= 4 ? (attemptsLeft <= 2 ? "/|\\   |" : "/|    |") : " |    |";
    hangman[4] = attemptsLeft <= 1 ? "/ \\   |" : attemptsLeft <= 3 ? "/     |" : "      |";
    hangman[5] = "      |";
    hangman[6] = "______|";

    foreach (var line in hangman)
    Console.WriteLine(line);
}