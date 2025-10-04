//using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application1.ViewModels
{
    public class UsersViewModel:GenericViewModel
    {
        [Required]

        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Role { get; set; }
        public string Message { get; set; }


        public bool IsAuthenticated { get; set; }

        public string Token { get; set; }



    }
}
