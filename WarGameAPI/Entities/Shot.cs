using System;
using System.Collections.Generic;

namespace WarGameAPI.Entities
{
    public partial class Shot
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }

        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
    }
}
