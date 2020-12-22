using System;
using System.Collections.Generic;
using System.Text;

namespace NeoTalent.Core.Interfaces
{
    public interface IGameService
    {
        int[,] CreateBoard(string[] BoardSettings);
        List<string> MoveCat(string[] setOfMovements);
    }
}
