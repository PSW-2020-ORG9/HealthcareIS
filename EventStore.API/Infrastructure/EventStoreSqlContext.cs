using Microsoft.EntityFrameworkCore;
using EventStore.Model.EventStore;
using System;

namespace EventStore.Model.Infrastructure
{
    public class EventStoreSqlContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string db = "psw";
        private readonly string pass = "password";
        
        public EventStoreSqlContext()
        {
            _connectionString = "server=localhost;port=3306;database=" + db + ";user=root;password=" + pass;
        }
        public EventStoreSqlContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString);
        }

        //Event Store
        public DbSet<EventES> EventES { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetCompositeKeys(modelBuilder);
            
            SeedData(modelBuilder);
        }

        protected virtual void SeedData(ModelBuilder modelBuilder)
        {
            
        }

        private static void SetCompositeKeys(ModelBuilder modelBuilder)
        {

        }
    }
    
    
}
