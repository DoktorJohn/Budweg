using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Budweg
{
    public class AdminDataHandler
    {
        private string databaseFileName;

        public string DatabaseFileName
        {
            get { return databaseFileName; }
        }

        public AdminDataHandler(string databaseFileName)
        {
            this.databaseFileName = databaseFileName;
        }

        public void AddPerson(Employee ansatte)
        {
            StreamWriter sw = new StreamWriter(databaseFileName, true);
            sw.WriteLine(ansatte.MakeTitle());
            sw.Close();
        }

        public void AddPersons(Employee[] people)
        {
            StreamWriter add = new StreamWriter(databaseFileName, true);

            for (int i = 0; i < people.Length; i++)
            {
                Employee ansatte = people[i];

                if (ansatte != null)
                {
                    add.WriteLine(ansatte.MakeTitle());
                }

            }

            add.Close();
        }

        public Employee LoadPerson()
        {
            StreamReader sr = new StreamReader(databaseFileName);
            string input = sr.ReadLine();
            sr.Close();

            string[] array = input.Split('.');
            string employeeID = array[0];
            string experience = array[1];
            string name = array[2];
            int phoneNumber = int.Parse(array[3]);
            bool admin = bool.Parse(array[4]);
            string workstation = array[5];
            bool atWork = bool.Parse(array[6]);

            Employee ansatte = new Employee(employeeID, experience, name, phoneNumber, admin, workstation, atWork);

            return ansatte;
        }

        public Employee[] LoadPersons()
        {
            Employee[] people = new Employee[1000];
            int itemcount = 0;
            StreamReader streamReader = new StreamReader(databaseFileName);

            foreach (string line in File.ReadAllLines(databaseFileName))
            {
                string[] dele = line.Split('.');
                string employeeID = dele[0];
                string experience = dele[1];
                string name = dele[2];
                int phoneNumber = int.Parse(dele[3]);
                bool admin = bool.Parse(dele[4]);
                string workstation = dele[5];
                bool atWork = bool.Parse(dele[6]);

                Employee ansatte = new Employee(employeeID, experience, name, phoneNumber, admin, workstation, atWork);
                people[itemcount] = ansatte;
                itemcount++;

            }
            streamReader.Close();
            return people;
        }

        public Employee FindEmployeeMedID(string employeeID) //metode for at finde en medarbejders ID
        {
            Employee[] ansatte = LoadPersons(); //Vi loader alle employees

            foreach (Employee ans in ansatte) //Looped gennemgår alle employees i employee array'ed
            {
                if (ans.EmployeeID == employeeID) //Hvis employee ID hos en employee = det brugeren indtaster
                {
                    return ans; // returner den ansatte
                }
                else //tilføjede denne linje hvis indtastet bruger ikke eksisterer
                {
                    return null;
                }
            }

            return null; //returner ingenting hvis den ikke fandt medarbejderen
        }

        public Employee FindEmployeeMedAdmin(string employeeID) //samme som ovenover, men for admin status
        {
            Employee[] ansatte = LoadPersons();

            foreach (Employee ans in ansatte)
            {

                if (ans.EmployeeID == employeeID && ans.Admin == true)
                {
                    return ans;
                }
                else if (ans.EmployeeID == employeeID && ans.Admin == false)
                {
                    Console.WriteLine("Access not granted");
                    return null;
                }

            }
            Console.WriteLine("Bruger findes ikke");
            return null;
        }



        public void UpdateWorkStation(string employeeID, string newWorkstation)
        {
            Employee[] ansatte = LoadPersons();

            for (int i = 0; i < ansatte.Length; i++)
            {
                if (ansatte[i] != null && ansatte[i].EmployeeID == employeeID)
                {
                    ansatte[i].Workstation = newWorkstation;
                    break;
                }
            }

            StreamWriter sw = new StreamWriter(databaseFileName, false);
            foreach (Employee ansat in ansatte)
            {
                if (ansat != null)
                {
                    sw.WriteLine(ansat.MakeTitle());
                }
            }
            sw.Close();

        }
        public void UpdateAtWork(string employeeID, bool newAtWork)
        {
            Employee[] ansatte = LoadPersons();

            for (int i = 0; i < ansatte.Length; i++)
            {
                if (ansatte[i] != null && ansatte[i].EmployeeID == employeeID)
                {
                    ansatte[i].AtWork = newAtWork;
                    break;
                }
            }

            StreamWriter sw = new StreamWriter(databaseFileName, false);
            foreach (Employee ansat in ansatte)
            {
                if (ansat != null)
                {
                    sw.WriteLine(ansat.MakeTitle());
                }
            }
            sw.Close();

        }



    }
}
