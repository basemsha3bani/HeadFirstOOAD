

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.GateWay
{
    public class ContextGateway<Entity>   : IContextGateway<Entity> where Entity : class
    {
        protected  AppDbContext _dbContext;


        public ContextGateway(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        public async  Task  Add(Entity repository) 
        {
            _dbContext.Entry(repository).State = EntityState.Added;

           await _dbContext.SaveChangesAsync();
        }
        public async  Task  Edit(Entity repository)
        {

            _dbContext.Entry(repository).State = EntityState.Modified;

          await _dbContext.SaveChangesAsync();



        }



        public async  Task  Edit(Entity repository, Entity withnewvalues)
        {
            _dbContext.Entry(repository).State = EntityState.Detached;

            _dbContext.Entry(withnewvalues).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

        }

        public async  Task Delete(Entity repository)
        {
            _dbContext.Entry(repository).State = EntityState.Deleted;

            await _dbContext.SaveChangesAsync();
        }

        public async  Task<Entity> GetById(Expression<Func<Entity, bool>> predicate)
        {
            return _dbContext.Set<Entity>().AsNoTracking().Where(predicate).FirstOrDefault();


        }

        public async  Task<List<Entity>> List(Expression<Func<Entity, bool>> predicate = null, params Expression<Func<Entity, object>>[] includeProperties)
        {

            if (predicate == null)
            {
                return await (includeProperties.Aggregate
             (_dbContext.Set<Entity>(), (current, includeProperty) => (DbSet<Entity>)current.Include(includeProperty)).ToListAsync());
            }

            return  await (includeProperties.Aggregate
               (_dbContext.Set<Entity>().Where(predicate), (current, includeProperty) => current.Include(includeProperty)).ToListAsync());
        }
        private  IDbContextTransaction _transaction;



        public  void CreateDatabaseTransaction()
        {
           
            _transaction = _dbContext.Database.BeginTransaction();
        }



        public  void Rollback()
        {
            _transaction.Rollback();
        }

        public   void Dispose()
        {
            _transaction.Dispose();
        }

        public  void Commit()
        {
            _transaction.Commit();
        }
    }
}
