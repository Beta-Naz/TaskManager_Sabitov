using Microsoft.EntityFrameworkCore;
using TaskManager_Sabitov.Classes.Database

namespace TaskManager_Sabitov.Context
{
    public class TasksContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public TasksContext()
        {
            Database.EnsureCreated();
            Tasks?.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.ConnectionData, Config.Version);
        }
    }
}
