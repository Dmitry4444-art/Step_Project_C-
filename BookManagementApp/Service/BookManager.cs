using BookManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementApp.Service;
public class BookManager
{
    // აქ ვინახავთ ყველა დამატებულ წიგნს მეხსიერებაში (List)
    private List<Book> books = new List<Book>();

    // ახალი წიგნის დამატება
    public void AddBook(Book book)
    {
        books.Add(book); // ვამატებთ სიაში
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" Wigni warmatebit daemata! ");
        Console.ResetColor();
    }

    // ყველა წიგნის ჩვენება
    public void ShowAllBooks()
    {
        if (books.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" Wignebis sia carielia. ");
            Console.ResetColor();
            return;
        }

        Console.WriteLine("  === Yvela Wigni ===  ");
        foreach (var book in books)
        {
            Console.WriteLine(book); // გამოვიძახებთ ToString() მეთოდს
        }
    }

    //  ძებნა სათაურით
    public void SearchByTitle(string title)
    {
        // ვპოულობთ ყველა წიგნს, რომლის სათაურიც ემთხვევა მითითებულს
        var foundBooks = books
            .Where(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (foundBooks.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Aseti sataurit wigni ver moidzebna. ");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("  Napovni wign(ebi): ");
            Console.ResetColor();
            foreach (var book in foundBooks)
            {
                Console.WriteLine(book);
            }
        }
    }

    // წაშლა სათაურის მიხედვით (case-insensitive) — ამოწმებს და შლის ყველა შესაბამის ჩანაწერს
    // ბრუნავს წაშლილი ელემენტების რაოდენობას
    public int RemoveByTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return 0;

        // ვპოულობთ ყველა მატჩს case-insensitive-ით
        var toRemove = books
            .Where(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            .ToList();

        // თუ არაფერია, ვბრუნებთ 0-ს
        if (toRemove.Count == 0)
            return 0;

        // წაშლა სიიდან
        foreach (var book in toRemove)
        {
            books.Remove(book);
        }

        return toRemove.Count;
    }
}
