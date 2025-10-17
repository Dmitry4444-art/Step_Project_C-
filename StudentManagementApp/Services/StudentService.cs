using StudentManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentManagementApp.Services;

// StudentService — პასუხისმგებელია ყველა ბიზნეს-ლოგიკაზე:
// - მონაცემთა შენახვა/ჩატვირთვა JSON-ში
// - CRUD ოპერაციები (Create, Read, Update, Delete)
// - ჯგუფური ოპერაციები (განსაზღვრული სორტირება)
public class StudentService
{
    // JSON ფაილის სახელწოდება (იგივე დირექტორიაში შეიქმნება თუ არ არსებობს)
    private readonly string _filePath = "students.json";

    // სტუდენტების ლოკალური სია (დაპორტირებული მეხსიერებაში)
    private List<Student> students = new List<Student>();

    // კონსტრუქტორი — განაატვირთებს მონაცემებს JSON ფაილიდან ავტომატურად.
    public StudentService()
    {
        LoadData();
    }

    // LoadData — კითხულობს ფაილს და დესერიალიზაციას უკეთებს.
    private void LoadData()
    {
        try
        {
            // თუ ფაილი არსებობს, ვკითხულობთ და ვცდილობთ დესერიალიზაციას
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);

                // თუ ფაილი არ ცარიელია, ვცდი მივხაროთ სტატიკური ობიექტები
                if (!string.IsNullOrWhiteSpace(json))
                    students = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
            }
            else
            {
                // თუ ფაილი არ არსებობს — შევქმნათ ცარიელი JSON მასივი, რათა პირდაპირ იყოს იდეალური სტარტი
                File.WriteAllText(_filePath, "[]");
                students = new List<Student>();
            }
        }
        catch (Exception ex)
        {
            // თუ რაიმე გადის გართულებით — ვაჩვენებთ მესიჯს და ვაგრძელებთ ცარიელი სიით
            Console.WriteLine($"⚠ მონაცემების ჩატვირთვის შეცდომა: {ex.Message}");
            students = new List<Student>();
        }
    }

    // SaveData — სერილიზაციას უკეთებს და წერსJson ფაილში (WriteIndented — უფრო წაკითხად)
    private void SaveData()
    {
        try
        {
            string json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            // შეცდომის შემთხვევაში ვბეჭდავთ შეტყობინებას (პროდში აქ შეიძლება exception-ი throw-დადო)
            Console.WriteLine($"⚠ მონაცემების შენახვის შეცდომა: {ex.Message}");
        }
    }

    // SeedData — იშვიათად გამოიყენება (პირველ გაშვებაზე) ტესტური მონაცემების ჩასართავად.
    public void SeedDataIfEmpty()
    {
        // თუ სიაში არ არის სტუდენტები, დავამატოთ რამდენიმე ნიმუში
        if (!students.Any())
        {
            students.Add(new Student("Nino Beridze", 1, 'A'));
            students.Add(new Student("Giorgi K.", 2, 'B'));
            students.Add(new Student("Ana T.", 3, 'C'));
            SaveData(); // აუცილებელია ჩაწერილათ JSON-შიაც
        }
    }

    // AddStudent — დამატების ფუნქცია; გადაამოწმებს უნიკალურობას და ცვლის სიას
    public void AddStudent(string name, int rollNumber, char grade)
    {
        // უნიკალურობის შემოწმება — თუ დუბლიკატი არსებობს, ვაბრუნებთ შეცდომას.
        if (students.Any(s => s.RollNumber == rollNumber))
            throw new ArgumentException($" Studenti siis nomrit {rollNumber} Ukve arsebobs. ");

        // ახალ ობიექტს ვქმნით და ვამატებთ სიის ბოლოს.
        var student = new Student(name, rollNumber, grade);
        students.Add(student);

        // აუცილებლად შენახვა — რომ ცვლილებები არ დაიკარგოს პროგრამის დახურვისას.
        SaveData();
    }

    // ShowAllStudents — აბრუნებს ყველა სტუდენტს (კონსოლზე ჩვენებისთვის ან UI-სთვის)
    public IEnumerable<Student> GetAllStudents()
    {
        // დავაბრუნოთ დამოწყებული სია RollNumber-ის მიხედვით (sorting).
        return students.OrderBy(s => s.RollNumber).ToList();
    }

    // FindByRollNumber — ჰუმანური ძებნა სიის ნომრით (single result)
    public Student FindByRollNumber(int rollNumber)
    {
        return students.FirstOrDefault(s => s.RollNumber == rollNumber);
    }

    // UpdateGrade — განაახლებს კონკრეტული სტუდენტის შეფასებას და შეინახავს.
    public void UpdateGrade(int rollNumber, char newGrade)
    {
        var student = FindByRollNumber(rollNumber);
        if (student == null)
            throw new ArgumentException($" Studenti siis nomrit {rollNumber} ar moidzebna. ");

        student.Grade = newGrade; // property იწვევს ვალიდაციას
        SaveData(); // მოვიცავთ ცვლილებებს ფაილში
    }

    // DeleteStudent — წაშლის სტუდენტს სიის ნომრის მიხედვით (თუ არ არსებობს — exception)
    public void DeleteStudent(int rollNumber)
    {
        var student = FindByRollNumber(rollNumber);
        if (student == null)
            throw new ArgumentException($" Studenti siis nomrit {rollNumber} ar moidzebna.");

        students.Remove(student); // წაშლა list-იდან
        SaveData(); // და შენახვა
    }

    // SortByGrade — აბრუნებს სიას მოწესრიგებულს შეფასების მიხედვით (A საუკეთესო -> F ყველაზე დაბალი)
    // შენიშნვა: Grade არის char (A,B,C,D,F), შესაბამისად არაერთგვაროვანი მდგომარეობის შემთხვევაში
    // შეიძლება დამატებითი კრიტერიუმი, როგორიცაა RollNumber-ს მიხედვით მეორეხარისხოვანი სორტი
    public IEnumerable<Student> GetStudentsSortedByGrade()
    {
        // ქულის ლოგიკა: სანამ char-ით შესაძლებელია პირდაპირ კავშირში ჩადება,
        // აქ ვქმნით "კარტურას" რათა დავუსახოთ A=1, B=2 ... F=5 და შევჯგუფოთ ისე.
        var rank = new Dictionary<char, int> { { 'A', 1 }, { 'B', 2 }, { 'C', 3 }, { 'D', 4 }, { 'F', 5 } };

        // ჩამოვწეროთ და დავუბრუნოთ sorted შედეგი
        return students
            .OrderBy(s => rank.ContainsKey(s.Grade) ? rank[s.Grade] : int.MaxValue) // ჯერ Grade მიხედვით
            .ThenBy(s => s.RollNumber) // შემდეგ RollNumber-ით (სტაბილური secondary sort)
            .ToList();
    }
}