using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManager.IBusinessLogic;
using TaskManager.WebApi.Controllers;

namespace TaskManager.WebApiTesting.EndpointsTesting;

[TestClass]
public class DeleteTodoTaskTesting
{

    [TestMethod]
    public void DeleteTodoTaskTest()
    {
        string usersToken = "aToken";
        int inventedTodoTaskId = 1;

        var todoTaskMock = new Mock<ITodoTaskService>(MockBehavior.Strict);

        todoTaskMock.Setup(r => r.DeleteTodoTask(inventedTodoTaskId, usersToken)).Verifiable();

        var controller = new TasksController(todoTaskMock.Object);

        var result = controller.DeleteTodoTaskAsync(inventedTodoTaskId, usersToken);
        var okResult = result.Result as OkResult;

        todoTaskMock.VerifyAll();

        Assert.IsInstanceOfType(okResult, typeof(OkResult));
    }

}


