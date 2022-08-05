using TaskManager.Domain;
using TaskManager.IBusinessLogic;
using TaskManager.BusinessLogic.Services.Authentication;
using TaskManager.IDataAccess;
using Moq;
using TaskManager.Exceptions.UsersExceptions;
using TaskManager.Exceptions.AuthenticationExceptions;

namespace TaskManager.BusinessLogicTesting.LoginTesting;

[TestClass]
public class LoginUnsuccessfullyTesting
{

    private Mock<IAuthenticationDataAccess> AuthenticationMock { get; set; }

    private Mock<IUserDataAccess> UserMock { get; set; }

    private ILoginService LoginService { get; set; }

    private User User { get; set; }

    [TestInitialize]
    public void TestSetup()
    {
        this.User = new User();
        this.User.Id = 60;
        this.User.EmailAddress = "Martin";
        this.User.Password = "password";

        Session session = new Session();
        session.Id = 1;
        session.Token = "asdasda";
        session.User = this.User;

        this.UserMock = new Mock<IUserDataAccess>(MockBehavior.Strict);
        this.AuthenticationMock = new Mock<IAuthenticationDataAccess>(MockBehavior.Strict);

        this.LoginService = new LoginService(this.AuthenticationMock.Object,
            this.UserMock.Object);
    }

    [ExpectedException(typeof(UserIsNullException))]
    [TestMethod]
    public void LoginUnsuccesfulBecauseUserDoesntExistTest()
    {
        string email = "Martinuser@gmail.com";
        string password = "password!!";

        this.User = null;

        this.UserMock.Setup(userDataAccess => userDataAccess.GetUserByEmail(email)).Returns(this.User);

        this.LoginService.Login(email, password);
    }

    [ExpectedException(typeof(UserIsLoggedException))]
    [TestMethod]
    public void LoginUnsuccesfulBecauseUserIsAlreadyLoggedTest()
    {
        this.User.EmailAddress = "Martinuser@gmail.com";
        this.User.Password = "password!!";

        string email = "Martinuser@gmail.com";
        string password = "password!!";

        this.UserMock.Setup(userDataAccess => userDataAccess.GetUserByEmail(email)).Returns(this.User);
        this.AuthenticationMock.Setup(authenticationDataAccess => authenticationDataAccess.UserIsLogged(this.User)).Returns(true);

        this.LoginService.Login(email, password);
    }

    [ExpectedException(typeof(PasswordMatchesUserRealPasswordException))]
    [TestMethod]
    public void LoginUnsuccesfulBecauseTypedPasswordIsDifferentFromUserPasswordTest()
    {
        this.User.EmailAddress = "Martinuser@gmail.com";
        this.User.Password = "password!!!";

        string email = "Martinuser@gmail.com";
        string password = "password!!";

        this.UserMock.Setup(userDataAccess => userDataAccess.GetUserByEmail(email)).Returns(this.User);

        this.LoginService.Login(email, password);
    }

    [TestMethod]
    [ExpectedException(typeof(UserIsNullException))]
    public void LoginUnsuccesfulBecauseEmailIsNullTest()
    {
        this.User.EmailAddress = "Martinuser@gmail.com";
        this.User.Password = "password!!!";

        string email = null;
        string password = "password!!!";

        this.User = null;
        this.UserMock.Setup(userDataAccess => userDataAccess.GetUserByEmail(email)).Returns(this.User);

        this.LoginService.Login(email, password);
    }

    [TestMethod]
    [ExpectedException(typeof(PasswordMatchesUserRealPasswordException))]
    public void LoginUnsuccesfulBecausePasswordIsNullTest()
    {
        this.User.EmailAddress = "Martinuser@gmail.com";
        this.User.Password = "password!!!";

        string email = "Martinuser@gmail.com";
        string password = null;

        this.UserMock.Setup(userDataAccess => userDataAccess.GetUserByEmail(email)).Returns(this.User);

        this.LoginService.Login(email, password);
    }

    [TestMethod]
    [ExpectedException(typeof(UserIsNullException))]
    public void LoginUnsuccesfulBecauseBothEmailAndPasswordAreNullTest()
    {
        this.User.EmailAddress = "Martinuser@gmail.com";
        this.User.Password = "password!!!";

        string email = null;
        string password = null;

        this.User = null;

        this.UserMock.Setup(userDataAccess => userDataAccess.GetUserByEmail(email)).Returns(this.User);

        this.LoginService.Login(email, password);
    }

}