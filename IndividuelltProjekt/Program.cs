using System;

namespace IndividuelltProjekt
{
    class Program
    {
        static void Main(string[] args)
        {
            Inloggning();
        }
        static void Inloggning()
        {
            string[] userName = new string[]
            {
                "Ett",
                "Two",
                "Tre",
                "Fyra",
                "Fem"
            };
            int[] passWord = new int[]
            {
                111,
                222,
                333,
                444,
                555
            };
            Console.WriteLine("Välkommen till internetbanken!");
            Console.WriteLine("Mata in användarnamn");
            string user = Console.ReadLine();
            for(int i =0;i<userName.Length;i++)
            {
                if (user == userName[i]) 
                {
                    Console.Clear();
                    Console.WriteLine("Välkommen {0}", userName[i]);
                    int amount = 0;
                    do
                    {
                        Console.WriteLine("Mata in pinkod:");
                        int pinkod = int.Parse(Console.ReadLine());
                        for (int k = 0; k < passWord.Length; k++)
                        {
                            if (pinkod == passWord[i])
                            {
                                menu();
                            }
                            else if (pinkod != passWord[i])
                            {
                                amount++;
                                Console.WriteLine("Mata in rätt pinkod:");
                                Console.WriteLine("Du har försökt {0} gånger. Efter tre försök avslutas programmet", amount);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Ogiltig inmatning.");
                            }
                        }
                    } while (amount != 3 ) ; //ELLER om pinkod inte är som passsWord i


                }
                else
                {
                    Console.WriteLine("Mata in rätt användarnamn");
                }
            }
        }
        static void menu()
        {
            bool sant = true;
            do 
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("1. Se dina konton och saldo");
                    Console.WriteLine("2. Överföring mellan konton");
                    Console.WriteLine("3. Ta ut pengar");
                    Console.WriteLine("4. Logga ut"); //Returnera till inloggning

                    int menuC = int.Parse(Console.ReadLine());
                    switch (menuC)
                    {
                        case 1:
                            SeKonton();
                            break;
                        case 2:
                            transfer();
                            break;
                        case 3:
                            taUt();
                            break;
                        case 4:
                            Console.WriteLine("Klicka Enter för att komma till huvudmenyn.");
                            Console.ReadKey();
                            Inloggning();
                            break;
                        default:
                            Console.WriteLine("Ogiltig inmatning.");
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Ogiltig inmatning");
                }
            } while (sant==true);
        }
        static void SeKonton()
        {
            //skriv ut de olika konton som användaren har och hur mkt pengar som finns där
            // konton ska ha både kronor och ören
            // alla users ska ha olika antal konton 
            // varje saldo ska ha ett namn som "lönekonto" eller sparkkonto" investeringskonto""semesterkonto"
            // saldon för alla konton sätts vid starten av programmet, så alla saldon startas om om programet startas om 
            Console.WriteLine("Klicka Enter för att komma till huvudmenyn.");
            Console.ReadKey();
        }
        static void transfer()
        {

            //kunna välja ett konto att ta pengar ifrån , ett konto at t lfytta pengarna till och sen en suma som ska flyttas
            //summan ska flyttas och sen ska användaden se summorna på de påverkade konotonta
            Console.WriteLine("Klicka Enter för att komma till huvudmenyn.");
            Console.ReadKey();
        }
        static void taUt()
        {

            //användare ska kunna väljha ett konto samt en summa
            //måste ange pinkod efteråt för att bekräfta att ta ut pengarna
            //pegrna ska sedan flyttas
            //skrvi ut det nya saldot på kontot.
            Console.WriteLine("Klicka Enter för att komma till huvudmenyn.");
            Console.ReadKey();
        }

        //commit meddealnde git
        //mint tre metoder
        //kommentera
        //fleketion redme fil  samt reflektion resonemang för vf i repositoyry  samt beskrivngin för helt ovetande 
    }
}
