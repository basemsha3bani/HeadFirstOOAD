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
   public class ContextGateway<Repository> where Repository:class
    {
        private static DbContext context;

        public static void SetContextInstance(DbContext dbContext)
        {
            if (context == null)
            {
                context = dbContext;
            }
           //return dbConext;
        }

        private ContextGateway() { }

        public async static Task  Add(IRepository repository) 
        {
            context.Entry(repository).State = EntityState.Added;

           await context.SaveChangesAsync();
        }
        public async static Task  Edit(IRepository repository)
        {

            context.Entry(repository).State = EntityState.Modified;

          await  context.SaveChangesAsync();



        }



        public async static Task  Edit(IRepository repository, IRepository withnewvalues)
        {
            context.Entry(repository).State = EntityState.Detached;

            context.Entry(withnewvalues).State = EntityState.Modified;
            await context.SaveChangesAsync();

        }

        public async static Task Delete(IRepository repository)
        {
            context.Entry(repository).State = EntityState.Deleted;

            await context.SaveChangesAsync();
        }

        public async static Task<Repository> GetById(Expression<Func<Repository, bool>> predicate) 
        {
            return context.Set<Repository>().AsNoTracking().Where(predicate).FirstOrDefault();


        }
     
        public async static Task<List<Repository>> List(Expression<Func<Repository, bool>> predicate = null, params Expression<Func<Repository, object>>[] includeProperties)
        {
            
            if (predicate == null)
            {
                return await (includeProperties.Aggregate
             (context.Set<Repository>(), (current, includeProperty) => (DbSet<Repository>)current.Include(includeProperty)).ToListAsync());
            }

            return  await (includeProperties.Aggregate
               (context.Set<Repository>().Where(predicate), (current, includeProperty) => current.Include(includeProperty)).ToListAsync());
        }
        private static IDbContextTransaction _transaction;



        public static void CreateDatabaseTransaction()
        {
            
            _transaction = context.Database.BeginTransaction();
        }



        public static void Rollback()
        {
            _transaction.Rollback();
        }

        public static  void Dispose()
        {
            _transaction.Dispose();
        }

        public static void Commit()
        {
            _transaction.Commit();
        }
    }
}
