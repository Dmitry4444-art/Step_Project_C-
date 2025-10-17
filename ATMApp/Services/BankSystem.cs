using ATMApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Services;

// BankSystem კლასი მართავს მთლიან ბანკომატის სისტემას
public class BankSystem
{

    private readonly string usersFile = "users.txt"; // მომხმარებლების მონაცემების ფაილი
    private readonly List<User> users = new List<User>(); // მომხმარებლების სია
    private User? currentUser = null; // მიმდინარე შემოსული მომხმარებელი

    // სისტემის მთავრი მეთოდი, რომელიც მართავს აპლიკაციას
    public void Run()
    {
        LoadUsers(); // მონაცემების ჩატვირთვა ფაილიდან

        while (true)
        {
            Console.Clear();
            Console.WriteLine("  === Bankomatis Sistema ===  ");
            Console.WriteLine(" 1. Registration ");
            Console.WriteLine(" 2. Shesvla ");
            Console.WriteLine(" 3. Leave ");
            Console.Write(" Airchiet operacia: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1": Register(); break;
                case "2": Login(); break;
                case "3": return; // პროგრამიდან გამოსვლა
                default: Console.WriteLine("  Araswori archevania!  "); break;
            }

            Console.WriteLine("  Gagrdzelebistvis daachiret Enters...  ");
            Console.ReadLine();
        }
    }

    // -------------------- Registration --------------------
    private void Register()
    {
        Console.Clear();
        Console.WriteLine("  === Registration ===  ");
        Console.Write(" Momxmareblis saxeli: ");
        string username = Console.ReadLine() ?? "";

        // თუ მომხმარებლის სახელი უკვე არსებობს
        if (users.Any(u => u.Username == username))
        {
            Console.WriteLine(" Momxmareblis aseti saaxeli dakavebulia! ");
            return;
        }

        Console.Write(" Password: ");
        string password = Console.ReadLine() ?? "";

        Console.Write(" Roli (client/admin): ");
        string role = Console.ReadLine()?.ToLower() ?? " client ";
        if (role != " admin ") role = " client "; // ნებისმიერი არასწორი როლი ხდება client

        users.Add(new User(username, password, role, 0, 0, false));
        SaveUsers();

        Console.WriteLine(" Registracia warmatebulad shesrulda! ");
    }

    // -------------------- Login --------------------
    private void Login()
    {
        Console.Clear();
        Console.WriteLine("  === Shesvla ===  ");
        Console.Write(" Momxmareblis saxeli: ");
        string username = Console.ReadLine() ?? "";
        Console.Write(" Password: ");
        string password = Console.ReadLine() ?? "";

        currentUser = users.FirstOrDefault(u => u.Username == username && u.Password == password);

        if (currentUser == null)
        {
            Console.WriteLine(" Araswori parolia an momxmareblis saxeli! ");
            return;
        }

        // როლების მიხედვით გადამისამართება შესაბამის მენიუში
        if (currentUser.Role == " admin ")
            AdminMenu();
        else
            ClientMenu();
    }

