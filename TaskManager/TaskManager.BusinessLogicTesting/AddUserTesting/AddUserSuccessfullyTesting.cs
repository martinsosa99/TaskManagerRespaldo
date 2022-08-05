using Moq;
using TaskManager.BusinessLogic.Services.Users;
using TaskManager.Domain;
using TaskManager.IBusinessLogic;
using TaskManager.IDataAccess;

namespace TaskManager.BusinessLogicTesting.AddUserTesting;

public class AddUserSuccessfullyTesting
{
    private Mock<IUserDataAccess> UserMock { get; set; }

    private IUserService UserService { get; set; }

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

        this.Users = new List<User>();
        this.Users.Add(this.User);

        this.UserMock = new Mock<IUserDataAccess>(MockBehavior.Strict);

        this.UserService = new UserService(this.UserMock.Object);
    }

    [TestMethod]
    public void AddUserTest()
    {
        this.UserMock.Setup(userDataAccess => userDataAccess.Users()).Returns(this.Users);
        this.UserMock.Setup(userDataAccess => userDataAccess.AddUser(this.User)).Verifiable();

        this.UserService.AddUser(this.User);

        this.UserMock.VerifyAll();
    }

}

