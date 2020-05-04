using System;
using System.Collections.Generic;

namespace WarGameAPI.Entities
{
    public partial class User
    {
        public User()
        {
            Decks = new HashSet<Deck>();
            Messages = new HashSet<Message>();
            Positions = new HashSet<Position>();
            ShipStates = new HashSet<ShipState>();
            Shots = new HashSet<Shot>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public int Points { get; set; }

        public virtual ICollection<Deck> Decks { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<ShipState> ShipStates { get; set; }
        public virtual ICollection<Shot> Shots { get; set; }
    }
}
