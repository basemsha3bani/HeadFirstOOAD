using DataModel;
using Microsoft.AspNetCore.Identity;
using ServicesClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace ServicesClasses
{
    public class AuthService:IAuthService
    {
        public async Task<LoginDataModel> GetTokenAsync(LoginDataModel model)
        {


            return null;

            //if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            //{
            //    LoginDataModel.Message = "Email or Password is incorrect!";
            //    return authModel;
            //}

            //var jwtSecurityToken = await CreateJwtToken(user);
            //var rolesList = await _userManager.GetRolesAsync(user);

            //authModel.IsAuthenticated = true;
            //authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            //authModel.Email = user.Email;
            //authModel.Username = user.UserName;
            //authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            //authModel.Roles = rolesList.ToList();

            //return authModel;
        }
    }
}
