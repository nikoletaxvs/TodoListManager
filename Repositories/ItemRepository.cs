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
        

        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task CreateItemAsync(int todoId, Item item)
        {
            var todo = await _context.Todos.FindAsync(todoId);
            todo.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(item.Id))
                {
                    return false;
                }
                throw;
            }
            return true;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return false;
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
        //public bool CreateItem(Item item)
        //{


        //    _context.Add(item);
        //    return Save();
        //}

        //public Item GetItem(int todoId,int ItemId)
        //{
        //    return _context.Items.Where(i => i.TodoId == todoId && i.Id == ItemId).FirstOrDefault();
        //}

        //public ICollection<Item> GetItemsOfTodo(int todoId)
        //{
        //    return _context.Items.Where(i => i.TodoId == todoId).ToList();
        //}

        //public bool ItemExists(int todoId, int ItemId)
        //{
        //    return _context.Items.Any(i => i.TodoId == todoId && i.Id == ItemId);
        //}

        //public bool Save()
        //{
        //    var saved = _context.SaveChanges();
        //    return saved > 0 ? true : false;
        //}
    }
}
