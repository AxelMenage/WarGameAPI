using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarGameAPI.Models
{
    public partial class ShortGame
    {
        public int Id { get; set; }
        public byte StateId { get; set; }
        public string StateName { get; set; }
        public int Player1Id { get; set; }
        public string Player1Nickname { get; set; }
        public int? Player2Id { get; set; }
        public string Player2Nickname { get; set; }
        public bool PosPlayer1Ok { get; set; }
        public bool PosPlayer2Ok { get; set; }
        public int PlayerTurnId { get; set; }
        public string PlayerTurnNickname { get; set; }
        public int? WinnerId { get; set; }
        public string WinnerNickname { get; set; }
        public int NbTurn { get; set; }
        public int? IngameDeckId { get; set; }
        public byte GameTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? DeckId { get; set; }
    }
}
