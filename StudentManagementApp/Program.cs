using StudentManagementApp.Services;


Console.ForegroundColor = ConsoleColor.Cyan;
// 1) ვქმნით StudentService ობიექტს — კონსტრუქტორი ავტომატურად წაიკითხავს students.json-ს
var service = new StudentService();

// 2) თუ დოკუმენტში ცარიელია, ვავსებთ ნიმუშით (არ არის აუცილებელი, მაგრამ გამოსადეგია ჯერ გასასწორებლად)
    service.SeedDataIfEmpty();

// 3) უსასრულო ციკლი — ჩვენთან არის მთავარი მენიუ
while (true)
{
    // 3.1) ჩვენება მენიუს კონსოლზე
    ShowMenu();

    // 3.2) კითხვაო — რა ექნება მომხმარებელს არჩევანი.
    Console.Write(" Airchiet (1-8): ");
    string choice = Console.ReadLine()?.Trim();

    // 3.3) Switch-case თითოეული ფუნქციისთვის
    switch (choice)
    {
        case "1":
            // ახალი სტუდენტის დამატება
            AddStudentFlow(service);
            break;

        case "2":
            // ყველა სტუდენტის ნახვა (სტანდარტული სტატიკური თანმიმდევრობით: RollNumber)
            ViewAllStudentsFlow(service);
            break;

        case "3":
            // ძებნა სიის ნომრით
            SearchStudentFlow(service);
            break;

        case "4":
            // შეფასების განახლება
            UpdateGradeFlow(service);
            break;

        case "5":
            // სტუდენტის წაშლა
            DeleteStudentFlow(service);
            break;

        case "6":
            // დალაგება შეფასების მიხედვით (A -> F)
            ViewSortedByGradeFlow(service);
            break;

        case "7":
            // დამატებით — ყველაფერი გასაწმენდად (DEBUG only) — არააუცილებელი, მაგრამ დამატებულია
            // ამ ვარიანტს არ ვქმნით ავტომატურად, მაგრამ შეგვიძლია ჩავრთოთ (აქ დატოვება — სურვილისამებრ).
            Console.WriteLine(" Miutitet (sauketeso praqtika: Araa rekomendirebuli gasagrdzeleblad). ");
            break;

        case "8":
            // გამოსვლა პროგრამიდან
            Console.WriteLine(" Programa dasrulda. Baayy! ");
            return;

        default:
            Console.WriteLine(" Sheiyvanet tqvni archevani (1–8). ");
            break;
    }

    Console.WriteLine(); // ხაზის განბნევა შემდეგი ოპერაციისთვის
}

// ShowMenu — ბეჭდავს ყველა ხელმისაწვდომ მენიუს ფუნქციას.
static void ShowMenu()
{
    Console.WriteLine("  === Studentebis martvis sistema ===  ");
    Console.WriteLine(" 1. Axali studentis damateba ");
    Console.WriteLine(" 2. Yvela studentis naxva ");
    Console.WriteLine(" 3. Studentis dzebna siis nomrit ");
    Console.WriteLine(" 4. Moswavlis shefaasebis ganaxleba ");
    Console.WriteLine(" 5. Studentis washla ");
    Console.WriteLine(" 6. Studentebis dalageba shefasebis mixedvit (A -> F) ");
    Console.WriteLine(" 7. (Dev-opcia) — Damatebiti operacia / reset (Arasavaldebuloa)");
    Console.WriteLine(" 8. Leave ");
}

// AddStudentFlow — მომთხოვნი პროცედურა ახალი სტუდენტის შესაყვანად
static void AddStudentFlow(StudentService service)
{
    try
    {
        // 1) შეკითხვები მომხმარებლისგან
        Console.Write(" Sheiyvanet saxeli: ");
        string name = Console.ReadLine();

        Console.Write(" Sheiyvanet siis nomeri (positive integer): ");
        if (!int.TryParse(Console.ReadLine(), out int rollNumber))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Shecdoma: Araswori ricxvis formati. ");
            Console.ResetColor();

            return;
        }

        Console.Write(" Sheiyvanet shefaseba (A/B/C/D/F): ");
        string gradeInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(gradeInput) || gradeInput.Length < 1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Shecdoma: Araswori shefasebis formati. ");
            Console.ResetColor();
            return;
        }
        char grade = char.ToUpper(gradeInput[0]);

        // 2) ვიძახებთ სერვისის ფუნქციას — თუ ჩააგდებს ვალიდაციის ექსეფშენს, გადავჭერთ catch-ში.
        service.AddStudent(name, rollNumber, grade);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" Studenti warmatebit damatebulia.");
        Console.ResetColor();
    }
    catch (Exception ex)
    {
        // ვაჩვენებთ მომხმარებელს, თუ რამე ვალიდაციამ ან სხვა შეცდომამ გამოიწვია.
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($" Shecdoma: {ex.Message}");
        Console.ResetColor();
    }
}

