using Microsoft.EntityFrameworkCore;
using TodoListManager.Models;

namespace TodoListManager.Interfaces
{
    public interface IItemRepository
    {
        //ICollection<Item> GetItemsOfTodo(int todoId);
        //Item GetItem(int todoId,int ItemId);
        //bool ItemExists(int todoId,int ItemId);
        //bool CreateItem( Item item);
        //bool Save();
          Task<Item> GetItemByIdAsync(int id);

          Task CreateItemAsync(int todoId, Item item);

          Task<bool> UpdateItemAsync(Item item);


          Task<bool> DeleteItemAsync(int id);

        
    }
}
