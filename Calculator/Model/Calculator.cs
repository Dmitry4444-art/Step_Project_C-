using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Model;

public class Calculator
{
    // Add: დააბრუნებს ორ რიცხვის summს
    public double Add(double a, double b)
    {
        return a + b;
    }


    // Subtract: დააბრუნებს a - b შედეგს
    public double Subtract(double a, double b)
    {
        return a - b;
    }


    // Multiply: დააბრუნებს a * b მიმრავლებს
    public double Multiply(double a, double b)
    {
        return a * b;
    }


    // Divide: უზრუნველყოფს გაყოფას, მაგრამ გადააგდებს გამონაკლისს, თუ b == 0 (ნულზე გაყოფა არ არსებობს რეალურებში)
    public double Divide(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException("Nulze gayofa sheudzlebelia!");
        //throw ქმნის სპეციალურ შეცდომას და აჩერებს მეთოდის შესრულებას
        //DevideByZeroException არის სპეციალური ტიპის შეცდომა, რომელიც მიუთითებს ნულზე გაყოფის მცდელობაზე, ჩაშენებულია .NET-ში
        return a / b;
    }


    // Power: a-ის b-ით ხარისხი — იყენებს Math.Pow-ს
    public double Power(double a, double b)
    {
        return Math.Pow(a, b);
    }


    // SquareRoot: აბრუნებს a-ს კვადრატულ ფესვს; თუ a < 0, გამოაგდებს არგუმენტის გამონაკლისს, რადგან რეალურებში ფესვი ნეგატიურიდან არ მივიღებთ
    public double SquareRoot(double a)
    {
        if (a < 0)
            throw new ArgumentException("Uaryofiti ricxvis fesvi ar arsebobs realur ricxvebshi!");
        return Math.Sqrt(a);
    }


    // Percentage: იცის, როგორ გამოითვალოს დააბრუნოს 'percent'% 'total'-იდან
    // მაგალითად: Percentage(200, 10) = 20 (10% of 200)
    public double Percentage(double total, double percent)
    {
        return (total * percent) / 100;
    }
}
