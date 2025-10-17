using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Model;

// User კლასი წარმოადგენს მომხმარებელს ბანკის სისტემაში
public class User
{
    // მომხმარებლის სახელი (საერთო წაკითხვა)
    public string Username { get; }

    // მომხმარებლის პაროლი (საერთო წაკითხვა)
    public string Password { get; }

    // როლი სისტემაში: "client" ან "admin"
    public string Role { get; }

    // მომხმარებლის საბალანსო თანხა
    public decimal Balance { get; set; }

    // მომხმარებლის მოთხოვნილი სესხის თანხა
    public decimal LoanAmount { get; set; }

    // ქეში აღნიშნავს, აქვს თუ არა მომხმარებელს დამუშავებული სესხი
    public bool HasPendingLoan { get; set; }

    // კონსტრუქტორი ახალი მომხმარებლის შექმნისთვის
    public User(string username, string password, string role, decimal balance, decimal loanAmount, bool hasPendingLoan)
    {
        Username = username;
        Password = password;
        Role = role;
        Balance = balance;
        LoanAmount = loanAmount;
        HasPendingLoan = hasPendingLoan;
    }

    // მეთოდი, რომელიც აბრუნებს მომხმარებლის მონაცემებს ფაილში ჩაწერის სტრუქტურით
    public string ToFileString()
    {
        return $"{Username}|{Password}|{Role}|{Balance}|{LoanAmount}|{HasPendingLoan}";
    }

    // სტატიკური მეთოდი, რომელიც ფაილიდან აღადგენს მომხმარებელს
    public static User FromFileString(string line)
    {
        var parts = line.Split('|');
        if (parts.Length < 6)
            throw new FormatException(" Araswori monacemebis formatia! ");

        return new User(
            parts[0],
            parts[1],
            parts[2],
            decimal.Parse(parts[3]),
            decimal.Parse(parts[4]),
            bool.Parse(parts[5])
        );
    }
}