using Application.EntityOperationsInterface;
using Application1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Application.Features.Users
{
    public class AuthService 
    {
        
        private readonly JWT _jwt;
        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        public AuthService(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            
            _jwt = this.generateJWTObject();
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public async Task<UsersViewModel> GetTokenAsync(UsersViewModel model)
        {



           var User= await _userManager.FindByNameAsync(model.UserName);
            if (!(User != null && await _userManager.CheckPasswordAsync(User, model.Password)))
            {
                model.Message = "Email or Password is incorrect!";
                return model;
            }

            var jwtSecurityToken = await CreateJwtToken(User);


            model.IsAuthenticated = true;
            model.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);




            return model;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(IdentityUser user)
        {

            var roleClaims = new List<Claim>();
           List<string> roles= (await _userManager.GetRolesAsync(user)).ToList();
            

            roleClaims=roles.Select( s=> new Claim("roles", s)).ToList();

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
            JWT jwt = new JWT
            {
                DurationInDays = Convert.ToDouble(root.GetSection("JWT").GetSection("DurationInDays").Value),
                Audience = root.GetSection("JWT").GetSection("Audience").Value,
                Issuer = root.GetSection("JWT").GetSection("Issuer").Value,
                Key = root.GetSection("JWT").GetSection("Key").Value
            };
               
            return jwt;
        }
    }

}
