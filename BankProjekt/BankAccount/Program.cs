using System;

namespace BankApp
{
    /// <summary>
    /// Kleine Demonstration der BankAccount-Klasse (nur zum Ausprobieren).
    /// Dies ist NICHT der Unit-Test, sondern ein manuelles Beispiel.
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            var konto1 = new BankAccount("Anna", 100);
            var konto2 = new BankAccount("Beat", 50);

            Console.WriteLine(konto1);   // Konto von Anna: 100.00 CHF
            Console.WriteLine(konto2);   // Konto von Beat: 50.00 CHF

            konto1.Einzahlen(50);
            Console.WriteLine($"Nach Einzahlung: {konto1.Kontostand} CHF");   // 150

            konto1.Abheben(30);
            Console.WriteLine($"Nach Abhebung:   {konto1.Kontostand} CHF");   // 120

            konto1.Überweisen(konto2, 20);
            Console.WriteLine($"Nach Überweisung Anna: {konto1.Kontostand} CHF");  // 100
            Console.WriteLine($"Nach Überweisung Beat: {konto2.Kontostand} CHF");  // 70

            // Beispiel für einen Fehlerfall:
            try
            {
                konto2.Abheben(1000);   // mehr als Guthaben
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Fehler abgefangen: {ex.Message}");
            }
        }
    }
}
