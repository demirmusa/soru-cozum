using System;

namespace Soru1.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("string1:");
            var string1 = Console.ReadLine();
            Console.Write("string2:");
            var string2 = Console.ReadLine();
            
            var combinasyonelStringContains = new CombinasyonelStringContains();
            
            Console.WriteLine(combinasyonelStringContains.KontrolEt(string1, string2));
        }
    }
}