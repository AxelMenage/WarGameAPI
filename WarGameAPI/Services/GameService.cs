using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarGameAPI.Entities;
using WarGameAPI.Entities.Views;
using WarGameAPI.Enum;
using WarGameAPI.Models;

namespace WarGameAPI.Services
{
    public interface IGameService
    {
        GamesView FindViewById(int id);
        Game FindById(int id);
        Game FindByUsersAssociation(int id1, int id2);
        List<GamesView> GetGamesByUser(int id, int? stateId, int limit);
        Task<GamesView> Create(ShortGame shortGame);
        Task<GamesView> ChangeState(Game game, byte stateId);
        Task<bool> Delete(Game game);
        bool GameContainsUser(Game game, int userId);
        bool GameIsActive(Game game);
        Task<GamesView> updatePosition(Position position);
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

        public List<GamesView> GetGamesByUser(int id, int? stateId, int limit = 0)
        {
            List<GamesView> games = new List<GamesView>();
            if(stateId != null)
            {
                if(stateId == (int)GameStateEnum.InProgress)
                    games = _wargameContext.GamesView.Where(x => (x.Player1Id == id || x.Player2Id == id) && (x.StateId == stateId || x.StateId == (int)GameStateEnum.WaitingPlacements)).ToList();
                else
                    games = _wargameContext.GamesView.Where(x => (x.Player1Id == id || x.Player2Id == id) && x.StateId == stateId).ToList();
            }
            else
                games = _wargameContext.GamesView.Where(x => x.Player1Id == id || x.Player2Id == id).ToList();

            if (limit > 0)
                return games.Take(limit).OrderBy(x => x.EndDate).ToList();
            else 
                return games;
        }

        public async Task<GamesView> Create(ShortGame shortGame)
        {
            try
            {
                Game gameExist = FindByUsersAssociation(shortGame.Player1Id, (int)shortGame.Player2Id);

                if(gameExist != null)
                {
                    throw new Exception("You are already playing against this user, please end this game first.");
                }
                Game game = new Game()
                {
                    GameTypeId = shortGame.GameTypeId,
                    GameStateId = (int)GameStateEnum.WaitingAcceptance,
                    Player1Id = shortGame.Player1Id,
                    Player2Id = shortGame.Player2Id,
                    UserIdTurn = (int)shortGame.Player2Id
                };

                if (shortGame.GameTypeId == (int)GameTypeEnum.Cards)
                {
                    throw new NotImplementedException();
                }

                await _wargameContext.AddAsync(game);
                await _wargameContext.SaveChangesAsync();

                return _wargameContext.GamesView.SingleOrDefault(x => x.Player1Id == shortGame.Player1Id && x.Player2Id == shortGame.Player2Id);
            }
            catch (Exception e)
            {
                throw new Exception("Error in friend's request : " + e.Message);
            }
        }

        public async Task<GamesView> ChangeState(Game game, byte stateId)
        {
            try
            {
                game.GameStateId = stateId;
                _wargameContext.Update(game);
                await _wargameContext.SaveChangesAsync();

                return _wargameContext.GamesView.SingleOrDefault(x => x.Player1Id == game.Player1Id && x.Player2Id == game.Player2Id);
            }
            catch (Exception e)
            {
                throw new Exception("Error in friend's acceptation request : " + e.Message);
            }
        }

        public GamesView FindViewById(int id)
        {
            return _wargameContext.GamesView.SingleOrDefault(x => x.Id == id);
        }

        public Game FindById(int id)
        {
            return _wargameContext.Game.SingleOrDefault(x => x.Id == id);
        }

        public Game FindByUsersAssociation(int id1, int id2)
        {
            return _wargameContext.Game.SingleOrDefault(x => (x.Player1Id == id1 && x.Player2Id == id2) || (x.Player1Id == id2 && x.Player2Id == id1));
        }

        public async Task<bool> Delete(Game game)
        {
            try
            {
                _wargameContext.Remove(game);
                await _wargameContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool GameContainsUser(Game game, int userId)
        {
            return game.Player1Id == userId || game.Player2Id == userId;
        }

        public bool GameIsActive(Game game)
        {
            return game.GameStateId == (byte)GameStateEnum.WaitingPlacements || game.GameStateId == (byte)GameStateEnum.InProgress;
        }

        public async Task<GamesView> updatePosition(Position position)
        {
            Game game = FindById(position.GameId);
            if (game.Player1Id == position.UserId) game.PosPlayer1Ok = true;
            else game.PosPlayer2Ok = true;
            if (game.PosPlayer1Ok && game.PosPlayer2Ok) game.GameStateId = (byte)GameStateEnum.InProgress;
            _wargameContext.Update(game);
            await _wargameContext.SaveChangesAsync();
            return _wargameContext.GamesView.SingleOrDefault(x => x.Id == game.Id);

        }
    }
}
