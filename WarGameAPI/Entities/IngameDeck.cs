using System;
using System.Collections.Generic;

namespace WarGameAPI.Entities
{
    public partial class IngameDeck
    {
        public IngameDeck()
        {
            Games = new HashSet<Game>();
            IngameDeckCards = new HashSet<IngameDeckCards>();
        }

        public int Id { get; set; }
        public int NbCards { get; set; }

        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<IngameDeckCards> IngameDeckCards { get; set; }
    }
}
