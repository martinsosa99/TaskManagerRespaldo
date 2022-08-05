using TaskManager.Domain;
using TaskManager.IBusinessLogic;
using TaskManager.WebApi.Controllers;
using TaskManager.DTO;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.WebApiTesting.EndpointsTesting;

[TestClass]
public class UpdateTodoTaskTesting
{

    [TestMethod]
    public void UpdateTodoTaskTest()
    {
        string usersToken = "aToken";

        UserDTO userDTO = new UserDTO();
        userDTO.Id = 1;
        userDTO.EmailAddress = "hfjdshjf@gfhjds.com";
        userDTO.Password = "hfgjdkhdfg";

        TodoTaskDTO todoTaskDTO = new TodoTaskDTO();
        todoTaskDTO.Id = 1;
        todoTaskDTO.Name = "a name";
        todoTaskDTO.Date = new DateTime(2065, 2, 3, 4, 5, 6);
        todoTaskDTO.User = userDTO;

        TodoTask todoTask = TodoTaskDTO.TodoTaskDTOToTodoTask(todoTaskDTO);

        var taskServiceMock = new Mock<ITodoTaskService>(MockBehavior.Strict);

        taskServiceMock.Setup(r => r.UpdateTodoTask(todoTask, usersToken)).Verifiable();

        var controller = new TasksController(taskServiceMock.Object);

        var result = controller.UpdateTodoTaskAsync(todoTaskDTO, usersToken);
        var okResult = result.Result as OkResult;

        taskServiceMock.VerifyAll();

        Assert.IsInstanceOfType(okResult, typeof(OkResult));
    }

}