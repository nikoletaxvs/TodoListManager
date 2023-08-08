using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListManager.Controllers;
using TodoListManager.Data;
using TodoListManager.Interfaces;
using TodoListManager.Models;
using Xunit;
using FakeItEasy;
using FluentAssertions;

namespace TodoListManager.Tests.Controllers
{
	public class TodosControllerTests{
        private readonly ITodoRepository _todoRepository;
        public TodosControllerTests()
		{
			_todoRepository = A.Fake<ITodoRepository>();
		}
		[Fact]
		public void TodosController_GetTodos_ReturnOk()
		{
			//Arrange
			var todos = A.Fake<ICollection<Todo>>();
			var controller = new TodosController(_todoRepository);

			//Act
			var result = controller.GetTodos();

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		
		[Fact]
		public void TodosController_CreateTodo_ReturnOk()
		{
			//Arrange 
			var todo = A.Fake<Todo>();
			var controller = new TodosController(_todoRepository);

			//Act
			var result = controller.CreateTodo(todo);

			//Assert
			result.Should().NotBeNull();
		}

		
	
	}
}
