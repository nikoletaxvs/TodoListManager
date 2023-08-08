using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListManager.Controllers;
using TodoListManager.Interfaces;
using TodoListManager.Models;
using TodoListManager.Repositories;

namespace TodoListManager.Tests.Controllers
{
    public class ItemsControllerTests
    {
        readonly private IItemRepository _itemRepository;
        public ItemsControllerTests()
        {
            _itemRepository = A.Fake<IItemRepository>();
        }
        [Fact]
        public void ItemsControllerTests_ReturnOk()
        {
            //Arrange
            var todo = A.Fake<Todo>();
            var controller = new ItemController(_itemRepository);
           
            //Act
            var result = controller.GetItems(todo.Id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
