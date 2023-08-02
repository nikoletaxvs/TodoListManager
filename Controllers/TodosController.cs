using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListManager.Data;
using TodoListManager.Models;

namespace TodoListManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
       
            private readonly TodoDbContext _context;

            public TodosController(TodoDbContext context)
            {
                _context = context;
            }

            // GET: api/Todos
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Todo>>> GetTodo()
            {
                if (_context.Todo == null)
                {
                    return NotFound();
                }
                return await _context.Todo.ToListAsync();
            }

            // GET: api/Todos/5
            [HttpGet("{id}")]
            public async Task<ActionResult<Todo>> GetTodo(int id)
            {
                if (_context.Todo == null)
                {
                    return NotFound();
                }
                var Todo = await _context.Todo.FindAsync(id);

                if (Todo == null)
                {
                    return NotFound();
                }

                return Todo;
            }

            // PUT: api/Todos/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
            public async Task<IActionResult> PutTodo(int id, Todo Todo)
            {
                if (id != Todo.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Todo).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            // POST: api/Todos
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            public async Task<ActionResult<Todo>> PostTodo(Todo Todo)
            {
                if (_context.Todo == null)
                {
                    return Problem("Entity set 'TodoDbContext.Todo'  is null.");
                }
                _context.Todo.Add(Todo);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTodo", new { id = Todo.Id }, Todo);
            }

            // DELETE: api/Todos/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteTodo(int id)
            {
                if (_context.Todo == null)
                {
                    return NotFound();
                }
                var Todo = await _context.Todo.FindAsync(id);
                if (Todo == null)
                {
                    return NotFound();
                }

                _context.Todo.Remove(Todo);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool TodoExists(int id)
            {
                return (_context.Todo?.Any(e => e.Id == id)).GetValueOrDefault();
            }
        }
    }

