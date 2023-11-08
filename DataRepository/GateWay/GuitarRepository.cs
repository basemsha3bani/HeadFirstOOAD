using Domain.DomainEntities;
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
