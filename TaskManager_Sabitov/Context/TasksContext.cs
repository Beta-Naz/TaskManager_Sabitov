using Microsoft.EntityFrameworkCore;
using TaskManager_Sabitov.Classes.Database

using TaskManager_Sabitov.Models;

namespace TaskManager_Sabitov.Context
{
    public class TasksContext : DbContext
    {
        public DbSet<Tasks> Tasks { get; set; }

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
