using Microsoft.EntityFrameworkCore;
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

       
        public async Task CreateTodo(Todo todo)
        {
            _context.Add(todo);
            await _context.SaveChangesAsync();
        }


        public async Task<Todo> GetTodoByIdAsync(int id)
        {
            return await _context.Todos.Include(t => t.Items).FirstOrDefaultAsync(t => t.Id == id);
        }
        public ICollection<Todo> GetTodos()
        {
            return _context.Todos.Include(t => t.Items).OrderBy(t => t.Id).ToList();
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

       
        public async Task<bool> UpdateTodo(Todo todo)
        {
            _context.Entry(todo).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(todo.Id))
                {
                    return false;
                }
                throw;
            }
            return true;
        }
        public async Task<bool> DeleteTodoAndItemsAsync(int id)
        {
            var todo = await _context.Todos.Include(t => t.Items).FirstOrDefaultAsync(t => t.Id == id);
            if (todo == null)
            {
                return false;
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
