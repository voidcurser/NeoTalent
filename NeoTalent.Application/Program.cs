using System;
using NeoTalent.Services;

namespace NeoTalent.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileService = new FileService();
            var file = fileService.ReadFile("..\\..\\..\\input.txt");

            var boardService = new GameService();
            boardService.CreateBoard(file);

            var movements = fileService.GetMovements(file);
            var returnMessages = boardService.MoveCat(movements);

            foreach (var msg in returnMessages)
            {
                Console.WriteLine(msg);
            }
            Console.WriteLine("***********PRESS ANY KEY TO FINISH************");
            Console.ReadKey();
        }
    }
}
