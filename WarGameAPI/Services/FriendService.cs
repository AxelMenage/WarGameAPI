using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarGameAPI.Entities;
using WarGameAPI.Entities.Views;
using WarGameAPI.Models;

namespace WarGameAPI.Services
{
    public interface IFriendService
    {
        Friends FindByAssociation(ShortFriend friend);
        List<FriendsView> FindViewsByUserId(int id);
        Task<FriendsView> Create(ShortFriend friend);
        Task<FriendsView> Update(Friends friend);
        Task<bool> Delete(Friends friend);
    }

    public class FriendService : IFriendService
    {
        private readonly wargameContext _wargameContext;
        private readonly IConfiguration _configuration;

        public FriendService(wargameContext wargameContext, IConfiguration configuration)
        {
            _wargameContext = wargameContext;
            _configuration = configuration;
        }

        public Friends FindByAssociation(ShortFriend friend)
        {
            try
            {
                return _wargameContext.Friends.SingleOrDefault(x => x.UserId1 == friend.Id1 && x.UserId2 == friend.Id2 || x.UserId1 == friend.Id2 && x.UserId2 == friend.Id1);
            }
            catch
            {
                return null;
            }
        }

        public List<FriendsView> FindViewsByUserId(int id)
        {
            try
            {
                return _wargameContext.FriendsView.Where(x => x.Id1 == id || x.Id2 == id).ToList();
            }
            catch
            {
                return null;
            }
        }

        public async Task<FriendsView> Create(ShortFriend friend)
        {
            try
            {
                Friends friendShip = new Friends()
                {
                    UserId1 = friend.Id1,
                    UserId2 = friend.Id2,
                    Indemand = true
                };
                await _wargameContext.AddAsync(friendShip);
                await _wargameContext.SaveChangesAsync();

                return _wargameContext.FriendsView.SingleOrDefault(x => x.Id1 == friend.Id1 && x.Id2 == friend.Id2);
            }
            catch (Exception e)
            {
                throw new Exception("Erreur lors de la création de la demande : " + e.Message);
            }
        }

        public async Task<FriendsView> Update(Friends friend)
        {
            try
            {
                friend.Indemand = true;
                _wargameContext.Update(friend);
                await _wargameContext.SaveChangesAsync();

                return _wargameContext.FriendsView.SingleOrDefault(x => x.Id1 == friend.UserId1 && x.Id2 == friend.UserId2);
            }
            catch (Exception e)
            {
                throw new Exception("Erreur lors de l'acceptation de la demande : " + e.Message);
            }
        }

        public async Task<bool> Delete(Friends friend)
        {
            try
            {
                _wargameContext.Remove(friend);
                await _wargameContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
