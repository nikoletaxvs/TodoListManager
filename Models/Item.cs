using System.Reflection.Metadata;

namespace TodoListManager.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int TodoId { get; set; } // Required foreign key property
        public Todo Todo { get; set; } = null!; // Required reference navigation to principal
    }
}
