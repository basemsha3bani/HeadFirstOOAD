using DataRepository.Configuration;
using Domain.Entities.Schema.dbo;
using Domain.Entities.Schema.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Threading.Tasks;

namespace DataRepository.GateWay
{
    public class DbConext : DbContext
    {

        public DbConext()
        {





        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            AppConfiguration configuration = new AppConfiguration();


            string conn = configuration.ConnectionString;

            optionsBuilder.UseSqlServer(conn);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {


            modelBuilder.Entity<Guitar>().HasKey(o => o.serialNumber);
            modelBuilder.Entity<Users>().HasKey(o => o.Id);



        }






        public DbSet<Guitar> guitars { get; set; }
        public DbSet<Users> Users { get; set; }


    }
}
