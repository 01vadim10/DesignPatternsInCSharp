using System.Collections.Generic;

namespace ChainOfResponsibility
{
    public abstract class Creature
    {
        public int Attack { get; set; }
        public int Defense { get; set; }

    }

    public class Goblin : Creature
    {
        public Goblin(Game game) { }
    }

    public class GoblinKing : Goblin
    {
        public GoblinKing(Game game) { }
    }

    public class Game
    {
        public IList<Creature> Creatures;
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
