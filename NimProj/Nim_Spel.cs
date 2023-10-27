namespace Nim_Spel
{
    /// <summary>
    /// Nim klass för Nimspelet
    /// </summary>
    class Nim
    {
        // Class variabler för att kunna använda i respektive metod.
        static string[] piles = new string[] { "|||||", "|||||", "|||||" };
        static bool player1Turn = true;
        static string userNameMultiOne;
        static string userNameMultiTwo;
        static int player1win = 0;
        static int player2win = 0;
        static int player1vsAiwin = 0;
        static int AIwin = 0;

        /// <summary>
        /// Main metoden för Nim spelet
        /// </summary>
        static void Main(string[] args)
        {
            // Initiera konsolen och visa spelinstruktioner
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Välkommen till Nim!");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Spelet börjar med att man placerar fem stickor i tre olika högar.\n");
            Console.WriteLine("Därefter turas spelarna om att plocka stickor från dem tills de är tomma.\n");
            Console.WriteLine("Den spelare som har plockat den sista stickan har vunnit spelet.\n");
            StartPage();
        }

        /// <summary>
        /// Visar startsidan med spellägesalternativ.
        /// </summary>
        static void StartPage()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Tryck 1 för Enspelarläge");
            Console.WriteLine("Tryck 2 för Tvåspelarläge");
            Console.WriteLine("Tryck 3 för att Avsluta.");

            string userSelect = Console.ReadLine();

            switch (userSelect)
            {
                case "1":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine();
                    Console.WriteLine("Du har valt Enspelarläge\n");
                    Console.ForegroundColor = ConsoleColor.Red;

                    PlayerName();

                    bool playAgain = true;
                    while (playAgain)
                    {
                        GameStatus();
                        while (true)
                        {
                            if (player1Turn)
                            {
                                PlayerMove();
                            }
                            else
                            {
                                AiMove();
                            }
                            if (GameOver())
                            {
                                HandleWinvsAI();

                                Console.WriteLine("Vill ni spela ett nytt parti? (ja/nej) Eller checka vinststatistik (1)");
                                string input = Console.ReadLine();
                                if (input == "1")
                                {
                                    WinRecordvsAI();
                                }
                                if (input != "ja")
                                {
                                    playAgain = false;
                                }
                                else
                                {
                                    StartNewGame();
                                }
                                break;
                            }
                            PlayerMoveSwitch();
                        }
                    }
                    break;

                case "2":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine();
                    Console.WriteLine("Du har valt Flerspelarläge");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;

                    MultiplayerName();

                    bool SpelaIgen = true;
                    while (SpelaIgen)
                    {
                        GameStatus();
                        while (true)
                        {
                            if (player1Turn)
                            {
                                PlayerMove();
                            }
                            else
                            {
                                PlayerMove();
                            }
                            if (GameOver())
                            {
                                HandleWin();

                                Console.WriteLine("Vill ni spela ett nytt parti? (ja/nej) eller checka vinststatistik (1)");
                                string input = Console.ReadLine();
                                if (input == "1")
                                {
                                    WinRecord();
                                }
                                if (input != "ja")
                                {
                                    SpelaIgen = false;
                                }
                                else
                                {
                                    StartNewGame();
                                }
                                break;
                            }
                            PlayerMoveSwitch();
                        }
                    }
                    break;

                case "3":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Något är fel med din input, vänligen ange ett nummer mellan 1 och 3");
                    StartPage();
                    break;
            }
        }

        /// <summary>
        /// Hanterar spelarens drag
        /// </summary>
        static void PlayerMove()
        {
            try
            {
                do
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{(player1Turn ? userNameMultiOne : userNameMultiTwo)}, gör ditt drag genom att skriva hög mellanslag antal pinnar att ta bort: ");

                    string s = Console.ReadLine();
                    string[] input = s.Split(' ');

                    if (!int.TryParse(input[0], out int pileIndex) || !int.TryParse(input[1], out int sticksToRemove) && input.Length != 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Fel inmatning, försök igen.");
                        continue;
                    }

                    pileIndex--;
                    
                    if (ValidMove(pileIndex, sticksToRemove))
                    {
                        SticksToRemoveFromPile(pileIndex, sticksToRemove);
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine();
                        Console.WriteLine("Ogiltigt drag, försök igen.\n");
                    }
                }
                while (true);

            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.WriteLine("Fel inmatning. Försök igen.");
                GameStatus();
                PlayerMove();
            }
        }

        /// <summary>
        /// Ha/// Hanterar inputs av namnen på tvåspelarläget.
        /// </summary>
        static void MultiplayerName()
        {
            while (true)
            {
                Console.WriteLine("Spelare 1, ange ditt namn:");
                userNameMultiOne = Console.ReadLine();

                Console.WriteLine("Spelare 2, ange ditt namn:");
                userNameMultiTwo = Console.ReadLine();

                if (userNameMultiOne == "" || userNameMultiTwo == "")
                {
                    Console.WriteLine("Vänligen lämna inga namn tomma");
                }
                else
                {
                    break;
                }
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine();
            Console.WriteLine($"Välkomna {userNameMultiOne} och {userNameMultiTwo}. Nu kör vi igång!");
        }

        /// <summary>
        /// Hanterar input av spelarens namn på enspelarläge.
        /// </summary>
        static void PlayerName()
        {
            while (true)
            {
                Console.WriteLine("Ange ditt namn:");
                userNameMultiOne = Console.ReadLine();

                if (userNameMultiOne == "")
                {
                    Console.WriteLine("Vänligen ange namn");
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Välkommen {userNameMultiOne}. Nu kör vi igång!");
        }

        /// <summary>
        /// Visa det aktuella spelläget.
        /// </summary>
        static void GameStatus()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine(string.Join(" ___ ", piles));
            Console.WriteLine();
        }

        /// <summary>
        /// Checkar om spelarens drag är giltigt eller ej.
        /// </summary>
        /// <param name="pileIndex">Index av hög (1-3).</param>
        /// <param name="sticksToRemove">Hur många minnar ska tas bort.</param>
        /// <returns>True om spelardrag är giltigt, annars false.</returns>
        static bool ValidMove(int pileIndex, int sticksToRemove)
        {
            return pileIndex >= 0 && pileIndex <= 2 && sticksToRemove <= piles[pileIndex].Length && sticksToRemove > 0;
        }

        /// <summary>
        /// Tar bort pinnar från specifierad hög.
        /// </summary>
        /// <param name="pileIndex">Hög index som pinnar ska tas bort ifrån.</param>
        /// <param name="sticksToRemove">Antal pinnar att ta bort.</param>
        static void SticksToRemoveFromPile(int pileIndex, int sticksToRemove)
        {
            piles[pileIndex] = new string('|', piles[pileIndex].Length - sticksToRemove);
            GameStatus();
        }

        /// <summary>
        /// Checkar om spelet är över genom att kolla om högarna är tomma.
        /// </summary>
        /// <returns>True om spelet är över, annars false.</returns>
        static bool GameOver()
        {
            return piles[0] == "" && piles[1] == "" && piles[2] == "";
        }

        /// <summary>
        /// Byter spelardrag
        /// </summary>
        /// <returns>True om det är spelar1Turn, annars false - spelare 2.</returns>
        static bool PlayerMoveSwitch()
        {
            return player1Turn = !player1Turn;
        }

        /// <summary>
        /// Implementerar AIns drag
        /// </summary>
        static void AiMove()
        {
            Random random = new Random();
            int pileIndex, sticksToRemove;

            do
            {
                pileIndex = random.Next(0, piles.Length);
                sticksToRemove = random.Next(1, 6);

            } while (!ValidMove(pileIndex, sticksToRemove));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Datorn tar bort {sticksToRemove} från hög {pileIndex + 1}");
            Console.WriteLine();
            SticksToRemoveFromPile(pileIndex, sticksToRemove);
        }

        /// <summary>
        /// Startar om spelet med de ursprungliga högarna.
        /// </summary>
        static void StartNewGame()
        {
            piles[0] = new string('|', 5);
            piles[1] = new string('|', 5);
            piles[2] = new string('|', 5);
            player1Turn = true;
        }

        /// <summary>
        /// Visar vinststatistik för tvåspelarläge.
        /// </summary>
        static void WinRecord()
        {
            Console.WriteLine($"{userNameMultiOne} har vunnit {player1win} gånger");
            Console.WriteLine($"{userNameMultiTwo} har vunnit {player2win} gånger");
            Console.WriteLine();
            StartNewGame();
            StartPage();
        }

        /// <summary>
        /// Visar vinststatistik för enspelarläge.
        /// </summary>
        static void WinRecordvsAI()
        {
            Console.WriteLine($"{userNameMultiOne} har vunnit {player1vsAiwin} gånger");
            Console.WriteLine($"Datorn har vunnit {AIwin} gånger");
            Console.WriteLine();
            StartNewGame();
            StartPage();
        }

        /// <summary>
        /// Hanterar vinstvillkoret i enspelarläge mot AI.
        /// </summary>
        static void HandleWinvsAI()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine($"{(player1Turn ? userNameMultiOne : "Datorn")} Vann!🎉🎉🎉");
            Console.WriteLine();
            if (player1Turn)
            {
                player1vsAiwin++;
            }
            else
            {
                AIwin++;
            }
        }

        /// <summary>
        /// Hanterar vinstvillkoret i tvåspelarläge.
        /// </summary>
        static void HandleWin()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine($"{(player1Turn ? userNameMultiOne : userNameMultiTwo)} Vann!🎉🎉🎉");
            Console.WriteLine();
            if (player1Turn)
            {
                player1win++;
            }
            else
            {
                player2win++;
            }
        }
    }
}
