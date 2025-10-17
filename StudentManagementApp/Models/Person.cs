using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementApp.Models;

// Person — ზოგადი "მშობელი" კლასი, რომელიც ინახავს ადამიანის ძირითად თვისებებს.
// Student კლასი გადის მემკვიდრეობით (inherits) ამ კლასიდან.
public class Person
{
    // private backing field — ინკაფსულაციის მაგალითი (სახელი პირდაპირ არ არის públicas).
    private string _name;

    // Name property — გარეთა გამოყენებისთვის; set ბლოკში არის შეყვანის ვალიდაცია.
    public string Name
    {
        get => _name; // აბრუნებს backing field-ს
        set
        {
            // ვალიდაცია: სახელი არ უნდა იყოს null, ცარიელი ან მხოლოდ ცარიელი სიმბოლოები.
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(" Saxelis veli ar sheidzleba iyos carieli. "); // უკუაგდება ცუდი ინპუტი
            _name = value.Trim(); // შენახვის წინTrim() — გვერდს აცილებს ზედმეტ სფეისებს
        }
    }

    // ცარიელი კონსტრუქტორი — საჭიროა JSON დესერიალიზაციისთვის და ზოგ სიტუაციებში reflection-ისას.
    public Person() { }

    // მთავარი (პარამეტრიანი) კონსტრუქტორი — ხელმისაწვდომია ობიექტის ხელოვნურად შექმნისას.
    public Person(string name)
    {
        Name = name; // იყენებს property-ს რათა შესრულდეს ვალიდაცია
    }

    // Virtual მეთოდი — შეიძლება გადაფარდეს შვილ კლასებში (polymorphism).
    public virtual string GetInfo()
    {
        // უბრუნებს ჩვენს პირად ინფორმაციას სტრინგის ფორმატში.
        return $"Name: {Name}";
    }
}
