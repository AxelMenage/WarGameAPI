﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WarGameAPI.Entities;
using WarGameAPI.Helpers;
using WarGameAPI.Models;

namespace WarGameAPI.Services
{
    public interface IUserService
    {
        Task<ShortUser> Authenticate(string username, string password);
        bool VerifyToken(string token);
        Task<User> Create(ShortUser user);
    }

    public class UserService : IUserService
    {
        private readonly wargameContext _wargameContext;
        private readonly IConfiguration _configuration;

        public UserService(wargameContext wargameContext, IConfiguration configuration)
        {
            _wargameContext = wargameContext;
            _configuration = configuration;
        }

        public async Task<ShortUser> Authenticate(string nickname, string password)
        {
            var user = await Task.Run(() => _wargameContext.User.SingleOrDefault(x => x.Nickname == nickname && x.Password == GenericService.EncryptText(password, "SHA1")));

            // return null if user not found
            if (user == null)
                return null;

            ShortUser returnedUser = new ShortUser()
            {
                Id = user.Id,
                Nickname = user.Nickname,
                Email = user.Email,

            };

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var appSettingsSecret = _configuration.GetValue<string>("AppSettings:Secret");
            var key = Encoding.ASCII.GetBytes(appSettingsSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                }),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            returnedUser.Token = tokenHandler.WriteToken(token);

            // authentication successful so return user details without password
            return returnedUser.WithoutPassword();
        }

        /// <summary>
        /// Création d'un nouvel utilisateur.
        /// </summary>
        /// <param name="utilisateur">L'objet utilisateur.</param>
        /// <returns>Retourne un objet Utilisateur.</returns>
        public async Task<User> Create(ShortUser user)
        {
            try
            {
                if(_wargameContext.User.SingleOrDefault(x => x.Email == user.Email) != null)
                {
                    throw new Exception("Email already exist. Try with another or login with your nickname.");
                }
                if (_wargameContext.User.SingleOrDefault(x => x.Nickname == user.Nickname) != null)
                {
                    throw new Exception("Nickname already exist. Choose another one or login with your account.");
                }
                // Create user.
                User newUser = new User()
                {
                    Email = user.Email,
                    Nickname = user.Nickname,
                    Password = GenericService.EncryptText(user.Password, "SHA1"),
                    Points = 0,
                };

                // Create user.
                await _wargameContext.AddAsync(newUser);
                await _wargameContext.SaveChangesAsync();

                return newUser;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Vérifier si le token est valide ou non.
        /// </summary>
        /// <param name="token">Le token.</param>
        /// <returns>Retourne true si le token est valide false sinon.</returns>
        public bool VerifyToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securitytoken = tokenHandler.ReadToken(token);

                // Vérifier la date de validité du token.
                return securitytoken.ValidTo >= DateTime.Now;
            }
            catch
            {
                // Le token est probablement faux.
                return false;
            }
        }
    }
}
