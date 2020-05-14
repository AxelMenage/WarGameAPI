using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarGameAPI.Entities;
using WarGameAPI.Models;

namespace WarGameAPI.Services
{
    public interface IMessageService
    {
        List<ShortMessage> GetMessagesByGame(int gameId);
        Task<bool> Create(Message message);
    }

    public class MessageService: IMessageService
    {
        private readonly wargameContext _wargameContext;
        private readonly IConfiguration _configuration;

        public MessageService(wargameContext wargameContext, IConfiguration configuration)
        {
            _wargameContext = wargameContext;
            _configuration = configuration;
        }

        public List<ShortMessage> GetMessagesByGame(int gameId)
        {
            return _wargameContext.Message.Where(x => x.GameId == gameId)
                .Select(x => new ShortMessage { 
                    Id = x.Id, 
                    Content = x.Content, 
                    GameId = x.GameId, 
                    Date = x.Date, 
                    UserIdSender = x.UserIdSender })
                .ToList();
        }

        public async Task<bool> Create(Message message)
        {
            try
            {
                await _wargameContext.AddAsync(message);
                await _wargameContext.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                throw new Exception("Error when creating message : " + e.Message);
            }
        }
    }
}
