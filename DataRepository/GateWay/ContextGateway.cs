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
   internal class ContextGateway<TModelRepository> where TModelRepository : class
    {
        private static DbConext dbConext;

        internal static void GetContextInstance()
        {
            if (dbConext == null)
            {
                dbConext = new DbConext();
            }
           //return dbConext;
        }

        private ContextGateway() { }

        internal async static Task  Add(IRepository repository) 
        {
            dbConext.Entry(repository).State = EntityState.Added;

           await dbConext.SaveChangesAsync();
        }
        internal async static Task  Edit(IRepository repository)
        {

            dbConext.Entry(repository).State = EntityState.Modified;

          await  dbConext.SaveChangesAsync();



        }



        internal async static Task  Edit(IRepository repository, IRepository withnewvalues)
        {
            dbConext.Entry(repository).State = EntityState.Detached;

            dbConext.Entry(withnewvalues).State = EntityState.Modified;
            await dbConext.SaveChangesAsync();

        }

        internal async static Task Delete(IRepository repository)
        {
            dbConext.Entry(repository).State = EntityState.Deleted;

            await dbConext.SaveChangesAsync();
        }

        internal async static Task<TModelRepository> GetById(Expression<Func<TModelRepository, bool>> predicate)
        {
            return dbConext.Set<TModelRepository>().AsNoTracking().Where(predicate).FirstOrDefault();


        }

        internal async static Task<List<TModelRepository>> List(Expression<Func<TModelRepository, bool>> predicate = null, params Expression<Func<TModelRepository, object>>[] includeProperties)
        {

            if (predicate == null)
            {
                return await (includeProperties.Aggregate
             (dbConext.Set<TModelRepository>(), (current, includeProperty) => (DbSet<TModelRepository>)current.Include(includeProperty)).ToListAsync());
            }

            return  await (includeProperties.Aggregate
               (dbConext.Set<TModelRepository>().Where(predicate), (current, includeProperty) => current.Include(includeProperty)).ToListAsync());
        }
        private static IDbContextTransaction _transaction;



        public static void CreateDatabaseTransaction()
        {
            GetContextInstance();
            _transaction = dbConext.Database.BeginTransaction();
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
