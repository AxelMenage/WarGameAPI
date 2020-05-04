using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarGameAPI.Entities;
using WarGameAPI.Models;

namespace WarGameAPI.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<ShortUser> WithoutPasswords(this IEnumerable<ShortUser> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static ShortUser WithoutPassword(this ShortUser user)
        {
            user.Password = null;
            return user;
        }
    }
}
