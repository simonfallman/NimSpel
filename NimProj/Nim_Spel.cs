namespace Nim_Spel
{
    class Nim
    {
        static string[] piles = new string[] {"|||||", "|||||", "|||||"};
        static bool player1Turn = true;
        static string userNameMultiOne;
        static string userNameMultiTwo;
        static int player1win = 0;
        static int player2win = 0;
        static int player1vsAiwin = 0;
        static int datornwin = 0;
        static void Main(string[] args)
        {
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
                    
                    SpelarNamn();
                    
                    bool playAgain = true;
                    while(playAgain)
                    {

                    SpelUppsättning();
                    while(true)
                    {
                        if(player1Turn)
                        {
                            SpelarDrag();
                        }
                        else
                        {
                            AIDrag();
                        }
                        if(GameOver())
                        {
                            HanteraVinstMotDatorn();

                            Console.WriteLine("Vill ni spela ett nytt parti? (ja/nej) Eller checka vinststatistik (1)");
                            string input = Console.ReadLine();
                            if(input == "1")
                            {
                                VinststatistikMotDatorn();
                                
                            }
                            if(input != "ja")
                            {
                                playAgain = false;
                            }
                            else{
                                StartNewGame();
                            }
                            break;
                        }
                        SpelarDragByte();
                    }
                    }
                    break;
                    
                case "2":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine();
                    Console.WriteLine("Du har valt Flerspelarläge");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;

                    SpelarnasNamn();
                    
                bool SpelaIgen = true;
                while(SpelaIgen)
                {
                    SpelUppsättning();
                    while(true)
                    {
                        if(player1Turn)
                        {
                            SpelarDrag();
                        }
                        else
                        {
                            SpelarDrag();
                        }
                        if(GameOver())
                        {
                            HanteraVinst();

                            Console.WriteLine("Vill ni spela ett nytt parti? (ja/nej) eller checka vinststatistik (1)");
                            string input = Console.ReadLine();
                            if(input == "1")
                            {
                                Vinststatistik();
                            }
                            if(input != "ja")
                            {
                                SpelaIgen = false;
                            }
                            else
                            {
                                StartNewGame();
                            }
                            break;
                        }
                        SpelarDragByte();
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

        //Vi skapar en metod för spelarens drag för att inte kladda i Main metoden dessutom kan vi kalla på denna metod när det är spelare 2s tur, 
        static void SpelarDrag()
        {
            try
            {
                do
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{(player1Turn ? userNameMultiOne : userNameMultiTwo)}, gör ditt drag genom att skriva hög mellanslag antal pinnar att ta bort: ");

                    string input = Console.ReadLine();
                    string[] svar = input.Split(' ');//Med hjälp av input.Split kan vi se våra värden på index 0 och 1 genom att skapa en string array för att sedan tryparsea input från spelaren

                    if(!int.TryParse(svar[0], out int högIndex) || !int.TryParse(svar[1], out int pinnarAttTaBort) && svar.Length != 2) //Vi sätter ! framför int.TryParse för att få bort alla andra spelarinputs som då skickar ett felmeddelande.
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Fel inmatning, försök igen.");
                        continue;
                    }

                    högIndex--; //Decrementar högIndex så användaren skriver från hög 1-3 istället för index för c# 0-2
                    
                    if(GiltigtDrag(högIndex, pinnarAttTaBort))
                    {
                        TaBortPinnarFrånHög(högIndex, pinnarAttTaBort);
                        break;//Denna break är kritisk annars så ber vi bara om ännu ett till spelardrag från samma spelare, vi måste helt enkelt efter ett giltigt drag breaka från får do-while loop.
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine();
                        Console.WriteLine("Ogiltigt drag, försök igen.\n");
                    }
                }
                while(true);//Så länge denna while loop är true så kommer vi försöka få ett spelardrag, när vi får ett giltigt drag så breaker vi ur denna do-while loopen

            }
            catch 
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.WriteLine("Fel inmatning. Försök igen.");
                SpelUppsättning();
                SpelarDrag();
            }
        }
        static void SpelarnasNamn()
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
        static void SpelarNamn()
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
        //Vi skriver ut vår array genom inbyggda metod string.Join så det ser snyggare ut för användaren.
        static void SpelUppsättning()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine(string.Join(" ___ ", piles));
            Console.WriteLine();

        }
        //Vi kollar om det är ett giltigt drag från spelaren, checkar så vi till exempel inte kan addera pinnar, ta bort 0 pinnar och att de måste ange en hög som finns (1-3)
        static bool GiltigtDrag(int högIndex, int pinnarAttTaBort)
        {
            return högIndex >= 0 && högIndex <= 2 && pinnarAttTaBort <= piles[högIndex].Length && pinnarAttTaBort > 0;
        }
        //Tar Bort pinnar från given hög, genom att skapa en ny string på given index plats för att sedan gör en ny string utav char '|' - antal pinnar spelaren har angett
        static void TaBortPinnarFrånHög(int högIndex, int pinnarAttTaBort)
        {
            piles[högIndex] = new string('|', piles[högIndex].Length - pinnarAttTaBort);
            SpelUppsättning();
        }
        //Kollar om Spelet är över om alla strings i string[] piles är tomma. Då returneras bool värdet true och breaker vår loop.
        static bool GameOver()
        {
            return piles[0] == "" && piles[1] == "" && piles[2] == "";
        }
        //Simpel metod som byter boolska värdet från true till false, med andra ord ett lätt sätt att byta spelardrag
        static bool SpelarDragByte()
        {
            return player1Turn = !player1Turn;
        }
        //Importerar en "AI" som tar fram random nummer, datorns random är inte så kallad "true random", men det är close enough för oss att kalla random
        static void AIDrag()
        {
            Random random = new Random();
            int pileIndex, sticksToRemove;

            do
            {
                pileIndex = random.Next(0, piles.Length);
                sticksToRemove = random.Next(1, 6); //Eftersom paramtern är exklusiv så menas det att den tar ett nummer mellan 1 och <6 men det menas att den tar ett nummer mellan 1 och 5.

            } while (!GiltigtDrag(pileIndex, sticksToRemove)); //Denna säger att så länge den inte ger oss ett giltigt drag så ska den fortsätta tills den får fram ett giltigt drag.

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Datorn tar bort {sticksToRemove} från hög {pileIndex + 1}");
            Console.WriteLine();
            TaBortPinnarFrånHög(pileIndex, sticksToRemove);
        }
        static void StartNewGame()
        {
            piles[0] = new string('|', 5);
            piles[1] = new string('|', 5);
            piles[2] = new string('|', 5);
            player1Turn = true;
        }
        static void Vinststatistik()
        {
            Console.WriteLine($"{userNameMultiOne} har vunnit {player1win} gånger");
            Console.WriteLine($"{userNameMultiTwo} har vunnit {player2win} gånger");
            Console.WriteLine();
            StartNewGame();
            StartPage();

        }
        static void VinststatistikMotDatorn()
        {
            Console.WriteLine($"{userNameMultiOne} har vunnit {player1vsAiwin} gånger");
            Console.WriteLine($"Datorn har vunnit {datornwin} gånger");
            Console.WriteLine();
            StartNewGame();
            StartPage();
        }
        static void HanteraVinstMotDatorn()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine($"{(player1Turn ? userNameMultiOne : "Datorn")} Vann!🎉🎉🎉");
            Console.WriteLine();
            if(player1Turn)
            {
                player1vsAiwin++;
            }
            else
            {
                datornwin++;
            }
        }
        static void HanteraVinst()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine($"{(player1Turn ? userNameMultiOne : userNameMultiTwo)} Vann!🎉🎉🎉");
            Console.WriteLine();
            if(player1Turn)
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