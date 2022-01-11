using System.Data.Entity;
using ToDoList.DataLayer.Model;

namespace ToDoList.DataLayer
{
    public class ToDoListDbContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItem { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>().ToTable("ToDoItem");
        }
    }
}