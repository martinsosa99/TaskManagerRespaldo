using TaskManager.IBusinessLogic;
using TaskManager.Domain;
using TaskManager.DTO;
using Microsoft.AspNetCore.Mvc;
using TaskManager.WebApi.Filters;

namespace TaskManager.WebApi.Controllers;

[ApiController]
[ExceptionFilterWebApi]
[Route("api/[controller]")]
public class TasksController : Controller
{

    public readonly ITodoTaskService TodoTaskService;

    public TasksController(ITodoTaskService todoTaskService)
    {
        this.TodoTaskService = todoTaskService;
    }

    [AccessAuthorizationToACertainEndpointFilter()]
    [HttpGet]
    public async Task<IActionResult> GetTodoTasksAsync([FromHeader(Name = "token")] string usersToken)
    {
        List<TodoTaskDTO> todoTasksDTO = new List<TodoTaskDTO>();

        await Task.Run(() =>
        {
            List<TodoTask> todoTasks = this.TodoTaskService.TodoTasks(usersToken);
            todoTasksDTO = TodoTaskDTO.TodoTasksToTodoTasksDTO(todoTasks);
        });

        return Ok(todoTasksDTO);
    }

    [AccessAuthorizationToACertainEndpointFilter()]
    [HttpPost]
    public async Task<IActionResult> AddTodoTaskAsync(TodoTaskDTO todoTaskDTO,
        [FromHeader(Name = "token")] string usersToken)
    {
        await Task.Run(() =>
        {
            TodoTask todoTask = TodoTaskDTO.TodoTaskDTOToTodoTask(todoTaskDTO);
            this.TodoTaskService.AddTodoTask(todoTask);
        });

        return Ok();
    }

    [AccessAuthorizationToACertainEndpointFilter()]
    [HttpPut]
    public async Task<IActionResult> UpdateTodoTaskAsync(TodoTaskDTO todoTaskDTO,
        [FromHeader(Name = "token")] string usersToken)
    {
        await Task.Run(() =>
        {
            TodoTask todoTask = TodoTaskDTO.TodoTaskDTOToTodoTask(todoTaskDTO);
            this.TodoTaskService.UpdateTodoTask(todoTask, usersToken);
        });

        return Ok();
    }

    [AccessAuthorizationToACertainEndpointFilter()]
    [HttpDelete("{todoTaskId}")]
    public async Task<IActionResult> DeleteTodoTaskAsync([FromRoute]int todoTaskId,
        [FromHeader(Name = "token")] string usersToken)
    {
        await Task.Run(() =>
        {
            this.TodoTaskService.DeleteTodoTask(todoTaskId, usersToken);
        });

        return Ok();
    }

}