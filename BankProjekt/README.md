# BankProjekt – BankAccount mit Unit Testing

Dieses Projekt enthält die **BankAccount-Klasse** (das zu testende Programm) und ein
**vollständiges Testprojekt** mit xUnit.

## Ordnerstruktur

```
BankProjekt/
├── BankAccount/                 <- Das zu testende Programm
│   ├── BankAccount.cs           <- Klasse mit Einzahlen/Abheben/Überweisen
│   ├── Program.cs               <- Kleine Demo (manuelles Ausprobieren)
│   └── BankAccount.csproj
└── BankAccount.Tests/           <- Das Testprojekt (xUnit)
    ├── BankAccountTests.cs      <- 17 Testmethoden (15 [Fact] + 2 [Theory])
    └── BankAccount.Tests.csproj
```

## Voraussetzungen

- .NET SDK (z.B. .NET 8): https://dotnet.microsoft.com/download
- Visual Studio, JetBrains Rider oder VS Code

## Testprojekt aufsetzen (so wurde es erstellt – für den Bericht)

```bash
dotnet new console -n BankAccount
dotnet new xunit -n BankAccount.Tests
cd BankAccount.Tests
dotnet add reference ../BankAccount/BankAccount.csproj
```

## Programm ausführen (Demo)

```bash
cd BankAccount
dotnet run
```

## Tests ausführen

```bash
cd BankAccount.Tests
dotnet test
```

Erwartete Ausgabe (sinngemäss):

```
Passed!  - Failed: 0, Passed: 21, Skipped: 0, Total: 21
```

(17 Testmethoden; die beiden [Theory]-Tests zählen wegen ihrer 6 [InlineData]-Zeilen
als mehrere Testfälle, daher 21 Tests insgesamt.)

## Überblick über die Tests

| Bereich       | Tests |
|---------------|-------|
| Konstruktor   | 4     |
| Einzahlen     | 4     |
| Abheben       | 5     |
| Überweisen    | 4     |

Getestete Techniken:
- Normalfälle (Assert.Equal)
- Exception-Tests (Assert.Throws)
- Grenzwerte (z.B. genau den ganzen Kontostand abheben)
- Parametrische Tests ([Theory] / [InlineData])
- Zustandstests (nach Überweisung beide Konten prüfen; Atomarität)

---

## Türkçe özet

- `BankAccount.cs` → test edilen program (banka hesabı). Hazır.
- `BankAccountTests.cs` → **17 test metodu** yazıldı, hepsi hazır.
- Çalıştırmak için: `dotnet test` → yaklaşık "Passed: 21" görmelisin.
- Bu çıktının ekran görüntüsünü raporun **Kontrollieren** bölümüne koy.
- Test projesinin nasıl kurulduğu (yukarıdaki komutlar) → raporun **Realisieren** bölümüne yaz.
