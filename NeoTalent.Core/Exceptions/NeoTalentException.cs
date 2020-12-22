using System;
using System.Collections.Generic;
using System.Text;

namespace NeoTalent.Core.Exceptions
{
    public class NeoTalentException : Exception
    {
        public NeoTalentException()
        {
        }

        public NeoTalentException(string message)
            : base(message)
        {
        }
    }
}
