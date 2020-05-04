using System;
using System.Collections.Generic;

namespace WarGameAPI.Entities
{
    public partial class DeckCards
    {
        public int DeckId { get; set; }
        public byte CardId { get; set; }
        public int Nb { get; set; }

        public virtual Card Card { get; set; }
        public virtual Deck Deck { get; set; }
    }
}
