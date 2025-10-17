using Translator_App.Models;
using Translator_App.Services;


Console.ForegroundColor = ConsoleColor.Blue;

TranslateService trServices = new TranslateService("service/translators.json");

while (true)
{
    Console.WriteLine("Airchie ena. En-Ka ; Ka-En ; En-Ru; Ru-En; Ka-Ru; Ru-Ka");

    var res = Console.ReadLine();

    Console.WriteLine("SHEMOIYVANE SITYVA");
    var sityva = Console.ReadLine();

    var translated = trServices.GetTranslate(res, sityva);
    if (translated != null)
    {
        Console.WriteLine(translated);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("chanaweri ar arsebobs mitxari ra chavwero mnishvnelobashi, tu ar ginda press left");
        Console.ResetColor();
        var userSugegst = Console.ReadLine();
        if (userSugegst != null)
        {
            trServices.AddTranslate(new Translator
            {
                Destination = userSugegst,
                Language = res,
                Source = sityva,
            });
        }

    }
}


//Console.ForegroundColor = ConsoleColor.Blue;
//— ყვითელი/ნეიტრალური ფერის ნაცვლად კონსოლის ტექსტი ხდება ლურჯი (მსგავსი გამორჩეული სტარტისათვის).

//TranslateService trServices = new TranslateService("service/translators.json");
//— ინიციალიზდება თარგმნების სერვისი. შენ გადასცემ url-ს, რომელიც ახლა არ გამოიყენება, მაგრამ სავარაუდოდ მოსახერხებელია ფაილური persistence-ისთვის.

//while (true) { ... } — უასრულბითი ციკლი. პროგრამა ამ შიდა ლოჯიკას არ ტოვებს; საჭიროების შემთხვევაში უნდა დაემატოს გამოსასვლელი პირობა (მაგ.: if (res=="exit") break;).

//Console.WriteLine("Airchie ena. En-Ka ; Ka-En ; En-Ru; Ru-En; Ka-Ru; Ru-Ka");
//— სთხოვს მომხმარებელს აირჩიოს ენის წყვილი (მაგ. En-Ge) — თუმცა ტექსტი "Airchie ena" (აირჩიე ენა).

//var res = Console.ReadLine();
//— მიიღება მომხმარებლის მიერ აკრეფილი ენის წყვილი.

//Console.WriteLine("SHEMOIYVANE SITYVA"); და var sityva = Console.ReadLine();
//— სთხოვს მომხმარებელს შეიყვანოს ტექსტი რომელიც უნდა ითარგმნოს და ის კითხულობს.

//var translated = trServices.GetTranslate(res, sityva);
//— მოძებნს შიდა ბაზიდან (სია translates) შესაბამის თარგმანს language == res და source == sityva პირობით.

//if (translated != null) { Console.WriteLine(translated); }
//— თუ თარგმანი არსებობს — ბეჭდავს ობიექტს (ToString()-ის გამო კონსოლზე გამოჩნდება Source:...;Destination:...;Language:...).

//else { ... } — თარგმანი არ არის ბაზაში:

//წითლად წერთ შეტყობინებას რომ ჩანაწერი არ არსებობს და სთხოვთ მომხმარებელს ჩაწეროს საკუთარი წინადადება (ან თარგმანი), მერე ამ მონაცემით "userSugegst" გადაიხურება.

//თუ მომხმარებელი რადად აქვს რაღაც ტექსტი (userSugegst != null) — თარგმანი იქმნება new Translator { Destination = userSugegst, Language = res, Source = sityva } და მიემატება TranslateService-ს AddTranslate-ით.