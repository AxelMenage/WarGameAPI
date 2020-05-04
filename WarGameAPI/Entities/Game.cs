using System;
using System.Collections.Generic;

namespace WarGameAPI.Entities
{
    public partial class Game
    {
        public Game()
        {
            Messages = new HashSet<Message>();
            Positions = new HashSet<Position>();
            ShipStates = new HashSet<ShipState>();
            Shots = new HashSet<Shot>();
        }

        public int Id { get; set; }
        public byte GameTypeId { get; set; }
        public byte GameStateId { get; set; }
        public int Player1Id { get; set; }
        public int? Player2Id { get; set; }
        public bool PosPlayer1Ok { get; set; }
        public bool PosPlayer2Ok { get; set; }
        public int UserIdTurn { get; set; }
        public int? UserIdWinner { get; set; }
        public int NbTurn { get; set; }
        public int? IngameDeckId { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime? Enddate { get; set; }

        public virtual GameState GameState { get; set; }
        public virtual IngameDeck IngameDeck { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<ShipState> ShipStates { get; set; }
        public virtual ICollection<Shot> Shots { get; set; }
    }
}
