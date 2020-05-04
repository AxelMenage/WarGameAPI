using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarGameAPI.Models
{
    public class ShortUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public int Points { get; set; }
        public string Token { get; set; }
    }
}
