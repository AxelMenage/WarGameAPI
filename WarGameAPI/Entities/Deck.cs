using System;
using System.Collections.Generic;

namespace WarGameAPI.Entities
{
    public partial class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
