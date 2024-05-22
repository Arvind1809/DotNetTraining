using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassAndObjects
{
    internal class Employee
    {
        public string FirstName;
        public string LastName;
        public int Age;

        public void PrintFullName()
        {
            Console.WriteLine(FirstName + " " + LastName);
        }
    }
}
