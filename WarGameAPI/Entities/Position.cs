using System;
using System.Collections.Generic;

namespace WarGameAPI.Entities
{
    public partial class Position
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public byte ShipId { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public bool Touche { get; set; }

        public virtual Game Game { get; set; }
        public virtual Ship Ship { get; set; }
        public virtual User User { get; set; }
    }
}
