using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarGameAPI.Entities.Views
{
    public partial class UserStatsView
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}
