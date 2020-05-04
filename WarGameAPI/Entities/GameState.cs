using System;
using System.Collections.Generic;

namespace WarGameAPI.Entities
{
    public partial class GameState
    {
        public GameState()
        {
            Games = new HashSet<Game>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
