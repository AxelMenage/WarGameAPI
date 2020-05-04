using System;
using System.Collections.Generic;

namespace WarGameAPI.Entities
{
    public partial class Ship
    {
        public Ship()
        {
            Positions = new HashSet<Position>();
            ShipStates = new HashSet<ShipState>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<ShipState> ShipStates { get; set; }
    }
}
