


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
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {

        CustomConfiguration _customConfiguration;
        public AppDbContext(CustomConfiguration configuration)
        {
            _customConfiguration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

       

        
        string conn = _customConfiguration._connectionString;

        optionsBuilder.UseSqlServer(conn);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            modelBuilder.Entity<Guitar>().HasKey(o => o.serialNumber);
           base.OnModelCreating(modelBuilder);



        }






        public DbSet<Guitar> Guitars { get; set; }

        









    }
}
