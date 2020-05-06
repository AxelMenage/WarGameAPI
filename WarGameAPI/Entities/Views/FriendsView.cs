using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarGameAPI.Entities.Views
{
    public partial class FriendsView
    {
        public int Id { get; set; }
        public int Id1 { get; set; }
        public string Nickname1 { get; set; }
        public int Points1 { get; set; }
        public int Id2 { get; set; }
        public string Nickname2 { get; set; }
        public int Points2 { get; set; }
        public bool Indemand { get; set; }
    }
}
