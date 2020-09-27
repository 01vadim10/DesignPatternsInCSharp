namespace FactoryPattern
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public static PersonFactory Factory = new PersonFactory();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}\n{nameof(Id)}: {Id}";
        }
    }

    public class PersonFactory
    {
        private int _personIndex;

        public Person CreatePerson(string name)
        {
            return new Person { Id = _personIndex++, Name = name };
        }
    }
}
