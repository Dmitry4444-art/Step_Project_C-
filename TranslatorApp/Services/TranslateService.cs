using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator_App.Services;

public class TranslateService
{
    protected List<Models.Translator> translates;
    private readonly string url;
    public TranslateService(string url)
    {
        this.url = url;
        translates = new List<Models.Translator>();
    }

    public Models.Translator GetTranslate(string language = "Ge-En", string text = "გამარჯობა")
    {
        return translates.FirstOrDefault(i => i.Language == language && i.Source == text);
    }

    public List<Models.Translator> GetTranslates()
    {
        return translates;
    }

    public void AddTranslate(Models.Translator req)
    {
        var exist = translates.FirstOrDefault(i => i.Language == req.Language && i.Source == req.Source && i.Destination == req.Destination);
        if (exist != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Msgavsi chanaweri ukve arsebobs ");
            Console.ResetColor();
        }
        else
        {
            translates.Add(req);
        }
    }
}
//translates — შინაგანი სია, სადაც შენახულია თარგმანების ჩამონათვალი.

//url — კონსტრუკტორში მიიღება, მაგრამ ამ კოდში არ გამოიყენება(შეინახება მხოლოდ). სავარაუდოდ შენ დაგეგმე ფაილში/სერვისში შენახვა(მაგ. "service/translators.json").

//კონსტრუქტორი(TranslateService(string url)) — ინიციალიზებს translates-ს და ინახავს url-ს.

//GetTranslate(language, text) — ეძებს პირველ ობიექტს translates-ში რომელიც აკმაყოფილებს ენისა და წყაროს ტექსტის პირობას; თუ ვერ პოულობს — აბრუნებს null.

//აქ მთელი ინპუტი ზუსტი შედარებით ვრცელდება(case-sensitive და სრული მატჩი).

//GetTranslates() — აბრუნებს სრულ სიას(პირიქით, სასურველია ცალკე კოპია ან IReadOnlyCollection).

//AddTranslate(req) — როცა მომხმარებელი დამატებით ამატებს თარგმანს:

//პირველად საძიებო: თუ ესეთი ჩანაწერი უკვე არსებობს(მისი Language, Source და Destination იდენტურია), მაშინ ბეჭდავს წითლად მესიჯს: "msgavsi chanaweri ukve arsebobs" (ანუ — მსგავსი ჩანაწერი უკვე არის).

//თუ არ არის — აბრუნებს სიაში ახალ ჩანაწერს.