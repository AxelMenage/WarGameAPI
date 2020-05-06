using System;
using System.Collections.Generic;

namespace WarGameAPI.Entities
{
    public partial class Friends
    {
        public int Id { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public bool Indemand { get; set; }

        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
    }
}
