using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarGameAPI.Entities.Views
{
    public partial class GamesView
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int Player1Id { get; set; }
        public string Player1Nickname { get; set; }
        public int Player2Id { get; set; }
        public string Player2Nickname { get; set; }
        public bool PosPlayer1Ok { get; set; }
        public bool PosPlayer2Ok { get; set; }
        public int PlayerTurnId { get; set; }
        public string PlayerTurnNickname { get; set; }
        public int WinnerId { get; set; }
        public string WinnerNickname { get; set; }
        public int nbTurn { get; set; }
        public int ingameDeckId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
