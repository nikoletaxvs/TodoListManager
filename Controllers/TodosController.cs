using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListManager.Data;
using TodoListManager.Interfaces;
using TodoListManager.Models;

namespace TodoListManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : Controller
    {
        private readonly ITodoRepository _todoRepository;
        public TodosController(ITodoRepository todoRepository)
        {
            _todoRepository= todoRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(IEnumerable<Todo>))]
        public IActionResult GetTodos()
        {
            var todos = _todoRepository.GetTodos();
            if(!ModelState.IsValid) {  
                return BadRequest(ModelState);
            }
            return Ok(todos);   
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(200,Type =typeof(Todo))]
        [ProducesResponseType(400)]
        public IActionResult GetTodo(int id)
        {
            if (!_todoRepository.TodoExists(id))
            {
                return NotFound();
            }
            var todo =_todoRepository.GetTodo(id);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(todo);    
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTodo([FromBody] Todo todoCreate)
        {
            if(todoCreate == null)
            {
                return BadRequest(ModelState);
            }
            var todo = _todoRepository.GetTodos()
                .Where(t =>t.Title.Trim().ToUpper() == todoCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();
            if(todo != null)
            {
                ModelState.AddModelError("", "Todo already exists");
                return StatusCode(422,ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newTodo = new Todo();
            newTodo = todoCreate;
            if (!_todoRepository.CreateTodo(newTodo))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500,ModelState);
            }

            return Ok("Successfully Created");
        }
    }
}

