using TaskManager.Domain;
using TaskManager.IBusinessLogic;
using TaskManager.BusinessLogic.Services.Tasks;
using TaskManager.IDataAccess;
using Moq;

namespace TaskManager.BusinessLogicTesting.AddTodoTaskTesting;

[TestClass]
public class AddTodoTaskSuccessfullyTesting
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
        this.SecondTodoTask.Id = 2;
        this.SecondTodoTask.Name = "Second task";
        this.SecondTodoTask.Date = new DateTime(2036, 12, 12, 12, 12, 12);
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
    public void AddTodoTaskTestOne()
    {
        this.UserMock.Setup(userDataAccess => userDataAccess.Users()).Returns(this.Users);
        this.TodoTaskMock.Setup(todoTaskDataAccess => todoTaskDataAccess.TodoTasks()).Returns(this.TodoTasks);
        this.TodoTaskMock.Setup(todoTaskDataAccess => todoTaskDataAccess.AddTodoTask(this.FirstTodoTask)).Verifiable();

        this.TodoTaskService.AddTodoTask(this.FirstTodoTask);

        this.UserMock.VerifyAll();
        this.TodoTaskMock.VerifyAll();
    }

    [TestMethod]
    public void AddTodoTaskTestTwo()
    {
        this.TodoTasks.Clear();
        this.TodoTasks.Add(this.FirstTodoTask);

        this.UserMock.Setup(userDataAccess => userDataAccess.Users()).Returns(this.Users);
        this.TodoTaskMock.Setup(todoTaskDataAccess => todoTaskDataAccess.TodoTasks()).Returns(this.TodoTasks);
        this.TodoTaskMock.Setup(todoTaskDataAccess => todoTaskDataAccess.AddTodoTask(this.SecondTodoTask)).Verifiable();

        this.TodoTaskService.AddTodoTask(this.SecondTodoTask);

        this.UserMock.VerifyAll();
        this.TodoTaskMock.VerifyAll();
    }

    [TestMethod]
    public void AddTodoTaskTestThree()
    {
        this.FirstTodoTask.Date = DateTime.Today;

        this.UserMock.Setup(userDataAccess => userDataAccess.Users()).Returns(this.Users);
        this.TodoTaskMock.Setup(todoTaskDataAccess => todoTaskDataAccess.TodoTasks()).Returns(this.TodoTasks);
        this.TodoTaskMock.Setup(todoTaskDataAccess => todoTaskDataAccess.AddTodoTask(this.FirstTodoTask)).Verifiable();

        this.TodoTaskService.AddTodoTask(this.FirstTodoTask);

        this.UserMock.VerifyAll();
        this.TodoTaskMock.VerifyAll();
    }

}