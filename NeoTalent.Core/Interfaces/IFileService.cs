using System;
using System.Collections.Generic;
using System.Text;

namespace NeoTalent.Core.Interfaces
{
    public interface IFileService
    {
        string[] ReadFile(string path);
        string[] GetMovements(string[] file);
    }
}
