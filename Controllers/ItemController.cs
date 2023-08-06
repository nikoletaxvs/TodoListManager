using Microsoft.AspNetCore.Mvc;
using TodoListManager.Interfaces;
using TodoListManager.Models;
using TodoListManager.Repositories;

namespace TodoListManager.Controllers
{
    [Route("api/Todos")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepository;
        public ItemController(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet("{TodoId}/Items")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Item>))]
        public IActionResult GetTodos(int todoId)
        {
            var items = _itemRepository.GetItemsOfTodo(todoId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(items);
        }
        [HttpGet("{TodoId}/Items/{ItemId}")]
        [ProducesResponseType(200, Type = typeof(Item))]
        [ProducesResponseType(400)]
        public IActionResult GetItem(int todoId, int id)
        {
            if (!_itemRepository.ItemExists(todoId,id))
            {
                return NotFound();
            }
            var item = _itemRepository.GetItem(todoId,id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(item);
        }
        [HttpPost("{TodoId}/Item/{ItemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateItem([FromBody] Item itemCreate)
        {
            if (itemCreate == null)
            {
                return BadRequest(ModelState);
            }
            var item = _itemRepository.GetItemsOfTodo(itemCreate.TodoId)
                .Where(i => i.Title.Trim().ToUpper() == itemCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (item != null)
            {
                ModelState.AddModelError("", "Item already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newItem = new Item();
            newItem = itemCreate;
            if (!_itemRepository.CreateItem(newItem))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }


            return Ok("Successfully Created");
        }

    }

}
