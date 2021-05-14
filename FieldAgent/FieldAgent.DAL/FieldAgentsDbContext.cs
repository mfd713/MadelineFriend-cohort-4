using Microsoft.EntityFrameworkCore;
using System;
using FieldAgent.Entities;
using Microsoft.Extensions.Configuration;

namespace FieldAgent.DAL
{
    public class FieldAgentsDbContext : DbContext
    {
        public DbSet<Agent> Agent { get; set; }
        public DbSet<Agency> Agency { get; set; }
        public DbSet<AgencyAgent> AgencyAgent { get; set; }
        public DbSet<Alias> Alias { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Mission> Mission { get; set; }
        public DbSet<SecurityClearance> SecurityClearance { get; set; }


        public FieldAgentsDbContext(): base()
        {

        }

        public FieldAgentsDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgencyAgent>().HasKey(aa => new { aa.AgencyId, aa.AgentId });
        }
        public static FieldAgentsDbContext GetDbContext()
        {
            var builder = new ConfigurationBuilder();

            builder.AddUserSecrets<FieldAgentsDbContext>();

            var config = builder.Build();

            var connectionString = config["ConnectionStrings:FieldAgent"];

            var options = new DbContextOptionsBuilder<FieldAgentsDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new FieldAgentsDbContext(options);
                
        }
    }
}
