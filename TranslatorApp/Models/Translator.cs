using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator_App.Models;

public class Translator
{
    public string Source { get; set; }        // შესული ტექსტი (მაგ. "გამარჯობა")
    public string Destination { get; set; }   // თარგმნილი ტექსტი (მაგ. "Hello")
    public string Language { get; set; }      // ენის წყვილი (მაგ. "Ge-En" ან "En-Ge")

    public override string ToString()
    {
        return $"Source:{Source};Destination:{Destination};Language: {Language}";
    }
}

