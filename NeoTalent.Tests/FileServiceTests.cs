using NeoTalent.Core.Exceptions;
using NeoTalent.Services;
using NUnit.Framework;
using static NeoTalent.Core.Enumerators.GameObjectEnumerator;

namespace NeoTalent.Tests
{
    [TestFixture]
    public class FileServiceTests
    {

        [Test]
        public void ReadFile_ValidPath_AllLines()
        {
            var service = new FileService();

            var result = service.ReadFile("..\\..\\..\\..\\NeoTalent.Application\\input.txt");

            Assert.That(result, Is.TypeOf<string[]>());
        }
        [Test]
        public void ReadFile_InvalidPath_ThrowException()
        {
            var service = new FileService();

            Assert.That(() => service.ReadFile("..\\..\\..\\..\\input.txt"), Throws.Exception.TypeOf<NeoTalentException>());
        }
        [Test]
        public void ReadFile_InvalidFile_ThrowException()
        {
            var service = new FileService();

            Assert.That(() => service.ReadFile("..\\..\\..\\..\\NeoTalent.Application\\InvalidFileForTesting.txt"), Throws.Exception.TypeOf<InvalidFileException>());
        }

        [Test]
        public void GetMovements_ValidData_ReturnAllMovements()
        {
            var service = new FileService();
            var file = service.ReadFile("..\\..\\..\\..\\NeoTalent.Application\\input.txt");

            var result = service.GetMovements(file);

            Assert.That(result, Is.TypeOf<string[]>());
        }

    }
}
