using Domain.Configuration;
using Domain.DomainEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Domain
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
            modelBuilder.Entity<DomainEntities.Security.Users>().HasKey(o => o.Id);



        }






        public DbSet<Guitar> guitars { get; set; }
        public DbSet<DomainEntities.Security.Users> Users { get; set; }


    }

}
