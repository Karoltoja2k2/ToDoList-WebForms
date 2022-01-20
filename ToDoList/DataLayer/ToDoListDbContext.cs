using System.Data.Entity;
using ToDoList.DataLayer.Model;

namespace ToDoList.DataLayer
{
    public class ToDoListDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<ToDoItem> ToDoItem { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("User")
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<ToDoItem>().ToTable("ToDoItem");
        }
    }
}