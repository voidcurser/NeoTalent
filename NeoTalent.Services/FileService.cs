using System;
using System.Collections.Generic;
using System.Text;
using NeoTalent.Core.Exceptions;
using NeoTalent.Core.Interfaces;

namespace NeoTalent.Services
{
    public class FileService : IFileService
    {
        private string[] _file;
        public string[] ReadFile(string path)
        {
            try
            {
                _file = System.IO.File.ReadAllLines(path);
            }
            catch (Exception ex)
            {
                throw new NeoTalentException(ex.Message);
            }

            if (!IsFileValid(_file))
                throw new InvalidFileException();

            return _file;
        }

        private bool IsFileValid(string[] file)
        {
            //TODO: tem de ser trocado por verificaçao regex para garantir que o ficheiro e valido
            if (file.Length < 5 || file[0].Length != 3 || file[2].Length != 3 || file[3].Length != 5) 
                return false;

            return true;
        }

        public string[] GetMovements(string[] file)
        {
            try
            {
                var movements = new string[_file.Length - 4];
                for (int i = 4; i < _file.Length; i++)
                {
                    movements[i - 4] = _file[i];
                }
                return movements;
            }
            catch (Exception ex )
            {

                throw new NeoTalentException(ex.Message);
            }
        }
    }
}
