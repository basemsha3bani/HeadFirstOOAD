﻿
using Application.EntityOperationsInterface;

using Domain.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServicesClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace ServicesClasses
{
    public class AuthService:IAuthService
    {
        private IUsersOperations _usersOperations;
        private readonly JWT _jwt;
        public AuthService(IUsersOperations usersOperations)
        {
            _usersOperations = usersOperations;
            _jwt = this.generateJWTObject(); 

        }
        public async Task<UsersViewModel> GetTokenAsync(UsersViewModel model)
        {



            List < UsersViewModel> users = await _usersOperations.CheckPasswordAsync(model);

            if (users.Count==0)
            {
                model.Message = "Email or Password is incorrect!";
                return model;
            }

            var jwtSecurityToken =  CreateJwtToken(users[0]);
          

            model.IsAuthenticated = true;
            model.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
           
            
          

            return model;
        }

        private JwtSecurityToken CreateJwtToken(UsersViewModel user)
        {
          
            var roleClaims = new List<Claim>();

          
                roleClaims.Add(new Claim("roles", user.Role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              
            }.Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        private JWT generateJWTObject()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            JWT jwt = root.GetSection("JWT").Get<JWT>();
            return jwt;
        }

        
    }
}
