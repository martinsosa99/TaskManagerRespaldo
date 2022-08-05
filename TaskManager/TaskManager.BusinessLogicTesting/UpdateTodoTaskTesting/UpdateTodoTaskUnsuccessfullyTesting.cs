using Moq;
using TaskManager.BusinessLogic.Services.Tasks;
using TaskManager.Domain;
using TaskManager.Exceptions.AuthenticationExceptions;
using TaskManager.Exceptions.TodoTaskExceptions;
using TaskManager.Exceptions.UsersExceptions;
using TaskManager.IBusinessLogic;
using TaskManager.IDataAccess;

namespace TaskManager.BusinessLogicTesting.UpdateTodoTaskTesting;

[TestClass]
public class UpdateTodoTaskUnsuccessfullyTesting
{

    private Mock<ITodoTaskDataAccess> TodoTaskMock { get; set; }

    private Mock<IAuthenticationDataAccess> AuthenticationMock { get; set; }

    private Mock<IUserDataAccess> UserMock { get; set; }

    private ITodoTaskService TodoTaskService { get; set; }

    private TodoTask FirstTodoTask { get; set; }

    private TodoTask SecondTodoTask { get; set; }

    private TodoTask ThirdTodoTask { get; set; }

    private List<TodoTask> TodoTasks { get; set; }

    private User User { get; set; }

    private List<User> Users { get; set; }

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
        this.FirstTodoTask.Date = new DateTime(2034, 12, 12, 12, 12, 12);
        this.FirstTodoTask.User = this.User;

        this.SecondTodoTask = new TodoTask();
        this.SecondTodoTask.Id = 1;
        this.SecondTodoTask.Name = "First task";
        this.SecondTodoTask.Date = new DateTime(2035, 12, 12, 12, 12, 12);
        this.SecondTodoTask.User = this.User;

        this.ThirdTodoTask = new TodoTask();
        this.ThirdTodoTask.Id = 2;
        this.ThirdTodoTask.Name = "Second task";
        this.ThirdTodoTask.Date = new DateTime(2035, 12, 12, 12, 12, 12);
        this.ThirdTodoTask.User = this.User;

        this.TodoTasks = new List<TodoTask>();
        this.TodoTasks.Add(this.SecondTodoTask);
        this.TodoTasks.Add(this.ThirdTodoTask);

        this.UserMock = new Mock<IUserDataAccess>(MockBehavior.Strict);
        this.TodoTaskMock = new Mock<ITodoTaskDataAccess>(MockBehavior.Strict);
        this.AuthenticationMock = new Mock<IAuthenticationDataAccess>(MockBehavior.Strict);

        this.TodoTaskService = new TodoTaskService(this.UserMock.Object, this.TodoTaskMock.Object,
            this.AuthenticationMock.Object);
    }

    [TestMethod]
    [ExpectedException(typeof(TodoTaskIsNullException))]
    public void UpdateTodoTaskUnsuccessfulBecauseTodoTaskIsNull()
    {
        string usersToken = "aToken";
        this.FirstTodoTask = null;

        this.TodoTaskService.UpdateTodoTask(this.FirstTodoTask, usersToken);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidTokenException))]
    public void UpdateTodoTaskUnsuccessfulBecauseTokenIsInvalidTestOne()
    {
        string usersToken = null;
        this.TodoTaskService.UpdateTodoTask(this.FirstTodoTask, usersToken);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidTokenException))]
    public void UpdateTodoTaskUnsuccessfulBecauseTokenIsInvalidTestTwo()
    {
        string usersToken = " ";
        this.TodoTaskService.UpdateTodoTask(this.FirstTodoTask, usersToken);
    }

    [TestMethod]
    [ExpectedException(typeof(TodoTaskNameException))]
    public void UpdateTodoTaskUnsuccessfulBecauseTodoTaskNameIsNull()
    {
        string usersToken = "aToken";
        this.FirstTodoTask.Name = null;

        this.TodoTaskService.UpdateTodoTask(this.FirstTodoTask, usersToken);
    }

    [TestMethod]
    [ExpectedException(typeof(TodoTaskNameException))]
    public void UpdateTodoTaskUnsuccessfulBecauseTodoTaskNameIsEmptyTestOne()
    {
        string usersToken = "aToken";
        this.FirstTodoTask.Name = "";

        this.TodoTaskService.UpdateTodoTask(this.FirstTodoTask, usersToken);
    }

    [TestMethod]
    [ExpectedException(typeof(TodoTaskNameException))]
    public void UpdateTodoTaskUnsuccessfulBecauseTodoTaskNameIsEmptyTestTwo()
    {
        string usersToken = "aToken";
        this.FirstTodoTask.Name = " ";

        this.TodoTaskService.UpdateTodoTask(this.FirstTodoTask, usersToken);
    }

    [TestMethod]
    [ExpectedException(typeof(TodoTaskDateException))]

    public void UpdateTodoTaskUnsuccessfulBecauseTodoTaskDateIsInvalidTest()
    {
        string usersToken = "aToken";
        this.FirstTodoTask.Date = new DateTime(1999, 12, 12, 12, 12, 12);

        this.TodoTaskService.UpdateTodoTask(this.FirstTodoTask, usersToken);
    }

    [TestMethod]
    [ExpectedException(typeof(UserIsNullException))]
    public void UpdateTodoTaskUnsuccessfulBecauseUserDoesntExistTest()
    {
        string usersToken = "aToken";
        this.Users.Clear();

        this.TodoTaskMock.Setup(todoTaskDataAccess => todoTaskDataAccess.TodoTasks()).Returns(this.TodoTasks);
        this.UserMock.Setup(userDataAccess => userDataAccess.Users()).Returns(this.Users);

        this.TodoTaskService.UpdateTodoTask(this.FirstTodoTask, usersToken);

        this.UserMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(TodoTaskIsRepeatedException))]
    public void UpdateTodoTaskUnsuccessfulBecauseItsRepeatedTest()
    {
        string usersToken = "aToken";

        this.FirstTodoTask.Name = "Second task";
        this.FirstTodoTask.Date = new DateTime(2035, 12, 12, 12, 12, 12);
        this.FirstTodoTask.User = this.User;

        this.TodoTaskMock.Setup(todoTaskDataAccess => todoTaskDataAccess.TodoTasks()).Returns(this.TodoTasks);
        this.UserMock.Setup(userDataAccess => userDataAccess.Users()).Returns(this.Users);

        this.TodoTaskService.UpdateTodoTask(this.FirstTodoTask, usersToken);

        this.UserMock.VerifyAll();
        this.TodoTaskMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(UserNotAllowedToProceedException))]
    public void UpdateTodoTaskUnsuccessfullBecauseTodoTaskDoesntBelongToUserTest()
    {
        string usersToken = "aToken";

        this.FirstTodoTask.Name = "SecondDDD task";
        this.FirstTodoTask.Date = new DateTime(2035, 12, 12, 12, 12, 12);
        this.FirstTodoTask.User = this.User;
        User nullUser = null;

        this.TodoTaskMock.Setup(todoTaskDataAccess => todoTaskDataAccess.TodoTasks()).Returns(this.TodoTasks);
        this.UserMock.Setup(userDataAccess => userDataAccess.Users()).Returns(this.Users);
        this.AuthenticationMock.Setup(authenticationDataAccess => authenticationDataAccess.GetUserByToken(usersToken)).Returns(nullUser);

        this.TodoTaskService.UpdateTodoTask(this.FirstTodoTask, usersToken);

        this.UserMock.VerifyAll();
        this.TodoTaskMock.VerifyAll();
    }

}