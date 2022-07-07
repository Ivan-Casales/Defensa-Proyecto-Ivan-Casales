using NUnit.Framework;
using Battleship;
using System;
using System.IO;

namespace Library.Tests
{   
    /// <summary>
    /// Se testean las nuevas clases del tipo ShotsInGame, la clase ShotsCountHolder
    /// y la clase Game modificada (con el ShotsCountHolder).
    /// </summary>
    [TestFixture]
    public class ShotsClassesTest
    {
        private User user1;

        private User user2;

        private Game game;


        [SetUp]
        public void Setup()
        {
            user1 = new User(35);
            user2 = new User(36);

            game = new Game(user1, user2);
        }

        [Test]
        public void WaterShots()
        {
            game.GetShotsCounter().GetWaterShots().AddShot();

            int shotsNumber = game.GetShotsCounter().GetWaterShots().GetShotsNumber();

            Assert.AreEqual(1, shotsNumber);

            game.GetShotsCounter().GetWaterShots().AddShot();

            shotsNumber = game.GetShotsCounter().GetWaterShots().GetShotsNumber();

            Assert.AreEqual(2, shotsNumber);
        }

        [Test]
        public void ShipsShots()
        {
            game.GetShotsCounter().GetShipsShots().AddShot();

            int shotsNumber = game.GetShotsCounter().GetShipsShots().GetShotsNumber();

            Assert.AreEqual(1, shotsNumber);

            game.GetShotsCounter().GetShipsShots().AddShot();

            shotsNumber = game.GetShotsCounter().GetShipsShots().GetShotsNumber();

            Assert.AreEqual(2, shotsNumber);
        }

    }
}