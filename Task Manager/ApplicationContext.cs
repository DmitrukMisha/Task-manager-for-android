using Microsoft.EntityFrameworkCore;
using Task_Manager.Models;

namespace Task_Manager
{
    public class ApplicationContext : DbContext
    {
        private string _databasePath;

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public ApplicationContext(string databasePath)
        {
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}