using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LinkGeneration.Controllers
{
    public class TodoController : Controller
    {
        // List containing Todo Items
        List<TodoTasks> TodoList = new List<TodoTasks>();

        // Gets Todo item details based on Id
        [Route("api/todos/{id}", Name = "GetTodoById")]
        public IActionResult GetTodo(int id)
        {
            var taskitem = TodoList.FirstOrDefault(x => x.Id == id);
            if (taskitem == null)
            {
                return NotFound();
            }
            return Ok(taskitem);
        }

        // Adds or POST a todo item to list
        [Route("api/todos")]
        public IActionResult PostTodo([FromBody]TodoTasks todoItems)
        {
            TodoList.Add(todoItems);
            // CreatedAtRoute generators link
            return CreatedAtRoute("GetTodoById", new { Controller = "Todo", id = todoItems.Id }, todoItems);
        }
    }

    public class TodoTasks
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
