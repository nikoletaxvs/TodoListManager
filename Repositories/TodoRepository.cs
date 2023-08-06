using TodoListManager.Data;
using TodoListManager.Interfaces;
using TodoListManager.Models;

namespace TodoListManager.Repositories
{
    public class TodoRepository:ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoRepository(TodoDbContext context){
            _context= context;
        }

        public bool CreateTodo(Todo todo)
        {
            _context.Add(todo);
            return Save();
        }

        public Todo GetTodo(int id)
        {
            return _context.Todos.Where(t => t.Id == id).FirstOrDefault();
        }

        public ICollection<Todo> GetTodos()
        {
            return _context.Todos.OrderBy(t => t.Id).ToList();
        }

        public bool Save()
        {
           var saved =_context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TodoExists(int id)
        {
            return _context.Todos.Any(t => t.Id == id);
        }
    }
}
