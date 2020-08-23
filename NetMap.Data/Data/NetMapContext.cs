using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetMap.Data.Data
{
    public class NetMapContext : DbContext
    {
        public readonly ILogger<NetMapContext> _logger;
        public NetMapContext(DbContextOptions<NetMapContext> options, ILogger<NetMapContext> logger) : base(options)
        {
            _logger = logger;
            _logger.LogInformation("DbContext is created");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Database=NetMapDb;Trusted_Connection=True;MultipleActiveResultSets=True;");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Post> posts { get; set; }
        public DbSet<Image> images { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Category> categories { get; set; }
    }
}
