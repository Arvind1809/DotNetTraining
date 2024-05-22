

namespace ClassAndObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            //Car car = new Car();

            //Console.WriteLine(car.color);

            //Student student = new Student();
            ////Console.WriteLine(student.Name);
            //student.getName();


            //Teacher t1 = new Teacher();
            //t1.getName();

            //Student student = new Student();
            //Console.WriteLine(student.Name);


            Employee employee = new Employee()
            {
                FirstName = "TestA",
                LastName = "TestB",
                Age = 19,
            };

            employee.PrintFullName();
            Console.WriteLine("Age: {0}",employee.Age);

            //Employee employee2 = new Employee();
            //employee2.FirstName = "TestC";
            //employee2.LastName = "TestD";

            
            //employee2.PrintFullName();

            Console.ReadLine();
        }
    }

    class Car
    {
        public string color = "Black";
    }
}
