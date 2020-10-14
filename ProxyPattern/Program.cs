namespace ProxyPattern
{
    public class Person
    {
        public int Age { get; set; }

        public string Drink()
        {
            return "drinking";
        }

        public string Drive()
        {
            return "driving";
        }

        public string DrinkAndDrive()
        {
            return "driving while drunk";
        }
    }

    public class ResponsiblePerson
    {
        public ResponsiblePerson(Person person)
        {
            // todo
        }

        public int Age { /* todo implement property */ }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
