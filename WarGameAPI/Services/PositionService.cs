using Microsoft.AspNetCore.Mvc;
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
    public interface IPositionService
    {
        List<ShortPosition> GetAllByUserAndGame(int userId, int gameId);
        Task<GamesView> Create(List<Position> positions);
        Position FindByCoordinates(int userId, int gameId, int x, int y);
        Task<bool> setTouched(Position positon, bool touched = true);
    }

    public class PositionService: IPositionService
    {
        private readonly wargameContext _wargameContext;
        private readonly IConfiguration _configuration;
        private readonly IGameService _gameService;

        public PositionService(wargameContext wargameContext, IConfiguration configuration, IGameService gameService)
        {
            _wargameContext = wargameContext;
            _configuration = configuration;
            _gameService = gameService;
        }

        public Position FindByCoordinates(int userId, int gameId, int x, int y)
        {
            return _wargameContext.Position.SingleOrDefault(pos => pos.CoordX == x && pos.CoordY == y && pos.UserId == userId && pos.GameId == gameId);
        }

        public List<ShortPosition> GetAllByUserAndGame(int userId, int gameId)
        {
            return _wargameContext.Position.Where(x => x.GameId == gameId && x.UserId == userId).Select(x => 
            new ShortPosition { Id = x.Id, GameId = x.GameId, UserId = x.UserId, CoordX = x.CoordX, CoordY = x.CoordY, Touche = x.Touche })
                .ToList();
        }

        public async Task<GamesView> Create(List<Position> positions)
        {
            try
            {
                foreach(Position position in positions)
                {
                    await _wargameContext.AddAsync(position);
                }

                await _wargameContext.SaveChangesAsync();
                return await _gameService.updatePosition(positions[0]);

            }
            catch (Exception e)
            {
                throw new Exception("Error in positions : " + e.Message);
            }
        }

        public async Task<bool> setTouched(Position positon, bool touched = true)
        {
            try
            {
                positon.Touche = touched;
                _wargameContext.Update(positon);
                await _wargameContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error in friend's acceptation request : " + e.Message);
            }
        }
    }
}
