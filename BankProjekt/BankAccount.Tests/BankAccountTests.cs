using System;
using Xunit;
using BankApp;

namespace BankAccount.Tests
{
    /// <summary>
    /// Unit-Tests für die Klasse BankAccount.
    /// Aufbau nach dem AAA-Pattern (Arrange, Act, Assert).
    /// Namenskonvention: Methode_Szenario_ErwartetesErgebnis
    /// </summary>
    public class BankAccountTests
    {
        // =====================================================================
        // 1) KONSTRUKTOR
        // =====================================================================

        [Fact]
        public void Konstruktor_GueltigeWerte_SetztInhaberUndKontostand()
        {
            // Arrange & Act
            var konto = new BankApp.BankAccount("Anna", 100);

            // Assert
            Assert.Equal("Anna", konto.Inhaber);
            Assert.Equal(100, konto.Kontostand);
        }

        [Fact]
        public void Konstruktor_OhneStartguthaben_KontostandIstNull()
        {
            var konto = new BankApp.BankAccount("Beat");
            Assert.Equal(0, konto.Kontostand);
        }

        [Fact]
        public void Konstruktor_LeererInhaber_WirftArgumentException()
        {
            // Ein leerer Inhaber ist ungültig -> Exception erwartet
            Assert.Throws<ArgumentException>(() => new BankApp.BankAccount("", 100));
        }

        [Fact]
        public void Konstruktor_NegativesStartguthaben_WirftArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new BankApp.BankAccount("Anna", -50));
        }

        // =====================================================================
        // 2) EINZAHLEN
        // =====================================================================

        [Fact]
        public void Einzahlen_PositiverBetrag_ErhoehtKontostand()
        {
            // Arrange
            var konto = new BankApp.BankAccount("Test", 100);
            // Act
            konto.Einzahlen(50);
            // Assert
            Assert.Equal(150, konto.Kontostand);
        }

        [Theory]
        [InlineData(0, 100, 100)]
        [InlineData(100, 0.5, 100.5)]
        [InlineData(200, 300, 500)]
        public void Einzahlen_VerschiedeneBetraege_KorrekterKontostand(
            decimal start, decimal einzahlung, decimal erwartet)
        {
            var konto = new BankApp.BankAccount("Test", start);
            konto.Einzahlen(einzahlung);
            Assert.Equal(erwartet, konto.Kontostand);
        }

        [Fact]
        public void Einzahlen_NegativerBetrag_WirftArgumentException()
        {
            var konto = new BankApp.BankAccount("Test", 100);
            Assert.Throws<ArgumentException>(() => konto.Einzahlen(-10));
        }

        [Fact]
        public void Einzahlen_Null_WirftArgumentException()
        {
            var konto = new BankApp.BankAccount("Test", 100);
            Assert.Throws<ArgumentException>(() => konto.Einzahlen(0));
        }
        // =====================================================================
        // 3) ABHEBEN
        // =====================================================================

        [Fact]
        public void Abheben_GueltigerBetrag_ReduziertKontostand()
        {
            var konto = new BankApp.BankAccount("Test", 100);
            konto.Abheben(30);
            Assert.Equal(70, konto.Kontostand);
        }

        [Theory]
        [InlineData(100, 30, 70)]
        [InlineData(50, 50, 0)]      // Grenzwert: genau der gesamte Kontostand
        [InlineData(200, 1, 199)]
        public void Abheben_GueltigerBetrag_KorrekterRest(
            decimal start, decimal betrag, decimal erwartet)
        {
            var konto = new BankApp.BankAccount("Test", start);
            konto.Abheben(betrag);
            Assert.Equal(erwartet, konto.Kontostand);
        }

        [Fact]
        public void Abheben_MehrAlsGuthaben_WirftInvalidOperationException()
        {
            var konto = new BankApp.BankAccount("Test", 100);
            Assert.Throws<InvalidOperationException>(() => konto.Abheben(200));
        }

        [Fact]
        public void Abheben_NegativerBetrag_WirftArgumentException()
        {
            var konto = new BankApp.BankAccount("Test", 100);
            Assert.Throws<ArgumentException>(() => konto.Abheben(-10));
        }

        [Fact]
        public void Abheben_FehlgeschlagenLaesstKontostandUnveraendert()
        {
            // Nach einem fehlgeschlagenen Abheben muss der Kontostand gleich bleiben
            var konto = new BankApp.BankAccount("Test", 100);
            try { konto.Abheben(500); } catch (InvalidOperationException) { }
            Assert.Equal(100, konto.Kontostand);
        }

        // =====================================================================
        // 4) ÜBERWEISEN (Zustandstests)
        // =====================================================================

        [Fact]
        public void Ueberweisen_GueltigerBetrag_BeideKontenKorrekt()
        {
            // Arrange
            var quelle = new BankApp.BankAccount("Anna", 100);
            var ziel   = new BankApp.BankAccount("Beat", 50);

            // Act
            quelle.Überweisen(ziel, 30);

            // Assert
            Assert.Equal(70, quelle.Kontostand);   // Quelle reduziert
            Assert.Equal(80, ziel.Kontostand);     // Ziel erhöht
        }

        [Fact]
        public void Ueberweisen_UngenuegendGuthaben_KeineVeraenderung()
        {
            // Atomarität: schlägt die Überweisung fehl, darf sich NICHTS ändern
            var quelle = new BankApp.BankAccount("Anna", 20);
            var ziel   = new BankApp.BankAccount("Beat", 50);

            try { quelle.Überweisen(ziel, 100); }
            catch (InvalidOperationException) { }

            Assert.Equal(20, quelle.Kontostand);   // unverändert
            Assert.Equal(50, ziel.Kontostand);     // unverändert
        }

        [Fact]
        public void Ueberweisen_AufNullKonto_WirftArgumentNullException()
        {
            var quelle = new BankApp.BankAccount("Anna", 100);
            Assert.Throws<ArgumentNullException>(() => quelle.Überweisen(null!, 10));
        }

        [Fact]
        public void Ueberweisen_AufDasselbeKonto_WirftInvalidOperationException()
        {
            var konto = new BankApp.BankAccount("Anna", 100);
            Assert.Throws<InvalidOperationException>(() => konto.Überweisen(konto, 10));
        }
    }
}
