using System;
using System.Text;
using System.Collections.Generic;

namespace MementoPatterns
{
    public class Token
    {
        public int Value = 0;

        public Token(int value)
        {
            this.Value = value;
        }
    }

    public class Memento
    {
        // todo
    }

    public class TokenMachine
    {
        public List<Token> Tokens = new List<Token>();

        public Memento AddToken(int value)
        {
            // todo
        }

        public Memento AddToken(Token token)
        {
            // todo (yes, please do both overloads)
        }

        public void Revert(Memento m)
        {
            // todo
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
