using Domain.DomainEntities.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository.GateWay
{
    public class UserRepository<user> : ContextGateway<user> where user : Users
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
