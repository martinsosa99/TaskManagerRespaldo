using TaskManager.Domain;
using TaskManager.IBusinessLogic;
using TaskManager.BusinessLogic.Services.Tasks;
using TaskManager.IDataAccess;
using Moq;
using TaskManager.Exceptions.TodoTaskExceptions;
using TaskManager.Exceptions.UsersExceptions;
using TaskManager.Exceptions.AuthenticationExceptions;

namespace TaskManager.BusinessLogicTesting.DeleteTodoTaskTesting;

[TestClass]
public class DeleteTodoTaskUnsuccessfullyTesting
{

    private Mock<ITodoTaskDataAccess> TodoTaskMock { get; set; }

    private Mock<IAuthenticationDataAccess> AuthenticationMock { get; set; }

    private IUserDataAccess NullUserDataAccess { get; set; }

    private ITodoTaskService TodoTaskService { get; set; }

    private List<TodoTask> TodoTasks { get; set; }

    private TodoTask TodoTask { get; set; }

    private User User { get; set; }

    [TestInitialize]
    public void TestSetup()
    {
        this.User = new User();
        this.User.Id = 1;
        this.User.EmailAddress = "adsdsads@dsadsadsa.com";
        this.User.Password = "dasdasdassda";

        this.TodoTask = new TodoTask();
        this.TodoTask.Id = 1;
        this.TodoTask.Name = "A TASK";
        this.TodoTask.Date = DateTime.Today;
        this.TodoTask.User = this.User;

        this.TodoTasks = new List<TodoTask>();

        this.NullUserDataAccess = null;
        this.TodoTaskMock = new Mock<ITodoTaskDataAccess>(MockBehavior.Strict);
        this.AuthenticationMock = new Mock<IAuthenticationDataAccess>(MockBehavior.Strict);

        this.TodoTaskService = new TodoTaskService(this.NullUserDataAccess, this.TodoTaskMock.Object,
            this.AuthenticationMock.Object);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidTokenException))]
    public void DeleteTodoTaskUnsuccessfullBecauseTokenIsInvalidTestOne()
    {
        string usersToken = null;

        this.TodoTaskService.DeleteTodoTask(1, usersToken);

        this.TodoTaskMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidTokenException))]
    public void DeleteTodoTaskUnsuccessfullBecauseTokenIsInvalidTestTwo()
    {
        string usersToken = " ";

        this.TodoTaskService.DeleteTodoTask(1, usersToken);

        this.TodoTaskMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(TodoTaskIsNullException))]
    public void DeleteTodoTaskUnsuccessfullBecauseTodoTaskDoesntExistTest()
    {
        string usersToken = "aToken";

        this.TodoTaskMock.Setup(todoTaskDataAccess => todoTaskDataAccess.TodoTasks()).Returns(this.TodoTasks);

        this.TodoTaskService.DeleteTodoTask(1, usersToken);

        this.TodoTaskMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(UserNotAllowedToProceedException))]
    public void DeleteTodoTaskUnsuccessfullBecauseTodoTaskDoesntBelongToUserTest()
    {
        string usersToken = "aToken";
        this.TodoTasks.Add(this.TodoTask);
        User nullUser = null;

        this.TodoTaskMock.Setup(todoTaskDataAccess => todoTaskDataAccess.TodoTasks()).Returns(this.TodoTasks);
        this.AuthenticationMock.Setup(authenticationDataAccess => authenticationDataAccess.GetUserByToken(usersToken)).Returns(nullUser);

        this.TodoTaskService.DeleteTodoTask(1, usersToken);

        this.TodoTaskMock.VerifyAll();
        this.AuthenticationMock.VerifyAll();
    }

}