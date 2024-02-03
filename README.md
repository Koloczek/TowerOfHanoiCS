# <img src="logo.png" alt="Balancer" height="128px">

# Tower of Hanoi in CS
 Gra "Wieża Hanoi" to klasyczna łamigłówka logiczna, która polega na przenoszeniu stosu różnej wielkości krążków z pierwszego słupka na ostatni.

## Zasady gry
```csharp
Console.WriteLine("Gra używa 3 klawiszy. Strzałek lewo / prawo do poruszania sie");
Console.WriteLine("oraz spacji do zatwierdzania");
Console.WriteLine();
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
$\color{lightblue}{\textsf{towers: }}$ Tablica trzech stosów reprezentująca wieże, na których umieszczane są dyski.\
$\color{lightblue}{\textsf{moves: }}$ Licznik wykonanych ruchów.\
$\color{lightblue}{\textsf{numberOfDisks: }}$ Początkowa liczba dysków w grze.\
$\color{lightblue}{\textsf{s.electTower: }}$ Indeks aktualnie wybranej wieży.


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
$\color{lightblue}{\textsf{Inicjalizacja wież: }}$ Na początku metody inicjalizowane są trzy wieże jako stosy (Stack<int>) przechowujące dyski. Każdy stos reprezentuje jedną wieżę w grze Wieże Hanoi.\
$\color{lightblue}{\textsf{Pętla główna: }}$ Metoda wchodzi w pętlę while, która trwa dopóki zmienna isRunning jest prawdziwa. Pętla ta odpowiada za wyświetlanie menu głównego i obsługę akcji użytkownika.\
$\color{lightblue}{\textsf{Wyśwetlanie menu: }}$ W każdym obrocie pętli menu gry jest czyszczone i wyświetlane na nowo, oferując użytkownikowi trzy opcje: Graj, Zasady, Wyjdź.\
$\color{lightblue}{\textsf{Odczyt i obsługa wyboru: }}$ Program odczytuje wybór użytkownika jako ciąg znaków, który następnie próbuje przekonwertować na liczbę całkowitą. W zależności od wyniku konwersji, program wykonuje jedną z akcji: rozpoczęcie gry, wyświetlenie zasad, lub zakończenie działania programu.\
$\color{lightblue}{\textsf{Zarządzanie akcjami: }}$ W przypadku wyboru rozpoczęcia gry, użytkownik jest proszony o wybór liczby dysków, po czym gra jest resetowana i rozpoczyna się główna pętla gry. Wybór zasad powoduje wyświetlenie instrukcji gry. Opcja wyjścia kończy działanie programu.\
$\color{lightblue}{\textsf{Komunikat o lontynuacji: }}$ Po każdej akcji, o ile gra nie została zakończona, użytkownik jest informowany.

## Metoda Rules
Wyświetla zasady gry w Wieże Hanoi.

```csharp
static void Rules()
            {
                Console.WriteLine("Gra używa 3 klawiszy. Strzałek lewo / prawo do poruszania sie");
                Console.WriteLine("oraz spacji do zatwierdzania");
                Console.WriteLine();
                Console.WriteLine("1. W każdym ruchu możesz przenieść tylko jeden dysk.");
                Console.WriteLine("Dysk ten musi być górny na jednym ze słupków.");
                Console.WriteLine("2. Dysk można położyć tylko na większym dysku lub na pustym słupku.");
                Console.WriteLine("Nigdy nie wolno kłaść większego dysku na mniejszym.");
                Console.WriteLine("3. Celem gry jest przeniesienie wszystkich dysków ze słupka startowego");
                Console.WriteLine("na słupek końcowy (ostatni).");
                Console.WriteLine("Każdy słupek może służyć jako tymczasowy magazyn dla dysków, przy czym");
                Console.WriteLine("zawsze należy przestrzegać wyżej wspomnianych zasad.");
                Console.WriteLine();
            }
