using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarGameAPI.Models
{
    public partial class ShortShip
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
    }
}
