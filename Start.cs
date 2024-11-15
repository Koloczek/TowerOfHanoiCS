using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ProjectGameCS
{
    class Program
    {
        static Stack<int>[] towers = new Stack<int>[3];
        static int moves = 0;
        static int numberOfDisks = 3;
        static int selectedTower = 0;

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

            static void WriteLineInColor(string message, ConsoleColor color)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            }


            static bool MoveDisk(int from, int to)
            {
                if (towers[from].Count == 0 || (towers[to].Count > 0 && towers[from].Peek() > towers[to].Peek()))
                {
                    return false;
                }

                towers[to].Push(towers[from].Pop());
                return true;
            }


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


            static void PrintDisk(int diskSize, int baseWidth)
            {
                int padding = (baseWidth - diskSize * 2) / 2;
                string disk = new string('=', diskSize * 2);
                Console.Write(new string(' ', padding) + disk + new string(' ', padding));
            }

            static bool CheckWin()
            {
                return towers[0].Count == 0 && towers[1].Count == 0 && towers[2].Count == numberOfDisks;
            }

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
        }
    }
}
