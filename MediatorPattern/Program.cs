using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MediatorPattern
{
    public class Participant
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        private Mediator mediator;

        public Participant(Mediator mediator)
        {
            Id = Guid.NewGuid();
            this.mediator = mediator;
            mediator.AddParticipant(this);
        }

        public void Say(int n)
        {
            mediator.Broadcast(Id, n);
        }
    }

    public class Mediator
    {
        private List<Participant> participants = new List<Participant>();

        public void Broadcast(Guid sourceId, int value)
        {
            foreach (var participant in participants)
            {
                if (participant.Id != sourceId)
                {
                    participant.Value = value;
                }
            }
        }

        public void AddParticipant(Participant newParticipant)
        {
            participants.Add(newParticipant);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    [TestFixture]
    public class TestSuite
    {
        [Test]
        public void Test()
        {
            Mediator mediator = new Mediator();
            var p1 = new Participant(mediator);
            var p2 = new Participant(mediator);

            Assert.That(p1.Value, Is.EqualTo(0));
            Assert.That(p2.Value, Is.EqualTo(0));

            p1.Say(2);

            Assert.That(p1.Value, Is.EqualTo(0));
            Assert.That(p2.Value, Is.EqualTo(2));

            p2.Say(4);

            Assert.That(p1.Value, Is.EqualTo(4));
            Assert.That(p2.Value, Is.EqualTo(2));
        }
    }
}
