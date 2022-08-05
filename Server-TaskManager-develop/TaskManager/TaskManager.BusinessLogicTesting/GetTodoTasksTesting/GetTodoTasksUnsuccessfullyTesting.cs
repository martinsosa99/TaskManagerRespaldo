using TaskManager.Domain;
using TaskManager.IBusinessLogic;
using TaskManager.BusinessLogic.Services.Tasks;
using TaskManager.IDataAccess;
using Moq;
using TaskManager.Exceptions.AuthenticationExceptions;

namespace TaskManager.BusinessLogicTesting.GetTodoTasksTesting;

[TestClass]
public class GetTodoTasksUnsuccessfullyTesting
{

    private Mock<ITodoTaskDataAccess> TodoTaskMock { get; set; }

    private Mock<IAuthenticationDataAccess> AuthenticationMock { get; set; }

    private IUserDataAccess NullUserDataAccess { get; set; }

    private ITodoTaskService TodoTaskService { get; set; }

    private TodoTask FirstTodoTask { get; set; }

    private TodoTask SecondTodoTask { get; set; }

    private List<TodoTask> TodoTasks { get; set; }

    private User User { get; set; }

    private List<User> Users { get; set; }

    private const int EMPTY = 0;

    [TestInitialize]
    public void TestSetup()
    {
        this.User = new User();
        this.User.Id = 1;
        this.User.EmailAddress = "Martin";
        this.User.Password = "Sosa";

        this.Users = new List<User>();
        this.Users.Add(this.User);

        this.FirstTodoTask = new TodoTask();
        this.FirstTodoTask.Id = 1;
        this.FirstTodoTask.Name = "First task";
        this.FirstTodoTask.Date = new DateTime(2035, 12, 12, 12, 12, 12);
        this.FirstTodoTask.User = this.User;

        this.SecondTodoTask = new TodoTask();
        this.SecondTodoTask.Id = 2;
        this.SecondTodoTask.Name = "Second task";
        this.SecondTodoTask.Date = new DateTime(2036, 12, 12, 12, 12, 12);
        this.SecondTodoTask.User = this.User;

        this.TodoTasks = new List<TodoTask>();
        this.TodoTasks.Add(this.FirstTodoTask);
        this.TodoTasks.Add(this.SecondTodoTask);

        this.TodoTaskMock = new Mock<ITodoTaskDataAccess>(MockBehavior.Strict);
        this.AuthenticationMock = new Mock<IAuthenticationDataAccess>(MockBehavior.Strict);

        this.TodoTaskService = new TodoTaskService(this.NullUserDataAccess, this.TodoTaskMock.Object,
            this.AuthenticationMock.Object);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidTokenException))]
    public void GetTodoTasksUnsuccessfulBecauseTokenIsInvalidTestOne()
    {
        string usersToken = " ";

        List<TodoTask> todoTasks = this.TodoTaskService.TodoTasks(usersToken);

        this.TodoTaskMock.VerifyAll();

        Assert.IsTrue(todoTasks.Count == 2);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidTokenException))]
    public void GetTodoTasksUnsuccessfulBecauseTokenIsInvalidTestTwo()
    {
        string usersToken = null;

        List<TodoTask> todoTasks = this.TodoTaskService.TodoTasks(usersToken);

        this.TodoTaskMock.VerifyAll();

        Assert.IsTrue(todoTasks.Count == EMPTY);
    }

}