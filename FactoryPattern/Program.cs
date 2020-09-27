using System;

namespace FactoryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = Person.Factory.CreatePerson("James");
            var person2 = Person.Factory.CreatePerson("Annie");
            var person3 = Person.Factory.CreatePerson("Bob");
            Console.WriteLine(person);
            Console.WriteLine(person2);
            Console.WriteLine(person3);
            Console.ReadKey();
        }
    }
}
