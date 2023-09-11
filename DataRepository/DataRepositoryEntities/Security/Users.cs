using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository.DataRepositoryEntities.Security
{
    public class Users:IRepository
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
