using System;
using System.Collections.Generic;

namespace WarGameAPI.Entities
{
    public partial class Card
    {
        public Card()
        {
            IngameDeckCards = new HashSet<IngameDeckCards>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<IngameDeckCards> IngameDeckCards { get; set; }
    }
}
