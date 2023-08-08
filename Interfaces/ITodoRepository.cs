using TodoListManager.Models;

namespace TodoListManager.Interfaces
{
    public interface ITodoRepository
    {
        ICollection<Todo> GetTodos();
        Task<Todo> GetTodoByIdAsync(int id);
        bool TodoExists(int id);
        bool CreateTodo(Todo todo);
        bool UpgradeTodo(Todo todo);
        bool Save();
    }
}
