
using Domain.Configuration;
using Domain.DomainEntities;
using Domain.DomainEntities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace DataRepository.GateWay
{
    public class AppDbContext:DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
            //Dictionary<string, string> MigrationsSQL = this.GetAllMigrationsSQL();
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






        public DbSet<Guitar> Guitars { get; set; }

        public DbSet<Users> Users { get; set; }

      


     
      

    }
}
