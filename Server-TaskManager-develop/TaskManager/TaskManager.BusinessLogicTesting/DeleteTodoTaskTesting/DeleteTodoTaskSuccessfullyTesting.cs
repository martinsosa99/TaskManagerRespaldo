using TaskManager.Domain;
using TaskManager.IBusinessLogic;
using TaskManager.BusinessLogic.Services.Tasks;
using TaskManager.IDataAccess;
using Moq;

namespace TaskManager.BusinessLogicTesting.DeleteTodoTaskTesting;

[TestClass]
public class DeleteTodoTaskSuccessfullyTesting
{

    private Mock<ITodoTaskDataAccess> TodoTaskMock { get; set; }

    private Mock<IAuthenticationDataAccess> AuthenticationMock { get; set; }

    private IUserDataAccess NullUserDataAccess { get; set; }

    private ITodoTaskService TodoTaskService { get; set; }

    private TodoTask FirstTodoTask { get; set; }

    private List<TodoTask> TodoTasks { get; set; }

    private User User { get; set; }

    [TestInitialize]
    public void TestSetup()
    {
        this.User = new User();
        this.User.Id = 1;
        this.User.EmailAddress = "martin@dsdsdsd.com";
        this.User.Password = "password";

        this.FirstTodoTask = new TodoTask();
        this.FirstTodoTask.Id = 1;
        this.FirstTodoTask.Name = "First task";
        this.FirstTodoTask.Date = new DateTime(2035, 12, 12, 12, 12, 12);
        this.FirstTodoTask.User = this.User;

        this.TodoTasks = new List<TodoTask>();
        this.TodoTasks.Add(this.FirstTodoTask);

        this.NullUserDataAccess = null;
        this.TodoTaskMock = new Mock<ITodoTaskDataAccess>(MockBehavior.Strict);
        this.AuthenticationMock = new Mock<IAuthenticationDataAccess>(MockBehavior.Strict);

        this.TodoTaskService = new TodoTaskService(this.NullUserDataAccess, this.TodoTaskMock.Object,
            this.AuthenticationMock.Object);
    }

    [TestMethod]
    public void DeleteTodoTaskTestOne()
    {
        string usersToken = "aToken";

        this.TodoTaskMock.Setup(todoTaskDataAccess => todoTaskDataAccess.TodoTasks()).Returns(this.TodoTasks);
        this.AuthenticationMock.Setup(authenticationDataAccess => authenticationDataAccess.GetUserByToken(usersToken)).Returns(this.User);
        this.TodoTaskMock.Setup(todoTaskDataAccess => todoTaskDataAccess.DeleteTodoTask(this.FirstTodoTask)).Verifiable();

        this.TodoTaskService.DeleteTodoTask(1, usersToken);

        this.TodoTaskMock.VerifyAll();
    }

    [TestMethod]
    public void DeleteTodoTaskTestTwo()
    {
        string usersToken = "aToken";
        this.FirstTodoTask.Id = 2;

        this.TodoTaskMock.Setup(todoTaskDataAccess => todoTaskDataAccess.TodoTasks()).Returns(this.TodoTasks);
        this.AuthenticationMock.Setup(authenticationDataAccess => authenticationDataAccess.GetUserByToken(usersToken)).Returns(this.User);
        this.TodoTaskMock.Setup(todoTaskDataAccess => todoTaskDataAccess.DeleteTodoTask(this.FirstTodoTask)).Verifiable();

        this.TodoTaskService.DeleteTodoTask(2, usersToken);

        this.TodoTaskMock.VerifyAll();
    }

}