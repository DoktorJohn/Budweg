using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Budweg
{
    public class Employee
    {
        private string employeeID;
        private string experience;
        private string name;
        private int phoneNumber;
        private bool admin;
        private string workstation;
        private bool atWork;

        public string EmployeeID
        { set
            {
                if(employeeID.Length  > 5)
                {
                    employeeID = value;
                }
                else
                {
                    throw new Exception("Employee ID must be longer than 5 characters!");
                }

            }
            get { return  employeeID; }
        }

        public string Experience
        {
            set { experience = value; }
            get { return experience; }
        }

        public string Name
        {
            set { name = value; }
            get { return name; }
        }

        public int PhoneNumber
        {
            set { phoneNumber = value; }
            get { return phoneNumber; }
        }

        public bool Admin
        {
            set
            {
                if(admin == true || false)
                {
                    admin = value;
                }
                else
                {
                    throw new Exception("Must be true or false");
                }
                
            }
            get { return admin; }
        }

        public string Workstation
        {
            set { workstation = value; }
            get { return workstation; }
        }

        public bool AtWork
        {
            set
            {
                atWork = value;
            }
            get { return atWork; }
        }


        public Employee(string employeeID, string experience, string name, int phoneNumber, bool admin, string workstation, bool atWork)
        {
            this.employeeID = employeeID;
            this.experience = experience;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.admin = admin;
            this.workstation = workstation;
            this.atWork = atWork;

        }

        public string MakeTitle()
        {
            return employeeID + "." + experience + "." + name + "." + phoneNumber + "." + admin + "." + workstation + "." + atWork;
        }

    }
}
