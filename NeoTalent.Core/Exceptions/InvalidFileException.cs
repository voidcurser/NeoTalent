using System;
using System.Collections.Generic;
using System.Text;

namespace NeoTalent.Core.Exceptions
{
    public class InvalidFileException : Exception
    {
        public InvalidFileException()
        {
        }

        public InvalidFileException(string message)
            : base(message)
        {
        }
    }
}
