namespace Nim_Spel
{
    class Nim
    {
        static string[] högar = new string[] {"|||||", "|||||", "|||||"};
        static bool Spelare1Turn = true;
        static string userNameMultiOne;
        static string userNameMultiTwo;
        static int player1win = 0;
        static int player2win = 0;
        static int player1vsAiwin = 0;
        static int datornwin = 0;
        static void Main(string[] args)
        {
            Console.Clear();
            StartPage();
        }
        static void StartPage()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Välkommen till Nim!");
            Console.WriteLine();
            Console.WriteLine("Hur många är ni?");
            Console.WriteLine("Tryck 1 för Enspelarläge");
            Console.WriteLine("Tryck 2 för Tvåspelarläge");
            Console.WriteLine("Tryck 3 för Spelregler");
            Console.WriteLine("Tryck 4 för att Avsluta.");
            

            string userSelect = Console.ReadLine();

            switch (userSelect) 
            {
                case "1":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine();
                    Console.WriteLine("Du har valt Enspelarläge");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    
                    SpelarNamn();
                    
                    bool playAgain = true;
                    while(playAgain)
                    {

                    SpelUppsättning();
                    while(true)
                    {
                        if(Spelare1Turn)
                        {
                            SpelarDrag();
                        }
                        else
                        {
                            AIDrag();
                        }
                        if(GameOver())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine();
                            Console.WriteLine($"{(Spelare1Turn ? userNameMultiOne : "Datorn")} Vann!🎉🎉🎉");
                            Console.WriteLine();
                            if(Spelare1Turn)
                            {
                                player1vsAiwin++;
                            }
                            else{
                                datornwin++;
                            }
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
                        if(Spelare1Turn)
                        {
                            SpelarDrag();
                        }
                        else
                        {
                            SpelarDrag();
                        }
                        if(GameOver())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine();
                            Console.WriteLine($"{(Spelare1Turn ? userNameMultiOne : userNameMultiTwo)} Vann!🎉🎉🎉");
                            Console.WriteLine();
                            if(Spelare1Turn)
                            {
                                player1win++;
                            }
                            else
                            {
                                player2win++;
                            }
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
                    Console.WriteLine("Under konstruktion");
                    StartPage();
                    break;
                case "4":
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
                    Console.WriteLine($"{(Spelare1Turn ? userNameMultiOne : userNameMultiTwo)}, gör ditt drag genom att skriv hög mellanslag antal pinnar att ta bort: ");

                    string input = Console.ReadLine();
                    string[] svar = input.Split(' ');//Med hjälp av input.Split kan vi se våra värden på index 0 och 1 genom att skapa en string array för att sedan tryparsea input från spelaren

                    if(svar.Length != 2) //Vi kollar om svaret inte är t.ex. 1 3 5 eller liknande. Genom att vi skapat en array av spelarens input så kan vi checka att längden måste var 2 siffror
                    {
                        Console.WriteLine("Fel inmatning, du skrev inte högnummer och pinnar på ett korrekt sätt, försök igen.");
                    }
                    if(!int.TryParse(svar[0], out int högIndex) || !int.TryParse(svar[1], out int pinnarAttTaBort)) //Vi sätter ! framför int.TryParse för att få bort alla andra spelarinputs som då skickar ett felmeddelande.
                    {
                        Console.WriteLine("Fel inmatning, antagligen för att det inte är heltal.");
                        
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
                        Console.WriteLine("Ogiltigt drag, försök igen.");
                    }
                }
                while(true);//Så länge denna while loop är true så kommer vi försöka få ett spelardrag, när vi får ett giltigt drag så breaker vi ur denna do-while loopen

            }
            catch 
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.WriteLine("Något gick fel. Försök igen.");
                Console.WriteLine();
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
                Console.WriteLine();
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
                Console.WriteLine();
        }
        //Vi skriver ut vår array genom inbyggda metod string.Join så det ser snyggare ut för användaren.
        static void SpelUppsättning()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(string.Join(" ___ ", högar));
            Console.WriteLine();

        }
        //Vi kollar om det är ett giltigt drag från spelaren, checkar så vi till exempel inte kan addera pinnar, ta bort 0 pinnar och att de måste ange en hög som finns (1-3)
        static bool GiltigtDrag(int högIndex, int pinnarAttTaBort)
        {
            return högIndex >= 0 && högIndex <= 2 && pinnarAttTaBort <= högar[högIndex].Length && pinnarAttTaBort > 0;
        }
        //Tar Bort pinnar från given hög, genom att skapa en ny string på given index plats för att sedan gör en ny string utav char '|' - antal pinnar spelaren har angett
        static void TaBortPinnarFrånHög(int högIndex, int pinnarAttTaBort)
        {
            högar[högIndex] = new string('|', högar[högIndex].Length - pinnarAttTaBort);
            SpelUppsättning();
        }
        //Kollar om Spelet är över om alla strings i string[] högar är tomma. Då returneras bool värdet true och breaker vår loop.
        static bool GameOver()
        {
            return högar[0] == "" && högar[1] == "" && högar[2] == "";
        }
        //Simpel metod som byter boolska värdet från true till false, med andra ord ett lätt sätt att byta spelardrag
        static bool SpelarDragByte()
        {
            return Spelare1Turn = !Spelare1Turn;
        }
        //Importerar en "AI" som tar fram random nummer, datorns random är inte så kallad "true random", men det är close enough för oss att kalla random
        static void AIDrag()
        {
            Random random = new Random();
            int pileIndex, sticksToRemove;

            do
            {
                pileIndex = random.Next(0, högar.Length);
                sticksToRemove = random.Next(1, 6); //Eftersom paramtern är exklusiv så menas det att den tar ett nummer mellan 1 och <6 men det menas att den tar ett nummer mellan 1 och 5.

            } while (!GiltigtDrag(pileIndex, sticksToRemove)); //Denna säger att så länge den inte ger oss ett giltigt drag så ska den fortsätta tills den får fram ett giltigt drag.

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Datorn tar bort {sticksToRemove} från hög {pileIndex + 1}");
            Console.WriteLine();
            TaBortPinnarFrånHög(pileIndex, sticksToRemove);
        }
        static void StartNewGame()
        {
            högar[0] = new string('|', 5);
            högar[1] = new string('|', 5);
            högar[2] = new string('|', 5);
            Spelare1Turn = true;
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
    }
}