    // -------------------- Client Menu --------------------
    private void ClientMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($" Mogesalmebit, {currentUser!.Username}! ");
            Console.WriteLine($" Balance: {currentUser.Balance} Lari ");
            Console.WriteLine(" 1. Balansis chveneba ");
            Console.WriteLine(" 2. Tanxis shetana ");
            Console.WriteLine(" 3. Tanxis gatana ");
            Console.WriteLine(" 4. Sesxis motxovna ");
            Console.WriteLine(" 5. Leave ");
            Console.Write(" Airchhiet operacia: ");

            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1": Console.WriteLine($" Tqveni balansi: {currentUser.Balance} Lari "); break;
                case "2": Deposit(); break;
                case "3": Withdraw(); break;
                case "4": RequestLoan(); break;
                case "5":
                    SaveUsers();
                    currentUser = null;
                    return;
                default: Console.WriteLine(" Araswori archevania! "); break;
            }

            Console.WriteLine(" Daachiret Enters gasagrdzeleblad... ");
            Console.ReadLine();
        }
    }

    // -------------------- Admin Menu --------------------
    private void AdminMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($" [ADMIN] {currentUser!.Username} ");
            Console.WriteLine(" 1. Sesxis motxovnebis naxva ");
            Console.WriteLine(" 2. Leave ");
            Console.Write(" Choose operation: ");

            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1": ApproveLoans(); break;
                case "2": currentUser = null; return;
                default: Console.WriteLine(" Araswori archevania! "); break;
            }

            Console.WriteLine(" Daachiret Enters gasagrdzeleblad... ");
            Console.ReadLine();
        }
    }

    // -------------------- Deposit --------------------
    private void Deposit()
    {
        Console.Write(" Sheiyvanet shesatani tanxa: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
        {
            currentUser!.Balance += amount;
            SaveUsers();
            Console.WriteLine($" Shetana warmatebulad moxerxda! New balance: {currentUser.Balance} Lari ");
        }
        else
        {
            Console.WriteLine(" Araswori tanxaa! ");
        }
    }

    // -------------------- Withdraw --------------------
    private void Withdraw()
    {
        Console.Write(" Sheiyvanet gasatani tanxa: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
        {
            if (currentUser!.Balance >= amount)
            {
                currentUser.Balance -= amount;
                SaveUsers();
                Console.WriteLine($" Operaciam warmatebulad chaiara! New balance: {currentUser.Balance} Lari ");
            }
            else
            {
                Console.WriteLine(" Balansi ar aris sakmarisi! ");
            }
        }
        else
        {
            Console.WriteLine(" Araswori tanxaa! ");
        }
    }

    // -------------------- Request Loan --------------------
    private void RequestLoan()
    {
        if (currentUser!.HasPendingLoan)
        {
            Console.WriteLine(" Tqven ukve gaqvt sesxis motxovna gagzavnili! ");
            return;
        }

        Console.Write(" Sheiyvanet sesxis tanxa: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal loanAmount) && loanAmount > 0)
        {
            currentUser.LoanAmount = loanAmount;
            currentUser.HasPendingLoan = true;
            SaveUsers();
            Console.WriteLine(" Sesxis motxovna gagzavnilia! ");
        }
        else
        {
            Console.WriteLine(" Araswori tanxaa! ");
        }
    }

    // -------------------- Approve Loans (Admin) --------------------
    private void ApproveLoans()
    {
        var pendingLoans = users.Where(u => u.HasPendingLoan).ToList();

        if (!pendingLoans.Any())
        {
            Console.WriteLine("Mimdinare sesxis motxovnebi ar arsebobs.");
            return;
        }

        foreach (var user in pendingLoans)
        {
            Console.WriteLine($"User: {user.Username}");
            Console.WriteLine($"Motxovnili tanxa: {user.LoanAmount} Lari");
            Console.Write("Dadastureba? (y/n): ");
            string answer = Console.ReadLine() ?? "";

            if (answer.ToLower() == "y")
            {
                user.Balance += user.LoanAmount;
                user.LoanAmount = 0;
                user.HasPendingLoan = false;
                Console.WriteLine(" Sesxi dadasturdaa! ");
            }
            else
            {
                user.LoanAmount = 0;
                user.HasPendingLoan = false;
                Console.WriteLine(" Sesxi uaryofilia. ");
            }
        }

        SaveUsers();
    }

    // -------------------- Load Users --------------------
    private void LoadUsers()
    {
        if (!File.Exists(usersFile)) return;

        foreach (var line in File.ReadAllLines(usersFile))
        {
            try
            {
                users.Add(User.FromFileString(line));
            }
            catch
            {
                // თუ ფაილში არის არასწორი მონაცემი, უბრალოდ გამოტოვება
            }
        }
    }

    // -------------------- Save Users --------------------
    private void SaveUsers()
    {
        File.WriteAllLines(usersFile, users.Select(u => u.ToFileString()));
    }
}