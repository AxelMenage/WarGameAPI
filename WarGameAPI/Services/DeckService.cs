using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarGameAPI.Entities;

namespace WarGameAPI.Services
{
    public interface IDeckService
    {
        Deck findById(int id);
        int GenerateIngameDeck(int gameId, int deckId);
    }

    public class DeckService : IDeckService
    {
        private readonly wargameContext _wargameContext;
        private readonly IConfiguration _configuration;

        public DeckService(wargameContext wargameContext, IConfiguration configuration)
        {
            _wargameContext = wargameContext;
            _configuration = configuration;
        }

        public Deck findById(int id)
        {
            return _wargameContext.Deck.SingleOrDefault(x => x.Id == id);
        }

        public int GenerateIngameDeck(int gameId, int deckId)
        {
            throw new NotImplementedException();
        }
    }
}
