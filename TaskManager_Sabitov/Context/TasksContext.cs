using Microsoft.EntityFrameworkCore;
using TaskManager_Sabitov.Classes.Database;
using TaskManager_Sabitov.Models;

namespace TaskManager_Sabitov.Context
{
    public class TasksContext : DbContext
    {
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Priority> Priorities { get; set; }

        public TasksContext()
        {
            Database.EnsureCreated();
            SeedPriorities();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.ConnectionData, Config.Version);
        }
        private void SeedPriorities()
        {
            if (Priorities != null && !Priorities.Any())
            {
                Priorities.AddRange(
                    new Priority { Name = "Низкий", Level = 1 },
                    new Priority { Name = "Средний", Level = 2 },
                    new Priority { Name = "Высокий", Level = 3 },
                    new Priority { Name = "Критичный", Level = 4 }
                );
                SaveChanges();
            }
        }
    }
}