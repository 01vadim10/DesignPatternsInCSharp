using System;
using NUnit.Framework;

namespace StatePattern
{
    public enum State
    {
        Locked,
        Opened,
        Error,
        EnteringDigits
    }

    public class CombinationLock
    {
        private int[] combination { get; }
        private State state;
        private int position;

        public CombinationLock(int[] combination)
        {
            this.combination = combination;
            Status = "LOCKED";
        }

        // you need to be changing this on user input
        public string Status;

        public void EnterDigit(int digit)
        {
            if (position == 0)
            {
                Status = String.Empty;
                state = State.EnteringDigits;
            }

            if (position < combination.Length - 1 && combination[position++] == digit)
            {
                Status += digit.ToString();
            }
            else if (position == combination.Length - 1 && combination[position++] == digit)
            {
                state = State.Opened;
            }
            else
            {
                state = State.Error;
            }

            switch (state)
            {
                case State.Locked:
                    Status = "LOCKED";
                    break;
                case State.Opened:
                    Status = "OPEN";
                    break;
                case State.Error:
                    Status = "ERROR";
                    break;
                case State.EnteringDigits:
                    return;
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
    public class Test
    {
        [Test]
        public void CheckLock()
        {
            var cl = new CombinationLock(new []{1, 2, 3, 4, 5});
            Assert.That(cl.Status, Is.EqualTo("LOCKED"));
            cl.EnterDigit(1);
            Assert.That(cl.Status, Is.EqualTo("1"));
            cl.EnterDigit(2);
            Assert.That(cl.Status, Is.EqualTo("12"));
            cl.EnterDigit(2);
            Assert.That(cl.Status, Is.EqualTo("ERROR"));
            //cl.EnterDigit(4);
            //Assert.That(cl.Status, Is.EqualTo("1234"));
            //cl.EnterDigit(5);
            //Assert.That(cl.Status, Is.EqualTo("OPEN"));
        }
    }
}