```

## Metoda Game

**Metoda Game** to kluczowy element gry Wieże Hanoi w tej aplikacji. Odpowiada za zarządzanie procesem gry, w tym za wybór wieży, przenoszenie dysków oraz sprawdzanie warunków zwycięstwa. Poniżej znajduje się szczegółowy opis działania tej metody:

```csharp
static void Game()
            {
                ResetGame();

                int? sourceTower = null;

                while (!CheckWin())
                {
                    PrintTowers();
                    Console.WriteLine("Użyj strzałek lewo/prawo do wyboru wieży, spacja do wyboru/przeniesienia dysku.");

                    var key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.LeftArrow:
                            selectedTower = Math.Max(0, selectedTower - 1);
                            break;
                        case ConsoleKey.RightArrow:
                            selectedTower = Math.Min(towers.Length - 1, selectedTower + 1);
                            break;
                        case ConsoleKey.Spacebar:
                            if (sourceTower == null)
                            {
                                sourceTower = selectedTower;
                            }
                            else
                            {
                                if (!MoveDisk(sourceTower.Value, selectedTower))
                                {
                                    WriteLineInColor("Nie można przenieść większego dysku na mniejszy. Spróbuj ponownie.", ConsoleColor.Red);
                                    Thread.Sleep(2000);
                                }
                                else
                                {
                                    moves++;
                                    PrintTowers();
                                    if (CheckWin())
                                    {
                                        Console.WriteLine("Gratulacje! Wygrałeś w {0} ruchach!", moves);
                                        Thread.Sleep(1000);
                                        return;
                                    }
                                }
                                sourceTower = null;
                            }
                            break;

                    }
                }

                Console.WriteLine("Gratulacje! Wygrałeś w {0} ruchach!", moves);
            }
```
$\color{lightblue}{\textsf{Reset gry: }}$ Na początku metoda wywołuje funkcję ResetGame, aby przygotować wieże i licznik ruchów do nowej gry.\
$\color{lightblue}{\textsf{Wybór wieży: }}$ Użytkownik wybiera wieżę, korzystając z klawiszy strzałek. Zmienna selectedTower jest aktualizowana, aby wskazać aktualnie wybraną wieżę.\
$\color{lightblue}{\textsf{Przenoszenie dysków: }}$ Za pomocą klawisza spacji użytkownik może wybrać dysk do przeniesienia (ustawiając sourceTower) i przenieść go do innej wieży. Operacja przeniesienia jest obsługiwana przez metodę MoveDisk, która sprawdza, czy przeniesienie jest możliwe, zgodnie z zasadami gry.\
$\color{lightblue}{\textsf{Komunikat błedu: }}$ Jeśli próba przeniesienia dysku jest niepoprawna (np. przeniesienie większego dysku na mniejszy), wyświetlany jest komunikat o błędzie i użytkownik ma możliwość ponownego podjęcia akcji.\
$\color{lightblue}{\textsf{Sprawdzenie warunków zwycięstwa: }}$ Po każdym udanym przeniesieniu dysku, metoda CheckWin sprawdza, czy wszystkie dyski zostały przeniesione na ostatnią wieżę w odpowiedniej kolejności. Jeśli tak, gra kończy się zwycięstwem.\
$\color{lightblue}{\textsf{Zakończenie gry: }}$ Gdy użytkownik ułoży wszystkie dyski na ostatniej wieży, wyświetlany jest komunikat gratulacyjny z informacją o liczbie wykonanych ruchów, a gra się kończy, wracając do głównego menu.

## Metoda WriteLineInColor

```csharp
static void WriteLineInColor(string message, ConsoleColor color)
{
    Console.ForegroundColor = color; // Ustawienie koloru tekstu
    Console.WriteLine(message);      // Wyświetlenie komunikatu
    Console.ResetColor();            // Resetowanie koloru do domyślnego
}
```
Metoda ta pozwala na wyświetlenie komunikatów w konsoli w wybranym kolorze. Jest to szczególnie przydatne do zwrócenia uwagi użytkownika na ważne informacje.

## Metoda MoveDisk

```csharp
static bool MoveDisk(int from, int to)
            {
                if (towers[from].Count == 0 || (towers[to].Count > 0 && towers[from].Peek() > towers[to].Peek()))
                {
                    return false;
                }

                towers[to].Push(towers[from].Pop());
                return true;
            }
