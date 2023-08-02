using Microsoft.EntityFrameworkCore;

namespace TodoListManager.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoListManager.Models.Todo> Todo { get; set; }
        public DbSet<TodoListManager.Models.Item> Item { get; set; }
    
    }
}
