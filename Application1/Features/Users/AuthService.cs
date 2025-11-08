using Application.EntityOperationsInterface;
using Application1.ViewModels;
using Azure.Security.KeyVault.Secrets;
using Domain.Entities.Schema.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.Configuration;
using Utils.JWTConfiguration;

namespace Application.Features.Users
{
    public class UserService    
    {
        protected UserManager<ApplicationUser> _userManager;
        protected RoleManager<IdentityRole> _roleManager;
        protected CustomConfiguration _customConfiguration;
       

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,CustomConfiguration customConfiguration)
        {

            
            _userManager = userManager;
            _roleManager = roleManager;
            _customConfiguration = customConfiguration;
            

        }
    }
    public class AuthService: UserService
    {
        
        private readonly JWT _jwt;
        private JWTPopulator _jWTPopulator;


        public AuthService(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,CustomConfiguration customConfiguration,JWTPopulator jWTPopulator)
            :base( userManager, roleManager,customConfiguration)
        {
            _jWTPopulator = jWTPopulator;

            _jwt = this.generateJWTObject();
           

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

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
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
            
            JWT jwt = new JWT();
           
            if(_customConfiguration.GetType() == typeof(ProductionConfiguration))
            {
                SecretClient secretClient = (SecretClient)_customConfiguration._jwt;
                _jWTPopulator.PopulateJWTFromSecretValues(jwt, secretClient);
            }
            if (_customConfiguration.GetType() == typeof(localConfiguration))
            {
                IConfigurationSection configurationSection = (IConfigurationSection)_customConfiguration._jwt;
                _jWTPopulator.PopulateJWTFromConfig(jwt,configurationSection);
            }
            
               
            return jwt;
        }
        
      
    }
    public class RegistrationService : UserService
    {
        public RegistrationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,CustomConfiguration customConfiguration) : 
            base(userManager, roleManager,customConfiguration)
        {
        }

        public async Task<UsersViewModel> RegisterAsync(UsersViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
            {
                model.Message = "Username and password are required.";
                return model;
            }

            // 3. Check existing user
            var existingUser = await _userManager.FindByNameAsync(model.UserName);
            if (existingUser != null)
            {
                model.Message = "User already exists.";
                return model;
            }

            // 4. Create IdentityUser
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.UserName // If username is an email; adjust if not.
            };

            // 5. Create user with password
            var createResult = await _userManager.CreateAsync(user, model.Password);
            if (!createResult.Succeeded)
            {
                model.Message = string.Join(" ", createResult.Errors.Select(e => e.Description));
                return model;
            }

            // 6. Ensure "member" role exists
            const string memberRole = "member";
            if (!await _roleManager.RoleExistsAsync(memberRole))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(memberRole));
                if (!roleResult.Succeeded)
                {
                    model.Message = "User created but failed to create required role: " +
                                    string.Join(" ", roleResult.Errors.Select(e => e.Description));
                    return model;
                }
            }

            // 7. Add user to "member" role
            var addToRoleResult = await _userManager.AddToRoleAsync(user, memberRole);
            if (!addToRoleResult.Succeeded)
            {
                model.Message = "User created but failed to assign role: " +
                                string.Join(" ", addToRoleResult.Errors.Select(e => e.Description));
                return model;
            }

            // 8. Success
            model.Message = "User registered successfully.";
            model.IsAuthenticated = false; // Registration does not authenticate by default
            return model;
        }

    }
}