```
MoveDisk to metoda, która odpowiada za przenoszenie dysku z jednej wieży na inną. Sprawdza ona, czy ruch jest dozwolony zgodnie z zasadami gry (nie można położyć większego dysku na mniejszym) i wykonuje ruch, jeśli jest możliwy.

## Metoda PrintTowers

Metoda PrintTowers pełni rolę w wizualizacji stanu gry w Wieżach Hanoi. Jej zadaniem jest wyświetlenie trzech wież i umieszczonych na nich dysków.

```csharp
static void PrintTowers()
            {
                Console.Clear();
                Console.WriteLine("Ruchy: " + moves);

                int maxHeight = towers.Max(t => t.Count);
                int baseWidth = numberOfDisks * 2 + 1;
                int towerSpacing = 3;

                for (int level = maxHeight - 1; level >= 0; level--)
                {
                    for (int towerIndex = 0; towerIndex < towers.Length; towerIndex++)
                    {
                        if (towerIndex == selectedTower)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                        }

                        var towerList = towers[towerIndex].ToList();
                        int diskSize = maxHeight - level - 1 < towerList.Count ? towerList[maxHeight - level - 1] : 0;

                        PrintDisk(diskSize, baseWidth);

                        if (towerIndex == selectedTower)
                        {
                            Console.ResetColor();
                        }

                        if (towerIndex < towers.Length - 1)
                        {
                            Console.Write(new string(' ', towerSpacing));
                        }
                    }
                    Console.WriteLine();
                }
                string baseLine = new string('-', towers.Length * (baseWidth + towerSpacing) - towerSpacing);
                Console.WriteLine(baseLine);

                for (int towerIndex = 0; towerIndex < towers.Length; towerIndex++)
                {
                    if (towerIndex == selectedTower)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }

                    string label = "[T" + (towerIndex + 1) + "]";
                    int labelPadding = (baseWidth - label.Length) / 2;
                    Console.Write(new string(' ', labelPadding) + label + new string(' ', labelPadding));

                    if (towerIndex == selectedTower)
                    {
                        Console.ResetColor();
                    }

                    if (towerIndex < towers.Length - 1)
                    {
                        Console.Write(new string(' ', towerSpacing));
                    }
                }
                Console.WriteLine();
            }
```
$\color{lightblue}{\textsf{Czyszczenie ekranu i wyświetlanie ruchów: }}$ Na początku metoda czyści ekran, aby zapewnić aktualny widok wież i wyświetla liczbę ruchów wykonanych przez gracza.\
$\color{lightblue}{\textsf{Iteracja przez poziomy wież: }}$ Metoda iteruje od góry do dołu przez każdy poziom wież. Dla każdego poziomu iteruje przez wszystkie trzy wieże, wyświetlając dyski lub puste przestrzenie.\
$\color{lightblue}{\textsf{Zaznaczenie wybranej wieży: }}$ Jeśli wieża jest aktualnie wybrana przez gracza (wskazana przez selectedTower), jej tło zostaje zmienione na ciemnoszare, aby to zaznaczyć.\
$\color{lightblue}{\textsf{Wyświetlanie dysków: }}$ Dla każdej wieży na danym poziomie, metoda PrintDisk jest używana do wyświetlenia dysku (lub pustego miejsca, jeśli nie ma dysku na danym poziomie). Rozmiar dysku i jego pozycja są obliczane, aby zapewnić poprawną wizualizację.\
$\color{lightblue}{\textsf{Wyświetlanie linii bazowej i etykiet wież: }}$ Na dole wież metoda wyświetla linię bazową, aby oddzielić wieże od ich etykiet. Etykiety są wyświetlane pod każdą wieżą, wyśrodkowane względem szerokości wieży. Wybrane wieże są ponownie zaznaczone.

## Metoda PrintDisk

PrintDisk wspiera PrintTowers przez wyświetlenie indywidualnego dysku o określonym rozmiarze, centrując go względem szerokości podstawy wieży.

```csharp
static void PrintDisk(int diskSize, int baseWidth)
            {
                int padding = (baseWidth - diskSize * 2) / 2;
                string disk = new string('=', diskSize * 2);
                Console.Write(new string(' ', padding) + disk + new string(' ', padding));
            }
