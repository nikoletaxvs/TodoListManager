using Microsoft.EntityFrameworkCore;
using TodoListManager.Models;
namespace TodoListManager.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Item> Items { get; set; }

       
    }
}
