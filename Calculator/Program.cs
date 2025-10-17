using CalculatorApp.Model;
using System;


// კონსოლის სათაური და ფერის ინიციალიზაცია — ვიზუალური გამოკვეთა მომხმარებლისთვის
Console.Title = " === Konsoluri kalkulatori === ";
Console.ForegroundColor = ConsoleColor.Cyan;


// Calculator კლასის ინსტანსი — შეიცავს ყველა მათემატიკურ ოპერაციას
Calculator calc = new Calculator();


// მთავარ ლუპში გავმეორებთ ოპერაციებს მანამ, სანამ მომხმარებელი მოითხოვს გათიშვას
bool continueProgram = true;


while (continueProgram)
{
    // ეკრანის გასუფთავება ყოველი ახალ ჩარიცხვის დაწყებისას
    Console.Clear();
    Console.WriteLine(" === Konsoluiri kalkulatori === ");


    // ვკითხულობთ მომხმარებლისგან ორ რიცხვს. GetNumber უზრუნველყოფს ვალიდაციას
    double num1 = GetNumber(" SheiyvaneT pirveli ricxvi: ");
    double num2 = GetNumber(" Sheiyvanet meore ricxvi: ");


    // ოპერაციების მენიუ — მომხმარებელს მიუთითეთ რა არჩევანის გაკეთება შეუძლია
    Console.WriteLine(" Airchiet operacia: ");
    Console.WriteLine("1. Damateba (+) ");
    Console.WriteLine("2. Gamokleba (-) ");
    Console.WriteLine("3. Gamravleba (*) ");
    Console.WriteLine("4. Gayofa (/) ");
    Console.WriteLine("5. Xarisxshi ayvana (^) ");
    Console.WriteLine("6. Fesvi (√ Pirvel ricxvze) ");
    Console.WriteLine("7. Procenti (% Meore ricxvi rogorc procenti) ");
    Console.Write(" Airchiet operaciis nomeri: ");


    // ვაქვთ მომხმარებლის არჩევანი როგორც string, რადგან შეიძლება არასწორი შეყვანა
    string choice = Console.ReadLine();
    double result = 0;


    try
    {
        // შესაბამისი ოპერაციის შესრულება წართმეული Calculator კლასის მეთოდების გამოყენებით
        switch (choice)
        {
            case "1":
                // დამატება
                result = calc.Add(num1, num2);
                Console.WriteLine($"Shedegi: {num1} + {num2} = {result}");
                break;
            case "2":
                // გამოკლების ოპერაცია
                result = calc.Subtract(num1, num2);
                Console.WriteLine($"Shedegi: {num1} - {num2} = {result}");
                break;
            case "3":
                // გასამრავლებელი
                result = calc.Multiply(num1, num2);
                Console.WriteLine($"Shedegi: {num1} * {num2} = {result}");
                break;
            case "4":
                // გაყოფა — Divide მეთოდი შესაძლო გამონაკლისს აგზავნის (ნულზე გაყოფის შემთხვევაში)
                result = calc.Divide(num1, num2);
                Console.WriteLine($"Shedegi: {num1} / {num2} = {result}");
                break;
            case "5":
                // ხარისხის გაზრდა (ათეული ძალა)
                result = calc.Power(num1, num2);
                Console.WriteLine($"Shedegi: {num1} ^ {num2} = {result}");
                break;
            case "6":
                // კვადრატული ფესვი — აქ მხოლოდ პირველი რიცხვი გამოადგებათ; ნეგატიური არგუმენტი გამოესახება როგორც ошибка
                result = calc.SquareRoot(num1);
                Console.WriteLine($"Shedegi: √{num1} = {result}");
                break;
            case "7":
                // პროცენტი — რიცხვი 2 აღიქმება როგორც პროცენტის მნიშვნელობა (მაგ: 20% of 50)
                result = calc.Percentage(num1, num2);
                Console.WriteLine($"Shedegi: {num2}% of {num1} = {result}");
                break;
            default:
                // თუ მომხმარებლებმა შეყვანა არასწორია — ვიდენტიფიცირებთ როგორც შეცდომა
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Shecdoma: Araswori operaciis nomeri!");
                Console.ResetColor();
                break;
        }
    }
    catch (Exception ex)
    {
        // ყველა გამონაკლისი აქ დაიჭირება და მომხმარებელს მეგობრულად შეატყობინებს შეცდომის ტექსტს
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($" Shecdoma: {ex.Message} ");
        Console.ResetColor();
    }


    // კითხვა: გაგრძელდეს თუ არა პროგრამა — მომხმარებელი აკონტროლებს ლუპის გაგრძელებას
    Console.Write(" Gsurt gagrdzeleba? (y/n): ");
    string answer = Console.ReadLine().ToLower();
    continueProgram = (answer == "y");
}


// პროგრამის დასრულებისმდე მადლობის შეტყობინება
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine(" Madloba gamoyenebistvis! Warmatebebi :) ");
Console.ResetColor();




// ფუნქცია GetNumber — რიცხვის წამოღება და ვალიდაცია
static double GetNumber(string message)
{
    double number;
    while (true)
    {
        Console.Write(message);
        string input = Console.ReadLine();


        // double.TryParse უზრუნველყოფს, რომ არ მოხდეს გამონაკლისი არასაკანონო ფორმატით შესვლის დროს
        if (double.TryParse(input, out number))
            return number; // თუ დამტკიცებულია, ვაბრუნებთ რიცხს


        // შეცდომის შეტყობინება — რიკვესტი ვალის შესახებ
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" Shecdoma: gtxovt sheiyvanot swori ricxvi! ");
        Console.ResetColor();
    }
}