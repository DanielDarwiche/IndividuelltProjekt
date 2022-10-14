using System;
using System.Threading;

namespace IndividuelltProjekt
{
    class Program
    {
        private static Random _random = new Random();
        private static ConsoleColor GetRandomConsoleColor()//JAG ÄNDRAR FÄRG MED METOD FÖR ATT GÖRA TEXTEN TYDLIGARE
        {
            var consoleColors = Enum.GetValues(typeof(ConsoleColor));
            return (ConsoleColor)consoleColors.GetValue(_random.Next(consoleColors.Length));
        }
        public static void Main(string[] args)
        {//HÄR ANGER JAG 2D-ARRAYS FÖR PENGAR, KONTON , ANVÄNDARE OCH PINKOD
            string[,] konton = new string[5, 4] //SKAPAR KONTON MED NAMN SAMT OLIKA ANTAL KONTON
            {
                {
                    "HANNA","SparKonto","LöneKonto","MatKonto",//3
                },
                {
                    "KIM","LöneKonto","ISKkonto",""//2
                },
                {
                    "ALEX","LöneKonto","ReseKonto",""//2  varav ett tomt konto
                },
                {
                    "DANIEL","SparKonto","SemesterKonto","HobbyKonto"//3
                },
                {
                    "SARA","Lönekonto" ,"",""//1
                },
            };
            decimal[,] pengar = new decimal[5, 4]
            {
                {
                    0,100, 200, 400  //Skapar konton med olika antal summor, där det inte är tomma konton.
                },
                {
                   0, 450,300,0
                },
                {
                    0,300,343,0
                },
                {
                    0,234,57,34
                },
                {
                   0, 900,0,0
                },
            };
            string[,] userName = new string[5, 2] //Skapar användarnamn med lösenord
            {
                {
                    "HANNA","111"
                },
                {
                    "KIM","222"
                },
                {
                    "ALEX","333"
                },
                {
                    "DANIEL","444"
                },
                {
                    "SARA","555"
                },
            };
            int person = 0;
            Inloggning(person, konton, pengar, userName);
        }
        public static void menu(string user, int person, string[,] konton, decimal[,] pengar, string[,] userName)
        {
            bool sant = true;
            do
            {
                    Console.Clear();                //Loopar en meny med val där man kan ange 1-4.
                    Console.WriteLine("1. Se dina konton och saldo");//VALEN ÄR OLIKA METODER
                    Console.WriteLine("2. Överföring mellan konton");
                    Console.WriteLine("3. Ta ut pengar");
                    Console.WriteLine("4. Logga ut");
                    string menuC = Console.ReadLine();
                    switch (menuC)
                    {
                        case "1":
                            Console.Clear();
                            SeKonton(user, konton, pengar, userName,person);
                            break;
                        case "2":
                            transfer(user, person, konton, pengar, userName);
                            break;
                        case "3":
                            taUt(person, user, konton, pengar, userName);
                            break;
                        case "4":
                            Console.WriteLine("Klicka Enter för att komma till huvudmenyn.");
                            Console.ReadKey();
                            Console.Clear();
                            Inloggning(person, konton, pengar, userName);
                            break;
                        default:
                            Console.WriteLine("Ogiltig inmatning.");
                        Thread.Sleep(500);
                            break;
                    }
            } while (sant == true);
        }
        public static void Inloggning(int person, string[,] konton, decimal[,] pengar, string[,] userName)
        {
            int amount = 0;
            int pinkod = 0;
            do
            {
                Console.WriteLine("Välkommen till internetbanken!"); //Loopar inloggsmöjlighet tills man angett rätt namn
                Console.WriteLine("Mata in användarnamn:");//MEN PINKOD KAN BARA ANGES 3 GÅNGER, ANNARS AVSLUTAS PROG
                string user = Console.ReadLine().ToUpper();
                    for (int col = 0; col <= userName.GetUpperBound(0); col++)
                    {
                        if (user == userName[col, 0])//SÖKER EFTER ANGIVNA NAMNET I ARRAY på rad col samt index 0 
                        {
                            user = userName[col, 0];
                            Console.Clear();
                            Console.WriteLine("Välkomen {0}!", user);
                            do
                            {
                                try
                                {
                                    Console.WriteLine("Var god mata in pinkod:");
                                    pinkod = int.Parse(Console.ReadLine());
                                }
                                catch
                                {
                                    Console.WriteLine("Ange endast siffor");
                                }
                                for (int pinSök = 0; pinSök <= userName.GetUpperBound(0); pinSök++)
                                {
                                    if (pinkod == int.Parse(userName[col, 1]))
                                    {                          //om pinkoden är rätt så loggas man in och skickas till meny
                                        Console.Clear();
                                        Console.WriteLine("Du har lyckats logga in {0}!", user);
                                        Thread.Sleep(700);
                                        menu(user, person, konton, pengar, userName);
                                    }
                                    else
                                    {
                                        amount++;
                                        Console.WriteLine("Försök " + amount);
                                        if (amount == 3)
                                        {
                                            Console.WriteLine("Du har angett fel pinkod 3 gånger. Programmet stängs ner...");
                                            Thread.Sleep(2000);
                                        Environment.Exit(0); //STÄNGER PROGRAMMET OM PINKOD ÄR FEL 3 GÅNGER
                                        }
                                        break;
                                    }
                                }
                            } while (amount < 3);
                        }
                    }
                Console.Clear();
            } while (amount < 3);
        }
        public static void SeKonton(string user, string[,] konton, decimal[,] pengar, string[,] userName, int person)
        {
            Console.ForegroundColor = GetRandomConsoleColor();
            for (int i = 0; i <= konton.GetUpperBound(0); i++)  //SÄTTER I SOM VALD ANVÄNDARES INDEXSIFFRA  
            {
                if (user == konton[i, 0])
                    person = i;
            }
            for (int j = 1; j < pengar.GetUpperBound(0); j++)//SKRIVER UT SALDON SAMT SUMMOR FÖR VALD ANVÄNDARE
            {
                if (user == konton[person, 0])//OM KONTOT INTE HAR ETT NAMN/ÄR TOMT STÅR DET "TOMT KONTO"
                {
                    if (konton[person, j] == "")
                    {
                        Console.WriteLine("Tomt konto");
                    }
                    else
                    {
                        Console.WriteLine("{0}:", konton[person, j]);//ANNARS SKRIVS KONTONAMNET UT
                    }
                    string money = string.Format("{0:c}", pengar[person, j]);//OAVSETT SKRIVS SALDOT UT
                    Console.WriteLine(money);//SALDOT OMVANDLAS MED HJÄLP AV FORMATERINGEN HÄR,TILL KR/ÖRE
                    Console.WriteLine();
                }
            }
            Enter(user, person, konton,pengar, userName);
        }
        public static void taUt(int person, string user, string[,] konton, decimal[,] pengar, string[,] userName)
        {
            Console.ForegroundColor = GetRandomConsoleColor();
            for (int i = 0; i <= konton.GetUpperBound(0); i++)  //TAR ÄNNU EN GÅNG IN RÄTT PERSONS INDEXSIFFRA
            {
                if (user == konton[i, 0])
                    person = i;
            }
            int numb = 0;
            for (int j = 1; j < pengar.GetUpperBound(0); j++)//VISAR IGEN KONTON OCH SUMMOR
            {
                numb++;
                if (user == konton[person, 0])
                {
                    if (konton[person, j] == "")
                    {
                        Console.WriteLine("{0}:", numb);
                        Console.WriteLine("Tomt konto");
                    }
                    else
                    {
                        Console.WriteLine("{0}:", numb);
                        Console.WriteLine("{0}:", konton[person, j]);

                    }
                    string money = string.Format("{0:c}", pengar[person, j]);
                    Console.WriteLine(money);
                    Console.WriteLine();
                }
            }
            Console.ForegroundColor = GetRandomConsoleColor();
            int valtKonto = 0;
            do                  //SCOOP FÖR ATT ANGE VILKET KONTO MAN VILL TA UT PENGAR IFRÅN
            {
                try
                {
                    Console.WriteLine("Välj konto som du vill ta ut pengar ifrån");
                    valtKonto = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Ogiltig inmatning");
                    Thread.Sleep(800);
                    Console.Clear();
                }
            } while (valtKonto != 1 && valtKonto != 2 && valtKonto != 3);
            if (pengar[person, valtKonto] == 0)
            {
                Console.Clear();
                Console.WriteLine("Du kan inte ta ut pengar från ett tomt konto.");
                taUt(person, user, konton, pengar, userName);
            }
            else
            {
                int försök = 1;//FÖRSÖK DEKLARERAR ANTAL FÖRSÖK MAN SKRIVIT PINKOD, HÄR ÄR DET INGEN GRÄNS
                decimal uttag = 0;//UTTAG DEKLARERAR SUMMAN MAN VILL TA UT
                do
                {
                    try
                    {
                        Console.WriteLine("Hur mycket pengar vill du ta ut?");
                        uttag = Decimal.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Ogiltig inmatning");
                    }
                } while (uttag !> pengar[person, valtKonto] &&uttag!=pengar[person, valtKonto] );
                int ange = 0;
                    do//DO LOOP TILLS MAN ANGIVIT RÄTT PINKOD
                    {
                      try
                      {
                        Console.WriteLine("Bekräfta uttag genom att ange pinkod:");
                        ange = int.Parse(Console.ReadLine());
                      }
                      catch
                      {
                        Console.WriteLine("Ogilitig Inmatning");
                      }
                       Console.WriteLine("Försök {0}", försök);
                       försök++;
                } while (ange!= int.Parse(userName[person, 1]));
                Console.Clear();
                Console.ForegroundColor = GetRandomConsoleColor();//EFTER RÄTT SVAR TAS PENGARNA UT OCH VISAR FÖRÄNDRINGEN
                Console.WriteLine("Efter uttaget:");
                pengar[person, valtKonto] -= uttag;//TAR BORT PENGARNA MAN TAGIT UT FRÅN KONTOT
                Console.WriteLine(konton[person, valtKonto]);
                string money = string.Format("{0:c}", pengar[person, valtKonto]);
                Console.WriteLine(money);
                Console.WriteLine();
                Console.WriteLine("Konton samt saldon efter uttag:");
                for (int j = 1; j < pengar.GetUpperBound(0); j++)//VISAR SUMMOR SAMT KONTON IGEN
                {
                    if (user == konton[person, 0])
                    {
                        if (konton[person, j] == "")
                        {
                            Console.WriteLine("Tomt konto");
                        }
                        else
                        {
                            Console.WriteLine("{0}:", konton[person, j]);
                        }
                         money = string.Format("{0:c}", pengar[person, j]);
                        Console.WriteLine(money);
                        Console.WriteLine();
                    }
                }
                Enter(user, person, konton, pengar, userName);
            }
        }
        public static void transfer(string user, int person, string[,] konton, decimal[,] pengar, string[,] userName)
        {
            Console.ForegroundColor = GetRandomConsoleColor();
            int nu = 1;
            string money = "";
            for (int i = 0; i <= konton.GetUpperBound(0); i++) 
            {
                if (user == konton[i, 0])
                    person = i;
            }
            for (int j = 1; j < pengar.GetUpperBound(0); j++)//VISAR KONTON SAMT SALDON
            {
                if (user == konton[person, 0])
                {
                    if (konton[person, j] == "")
                    {
                        Console.WriteLine("{0}:",nu);//VISAR ÄVEN EN SIFFRA ÖVER KONTOT,1,2,3:
                        Console.WriteLine("Tomt konto");
                    }
                    else
                    {
                        Console.WriteLine("{0}:", nu);
                        Console.WriteLine("{0}:", konton[person, j]);
                    }
                    money = string.Format("{0:c}", pengar[person, j]);
                    Console.WriteLine(money);
                    Console.WriteLine();
                    nu++;
                }
            }
            int valtKonto = 0;//SCOOP FÖR ATT VÄLJA KONTO ATT TA UT PENGAR IFRÅN,TILLS MAN VALT 1/2/3
            do
            {
                try
                {
                    Console.WriteLine("Välj konto som du vill ta ut pengar ifrån");
                    valtKonto = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Ogiltig inmatning");
                    Thread.Sleep(800);
                    Console.Clear();
                }
            } while (valtKonto != 1 && valtKonto != 2 && valtKonto != 3);
            if (pengar[person, valtKonto] == 0)
            {
                Console.Clear();
                Console.WriteLine("Du kan inte ta ut pengar från ett tomt konto.");
            }
            else                    
            {
                int inKonto = 0;//INKONTO ÄR VARIABELN FÖR INDEXSIFFRAN TILL KONTOT MAN VILL FÖRA ÖVER PENGAR TILL
                do
                {
                    try
                    {
                        Console.WriteLine("Vilket konto vill du föra över pengar till?");
                        inKonto = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Ogiltig inmatning");
                    }
                } while (inKonto != 2 && inKonto != 1 && inKonto != 3);
                decimal uttag = 0;//UTTAG ÄR VARIABELN FÖR SUMMAN MAN VILL FÖRA ÖVER 
                do
                {
                    try
                    {
                        Console.WriteLine("Hur mycket pengar vill du föra över?");
                        uttag = Decimal.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Ogiltig inmatning");
                    }
                }//LOOPAS TILL ATT UTTAGSUMMAN  ÄR LIKA MED ELLER  STÖRRE ÄN SALDOT MAN INNEHAR 
                while (uttag !> pengar[person, valtKonto] && uttag != pengar[person, valtKonto]);
                int ange = 0;
                    do
                    {
                        try
                        {
                            Console.WriteLine("Bekräfta uttag genom att ange pinkod:");
                            ange = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Ogiltig inmatning");
                        }
                    } while (ange != int.Parse(userName[person, 1]));  
                pengar[person, inKonto] += uttag;//FÖR ÖVER PENGARNA SAMT TAR BORT FRÅN VALDA KONTON
                pengar[person, valtKonto] -= uttag;
                Console.ForegroundColor = GetRandomConsoleColor();
                Console.WriteLine("Efter överföringen:");
                for (int j = 1; j < pengar.GetUpperBound(0); j++)//SALDON SAMT KONTON EFTER ÖVERFÖRING
                {
                        if (konton[person, j] == "")
                        {
                            Console.WriteLine("Tomt konto");
                        }
                        else
                        {
                            Console.WriteLine("{0}:", konton[person, j]);
                        }
                         money = string.Format("{0:c}", pengar[person, j]);
                        Console.WriteLine(money);
                        Console.WriteLine();
                }
            }
            Enter(user, person, konton, pengar, userName);
        }
        public static void Enter(string user, int person, string[,] konton, decimal [,] pengar, string[,] userName)
        {
            do
            {//METOD FÖR ATT RETURNERA TILL MENY OCH DET FUNKAR BARA OM MAN TRYCKER ENTER
                Console.WriteLine("Klicka Enter för att komma till huvudmenyn.");   
                string enter = Console.ReadLine();
                if (enter != "")
                {
                    Console.WriteLine("Du måste ange Enter för att gå vidare");
                    Thread.Sleep(500);
                }
                else
                {
                    menu(user, person, konton, pengar, userName);
                }
            } while (true);
        }
    }
}
