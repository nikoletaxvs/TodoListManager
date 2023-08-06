using Microsoft.EntityFrameworkCore;
using TodoListManager.Data;
using TodoListManager.Interfaces;
using TodoListManager.Models;

namespace TodoListManager.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly TodoDbContext _context;
        public ItemRepository(TodoDbContext context)
        {
            _context= context;  
        }

        public bool CreateItem(Item item)
        {

 
            _context.Add(item);
            return Save();
        }

        public Item GetItem(int todoId,int ItemId)
        {
            return _context.Items.Where(i => i.TodoId == todoId && i.Id == ItemId).FirstOrDefault();
        }

        public ICollection<Item> GetItemsOfTodo(int todoId)
        {
            return _context.Items.Where(i => i.TodoId == todoId).ToList();
        }

        public bool ItemExists(int todoId, int ItemId)
        {
            return _context.Items.Any(i => i.TodoId == todoId && i.Id == ItemId);
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
