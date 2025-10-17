

using BookManagementApp.Models;  // Book კლასის მისაწვდომად
using BookManagementApp.Service;  // BookManager კლასის მისაწვდომად


Console.ForegroundColor = ConsoleColor.Yellow;

BookManager manager = new BookManager();  // ვქმნით წიგნების მენეჯერს
bool exit = false;  // პროგრამის დასრულების კონტროლი

Console.OutputEncoding = System.Text.Encoding.UTF8;  // ქართული ტექსტის მხარდაჭერა
Console.WriteLine("  ===   Wignebis martvis sistema   ===  ");


// მთავარ მენიუში ვტრიალებთ სანამ exit არ გახდება true
while (!exit)
{
    Console.WriteLine(" Choose operation: ");
    Console.WriteLine(" 1. Axali wignis damateba ");
    Console.WriteLine(" 2. Yvela wignis naxva ");
    Console.WriteLine(" 3. Wignis dzebna sataurit ");
    Console.WriteLine(" 4. Wignis washla (Delete by title)");
    Console.WriteLine(" 5. Leave ");
    Console.Write("   Choose (1-5): ");
    string choice = Console.ReadLine();


    // switch-ის საშუალებით ვამუშავებთ მომხმარებლის არჩევანს
    switch (choice)
    {
        case "1":
            AddNewBook(manager);  // ახალი წიგნის დამატება
            break;
        case "2":
            manager.ShowAllBooks();  // ყველა წიგნის ჩვენება
            break;
        case "3":
            SearchBook(manager);  // ძებნა სათაურით
            break;
        case "4":
            DeleteBookByTitle(manager); // new case
            break;
        case "5":
            exit = true;  // პროგრამის დახურვა
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" Naxvamdis! ");
            Console.ResetColor();
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Araaswori archevania, Scadet  tavidan ");
            Console.ResetColor();
            break;
    }
}


//  ფუნქცია ახალი წიგნის დასამატებლად
static void AddNewBook(BookManager manager)
{
    Console.Write(" Sheiyvanet wignis satauri: ");
    string title = Console.ReadLine()?.Trim();

    Console.Write("Sheiyvanet avtori: ");
    string author = Console.ReadLine()?.Trim();

    Console.Write("Sheiyvanet gamocemis weli: ");
    string yearInput = Console.ReadLine();


    // მონაცემების ვალიდაცია
    if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(" Yvela veli savaldebuloa! ");
        Console.ResetColor();
        return;
    }

    if (!int.TryParse(yearInput, out int year) || year < 0)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(" Miutitet swori gamocemis weli! ");
        Console.ResetColor();
        return;
    }

    // ახალი წიგნის ობიექტის შექმნა
    Book newBook = new Book(title, author, year);

    // წიგნის დამატება სიაში
    manager.AddBook(newBook);
}

// ფუნქცია წიგნის სათაურით მოსაძებნად
static void SearchBook(BookManager manager)
{
    Console.Write(" Sheiyvanet wignis satauri sadzebnelad: ");
    string title = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(title))
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(" Satauri savaldebuloa dziebistvis! ");
        Console.ResetColor();
        return;
    }

    manager.SearchByTitle(title);
}

//  წიგნის წაშლის ინტერფეისი
static void DeleteBookByTitle(BookManager manager)
{
    Console.Write(" Sheiyvanet wignis satauri romelic gindes washlac: ");
    string title = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(title))
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(" Satauri savaldebuloa dziebistvis! ");
        return;
    }

    int removed = manager.RemoveByTitle(title);

    if (removed == 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" Aseti sataurit wigni ver moidzebna, ar sheidzleba washlac. ");
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($" Washlilia {removed} wign(ebi) sataurit \"{title}\".");
        Console.ResetColor();
    }
}