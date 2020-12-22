using NeoTalent.Core.Exceptions;
using NeoTalent.Services;
using NUnit.Framework;
using static NeoTalent.Core.Enumerators.GameObjectEnumerator;

namespace NeoTalent.Tests
{
    [TestFixture]
    public class GameServiceTests
    {
        private string[] _boardSettings;

        [SetUp]
        public void SetUp()
        {
            _boardSettings = new string[]  {  "5 4",
                                                "1,1 1,3 3,3",
                                                "4 2",
                                                "0 1 N"
                                            };
        }
        [Test]
        public void CreateBoard_ValidBoardSettings_CreatedBoard()
        {
            var service = new GameService();

            var result = service.CreateBoard(_boardSettings);

            Assert.That(result, Is.TypeOf<int[,]>());
        }
        [Test]
        public void CreateBoard_ValidBoardSettings_CreatedBoardWithDogs()
        {
            var service = new GameService();

            var result = service.CreateBoard(_boardSettings);

            Assert.That(result, Is.TypeOf<int[,]>());
            Assert.That(result, Has.Member((int)GameObject.Dog));
        }
        [Test]
        public void CreateBoard_ValidBoardSettings_CreatedBoardWithFood()
        {
            var service = new GameService();

            var result = service.CreateBoard(_boardSettings);

            Assert.That(result, Is.TypeOf<int[,]>());
            Assert.That(result, Has.Member((int)GameObject.Food));
        }
        [Test]
        public void CreateBoard_ValidBoardSettings_CreatedBoardWithCat()
        {
            var service = new GameService();

            var result = service.CreateBoard(_boardSettings);

            Assert.That(result, Is.TypeOf<int[,]>());
            Assert.That(result, Has.Member((int)GameObject.Cat));
        }
        [Test]
        public void CreateBoard_InvalidBoardSettings_ThrowException()
        {
            var boardSettings = new string[] { " " };

            var service = new GameService();

            Assert.That(() => service.CreateBoard(boardSettings), Throws.Exception.TypeOf<NeoTalentException>());
        }
        [Test]
        public void CreateBoard_ValidMovements_FoundEmptyPlace()
        {
            var movements = new string[] { "R M" };

            var service = new GameService();
            service.CreateBoard(_boardSettings);

            var result = service.MoveCat(movements);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result, Has.Member("Still in Danger – the cat has not yet found the food or hit a dog"));
        }
        [Test]
        public void CreateBoard_ValidMovements_FoundDog()
        {
            var movements = new string[] { "R R M" };
            var service = new GameService();
            service.CreateBoard(_boardSettings);

            var result = service.MoveCat(movements);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result, Has.Member("Oh boy it's a dog, its over, just runnn!"));
        }
        [Test]
        public void CreateBoard_ValidMovements_FoundFood()
        {
            var movements = new string[] { "R M R", "M M M M" };
            var service = new GameService();
            service.CreateBoard(_boardSettings);

            var result = service.MoveCat(movements);

            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result, Has.Member("Success you reach the food!"));
        }
        [Test]
        public void CreateBoard_InvalidInputMovements_ThrowException()
        {
            var movements = new string[] { "1" };
            var service = new GameService();
            service.CreateBoard(_boardSettings);

            Assert.That(() => service.MoveCat(movements), Throws.Exception.TypeOf<NeoTalentException>());

        }
        [Test]
        public void CreateBoard_InvalidMovements_InvalidMessage()
        {
            var movements = new string[] { "R M L M M" };
            var service = new GameService();
            service.CreateBoard(_boardSettings);

            var result = service.MoveCat(movements);

            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result, Has.Member("Invalid Move"));
        }
    }
}
