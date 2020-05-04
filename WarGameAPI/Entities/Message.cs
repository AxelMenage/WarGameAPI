using System;
using System.Collections.Generic;

namespace WarGameAPI.Entities
{
    public partial class Message
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Content { get; set; }
        public int UserIdSender { get; set; }

        public virtual Game Game { get; set; }
        public virtual User UserSender { get; set; }
    }
}
