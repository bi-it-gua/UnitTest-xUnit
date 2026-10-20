using System;

namespace BankApp
{
    /// <summary>
    /// Bildet ein einzelnes Bankkonto ab. Stellt Methoden zum Einzahlen,
    /// Abheben und Überweisen bereit und verwaltet den aktuellen Kontostand.
    /// </summary>
    public class BankAccount
    {
        /// <summary>Kontoinhaber (Name).</summary>
        public string Inhaber { get; }

        /// <summary>Aktueller Kontostand.</summary>
        public decimal Kontostand { get; private set; }

        /// <summary>
        /// Erstellt ein neues Konto.
        /// </summary>
        /// <param name="inhaber">Name des Kontoinhabers.</param>
        /// <param name="startguthaben">Anfangsguthaben (darf nicht negativ sein).</param>
        public BankAccount(string inhaber, decimal startguthaben = 0)
        {
            if (string.IsNullOrWhiteSpace(inhaber))
                throw new ArgumentException("Inhaber darf nicht leer sein.", nameof(inhaber));
            if (startguthaben < 0)
                throw new ArgumentException("Startguthaben darf nicht negativ sein.", nameof(startguthaben));

            Inhaber = inhaber;
            Kontostand = startguthaben;
        }

        /// <summary>
        /// Zahlt einen Betrag auf das Konto ein.
        /// </summary>
        /// <param name="betrag">Positiver Betrag.</param>
        public void Einzahlen(decimal betrag)
        {
            if (betrag <= 0)
                throw new ArgumentException("Der Betrag muss positiv sein.", nameof(betrag));

            Kontostand += betrag;
        }

        /// <summary>
        /// Hebt einen Betrag vom Konto ab.
        /// </summary>
        /// <param name="betrag">Positiver Betrag, höchstens der aktuelle Kontostand.</param>
        public void Abheben(decimal betrag)
        {
            if (betrag <= 0)
                throw new ArgumentException("Der Betrag muss positiv sein.", nameof(betrag));
            if (betrag > Kontostand)
                throw new InvalidOperationException("Ungenügendes Guthaben.");

            Kontostand -= betrag;
        }

        /// <summary>
        /// Überweist einen Betrag von diesem Konto auf ein Zielkonto.
        /// Die Operation ist atomar: Schlägt das Abheben fehl, wird nichts verändert.
        /// </summary>
        /// <param name="zielKonto">Empfängerkonto.</param>
        /// <param name="betrag">Positiver Betrag.</param>
        public void Überweisen(BankAccount zielKonto, decimal betrag)
        {
            if (zielKonto == null)
                throw new ArgumentNullException(nameof(zielKonto), "Zielkonto darf nicht null sein.");
            if (zielKonto == this)
                throw new InvalidOperationException("Überweisung auf dasselbe Konto ist nicht erlaubt.");

            // Abheben prüft Betrag und Guthaben. Erst wenn es erfolgreich ist,
            // wird auf dem Zielkonto eingezahlt -> atomar.
            Abheben(betrag);
            zielKonto.Einzahlen(betrag);
        }

        public override string ToString()
        {
            return $"Konto von {Inhaber}: {Kontostand:0.00} CHF";
        }
    }
}
