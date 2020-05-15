using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSONImporter.Models
{
    public class DBContext : DbContext
    {
        public DbSet<Coach> Coach { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<TeamsDBModel> Teams { get; set; }
        public DbSet<TeamDBModel> Team { get; set; }

        public DBContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=traineeship;Username=postgres;Password=root");
        }
    }
}
