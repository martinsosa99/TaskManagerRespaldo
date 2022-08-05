using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManager.Domain;
using TaskManager.DTO;
using TaskManager.IBusinessLogic;
using TaskManager.WebApi.Controllers;

namespace TaskManager.WebApiTesting.EndpointsTesting;

[TestClass]
public class GetTodoTasksTesting
{

    private const int ONE = 1;

    [TestMethod]
    public void LoginTest()
    {
        string usersToken = "aToken";

        User user = new User();
        user.Id = 1;
        user.EmailAddress = "asdadsa@asdsad.com";
        user.Password = "dasdasdasds";

        TodoTask todoTask = new TodoTask();
        todoTask.Id = 1;
        todoTask.Name = "First task";
        todoTask.Date = DateTime.Today;
        todoTask.User = user;

        List<TodoTask> todoTasks = new List<TodoTask>();
        todoTasks.Add(todoTask);

        var todoTaskMock = new Mock<ITodoTaskService>(MockBehavior.Strict);

        todoTaskMock.Setup(r => r.TodoTasks(usersToken)).Returns(todoTasks);

        var controller = new TasksController(todoTaskMock.Object);

        var result = controller.GetTodoTasksAsync(usersToken);
        var okResult = result.Result as OkObjectResult;
        var todoTasksDTO = okResult.Value as List<TodoTaskDTO>;

        todoTaskMock.VerifyAll();

        Assert.IsTrue(((List<TodoTaskDTO>) todoTasksDTO).Count == ONE);
    }

}