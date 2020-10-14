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
        private Person person;

        public ResponsiblePerson(Person person)
        {
            this.person = person;
        }

        public int Age
        {
            get
            {
                return person.Age;
            }

            set
            {
                person.Age = value;
            }
        }

        public string Drink()
        {
            if (Age < 18)
            {
                return "too young";
            }

            return person.Drink();
        }

        public string Drive()
        {
            if (Age < 16)
            {
                return "too young";
            }

            return person.Drive();
        }

        public string DrinkAndDrive()
        {
            return "dead";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
