using Microsoft.Extensions.Hosting;

namespace TodoListManager.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Item> Items { get; } = new List<Item>(); // Collection navigation containing dependents
    }
}
