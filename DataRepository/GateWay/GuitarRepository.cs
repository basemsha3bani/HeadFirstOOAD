﻿
using Domain.Entities.Schema.dbo;
using Microsoft.EntityFrameworkCore;

namespace DataRepository.GateWay
{
    public class GuitarRepository<guitar> : ContextGateway<guitar> where guitar : Guitar
    {
        public GuitarRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
