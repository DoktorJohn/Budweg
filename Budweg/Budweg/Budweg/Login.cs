using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Budweg
{
    public class Login
    {
        private AdminDataHandler handler;

        public Login(AdminDataHandler handler)
        {
            this.handler = handler;
        }

        public void DoLogin() //login metode
        {
            Console.Write("Angiv medarbejdernummer: ");
            string ansatID = Console.ReadLine(); //medarbejderen taster sit ID
            Employee[] employees = handler.LoadPersons(); // vi loader alle ansatte
            Employee bruger = null;

            foreach (Employee ans in employees) //Looped gennemgår alle employees i employee array'ed
            {
                if (ans != null)
                {
                    if (ans.EmployeeID == ansatID) //Hvis employee ID hos en employee = det brugeren indtaster
                    {
                        bruger = ans;
                        break;
                    }
                }
                else
                {
                    break;
                }

            }


            if (bruger != null) //hvis ID'et stemmede overens med et ID i tekstfilen
            {
                Console.Clear();
                Console.WriteLine("Velkommen på arbejde " + bruger.Name + " !");
                Console.WriteLine("Vælg hvor du skal arbejde i dag");
                string[] workStations = new string[] { "1. Fabrikken", "2. køkkenet", "3. IT-afdelingen" }; //ny string array med de forskellige workstations

                for (int i = 0; i < workStations.Length; i++) //loop der gennemgår alle workstations indtil i er større end antallet af workstations
                {
                    string[] dele = workStations[i].Split('.'); //vi splitter for hvert punktum i workstation string array (fx. "1. fabrikken" = "1." "fabrikken")
                    Console.WriteLine((i + 1) + ": " + dele[1]); //her skriver consol bare 1. fabrikken, 2. køkkenet, 3. it-afdelingen
                }

                Console.WriteLine("Skriv 1, 2, 3 osv. alt efter arbejdsstationsnummer");
                int workstationvalg;

                if (int.TryParse(Console.ReadLine(), out workstationvalg) && workstationvalg >= 1 && workstationvalg <= workStations.Length)
                { //hvis workstationvalg er større eller lig med 1, og workstationvalg er mindre en mængden af workstations (3 pt.)
                    string valgtWorkStation = workStations[workstationvalg - 1].Split('.')[1]; //ny string bliver tildelt den valgte workstation (-1 da array starter på 0 i c#)
                    bool atWorkUpdate = true;
                    bruger.AtWork = atWorkUpdate;
                    bruger.Workstation = valgtWorkStation; //vi tildeler den valgte workstation til vores bruger variabel vi lavede længere oppe

                    Console.WriteLine("Din arbejdsstation i dag er valgt til at være: " + bruger.Workstation); //konsollen skriver hvor de arbejder i dag
                    handler.UpdateAtWork(bruger.EmployeeID, bruger.AtWork);
                    handler.UpdateWorkStation(bruger.EmployeeID, bruger.Workstation); //vi bruger vores updateworkstation metode til at opdatere workstation for brugeren   
                }
                else
                {
                    Console.WriteLine("Ugyldigt valg");
                }
            }
            else
            {
                Console.WriteLine("Medarbejdernummer passer ikke.");

            }
        }

        public void DoLogout() //logout metode
        {
            Console.Write("Angiv medarbejdernummer: ");
            string ansatID = Console.ReadLine(); //medarbejderen taster sit ID
            Employee[] employees = handler.LoadPersons(); // vi loader alle ansatte
            Employee bruger = null;

            foreach (Employee ans in employees) //Looped gennemgår alle employees i employee array'ed
            {
                if (ans != null)
                {
                    if (ans.EmployeeID == ansatID) //Hvis employee ID hos en employee = det brugeren indtaster
                    {
                        bruger = ans;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("BrugerID eksisterer ikke");
                    break;
                }
            }


            if(bruger != null)
            {
                Console.WriteLine("På gensyn " +  bruger.EmployeeID);
                bool atWorkUpdate = false;
                bruger.AtWork = atWorkUpdate;
                handler.UpdateAtWork(bruger.EmployeeID, bruger.AtWork);
            }
            
        }
    }
}
