using System;
using NUnit.Framework;

namespace ObserverPattern
{
    public class Game
    {
        public event EventHandler<BonusEventArgs> Bite;
        public event EventHandler Weak;
        public event EventHandler ResetEmp;

        public void Attack(Rat sender, int bonus, bool isIncreased)
        {
            Bite?.Invoke(sender, new BonusEventArgs
            {
                BaseAttack = bonus,
                IsIncreased = isIncreased
            });
        }

        public void ResetEmpowering()
        {
            ResetEmp?.Invoke(this, EventArgs.Empty);
        }

        public void WeakerAttack()
        {
            Weak?.Invoke(this, EventArgs.Empty);
        }
    }

    public class BonusEventArgs
    {
        public int BaseAttack;
        public bool IsIncreased;
    }

    public class Rat : IDisposable
    {
        public int Attack = 1;
        private Game game;
        private bool isIncreased;

        public Rat(Game game)
        {
            this.game = game;
            
            game.Bite += Game_Bite;
            game.Weak += GameOnWeak;
            game.ResetEmp += GameOnResetEmp;

            game.ResetEmpowering();
            game.Attack(this, Attack, false);
        }

        private void GameOnResetEmp(object sender, EventArgs e)
        {
            isIncreased = false;
        }

        private void GameOnWeak(object sender, EventArgs e)
        {
            Attack--;
        }

        private void Game_Bite(object sender, BonusEventArgs e)
        {
            var ratSender = sender as Rat;
            if (ratSender != null && ratSender != this)
            {
                if (e.BaseAttack == 1 && e.IsIncreased == false)
                {
                    if (isIncreased == false)
                    {
                        game.Attack(this, ++Attack, true); 
                    }
                    isIncreased = true;
                }
                else if(Attack == e.BaseAttack)
                {
                    game.Attack(this, ++Attack, true);
                }
                else
                {
                    isIncreased = true;
                    Attack = e.BaseAttack;
                }
            }
        }

        public void Dispose()
        {
            game.Bite -= Game_Bite;
            game.Weak -= GameOnWeak;

            game.WeakerAttack();
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
        public void PlayingByTheRules()
        {
            Assert.That(typeof(Game).GetFields(), Is.Empty);
            Assert.That(typeof(Game).GetProperties(), Is.Empty);
        }

        [Test]
        public void SingleRatTest()
        {
            var game = new Game();
            var rat = new Rat(game);
            Assert.That(rat.Attack, Is.EqualTo(1));
        }

        [Test]
        public void TwoRatTest()
        {
            var game = new Game();
            var rat = new Rat(game);
            var rat2 = new Rat(game);
            Assert.That(rat.Attack, Is.EqualTo(2));
            Assert.That(rat2.Attack, Is.EqualTo(2));
        }

        [Test]
        public void ThreeRatsOneDies()
        {
            var game = new Game();

            var rat = new Rat(game);
            Assert.That(rat.Attack, Is.EqualTo(1));

            var rat2 = new Rat(game);
            Assert.That(rat.Attack, Is.EqualTo(2));
            Assert.That(rat2.Attack, Is.EqualTo(2));

            using (var rat3 = new Rat(game))
            {
                Assert.That(rat.Attack, Is.EqualTo(3));
                Assert.That(rat2.Attack, Is.EqualTo(3));
                Assert.That(rat3.Attack, Is.EqualTo(3));
            }

            Assert.That(rat.Attack, Is.EqualTo(2));
            Assert.That(rat2.Attack, Is.EqualTo(2));
        }
    }
}
