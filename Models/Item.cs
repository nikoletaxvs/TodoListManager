using System.Reflection.Metadata;

namespace TodoListManager.Models
{
    public class Item
    {
       
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
    }
}
