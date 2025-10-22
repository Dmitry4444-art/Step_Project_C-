

Dictionary<string, List<string>> categories = new Dictionary<string, List<string>>()
{
    { "Animals", new List<string> { "elephant", "giraffe", "kangaroo", "dolphin", "crocodile" } }, // კატეგორია "Animals" და მისში არსებულია ცხოველების სია
    { "Fruits", new List<string> { "pineapple", "strawberry", "blueberry", "watermelon", "pomegranate" } }, // კატეგორია "Fruits" და ხილების ნუსხა
    { "Countries", new List<string> { "argentina", "finland", "mozambique", "singapore", "uzbekistan" } }, // კატეგორია "Countries" და ქვეყნების ნუსხა
    { "Colors", new List<string> { "turquoise", "magenta", "crimson", "lavender", "chartreuse" } }, // კატეგორია "Colors" და ფერთა ნუსხა
    { "Sports", new List<string> { "basketball", "cricket", "badminton", "volleyball", "snowboarding" } } // კატეგორია "Sports" და სპორტების ნუსხა
}; // Dictionary-ს დახურვა — გვაქვს კატეგორიების და თითოეულის სიტყვების ასოცირებული სია

Random random = new Random(); // შემთხვევითი რიცხვის გენერატორის შექმნა თამაშისთვის (სიტყვის შემთხვევით არჩევისთვის)
string chosenCategory = categories.Keys.ToList()[random.Next(categories.Count)]; // შემთხვევით მივიღოთ ერთ-ერთი კატეგორიის კი (გავაკეთეთ Keys -> List და ავარჩიეთ index)
string word = categories[chosenCategory][random.Next(categories[chosenCategory].Count)].ToLower(); // შემთხვევითი სიტყვა უკვე არჩეული კატეგორიიდან და გადავიყვანოთ პატარა ასოებში (_word_ ტექსტისთვის)

char[] guessedWord = new string('_', word.Length).ToCharArray(); // ვქმნით სიმბოლოების მასივს '_' სიმბოლოებით სიტყვის სიგრძის მიხედვით (ფარული სიტყვა)
List<char> guessedLetters = new List<char>(); // ჩამონათვალი იმ ასოების შესახებ, რომლებიც სხვამ დაამხე/გამოიცნო
int attemptsLeft = 7; // მცდელობების საწყისი რაოდენობა (თამაშის სასტანდარტო 'ხაზოვანი' სიცოცხლე)

Console.WriteLine(" Welcome to Hangman! "); // მისალოცი ტექსტი კონსოლში
Console.WriteLine($" Category: {chosenCategory} "); // ნათვევთ მომხმარებელს რომელია არჩეული კატეგორია

