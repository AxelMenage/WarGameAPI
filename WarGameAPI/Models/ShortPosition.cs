using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarGameAPI.Models
{
    public partial class ShortPosition
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public byte ShipId { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public bool Touche { get; set; }
    }
}
