# Tower of Hanoi in CS
 Gra "Wieża Hanoi" to klasyczna łamigłówka logiczna, która polega na przenoszeniu stosu różnej wielkości krążków z pierwszego słupka na ostatni.

## Zasady gry
```csharp
      Console.WriteLine("1. W każdym ruchu możesz przenieść tylko jeden dysk.");
      Console.WriteLine("Dysk ten musi być górny na jednym ze słupków.");
      Console.WriteLine("2. Dysk można położyć tylko na większym dysku lub na pustym słupku.");
      Console.WriteLine("Nigdy nie wolno kłaść większego dysku na mniejszym.");
      Console.WriteLine("3. Celem gry jest przeniesienie wszystkich dysków ze słupka startowego");
      Console.WriteLine("na słupek końcowy (ostatni).");
      Console.WriteLine("Każdy słupek może służyć jako tymczasowy magazyn dla dysków, przy czym");
      Console.WriteLine("zawsze należy przestrzegać wyżej wspomnianych zasad.");
      Console.WriteLine();
```

# The Code

## Definicje Początkowe
```csharp
     static Stack<int>[] towers = new Stack<int>[3];
     static int moves = 0;
     static int numberOfDisks = 3;
     static int selectedTower = 0;
```
***towers:*** Tablica trzech stosów reprezentująca wieże, na których umieszczane są dyski.\
***moves:*** Licznik wykonanych ruchów.\
***numberOfDisks:*** Początkowa liczba dysków w grze.\
***selectedTower:*** Indeks aktualnie wybranej wieży.\


## Metoda Main
```csharp
static void Main()
{
    for (int i = 0; i < towers.Length; i++)
    {
        towers[i] = new Stack<int>();
    }

    bool isRunning = true;

    while (isRunning)
    {
        Console.Clear();
        Console.WriteLine("Wieże Hanoi");
        Console.WriteLine("1. Graj");
        Console.WriteLine("2. Zasady");
        Console.WriteLine("3. Wyjdź");
        Console.WriteLine();

        string wyborEkranuString = Console.ReadLine();
        int wyborEkranuInt;

        if (int.TryParse(wyborEkranuString, out wyborEkranuInt))
        {
            switch (wyborEkranuInt)
            {
                case 1:
                    ChooseNumberOfDisks();
                    ResetGame();
                    Game();
                    break;
                case 2: 
                    Console.Clear();
                    Console.WriteLine("Zasady gry:");
                    Console.WriteLine();
                    Rules();
                    break;
                case 3: 
                    Console.Clear();
                    Console.WriteLine("Zamykanie programu");
                    isRunning = false;
                    Thread.Sleep(1000);
                    break;
                default: 
                    Console.WriteLine("Podano nieprawidłową opcję menu.");
                    break;
            }

            if (isRunning)
            {
                Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey();
            }
        }
    }
}
```
***nicjalizacja wież:*** Na początku metody inicjalizowane są trzy wieże jako stosy (Stack<int>) przechowujące dyski. Każdy stos reprezentuje jedną wieżę w grze Wieże Hanoi.\

***Pętla główna:*** Metoda wchodzi w pętlę while, która trwa dopóki zmienna isRunning jest prawdziwa. Pętla ta odpowiada za wyświetlanie menu głównego i obsługę akcji użytkownika.\

***Wyświetlenie menu:*** W każdym obrocie pętli menu gry jest czyszczone i wyświetlane na nowo, oferując użytkownikowi trzy opcje: Graj, Zasady, Wyjdź.\

***Odczyt i obsługa wyboru:*** Program odczytuje wybór użytkownika jako ciąg znaków, który następnie próbuje przekonwertować na liczbę całkowitą. W zależności od wyniku konwersji, program wykonuje jedną z akcji: rozpoczęcie gry, wyświetlenie zasad, lub zakończenie działania programu.\

***Zarządzanie akcjami:*** W przypadku wyboru rozpoczęcia gry, użytkownik jest proszony o wybór liczby dysków, po czym gra jest resetowana i rozpoczyna się główna pętla gry. Wybór zasad powoduje wyświetlenie instrukcji gry. Opcja wyjścia kończy działanie programu.\

***Komunikat o kontynuacji:*** Po każdej akcji, o ile gra nie została zakończona, użytkownik jest informowany\



