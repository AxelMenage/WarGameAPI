using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarGameAPI.Entities;
using WarGameAPI.Models;

namespace WarGameAPI.Services
{
    public interface IShipService
    {
        List<ShortShip> GetAll();
        Ship FindById(int id);
    }

    public class ShipService: IShipService
    {
        private readonly wargameContext _wargameContext;
        private readonly IConfiguration _configuration;

        public ShipService(wargameContext wargameContext, IConfiguration configuration)
        {
            _wargameContext = wargameContext;
            _configuration = configuration;
        }

        public List<ShortShip> GetAll()
        {
            return _wargameContext.Ship.Select(x => new ShortShip { Id = x.Id, Name = x.Name, SizeX = x.SizeX, SizeY = x.SizeY}).ToList();
        }

        public Ship FindById(int id)
        {
            return _wargameContext.Ship.SingleOrDefault(x => x.Id == id);
        }
    }
}
