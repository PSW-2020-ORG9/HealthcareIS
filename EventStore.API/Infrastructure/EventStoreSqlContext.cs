using EventStore.API.Model.EventStore;
using EventStore.API.Model.EventStore.WPFActionEvents;
using Microsoft.EntityFrameworkCore;

namespace EventStore.API.Infrastructure
{
    public class EventStoreSqlContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string db = "";
        private readonly string pass = "";
        
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
        public DbSet<SchedulingEvent> SchedulingEvents { get; set; }
        public DbSet<WPFActionEvent> WPFActionEvents { get; set; }
        public DbSet<EquipmentLookupActionEvent> EquipmentLookupActionEvents { get; set; }
        public DbSet<FloorChangeActionEvent> FloorChangeActionEvents { get; set; }
        public DbSet<MedicationLookupActionEvent> MedicationLookupActionEvents { get; set; }

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
