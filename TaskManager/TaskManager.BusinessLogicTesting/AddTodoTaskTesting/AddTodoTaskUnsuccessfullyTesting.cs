using TaskManager.Domain;
using TaskManager.IBusinessLogic;
using TaskManager.BusinessLogic.Services.Tasks;
using TaskManager.IDataAccess;
using Moq;
using TaskManager.Exceptions.TodoTaskExceptions;
using TaskManager.Exceptions.UsersExceptions;

namespace TaskManager.BusinessLogicTesting.AddTodoTaskTesting;

[TestClass]
public class AddTodoTaskUnsuccessfullyTesting
{

    private Mock<ITodoTaskDataAccess> TodoTaskMock { get; set; }

    private Mock<IUserDataAccess> UserMock { get; set; }

    private IAuthenticationDataAccess NullAuthenticationDataAccess { get; set; }

    private ITodoTaskService TodoTaskService { get; set; }

    private TodoTask FirstTodoTask { get; set; }

    private TodoTask SecondTodoTask { get; set; }

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
        this.FirstTodoTask.Date = new DateTime(2035, 12, 12, 12, 12, 12);
        this.FirstTodoTask.User = this.User;

        this.SecondTodoTask = new TodoTask();
        this.SecondTodoTask.Id = this.FirstTodoTask.Id;
        this.SecondTodoTask.Name = this.FirstTodoTask.Name;
        this.SecondTodoTask.Date = this.FirstTodoTask.Date;
        this.SecondTodoTask.User = this.User;

        this.TodoTasks = new List<TodoTask>();
        this.TodoTasks.Add(this.SecondTodoTask);

        this.UserMock = new Mock<IUserDataAccess>(MockBehavior.Strict);
        this.TodoTaskMock = new Mock<ITodoTaskDataAccess>(MockBehavior.Strict);
        this.NullAuthenticationDataAccess = null;

        this.TodoTaskService = new TodoTaskService(this.UserMock.Object, this.TodoTaskMock.Object,
            this.NullAuthenticationDataAccess);
    }

    [TestMethod]
    [ExpectedException(typeof(TodoTaskIsNullException))]
    public void AddTodoTaskUnsuccessfulBecauseTodoTaskIsNull()
    {
        this.FirstTodoTask = null;

        this.TodoTaskService.AddTodoTask(this.FirstTodoTask);
    }

    [TestMethod]
    [ExpectedException(typeof(TodoTaskNameException))]
    public void AddTodoTaskUnsuccessfulBecauseTodoTaskNameIsNull()
    {
        this.FirstTodoTask.Name = null;

        this.TodoTaskService.AddTodoTask(this.FirstTodoTask);
    }

    [TestMethod]
    [ExpectedException(typeof(TodoTaskNameException))]
    public void AddTodoTaskUnsuccessfulBecauseTodoTaskNameIsEmptyTestOne()
    {
        this.FirstTodoTask.Name = "";

        this.TodoTaskService.AddTodoTask(this.FirstTodoTask);
    }

    [TestMethod]
    [ExpectedException(typeof(TodoTaskNameException))]
    public void AddTodoTaskUnsuccessfulBecauseTodoTaskNameIsEmptyTestTwo()
    {
        this.FirstTodoTask.Name = " ";

        this.TodoTaskService.AddTodoTask(this.FirstTodoTask);
    }

    [TestMethod]
    [ExpectedException(typeof(TodoTaskDateException))]

    public void AddTodoTaskUnsuccessfulBecauseTodoTaskDateIsInvalidTest()
    {
        this.FirstTodoTask.Date = new DateTime(1999, 12, 12, 12, 12, 12);

        this.TodoTaskService.AddTodoTask(this.FirstTodoTask);
    }

    [TestMethod]
    [ExpectedException(typeof(UserIsNullException))]
    public void AddTodoTaskUnsuccessfulBecauseUserDoesntExistTest()
    {
        this.Users.Clear();

        this.UserMock.Setup(userDataAccess => userDataAccess.Users()).Returns(this.Users);

        this.TodoTaskService.AddTodoTask(this.FirstTodoTask);

        this.UserMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(TodoTaskIsRepeatedException))]
    public void AddTodoTaskUnsuccessfulBecauseItsRepeatedTest()
    {
        this.UserMock.Setup(userDataAccess => userDataAccess.Users()).Returns(this.Users);
        this.TodoTaskMock.Setup(todoTaskDataAccess => todoTaskDataAccess.TodoTasks()).Returns(this.TodoTasks);

        this.TodoTaskService.AddTodoTask(this.FirstTodoTask);

        this.UserMock.VerifyAll();
        this.TodoTaskMock.VerifyAll();
    }

}