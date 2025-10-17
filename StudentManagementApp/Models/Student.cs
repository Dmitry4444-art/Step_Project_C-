using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementApp.Models;

// Student — Person-ის მემკვიდრე; გამაფრთხილებელი: იყენებს ინკაფსულაციას და property-ებს ვალიდაციით.
public class Student : Person
{
    // backing fields — ინკაფსულაციისთვის (პირდაპირ მოდიფიკაცია შეუძლებელია).
    private int _rollNumber;
    private char _grade;

    // RollNumber property — აჩვენებს და ამოწმებს სიის (როლის) ნომერს.
    public int RollNumber
    {
        get => _rollNumber;
        set
        {
            // ვალიდაცია: როლი უნდა იყოს დადებითი მთელი რიცხვი.
            if (value <= 0)
                throw new ArgumentException(" Siis nomeri unda iyos dadebiti mteli ricxvi. ");
            _rollNumber = value;
        }
    }

    // Grade property — ორმხრივი ვალიდაცია იმისათვის, რომ შეფასება იყოს A/B/C/D/F.
    public char Grade
    {
        get => _grade;
        set
        {
            // გადავიყვანოთ დიდი ასოში და შემოწმება დაშვებული მნიშვნელობების მიხედვით.
            char upper = char.ToUpper(value);
            var allowed = new[] { 'A', 'B', 'C', 'D', 'F' };
            if (!allowed.Contains(upper))
                throw new ArgumentException(" Shefaseba unda iyos ert-erti: A, B, C, D, F. ");
            _grade = upper;
        }
    }

    // ცარიელი კონსტრუქტორი — JSON დესერიალიზაციისა და reflection-სთვის.
    public Student() { }

    // მთავარი კონსტრუქტორი — შექმნის Student ობიექტს და გამართავს ვალიდაციებს.
    public Student(string name, int rollNumber, char grade) : base(name)
    {
        // მნიშვნელობები ჩვენი property-ების მეშვეობით გადასცემით ვალიდაცია შესრულდება.
        RollNumber = rollNumber;
        Grade = grade;
    }

    // override GetInfo() — ვერც 부모ის მეთოდს; ნათლად გვიჩვენებს Student-ის ყველა თვისებას.
    public override string GetInfo()
    {
        return $"Name: {Name}, Roll Number: {RollNumber}, Grade: {Grade}";
    }

    // override ToString() — ნიპედ გამოყენებისთვის, მაგალითად Console.WriteLine(student).
    public override string ToString()
    {
        return GetInfo();
    }
}
