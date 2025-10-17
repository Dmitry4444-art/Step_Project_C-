using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementApp.Models;

public class Book
{
    // წიგნის სათაური (მხოლოდ წაკითხვადი)
    public string Title { get; private set; }

    // წიგნის ავტორი (მხოლოდ წაკითხვადი)
    public string Author { get; private set; }

    // გამოცემის წელი (მხოლოდ წაკითხვადი)
    public int Year { get; private set; }

    // კონსტრუქტორი – იძახება ახალი წიგნის შექმნისას
    public Book(string title, string author, int year)
    {
        Title = title;   // ვანიჭებთ სათაურს
        Author = author; // ვანიჭებთ ავტორს
        Year = year;     // ვანიჭებთ გამოცემის წელს
    }

    // ეს მეთოდი განსაზღვრავს როგორ გამოისახოს წიგნი Console-ზე
    public override string ToString()
    {
        return $"  Title: {Title} | Author: {Author} | Gamocemis weli: {Year}";
    }
}