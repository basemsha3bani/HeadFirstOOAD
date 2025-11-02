


using Domain.Entities.Schema.dbo;
using Domain.Entities.Schema.Security;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Utils.Configuration;

namespace DataRepository.GateWay
{
    public class AppDbContext: IdentityDbContext<IdentityUser>
    {


        public AppDbContext()
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
           base.OnModelCreating(modelBuilder);



        }






        public DbSet<Guitar> Guitars { get; set; }

        public DbSet<Users> users { get; set; }









    }
}
