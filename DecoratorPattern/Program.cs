namespace DecoratorPattern
{
    public class Bird
    {
        public int Age { get; set; }

        public string Fly()
        {
            return (Age < 10) ? "flying" : "too old";
        }
    }

    public class Lizard
    {
        public int Age { get; set; }

        public string Crawl()
        {
            return (Age > 1) ? "crawling" : "too young";
        }
    }

    public class Dragon // no need for interfaces
    {
        private Bird bird = new Bird();
        private Lizard lizard = new Lizard();
        
        private int _age;
       
        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                bird.Age = value;
                lizard.Age = value;
            }
        }

        public string Fly() => bird.Fly();

        public string Crawl() => lizard.Crawl();
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