// ViewAllStudentsFlow — აკეთებს GetAllStudents-ს და ბეჭდავს შედეგს კონსოლზე.

static void ViewAllStudentsFlow(StudentService service)
{
    var all = service.GetAllStudents();
    if (!all.Any())
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(" Sia carielia. ");
        Console.ResetColor();
        return;
    }

    Console.WriteLine("  === Yvela studenti (RollNumber-is mixedvit) ===  ");
    foreach (var s in all)
        Console.WriteLine(s.GetInfo());
}

// SearchStudentFlow — ეძებს ინდივიდუალურ სტუდენტს როლით და ბეჭდავს დეტალებს.
static void SearchStudentFlow(StudentService service)
{
    Console.Write(" Sheiyvanet siis nomeri sadzebnelad: ");
    if (!int.TryParse(Console.ReadLine(), out int roll))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" Araswori ricxvis formatia. ");
        Console.ResetColor();
        return;
    }

    var student = service.FindByRollNumber(roll);
    if (student == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($" Studenti siis nomrit {roll} ar moidzebna.");
        Console.ResetColor();
    }
    else
    {
        Console.WriteLine(student.GetInfo());
    }
        
}

// UpdateGradeFlow — ეძლევა შესაძლებლობა განაახლოს სტუდენტის შეფასება.
static void UpdateGradeFlow(StudentService service)
{
    Console.Write(" Sheiyvanet siis nomeri romlis shefasebasac ganaaxlebt: ");
    if (!int.TryParse(Console.ReadLine(), out int roll))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" Araswori ricxvia. ");
        Console.ResetColor();
        return;
    }

    Console.Write(" Sheiyvanet axali shefaseba (A/B/C/D/F): ");
    string gradeInput = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(gradeInput) || gradeInput.Length < 1)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" Araswori shefasebis formati. ");
        Console.ResetColor();
        return;
    }
    char newGrade = char.ToUpper(gradeInput[0]);

    try
    {
        service.UpdateGrade(roll, newGrade);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" Shefaseba ganaxlda warmatebit. ");
        Console.ResetColor();
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($" Shecdoma: {ex.Message} ");
        Console.ResetColor();
    }
}

// DeleteStudentFlow — წაშლის სტუდენტს სიის ნომრით, წინასწარ ჰკითხავს მომხმარებელს დადასტურებას.
static void DeleteStudentFlow(StudentService service)
{
    Console.Write(" Sheiyvanert siis nomeri wasashlelad: ");
    if (!int.TryParse(Console.ReadLine(), out int roll))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" Araswori ricxvis formatia. ");
        Console.ResetColor();
        return;
    }

    var student = service.FindByRollNumber(roll);
    if (student == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($" Studenti siis nomrit {roll} ar moidzebna.");
        Console.ResetColor();
        return;
    }

    // დადასტურება
    Console.Write($" Darwmunebuli xart rom gindat washla: {student.GetInfo()}? (y/n): ");
    string confirm = Console.ReadLine()?.Trim().ToLower();
    if (confirm == "y" || confirm == "yes")
    {
        try
        {
            service.DeleteStudent(roll);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Studenti warmatebit waishala. ");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" Shecdoma: {ex.Message}");
            Console.ResetColor();
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" Operacia gauqmda momxmareblis mier. ");
        Console.ResetColor();
    }
}

// ViewSortedByGradeFlow — აჩვენებს სტუდენტებს შეფასების მიხედვით (A->F), შემდეგ RollNumber-ის მიხედვით.
static void ViewSortedByGradeFlow(StudentService service)
{
    var sorted = service.GetStudentsSortedByGrade();
    if (!sorted.Any())
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(" Sia carielia. ");
        Console.ResetColor();
        return;
    }

    Console.WriteLine("  === Studentebis sia shefasebis mixedvit (A->F) ===  ");
    foreach (var s in sorted)
        Console.WriteLine(s.GetInfo());
} 