using TodoListManager.Models;

namespace TodoListManager.Interfaces
{
    public interface ITodoRepository
    {
        ICollection<Todo> GetTodos();
        Task<Todo> GetTodoByIdAsync(int id);
        bool TodoExists(int id);
        Task CreateTodo(Todo todo);
        Task<bool> UpdateTodo(Todo todo);
        Task<bool> DeleteTodoAndItemsAsync(int id);
        bool Save();
    }
}
