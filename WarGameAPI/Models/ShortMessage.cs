using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarGameAPI.Models
{
    public partial class ShortMessage
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Content { get; set; }
        public int UserIdSender { get; set; }
        public DateTime Date { get; set; }
    }
}
