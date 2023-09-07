using DataRepository.Configuration;
using DataRepository.DataRepositoryEntities;
using DataRepository.DataRepositoryEntities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Threading.Tasks;

namespace DataRepository.GateWay
{
    public class DbConext: DbContext
    {
        
       public DbConext()
        {
           

           

            
        }
        protected  override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            AppConfiguration configuration = new AppConfiguration();

          
            string conn = configuration.ConnectionString;

            optionsBuilder.UseSqlServer(conn);
         
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

           
            modelBuilder.Entity<DataRepositoryEntities.Guitar>().HasKey(o => o.serialNumber);
            modelBuilder.Entity<DataRepositoryEntities.Security.Users>().HasKey(o => o.Id);
            


        }




       

        public DbSet<DataRepositoryEntities.Guitar> guitars { get; set; }


    }
}
