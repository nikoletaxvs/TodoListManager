using TodoListManager.Models;

namespace TodoListManager.Interfaces
{
    public interface ITodoRepository
    {
        ICollection<Todo> GetTodos();
        Todo GetTodo(int id);
        bool TodoExists(int id);
        bool CreateTodo(Todo todo);
        bool Save();
    }
}
