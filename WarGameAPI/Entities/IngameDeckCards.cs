using System;
using System.Collections.Generic;

namespace WarGameAPI.Entities
{
    public partial class IngameDeckCards
    {
        public int Id { get; set; }
        public int IngameDeckId { get; set; }
        public byte CardId { get; set; }

        public virtual Card Card { get; set; }
        public virtual IngameDeck IngameDeck { get; set; }
    }
}
