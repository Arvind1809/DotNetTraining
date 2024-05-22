using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassAndObjects
{
    public class Student
    {
        public int Id { get; set; }
        public string Name = "Sujit";

        public void getName() 
        {
            Console.WriteLine(Name);
        }

    }
}
