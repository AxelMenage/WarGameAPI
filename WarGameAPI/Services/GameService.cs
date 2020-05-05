using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarGameAPI.Entities;
using WarGameAPI.Entities.Views;

namespace WarGameAPI.Services
{
    public interface IGameService
    {
        List<GamesView> GetGamesByUser(int id, bool? finished);
    }

    public class GameService : IGameService
    {
        private readonly wargameContext _wargameContext;
        private readonly IConfiguration _configuration;

        public GameService(wargameContext wargameContext, IConfiguration configuration)
        {
            _wargameContext = wargameContext;
            _configuration = configuration;
        }

        public List<GamesView> GetGamesByUser(int id, bool? finished)
        {
            if(finished == null)
                    return _wargameContext.GamesView.Where(x => x.Player1Id == id || x.Player2Id == id).ToList();
            else if((bool)finished)
                return _wargameContext.GamesView.Where(x => x.Player1Id == id || x.Player2Id == id).Where(x => x.endDate != null).ToList();
            else
                return _wargameContext.GamesView.Where(x => x.Player1Id == id || x.Player2Id == id).Where(x => x.endDate == null).ToList();
        }
    }
}
