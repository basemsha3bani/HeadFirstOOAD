using DataRepository.Configuration;
using DataRepository.DataRepositoryEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DataRepository.GateWay
{
    public class DbConext:DbContext
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

        }




       

        public DbSet<DataRepositoryEntities.Guitar> guitars { get; set; }


    }
}
