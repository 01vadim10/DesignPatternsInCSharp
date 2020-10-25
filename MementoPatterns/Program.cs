using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

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
        public List<int> Values = new List<int>();

        public Memento(List<Token> tokens)
        {
            foreach (var token in tokens)
            {
                Values.Add(token.Value);
            }
        }
    }

    public class TokenMachine
    {
        public List<Token> Tokens = new List<Token>();

        public TokenMachine()
        {
        }

        public Memento AddToken(int value)
        {
            var token = new Token(value);

            return AddToken(token);
        }

        public Memento AddToken(Token token)
        {
            Tokens.Add(token);
            var memento = new Memento(Tokens);

            return memento;
        }

        public void Revert(Memento m)
        {
            if (Tokens.Count > 0)
            {
                Tokens.Clear();
                foreach (var value in m.Values)
                {
                    var token = new Token(value);
                    Tokens.Add(token);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void SingleTokenTest()
        {
            var tm = new TokenMachine();
            var m = tm.AddToken(123);
            tm.AddToken(456);
            tm.Revert(m);
            Assert.That(tm.Tokens.Count, Is.EqualTo(1));
            Assert.That(tm.Tokens[0].Value, Is.EqualTo(123));
        }

        [Test]
        public void TwoTokenTest()
        {
            var tm = new TokenMachine();
            tm.AddToken(1);
            var m = tm.AddToken(2);
            tm.AddToken(3);
            tm.Revert(m);
            Assert.That(tm.Tokens.Count, Is.EqualTo(2));
            Assert.That(tm.Tokens[0].Value, Is.EqualTo(1),
                $"First token should have value 1, you got {tm.Tokens[0].Value}");
            Assert.That(tm.Tokens[1].Value, Is.EqualTo(2));
        }

        [Test]
        public void FiddledTokenTest()
        {
            var tm = new TokenMachine();
            Console.WriteLine("Made a token with value 111 and kept a reference");
            var token = new Token(111);
            Console.WriteLine("Added this token to the list");
            tm.AddToken(token);
            var m = tm.AddToken(222);
            Console.WriteLine("Changed this token's value to 333 :)");
            token.Value = 333;
            tm.Revert(m);

            Assert.That(tm.Tokens.Count, Is.EqualTo(2),
                $"At this point, token machine should have exactly two tokens, you got {tm.Tokens.Count}");

            Assert.That(tm.Tokens[0].Value, Is.EqualTo(111),
                $"You got the token value wrong here. Hint: did you init the memento by value?");
        }
    }
}