```

## Metoda CheckWin

```csharp
static bool CheckWin()
            {
                return towers[0].Count == 0 && towers[1].Count == 0 && towers[2].Count == numberOfDisks;
            }
```
**CheckWin** sprawdza, czy wszystkie dyski zostały przeniesione na ostatnią wieżę, co oznacza wygraną.

## Metoda ResetGame
przygotowuje grę do nowego rozpoczęcia, resetując wieże, dyski i licznik ruchów

```csharp
static void ResetGame()
            {
                towers = new Stack<int>[3];
                for (int i = 0; i < towers.Length; i++)
                {
                    towers[i] = new Stack<int>();
                }

                for (int i = numberOfDisks; i > 0; i--)
                {
                    towers[0].Push(i);
                }

                moves = 0;
            }
```
$\color{lightblue}{\textsf{Inicjalizacja wież: }}$ Tworzy trzy nowe, puste stosy reprezentujące wieże w grze. Ta czynność zapewnia, że wszystkie poprzednie dyski i stany wież zostaną usunięte, a wieże będą gotowe do nowej gry.\
$\color{lightblue}{\textsf{Wypełnianie pierwszej wieży: }}$ Wkłada dyski na pierwszą wieżę (wieża numer 0 w tablicy towers). Dyski są układane od największego (na dole) do najmniejszego (na górze), co odpowiada ich początkowemu ułożeniu w klasycznej grze w Wieże Hanoi. Liczba dysków (numberOfDisks) może być ustalona przez gracza przed rozpoczęciem gry, co pozwala na dostosowanie poziomu trudności.\
$\color{lightblue}{\textsf{Reset liczby ruchów: }}$ Zeruje licznik ruchów (moves), co jest istotne dla śledzenia postępów gracza w nowej grze.\
$\color{lightblue}{\textsf{Wybór początkowej wieży: }}$ Ustawia zmienną selectedTower na pierwszą wieżę, co jest punktem wyjścia dla interakcji gracza z grą.

## Metoda ChooseNumberOfDisks
Pozwala użytkownikowi na wybranie liczby dysków przed rozpoczęciem gry, zapewniając możliwość dostosowania poziomu trudności.

```csharp
static void ChooseNumberOfDisks()
            {
                while(true) { 
                    Console.WriteLine("Wybierz liczbę dysków (3-8):");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int disks) && disks >= 3 && disks <= 8)
                    {
                        numberOfDisks = disks;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowa wartość, domyślna wartość to: 3");
                    }
                }
            }
```
$\color{lightblue}{\textsf{Pętla: }}$ Metoda używa pętli while, która działa do momentu, aż użytkownik wprowadzi prawidłową liczbę dysków. Dzięki temu użytkownik ma nieograniczone próby na wprowadzenie poprawnej wartości.\
$\color{lightblue}{\textsf{Prośba o wejście: }}$ Użytkownik jest proszony o podanie liczby dysków, z którymi chce grać. Program określa minimalną i maksymalną dozwoloną liczbę dysków (w tym przypadku od 3 do 8), co jest standardowym zakresem zapewniającym rozsądną grę pod względem trudności i długości.\
$\color{lightblue}{\textsf{Walidacja wejścia: }}$ Używając int.TryParse, metoda próbuje przekonwertować wprowadzony ciąg znaków na liczbę całkowitą i jednocześnie sprawdza, czy liczba mieści się w dozwolonym zakresie. Jest to ważne dla zapewnienia, że gra zostanie zainicjowana z właściwą liczbą dysków.\
$\color{lightblue}{\textsf{Informacja zwrotna dla użytkownika: }}$ Jeśli wejście jest nieprawidłowe, użytkownik jest informowany o błędzie i proszony o ponowne wprowadzenie danych. Zapewnia to jasną komunikację i pomaga uniknąć frustracji związanej z niejasnymi wymaganiami dotyczącymi wejścia.\
$\color{lightblue}{\textsf{Aktualizacja stanu gry: }}$ Po wprowadzeniu prawidłowej liczby dysków, wartość ta jest zapisywana w zmiennej numberOfDisks, co bezpośrednio wpływa na konfigurację gry przy następnym jej uruchomieniu.
