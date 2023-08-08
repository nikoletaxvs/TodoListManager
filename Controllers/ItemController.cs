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
        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        [HttpGet("{id}/items/{itemId}")]
        public async Task<ActionResult<Item>> GetItemById(int id, int itemId)
        {
            var item = await _itemRepository.GetItemByIdAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost("{id}/items")]
        public async Task<ActionResult<Item>> CreateItem(int id, Item item)
        {
            await _itemRepository.CreateItemAsync(id, item);
            return CreatedAtAction(nameof(GetItemById), new { id = id, itemId = item.Id }, item);
        }

        [HttpPut("{id}/items/{itemId}")]
        public async Task<IActionResult> UpdateItem(int id, int itemId, Item item)
        {
            if (itemId != item.Id)
            {
                return BadRequest();
            }

            var updated = await _itemRepository.UpdateItemAsync(item);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}/items/{itemId}")]
        public async Task<IActionResult> DeleteItem(int id, int itemId)
        {
            var deleted = await _itemRepository.DeleteItemAsync(itemId);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
        //[HttpGet("{TodoId}/Items")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Item>))]
        //public IActionResult GetItems(int todoId)
        //{
        //    var items = _itemRepository.GetItemsOfTodo(todoId);
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return Ok(items);
        //}
        //[HttpGet("{TodoId}/Items/{ItemId}")]
        //[ProducesResponseType(200, Type = typeof(Item))]
        //[ProducesResponseType(400)]
        //public IActionResult GetItem(int todoId, int id)
        //{
        //    if (!_itemRepository.ItemExists(todoId,id))
        //    {
        //        return NotFound();
        //    }
        //    var item = _itemRepository.GetItem(todoId,id);
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return Ok(item);
        //}
        //[HttpPost("{TodoId}/Item/{ItemId}")]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult CreateItem([FromBody] Item itemCreate)
        //{
        //    if (itemCreate == null)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var item = _itemRepository.GetItemsOfTodo(itemCreate.TodoId)
        //        .Where(i => i.Title.Trim().ToUpper() == itemCreate.Title.TrimEnd().ToUpper())
        //        .FirstOrDefault();
        //    if (item != null)
        //    {
        //        ModelState.AddModelError("", "Item already exists");
        //        return StatusCode(422, ModelState);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var newItem = new Item();
        //    newItem = itemCreate;
        //    if (!_itemRepository.CreateItem(newItem))
        //    {
        //        ModelState.AddModelError("", "Something went wrong while saving");
        //        return StatusCode(500, ModelState);
        //    }

        //    return Ok("Successfully Created");
        //}

    }

}
