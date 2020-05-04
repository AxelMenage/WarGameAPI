using System;
using System.Collections.Generic;

namespace WarGameAPI.Entities
{
    public partial class ShipState
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public byte ShipId { get; set; }
        public int Life { get; set; }

        public virtual Game Game { get; set; }
        public virtual Ship Ship { get; set; }
        public virtual User User { get; set; }
    }
}
