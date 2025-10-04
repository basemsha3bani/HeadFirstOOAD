using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application1.Contracts
{
    public interface IContextGateway<Entity> where Entity : class
    {
        Task Add(Entity repository);



        public  Task Edit(Entity repository, Entity withnewvalues);

        public  Task Delete(Entity repository);

        public  Task<Entity> GetById(Expression<Func<Entity, bool>> predicate);

        public  Task<List<Entity>> List(Expression<Func<Entity, bool>> predicate = null, params Expression<Func<Entity, object>>[] includeProperties);
    }
}
