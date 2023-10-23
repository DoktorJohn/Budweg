
using System.ComponentModel.Design;
using System.Diagnostics.SymbolStore;
using System.Linq.Expressions;
using System.Threading.Tasks.Sources;

namespace Budweg
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //HOVEDMENU
            Console.ForegroundColor = ConsoleColor.Green;
            Menu mainMenu = new Menu(@"
              ____            _                    
             |  _ \          | |                   
             | |_) |_   _  __| |_      _____  __ _ 
             |  _ <| | | |/ _` \ \ /\ / / _ \/ _` |
             | |_) | |_| | (_| |\ V  V /  __/ (_| |
             |____/ \__,_|\__,_| \_/\_/ \___|\__, |
                                              __/ |
                                             |___/ 
");

            //HOVEDMENU
            mainMenu.MenuItems = new MenuItem[10];

            mainMenu.AddMenuItem("1. Tjek in/tjek ud");
            mainMenu.AddMenuItem("2. Print liste");
            mainMenu.AddMenuItem("3. Admin");

            AdminDataHandler handler = new AdminDataHandler("Ansatte.txt");
            Employee[] ansatte = new Employee[1000];

            bool notexit = true;
            bool tr = true;


            while (notexit)
            {
                Console.Clear();
                mainMenu.Show();
                Console.WriteLine("Vælg en menu ved at taste et gyldigt menupunkt og 'enter'. Tast 0 for at afslutte.");
                int select = mainMenu.selectMenuItem();

                try
                {

                    // Valg i hovedmenuen
                    switch (select)
                    {

                        // 1. CHECK IND/UD
                        case 1:
                            Console.Clear();
                            Menu checkIn = new Menu("Tjek ind og ud");
                            checkIn.MenuItems = new MenuItem[10];
                            checkIn.AddMenuItem("1. Tjek ind");
                            checkIn.AddMenuItem("2. Tjek ud");
                            checkIn.AddMenuItem("3. Tilbage");
                            checkIn.Show();

                            int checkinSelect = checkIn.selectMenuItem();

                            switch (checkinSelect)
                            {
                                //Tjek ind
                                case 1:

                                    while (true)
                                    {
                                        Console.Clear();
                                        Menu checkIndMenu = new Menu("Tjek ind");
                                        checkIndMenu.Show();

                                        Login tjekind = new Login(handler);
                                        tjekind.DoLogin(); //logind metoden kører



                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                    }

                                    break;


                                //Tjek ud
                                case 2:
                                    Console.Clear();
                                    Menu checkUdMenu = new Menu("Tjek ud");
                                    checkUdMenu.Show();

                                    Login tjekud = new Login(handler);
                                    tjekud.DoLogout(); //logind metoden kører


                                    Console.ReadKey();
                                    Console.Clear();
                                    break;

                                //Tilbage
                                case 3:
                                    Console.Clear();
                                    break;

                                //Tilbage
                                default:
                                    Console.Clear();
                                    break;
                            }
                            break;


                        //2. SØG
                        case 2:
                            Console.Clear();
                            Menu search = new Menu("Søg: ");
                            search.MenuItems = new MenuItem[10];
                            search.AddMenuItem("1. Print medarbejder liste");
                            search.AddMenuItem("2. Tilbage");
                            search.Show();
                            int searchSelect = search.selectMenuItem();

                            switch (searchSelect)
                            {
                                case 1:
                                    Console.Clear();
                                    Employee[] vis = handler.LoadPersons(); // Vi viser de indskrevne dataer for de ansatte
                                    for (int i = 0; i < vis.Length; i++) // loop der udskriver ansatte[i] og lægger i+1 for hver gang loopet køres for at vise alle indastede ansatte                             
                                    {
                                        if (vis[i] != null && vis[i].AtWork == true) // ved sgu ikke hvorfor, men programmet kører ikke hvis ikke man skriver at ansatte skal være over 0
                                        {
                                            Console.WriteLine
                                                ("Employee number: " + (i + 1) + "\n" +
                                                "Employee ID: " + vis[i].EmployeeID + "\n" +
                                                "Experience: " + vis[i].Experience + "\n" +
                                                "Name: " + vis[i].Name + "\n" +
                                                "Phone number: " + vis[i].PhoneNumber + "\n" +
                                                "Employee admin?: " + vis[i].Admin + "\n" +
                                                "Workstation: " + vis[i].Workstation + "\n" +
                                                "At work? " + vis[i].AtWork + "\n");
                                            // data på ansatte printes for hver ansat indtastet (se vis[i] + 1 for hvert loop)
                                        }

                                    }

                                    Console.WriteLine("Tryk valgfri knap for at fortsætte");
                                    Console.ReadKey();
                                    break;

                                case 2:
                                    Console.Clear();
                                    break;

                                default:
                                    Console.WriteLine("Tast venligst kun 1 eller 2!");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                            }

                            break;

                        //3. ADMIN
                        case 3:
                            Console.Clear();
                            Console.Write("Indtast medarbejderID: ");
                            string IDet = Console.ReadLine();
                            Console.Clear();

                            Employee adminen = handler.FindEmployeeMedAdmin(IDet); //vi bruger metoden til at se om ID stemmer overens og om deres ID er admin true/false

                            //Admins kodeord, hvis det er rigtigt
                            if (adminen != null)
                            {
                                Console.Clear();

                                //Admin menu
                                Menu Admin = new Menu("Admin Menu ");
                                Admin.MenuItems = new MenuItem[10];
                                Admin.AddMenuItem("1. Indstil medarbejder");
                                Admin.AddMenuItem("2. Se medarbejdere");
                                Admin.AddMenuItem("3. Hovedmenu");

                                Admin.Show();
                                int adminSelect = Admin.selectMenuItem();

                                switch (adminSelect)
                                {

                                    case 1:
                                        while (true)
                                        {
                                            Console.Clear();
                                            // Admin taster alle data
                                            Console.WriteLine("Indtast medarbejderID:");
                                            string ID = Console.ReadLine();
                                            Console.WriteLine("Indtast erfaring:");
                                            string erfaring = Console.ReadLine();
                                            Console.WriteLine("Indtast medarbejderens navn:");
                                            string navn = Console.ReadLine();
                                            Console.WriteLine("Indtast telefon nummer:");
                                            int telefonNummer = int.Parse(Console.ReadLine());
                                            Console.WriteLine("Indtast medarbejder status (admin? true/false)");
                                            bool admin = bool.Parse(Console.ReadLine());
                                            Console.WriteLine("Indtast hvor medarbejderen skal arbejede i dag");
                                            string workstation = Console.ReadLine();
                                            Console.WriteLine("Er medarbejderen på arbejde i dag? (true/false)");
                                            bool atWork = bool.Parse(Console.ReadLine());

                                            // Dataerne bliver gemt som en ansat objekt
                                            Employee ansat = new Employee(ID, erfaring, navn, telefonNummer, admin, workstation, atWork);
                                            Console.Clear();
                                            Console.WriteLine($"Indtastet medarbejderdata: \n{ID} \n{erfaring} \n{navn} \n{telefonNummer} \n{admin} \n{workstation} \n {atWork}");
                                            handler.AddPerson(ansat); //vi adder den nye ansatte

                                            Console.WriteLine("Vil du tilføje flere ansatte? (skriv ja eller nej)");
                                            string svar = Console.ReadLine();

                                            if (svar.ToLower() == "nej")
                                            {
                                                break;
                                            }

                                        }
                                        Console.Clear();
                                        break;

                                    case 2:
                                        Console.Clear();
                                        Employee[] vis = handler.LoadPersons(); // Vi viser de indskrevne dataer for de ansatte
                                        for (int i = 0; i < vis.Length; i++) // loop der udskriver ansatte[i] og lægger i+1 for hver gang loopet køres for at vise alle indastede ansatte                             
                                        {
                                            if (vis[i] != null) // ved sgu ikke hvorfor, men programmet kører ikke hvis ikke man skriver at ansatte skal være over 0
                                            {
                                                Console.WriteLine
                                                    ("Employee number: " + (i + 1) + "\n" +
                                                    "Employee ID: " + vis[i].EmployeeID + "\n" +
                                                    "Experience: " + vis[i].Experience + "\n" +
                                                    "Name: " + vis[i].Name + "\n" +
                                                    "Phone number: " + vis[i].PhoneNumber + "\n" +
                                                    "Employee admin?: " + vis[i].Admin + "\n" +
                                                    "Workstation: " + vis[i].Workstation + "\n" +
                                                    "At work? " + vis[i].AtWork + "\n");
                                                // data på ansatte printes for hver ansat indtastet (se vis[i] + 1 for hvert loop)
                                            }

                                        }

                                        Console.WriteLine("Tryk valgfri knap for at fortsætte");
                                        Console.ReadKey();
                                        break;

                                    case 3:
                                        Console.Clear();
                                        break;

                                }
                                break;
                            }
                            else //hvis metoden returnerer null så:
                            {
                                Console.WriteLine("Forkert, tryk valgfri tast for at komme til hovedmenuen.");
                                Console.ReadKey();
                            }

                            break;


                        // Afslut, notexit bliver false, og vi går ud af while-loopen
                        case 0:
                            Console.Clear();
                            notexit = false;
                            break;


                        //Hvis forkert, indtast gyldigt kommando, notexit er stadig true, vi bliver i while-loopen
                        //Programmet hviser hovedmenuen, da vi stadig er i while-loopen
                        default:
                            Console.Clear();
                            Console.WriteLine("Indtast en gyldigt kommando.");
                            notexit = true;
                            Console.ReadKey();
                            Console.Clear();
                            continue;

                    }

                }
                
                catch
                {

                    Console.WriteLine("Angiv et gyldigt kommando");


                }

            }

            //Når vi er gået ud af while-loopen, kan vi afslutte

            Console.Clear();
            Console.WriteLine("Tryk valfri tast for at afslutte.");

            Console.ReadKey();

        }

    }

}



    
