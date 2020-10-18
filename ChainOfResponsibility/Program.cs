using System;
using System.Collections.Generic;
using System.Text;

namespace ChainOfResponsibility
{
    public abstract class Creature
    {
        public int Attack { get; set; }
        public int Defense { get; set; }

        public override string ToString()
        {
            return $"Attack: {Attack}\nDefense: {Defense}";
        }
    }

    public class Goblin : Creature
    {
        private Game game;
        
        public Goblin(Game game)
        {
            this.game = game;
            Attack = 1;
            Defense = 1;

            foreach (var creature in game.Creatures)
            {
                if (!(creature is Goblin goblin)) 
                    continue;
                Defense++;
                goblin.Defense++;
            }
        }
    }

    public class GoblinKing : Goblin
    {
        public GoblinKing(Game game) : base(game)
        {
            Attack += 2;
            Defense += 2;

            foreach (var creature in game.Creatures)
            {
                if (creature is Goblin goblin)
                {
                    goblin.Attack++;
                }
            }
        }
    }

    public class Game
    {
        public IList<Creature> Creatures = new List<Creature>();
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var creature in Creatures)
            {
                sb.AppendLine(creature.ToString());
            }

            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var goblin = new Goblin(game);
            game.Creatures.Add(goblin);

            var goblinKing = new GoblinKing(game);
            game.Creatures.Add(goblinKing);

            var goblin2 = new Goblin(game);
            game.Creatures.Add(goblin2);

            Console.WriteLine(game);
            Console.ReadKey();
        }
    }
}
