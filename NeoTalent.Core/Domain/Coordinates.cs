using System;
using System.Collections.Generic;
using System.Text;
using static NeoTalent.Core.Enumerators.CompassEnumerator;

namespace NeoTalent.Core.Domain
{
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Compass Dir { get; set; }
    }
}