while (attemptsLeft > 0 && new string(guessedWord) != word) // მთავარ ციკლში — ვიმუშავებთ სანამ მცდელობები დაგვრჩებათ და სიტყვა არ არის სრულად გამომხსნილი
{
    DrawHangman(attemptsLeft); // ვსურათობთ ფიგურას კონსოლში მიმდინარე მცდელობების რაოდენობის მიხედვით
    Console.WriteLine(" Word: " + string.Join(" ", guessedWord)); // ვაჩვენებთ ვირუსულ (დაფარული) სიტყვას, ოვერებში დაშორებით
    Console.WriteLine($" Attempts left: {attemptsLeft} "); // ვაჩვენებთ დარჩენილ მცდელობათა რაოდენობას
    Console.WriteLine($" Guessed letters: {string.Join(", ", guessedLetters)} "); // ვაჩვენებთ უკვე გამოცნობილ/შემოყვანილ ასოებს
    Console.Write(" Enter a letter: "); // შეტყობინება მომხმარებელს ასოს შეყვანისთვის
    string input = Console.ReadLine().ToLower().Trim(); // ვკითხულობთ ხაზს, გადავიყვანოთ პატარა ასოებში და ამოვშალოთ სიცარიელეები ორივე მხრიდან

    if (string.IsNullOrWhiteSpace(input) || input.Length != 1 || !char.IsLetter(input[0])) // ვამოწმებთ არ არის თუ შეყვანა ცარიელი, თუ არ არის ერთი სიმბოლო ან სიმბოლო არ არის ასო
    {
        Console.WriteLine(" Invalid input! Please enter a single letter. "); // ვატყობინებთ მომხმარებელს არასწორ შეყვანას
        continue; // გადავდივართ ციკლის შემდეგი შემობრუნებაზე (კითხვის ნორმალიზაციის ლუპი)
    }

    char guess = input[0]; // თუ შემოწმება წარმატებულია, ვიღებთ პირველ სიმბოლოს როგორც ხაზი გათვალისწინებული ასო

    if (guessedLetters.Contains(guess)) // თუ ეს ასო უკვე არის გამოცნობილი სიაში
    {
        Console.WriteLine(" You already guessed that letter! "); // ვაწვდინებთ შეტყობინებას და
        continue; // გადავდივართ შემდეგ საკიტხზე (არ ვყრით დამატებით შედეგს)
    }

    guessedLetters.Add(guess); // ვუმატებთ ახალ გამოცნობილ ასოს ჩამონათვალს

    if (word.Contains(guess)) // თუ სიტყვა შეიცავს ამ ასოს
    {
        for (int i = 0; i < word.Length; i++) // ვაწყობთ ციკლს თითოეული პოზიციისთვის სიტყვაში
            if (word[i] == guess) guessedWord[i] = guess; // თუ ამ პოზიციაზე სტანდარტულად არის ჩვენი მომავალი ასო — ვამჟღავნებთ guessedWord-ში
        Console.WriteLine(" Good guess! "); // ვაცნობთ მომხმარებელს სწორ პასუხს
    }
    else
    {
        attemptsLeft--; // შეცდომის დროს ვაკლებთ ერთი მცდელობა
        Console.WriteLine(" Wrong guess! "); // ვარჩევთ შეტყობინებას — არასწორი ვარაუდი
    }
}

if (new string(guessedWord) == word) // თუ საბოლოოდ guessedWord-ს კონვერტაცია შესაბამს ორიგინალურ სიტყვას
{
    Console.WriteLine($" Congratulations! You guessed the word: {word} "); // მომხმარებელს ვულოცავთ გამარჯვებას და ვაჩვენებთ სიტყვას
}
else
{
    DrawHangman(attemptsLeft); // ვხატავთ საბოლოო მდგომარეობას (ფიგურა შეუსრულებელია)
    Console.WriteLine($" Game over! The word was: {word} "); // ვაუწყებთ წაგებას და ვაჩვენებთ რა იყო სიტყვა
}


static void DrawHangman(int attemptsLeft) // დამხმარე მეთოდი ჰანგმენის ვიზუალის გამოსახატავად მიმდინარე მცდელობების მიხედვით
{
    string[] hangman = new string[7]; // ვქმნით მასივს რომელიც ინახავს ხაზ-ხაზად კონსოლის გამოსახულებას
    hangman[0] = "  ____"; // დახაზულობის პირველი ხაზი — თხელი პლატფორმა
    hangman[1] = " |    |"; // ზედა კუთხე და საყრდენი
    hangman[2] = attemptsLeft <= 6 ? " O    |" : "      |"; // თუ დარჩენილი მცდელობები <=6 — ვსაჩვენებთ თავს (ან სხვაგვარად ცარიელ ხაზს)
    hangman[3] = attemptsLeft <= 4 ? (attemptsLeft <= 2 ? "/|\\   |" : "/|    |") : " |    |"; // სხეულის დასხივება: ხელები/ტანი — უფრო ნაკლები მცდელობით ვამატებთ ხელებს
    hangman[4] = attemptsLeft <= 1 ? "/ \\   |" : attemptsLeft <= 3 ? "/     |" : "      |"; // ფეხების დამატება მცდელობების პროგრესირებით
    hangman[5] = "      |"; // ქვედა ხაზის გაგრძელება
    hangman[6] = "______|"; // ფსკერი — ბოლოჯერ ხაზი

    foreach (var line in hangman) // ვბეჭდავთ თითოეულ ხაზს კონსოლზე
        Console.WriteLine(line);
} // DrawHangman-მის კავშირის დახურვა
