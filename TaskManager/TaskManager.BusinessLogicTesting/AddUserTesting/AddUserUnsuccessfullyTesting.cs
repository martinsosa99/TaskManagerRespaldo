using Moq;
using TaskManager.BusinessLogic.Services.Users;
using TaskManager.Domain;
using TaskManager.Exceptions.UsersExceptions;
using TaskManager.IBusinessLogic;
using TaskManager.IDataAccess;

namespace TaskManager.BusinessLogicTesting.AddUserTesting;

[TestClass]
public class AddUserUnsuccessfullyTesting
{

    private Mock<IUserDataAccess> UserMock { get; set; }

    private IUserService UserService { get; set; }

    private User FirstUser { get; set; }

    private User SecondUser { get; set; }

    private List<User> Users { get; set; }

    [TestInitialize]
    public void TestSetup()
    {
        this.FirstUser = new User();
        this.FirstUser.Id = 0;
        this.FirstUser.EmailAddress = "martin@gmail.com";
        this.FirstUser.Password = "Sosa";

        this.SecondUser = new User();
        this.SecondUser.Id = 1;
        this.SecondUser.EmailAddress = "martin@gmail.com";
        this.SecondUser.Password = "Sosa";

        this.Users = new List<User>();
        this.Users.Add(this.SecondUser);

        this.UserMock = new Mock<IUserDataAccess>(MockBehavior.Strict);

        this.UserService = new UserService(this.UserMock.Object);
    }

    [TestMethod]
    [ExpectedException(typeof(UserIsNullException))]
    public void AddUserUnsuccessfulBecauseUserIsNull()
    {
        this.FirstUser = null;

        this.UserService.AddUser(this.FirstUser);

        this.UserMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(UserMailIsInvalidException))]
    public void AddUserUnsuccessfulBecauseUserEmailAddressIsNull()
    {
        this.FirstUser.EmailAddress = null;

        this.UserService.AddUser(this.FirstUser);

        this.UserMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(UserMailIsInvalidException))]
    public void AddUserUnsuccessfulBecauseUserEmailAddressIsEmptyTest()
    {
        this.FirstUser.EmailAddress = "";

        this.UserService.AddUser(this.FirstUser);

        this.UserMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(UserMailIsInvalidException))]
    public void AddUserUnsuccessfulBecauseUserEmailAddressIsInvalidTest()
    {
        this.FirstUser.EmailAddress = "gfhdgjfdsghdj";

        this.UserService.AddUser(this.FirstUser);

        this.UserMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(UserPasswordIsInvalidException))]
    public void AddUserUnsuccessfulBecauseUserPasswordIsNullTest()
    {
        this.FirstUser.Password = null;

        this.UserService.AddUser(this.FirstUser);

        this.UserMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(UserPasswordIsInvalidException))]
    public void AddUserUnsuccessfulBecauseUserPasswordIsEmptyTest()
    {
        this.FirstUser.Password = " ";

        this.UserService.AddUser(this.FirstUser);

        this.UserMock.VerifyAll();
    }
    
    [TestMethod]
    [ExpectedException(typeof(UserIsRepeatedException))]
    public void AddUserUnsuccessfulBecauseItsRepeatedTest()
    {
        this.UserMock.Setup(userDataAccess => userDataAccess.Users()).Returns(this.Users);

        this.UserService.AddUser(this.FirstUser);

        this.UserMock.VerifyAll();
    }

}